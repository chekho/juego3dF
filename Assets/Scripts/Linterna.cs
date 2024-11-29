using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
        }
    }
}
