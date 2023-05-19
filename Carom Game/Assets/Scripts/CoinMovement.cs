using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    public Vector3 minBoundaries;
    public Vector3 maxBoundaries;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Boundary();
    }

    public void Boundary()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, minBoundaries.x, maxBoundaries.x);
        currentPosition.z = Mathf.Clamp(currentPosition.z, minBoundaries.z, maxBoundaries.z);
        transform.position = currentPosition;
    }
}
