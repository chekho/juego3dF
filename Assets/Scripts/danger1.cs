using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class danger1 : MonoBehaviour
{
    public float oxygenReductionRate = 10f; // Cantidad de oxígeno a reducir por segundo
    private bool playerInside = false;
    private CanvasController canvasController;

    void Start()
    {
        // Encontrar la referencia al CanvasController en la escena
        canvasController = FindObjectOfType<CanvasController>();
    }

    void Update()
    {
        // Reducir oxígeno solo si el jugador está dentro del área peligrosa
        if (playerInside)
        {
            // Llama al método para reducir oxígeno en el CanvasController
            canvasController.DecreaseOxygenTime(Time.deltaTime * oxygenReductionRate);
        }
    }

    // Detecta cuando el jugador entra en el área peligrosa
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tenga el tag "Player"
        {
            playerInside = true;
        }
    }

    // Detecta cuando el jugador sale del área peligrosa
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}
