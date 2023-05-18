using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StrikerMovement : MonoBehaviour
{
    private float hitForceMultiplier = 17f;

    private GameObject mousePointA;
    private GameObject mousePointB;
    private GameObject arrow;
    private GameObject circle;
    private float currentDist;
    private float safeSpace;
    private float shootPower;
    private float maxDist = 3.5f;
    public float strikerLifeTime = 10f;
    private float timeRemaining;
    private bool hitted;

    public Vector3 minBoundaries;
    private Vector3 shootDirection;
    public Vector3 maxBoundaries;

    private PrefabManager prefab1;
    private CircularTimer prefab2;
    private BotBehaviour prefab3;

    void Start()
    {
        mousePointA = GameObject.FindGameObjectWithTag("PointA");
        mousePointB = GameObject.FindGameObjectWithTag("PointB");
        arrow = GameObject.FindGameObjectWithTag("Arrow");
        circle = GameObject.FindGameObjectWithTag("Circle");

        prefab1 = FindObjectOfType<PrefabManager>();
        prefab2 = FindObjectOfType<CircularTimer>();
        prefab3 = FindObjectOfType<BotBehaviour>();

        timeRemaining = strikerLifeTime;
    }

    private void Update()
    {
        if (prefab3.isBotPlayerActive == true)
        {
            EnemyStrikerHit();
        }
        StrikerLife();
        Boundary();
    }

    private void OnMouseDrag()
    {
        SliderInput();
    }

    private void OnMouseUp()
    {
        arrow.GetComponent<Renderer>().enabled = false;
        circle.GetComponent<Renderer>().enabled = false;

        if (prefab3.isBotPlayerActive == false)
        {
            StrikerHit();
        }
    }

    private void StrikerLife()
    {
        timeRemaining -= Time.deltaTime * 2f;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            prefab1.PopulateStriker();
            prefab2.StartTimer();
            Destroy(gameObject);
        }
    }

    public void Boundary()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, minBoundaries.x, maxBoundaries.x);
        currentPosition.z = Mathf.Clamp(currentPosition.z, minBoundaries.z, maxBoundaries.z);
        transform.position = currentPosition;
    }

    public void SliderInput()
    {
        currentDist = Vector3.Distance(mousePointA.transform.position, transform.position);

        if (currentDist <= maxDist)
        {
            safeSpace = currentDist * 6f;
        }
        else
        {
            safeSpace = maxDist * 6f;
        }

        ArrowAndCircleHandler();

        shootPower = Mathf.Abs(safeSpace) * hitForceMultiplier;
        Vector3 dimxy = mousePointA.transform.position - transform.position;
        float difference = dimxy.magnitude;
        shootDirection = Vector3.Normalize(dimxy);

        mousePointB.transform.position = transform.position + (-1 * maxDist * (dimxy / difference));
        mousePointB.transform.position = new Vector3(mousePointB.transform.position.x, 0.5f, mousePointB.transform.position.z);

        shootDirection = Vector3.Normalize(mousePointA.transform.position - transform.position);
    }

    private void ArrowAndCircleHandler()
    {
        arrow.GetComponent<Renderer>().enabled = true;
        circle.GetComponent<Renderer>().enabled = true;

        if (currentDist <= maxDist)
        {
            arrow.transform.position = new Vector3((2 * transform.position.x) - mousePointA.transform.position.x, 0.2f, (2 * transform.position.z) - mousePointA.transform.position.z);
        }
        else
        {
            Vector3 dimxy = mousePointA.transform.position - transform.position;
            float difference = dimxy.magnitude;
            arrow.transform.position = transform.position + (-1 * maxDist * (dimxy / difference));
            arrow.transform.position = new Vector3(arrow.transform.position.x, 0.5f, arrow.transform.position.z);
        }

        circle.transform.position = transform.position + new Vector3(0, 0, 0.04f);
        Vector3 dir = mousePointA.transform.position - transform.position;
        float rot;
        if (Vector3.Angle(dir, transform.forward) > 90)
        {
            rot = Vector3.Angle(dir, transform.right);
        }
        else
        {
            rot = Vector3.Angle(dir, transform.right) * -1;
        }

        arrow.transform.eulerAngles = new Vector3(90, 0, -rot);

        float scaleX = Mathf.Log(1 + safeSpace / 2, 2) * 2.2f;
        float scalez = Mathf.Log(1 + safeSpace / 2, 2) * 2.2f;

        arrow.transform.localScale = new Vector3(1 + scalez, 1 + scaleX, 0.1f);
        circle.transform.localScale = new Vector3(1 + scalez, 1 + scaleX, 0.1f);
    }

    public void StrikerHit()
    {
        if (!hitted)
        {
            Vector3 push = shootDirection * shootPower * -1;
            GetComponent<Rigidbody>().AddForce(push, ForceMode.Impulse);
            hitted = true;
        }
    }

    public void EnemyStrikerHit()
    {
        if (!hitted)
        {
            AiPlayerStrikerHit();
            hitted = true;
        }
    }

    private void AiPlayerStrikerHit()
    {
        System.Random rdm = new System.Random();
        int num1 = rdm.Next(9);
        int num2 = rdm.Next(1, 4);

        if (prefab1.blackCoinPositions.Count > 0)
        {
            Vector3 dimxy = prefab1.blackCoinPositions[num1] - transform.position;
            shootDirection = Vector3.Normalize(dimxy);
            shootPower = Mathf.Abs(num2 * 7f) * hitForceMultiplier;

            Vector3 push = shootDirection * shootPower;
            GetComponent<Rigidbody>().AddForce(push, ForceMode.Impulse);
        }
    }
}
