using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureInteraction : MonoBehaviour
{
    private bool playerInRange = false; // Verifica si el jugador est� cerca
    public CanvasController cc;

    void Start()
    { 
        cc = FindObjectOfType<CanvasController>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Space)) // Detecta la barra espaciadora
        {
            cc.ShowFurnitureMenu();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Aseg�rate de que el jugador tenga la etiqueta "Player"
        {
            playerInRange = true;
            Debug.Log("Jugador cerca del mueble.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Jugador sali� del rango del mueble.");
        }
    }

}
