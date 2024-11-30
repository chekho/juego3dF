using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject OxigenLevel;
    private RectTransform rtOxigenLevel;
    private float sizeOxigenLevel = 50f;

    public GameObject FurnitureMenu;

    public float porcentajeReduccion = 0.05f; // Reducción del 5% por segundo

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
            // Obtén el ancho actual de la imagen
            float anchoActual = rtOxigenLevel.sizeDelta.x; // Calcula la reducción por segundo
            if (anchoActual <= 0)
            {
                anchoActual = sizeOxigenLevel ;
            }
            float reduccion = sizeOxigenLevel * porcentajeReduccion * Time.deltaTime; // Calcula el nuevo ancho reduciéndolo gradualmente
            float nuevoAncho = Mathf.Max(0, anchoActual - reduccion); // Aplica el nuevo ancho al RectTransform de la imagen
            rtOxigenLevel.sizeDelta = new Vector2(nuevoAncho, rtOxigenLevel.sizeDelta.y);
        }
    }

}
