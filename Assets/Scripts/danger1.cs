using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class danger1 : MonoBehaviour
{
    public float oxygenReductionRate = 10f; // Cantidad de ox�geno a reducir por segundo
    private bool playerInside = false;
    private CanvasController canvasController;

    void Start()
    {
        // Encontrar la referencia al CanvasController en la escena
        canvasController = FindObjectOfType<CanvasController>();
    }

    void Update()
    {
        // Reducir ox�geno solo si el jugador est� dentro del �rea peligrosa
        if (playerInside)
        {
            // Llama al m�todo para reducir ox�geno en el CanvasController
            canvasController.DecreaseOxygenTime(Time.deltaTime * oxygenReductionRate);
        }
    }

    // Detecta cuando el jugador entra en el �rea peligrosa
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Aseg�rate de que el jugador tenga el tag "Player"
        {
            playerInside = true;
        }
    }

    // Detecta cuando el jugador sale del �rea peligrosa
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}
