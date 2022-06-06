using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    Light far;
    void Start()
    {
        far = GetComponent<Light>();
        far.enabled = false;
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.L))
        {
            far.enabled = !far.enabled;
        }
    }
}
