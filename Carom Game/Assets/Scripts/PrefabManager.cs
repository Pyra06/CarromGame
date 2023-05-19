using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public GameObject strikerPrefab;
    private GameObject instanceStriker;
    public GameObject whiteCoinPrefab;
    public GameObject blackCoinPrefab;
    public GameObject queenCoinPrefab;
    public GameObject board;

    private Vector3 startPosition1;
    private Vector3 startPosition2;
    private Vector3 shootDirection;
    public List<Vector3> blackCoinPositions = new List<Vector3>();

    public int initialCoinCount = 9;
    private bool isAlternatePosition = false;

    void Start()
    {
        startPosition1 = new Vector3(0f, 0.21f, -10f);
        startPosition2 = new Vector3(0f, 0.21f, 10f);
    }

    void Update()
    {
    }

    public void PopulateStriker()
    {
        instanceStriker = Instantiate(strikerPrefab, Vector3.zero, Quaternion.identity);

        if (isAlternatePosition)
        {
            instanceStriker.transform.position = startPosition2;
        }
        else
        {
            instanceStriker.transform.position = startPosition1;
        }

        isAlternatePosition = !isAlternatePosition;
    }

    public void PopulateCoins()
    {
        GameObject queenCoin = Instantiate(queenCoinPrefab, board.transform.position + new Vector3(0f, 0.2f, 0f), Quaternion.identity);
        queenCoin.transform.SetParent(transform);

        Vector3[] ring1Positions = CalculateRingPositions(6, 1.1f, 0.2f);
        Vector3[] ring2Positions = CalculateRingPositions(12, 2.2f, 0.2f);

        for (int i = 0; i < 3; i++)
        {
            GameObject whiteCoin = Instantiate(whiteCoinPrefab, ring1Positions[i * 2], Quaternion.identity);
            whiteCoin.transform.SetParent(transform);
            ApplyZeroMotion(whiteCoin);

            GameObject blackCoin = Instantiate(blackCoinPrefab, ring1Positions[(i * 2) + 1], Quaternion.identity);
            blackCoin.transform.SetParent(transform);
            ApplyZeroMotion(blackCoin);
        }

        for (int i = 0; i < 6; i++)
        {
            GameObject whiteCoin = Instantiate(whiteCoinPrefab, ring2Positions[i * 2], Quaternion.identity);
            whiteCoin.transform.SetParent(transform);
            ApplyZeroMotion(whiteCoin);

            GameObject blackCoin = Instantiate(blackCoinPrefab, ring2Positions[(i * 2) + 1] / 1.1f, Quaternion.identity);
            blackCoin.transform.SetParent(transform);
            ApplyZeroMotion(blackCoin);
        }
    }

    public void StoreCoinPositions()
    {
        GameObject[] blackcoins = GameObject.FindGameObjectsWithTag("BlackCoin");

        foreach (GameObject coin in blackcoins)
        {
            blackCoinPositions.Add(coin.transform.position);
        }
    }

    private Vector3[] CalculateRingPositions(int numCoins, float radius, float yOffset)
    {
        Vector3 centerPosition = board.transform.position + new Vector3(0f, yOffset, 0f);
        Vector3[] positions = new Vector3[numCoins];
        float angleIncrement = 360f / numCoins;

        for (int i = 0; i < numCoins; i++)
        {
            float angle = i * angleIncrement * Mathf.Deg2Rad;
            float x = centerPosition.x + Mathf.Cos(angle) * radius;
            float z = centerPosition.z + Mathf.Sin(angle) * radius;
            positions[i] = new Vector3(x, centerPosition.y, z);
        }

        return positions;
    }

    private void ApplyZeroMotion(GameObject coin)
    {
        Rigidbody rb = coin.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }
}
