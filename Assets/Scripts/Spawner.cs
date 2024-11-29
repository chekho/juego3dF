using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    public GameObject prefabEnemigo;
    public int maxEnemigos;
    public int numEnemigosActual = 0;

    
    void Start()
    {
        InvokeRepeating("GenerateEnemigo", 0, 3);   
    }

    void GenerateEnemigo()
    {
        if (numEnemigosActual < maxEnemigos)
        {
            Instantiate(prefabEnemigo, transform.position, Quaternion.identity);
            numEnemigosActual++;
        }
    }
}
