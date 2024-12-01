using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class ButtonsItemsController : MonoBehaviour
{
    public GameObject menu;
    public GameObject alertMenu; // Referencia al AlertMenu
    public string requiredItem; // Ítem necesario para este menú
    private CanvasController canvas;
    private PlayerMovement pm;

    private void Start()
    {
        canvas = FindObjectOfType<CanvasController>();
        pm = FindObjectOfType<PlayerMovement>();

        // Esconde todos los menús y el mensaje de alerta al inicio
        menu.SetActive(false);
        alertMenu.SetActive(false);
    }

    public void ShowMenu()
    {
        // Si el ítem está disponible, mostrar el menú
        HideAllMenus();
        // Ocultar el mensaje de alerta si estaba activo
        alertMenu.SetActive(false);

        // Validar si el jugador tiene el ítem requerido
        if (!pm.itemsCollected.Contains(requiredItem))
        {
            StartCoroutine(EsperarYHacerAlgo());
            return;
        }

        menu.SetActive(!menu.activeInHierarchy);
    }

    private void HideAllMenus()
    {
        var allMenus = FindObjectsOfType<ButtonsItemsController>();
        foreach (var menuController in allMenus)
        {
            if (menuController != this) // Excluir el menú actual
            {
                menuController.menu.SetActive(false);
            }
        }
    }

    private IEnumerator EsperarYHacerAlgo()
    {
        alertMenu.SetActive(true);
        yield return new WaitForSeconds(3f); // Espera 3 segundos Debug.Log("¡3 segundos han pasado!");
        alertMenu.SetActive(false);
    }


}
