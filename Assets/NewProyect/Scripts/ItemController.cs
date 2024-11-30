using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public string name; 
    private PlayerMovement pm;

    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colisión de " + other.tag + " con " + name);
        if (other.CompareTag("Player"))
        {
            pm.CollectItem(name);
            //destruir objeto
            gameObject.SetActive(false);
        }
    }

}
