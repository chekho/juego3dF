using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour
{
    private bool pause;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(pause == false)
            {
                Time.timeScale = 0;
                pause = true;
            } else
            {
                Time.timeScale = 1;
                pause = false;
            }
        }
    }
}
