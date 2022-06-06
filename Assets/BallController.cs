using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject robot;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(robot.transform.position.z - transform.position.z <= 15f)
        {
            rb.useGravity = true;
        }
    }
}
