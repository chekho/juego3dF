using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    public CanvasController CanvasController;
    private bool hasPlayer = false;

    private void Update()
    {
        if (hasPlayer) CanvasController.DecreaseOxygenTime(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            hasPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayer = false;
        }
    }
}
