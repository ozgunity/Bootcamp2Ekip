using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float movementFactor;
    public float period;
    public Vector3 movementVector;
    public Vector3 startingPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / period;  // continually growing over time

        const float tau = Mathf.PI * 2;  // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau);  // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f;   // recalculated to go from 0 to 1 so its cleaner

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
