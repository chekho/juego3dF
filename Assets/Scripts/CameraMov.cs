using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(-Input.GetAxisRaw("Mouse Y"), 0,0);
    }
}
