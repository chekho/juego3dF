using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsItemsController : MonoBehaviour
{
    public GameObject menu;
    private CanvasController canvas;
    private List<Image> images;
    private List<Image> buttons;

    private void Start()
    {
        canvas = FindObjectOfType<CanvasController>();
        buttons = canvas.transform.GetComponentsInChildren<Image>().Where(i => i.gameObject.activeInHierarchy).ToList();
        //images = canvas.GetComponentsInChildren<Image>().Where(i=>i.name.EndsWith("Menu")).ToList();
        images = canvas.imagesMenu;
        images.ForEach(img => {
            img.gameObject.SetActive(false);
        });
    }

    public void ShowMenu()
    {
        // Oculta todos los menús antes de mostrar el nuevo
        HideAllMenus();
        menu.SetActive(!menu.activeInHierarchy);
    }


    private void HideAllMenus()
    {
        var allMenus = FindObjectsOfType<ButtonsItemsController>();
        foreach (var menuController in allMenus)
        {
            menuController.menu.SetActive(false);
        }
    }
}
