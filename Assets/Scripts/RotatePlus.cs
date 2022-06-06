using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlus : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0, 0, 5 * speed * Time.deltaTime);
    }
}
