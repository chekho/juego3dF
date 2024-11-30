using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject OxigenLevel;
    private RectTransform rtOxigenLevel;
    private float sizeOxigenLevel = 50f;

    public GameObject FurnitureMenu;

    public float porcentajeReduccion = 0.05f; // Reducci�n del 5% por segundo

    public void ShowFurnitureMenu()
    {
        FurnitureMenu.SetActive(!FurnitureMenu.active);
    }

    private void Start()
    {
        rtOxigenLevel = OxigenLevel.GetComponent<RectTransform>();

    }

    private void Update()
    {

        {
            // Obt�n el ancho actual de la imagen
            float anchoActual = rtOxigenLevel.sizeDelta.x; // Calcula la reducci�n por segundo
            if (anchoActual <= 0)
            {
                anchoActual = sizeOxigenLevel ;
            }
            float reduccion = sizeOxigenLevel * porcentajeReduccion * Time.deltaTime; // Calcula el nuevo ancho reduci�ndolo gradualmente
            float nuevoAncho = Mathf.Max(0, anchoActual - reduccion); // Aplica el nuevo ancho al RectTransform de la imagen
            rtOxigenLevel.sizeDelta = new Vector2(nuevoAncho, rtOxigenLevel.sizeDelta.y);
        }
    }

}
