using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f; // in seconds

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if(period == 0)
        {
            return;
        }
        float cycles = Time.time / period;
        float rawSinWave = Mathf.Sin(cycles * 2 * Mathf.PI); // amout of cicles per 2Pi, values from -1 to 1
        Vector3 offset = movementVector * Mathf.Abs(rawSinWave);
        transform.position = startingPosition + offset;   
    }
}
