using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public GameObject OxigenLevel;
    public GameObject FurnitureMenu;
    public float porcentajeReduccion = 0.05f; // Reducción del 5% por segundo
    public GameObject ImagesPanel;
    public List<Image> imagesMenu;
    public TextMeshProUGUI OxigenLevelText;

    private RectTransform rtOxigenLevel;
    private float sizeOxigenLevel = 800f;
    private PlayerMovement pm;

    private void Start()
    {
        rtOxigenLevel = OxigenLevel.GetComponent<RectTransform>();
        pm = FindObjectOfType<PlayerMovement>();
        imagesMenu = gameObject.GetComponentsInChildren<Image>().Where(i => i.name.EndsWith("Menu")).ToList();
        rtOxigenLevel.sizeDelta = new Vector2(sizeOxigenLevel, rtOxigenLevel.sizeDelta.y);
        HideMenus();
    }

    public void HideMenus()
    {
        imagesMenu.ForEach(im => im.gameObject.SetActive(false));
    }

    private void Update()
    {
        // Actualización del nivel de oxígeno
        UpdateOxygenLevel();

        // Validar y mostrar ítems en el panel
        ValidateItems();
    }

    private void UpdateOxygenLevel()
    {
        float anchoActual = rtOxigenLevel.sizeDelta.x;
        if (anchoActual <= 0)
        {
            anchoActual = sizeOxigenLevel;
        }

        float reduccion = sizeOxigenLevel * porcentajeReduccion * Time.deltaTime;
        float nuevoAncho = Mathf.Max(0, anchoActual - reduccion);
        rtOxigenLevel.sizeDelta = new Vector2(nuevoAncho, rtOxigenLevel.sizeDelta.y);

        OxigenLevelText.text = "O2 = " + (Mathf.Round((nuevoAncho * 100) / sizeOxigenLevel)).ToString() + "%";
    }

    private void ValidateItems()
    {
        var listItems = pm.itemsToCollect;
        List<string> itemsCurrent = pm.itemsCollected;
        bool missingItems = false;

        foreach (var item in listItems)
        {
            var imageObject = ImagesPanel.transform.Find(item);
            if (imageObject != null)
            {
                GameObject f = imageObject.gameObject;
                Image img = f.GetComponent<Image>();
                Color newColor = img.color;

                if (!itemsCurrent.Contains(item))
                {
                    newColor.a = 0.3f; // Hacerlo semitransparente
                    missingItems = true; // Marcar que falta un ítem
                }
                else
                {
                    newColor.a = 1f; // Color sólido si el ítem está recogido
                }

                img.color = newColor;
            }
            else
            {
                Debug.LogWarning("No se encontró ningún GameObject con el nombre especificado.");
            }
        }
    }

    public void ShowFurnitureMenu()
    {
        FurnitureMenu.SetActive(!FurnitureMenu.active);
    }
}
