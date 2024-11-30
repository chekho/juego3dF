using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public GameObject OxigenLevel;
    public GameObject FurnitureMenu;
    public float porcentajeReduccion = 0.05f; // Reducción del 5% por segundo
    public GameObject ImagesPanel;
    public List<Image> imagesMenu;

    private RectTransform rtOxigenLevel;
    private float sizeOxigenLevel = 50f;
    private PlayerMovement pm;

    private void Start()
    {
        rtOxigenLevel = OxigenLevel.GetComponent<RectTransform>();
        pm = FindObjectOfType<PlayerMovement>();
        imagesMenu = gameObject.GetComponentsInChildren<Image>().Where(i => i.name.EndsWith("Menu")).ToList();
        HideMenus();
    }

    public void HideMenus()
    {

        imagesMenu.ForEach(im => im.gameObject.SetActive(false));
    }

    private void Update()
    {
        // Obtén el ancho actual de la imagen
        float anchoActual = rtOxigenLevel.sizeDelta.x; // Calcula la reducción por segundo
        if (anchoActual <= 0)
        {
            anchoActual = sizeOxigenLevel;
        }
        float reduccion = sizeOxigenLevel * porcentajeReduccion * Time.deltaTime; // Calcula el nuevo ancho reduciéndolo gradualmente
        float nuevoAncho = Mathf.Max(0, anchoActual - reduccion); // Aplica el nuevo ancho al RectTransform de la imagen
        rtOxigenLevel.sizeDelta = new Vector2(nuevoAncho, rtOxigenLevel.sizeDelta.y);

        var ListItems = pm.itemsToCollect;
        List<string> ItemsCurrent = pm.itemsCollected;
        foreach (var item in ListItems)
        {
            //obtener imagen
            var imageObject = ImagesPanel.transform.Find(item);
            if (imageObject != null)
            {
                GameObject f = imageObject.gameObject;
                Image img = f.GetComponent<Image>();
                Color newColor = img.color;
                if (!ItemsCurrent.Contains(item)) newColor.a = 0.5f;
                else newColor.a = 1f;
                img.color = newColor;
            }
            else { Debug.LogWarning("No se encontró ningún GameObject con el nombre especificado."); }

        }
    }

    public void ShowFurnitureMenu()
    {
        FurnitureMenu.SetActive(!FurnitureMenu.active);
    }

}
