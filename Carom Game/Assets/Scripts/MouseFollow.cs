using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    public float offset = 0.5f;
    private Vector3 tempPos;
    private GameObject striker;

    void Start()
    {
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        tempPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));

        transform.position = new Vector3(tempPos.x, offset, tempPos.z);
    }
}
