using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRotator : MonoBehaviour
{
    public float period;

    void Start()
    {
        
    }

    void Update()
    {
        float myNum = Time.time * period;
        float myAngle = ((Mathf.Sin(myNum) * 30) - 90);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, myAngle, 0), .1f);


    }
}
