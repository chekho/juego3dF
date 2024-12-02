using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public string name;
    private PlayerMovement pm;
    private CanvasController cc;
    public string password = "";

    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        cc = FindObjectOfType<CanvasController>();
        
        for (int i = 0; i < 4; i++)
        {
            password += Random.Range(0, 9).ToString();
        }
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
