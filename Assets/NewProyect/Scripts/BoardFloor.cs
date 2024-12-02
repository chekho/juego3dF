using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject PosterMenu;
    public GameObject PosterBoard;
    private CanvasController canvas;
    private PlayerMovement pm;

    private void Start()
    {
        canvas = FindObjectOfType<CanvasController>();
        pm = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PosterBoard.activeInHierarchy)
        {
            PosterMenu.SetActive(true);
            if (!pm.itemsCollected.Contains("Poster"))
            {
                pm.itemsCollected.Add("Poster");
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PosterBoard.SetActive(false);
            PosterMenu.SetActive(false);
        }
    }
}
