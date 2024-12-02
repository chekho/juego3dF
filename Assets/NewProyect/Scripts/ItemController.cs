using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public string name; 
    private PlayerMovement pm;
    private CanvasController cc;

    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        cc = FindObjectOfType<CanvasController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pm.CollectItem(name);
            gameObject.SetActive(false);
            cc.ShowAlertItemCollectedWithChangeInText((name + " recolectado"));
        }
    }

}
