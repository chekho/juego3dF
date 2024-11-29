using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public GameObject[] setPoints;
    public float speed = 2f;
    private int setPointsIndex = 0;

    private void Update()
    {
        MovePlatform();        
    }

    private void MovePlatform()
    {
        if(Vector3.Distance(transform.position, setPoints[setPointsIndex].transform.position)< 0.1f)
        {
            setPointsIndex++;

            if(setPointsIndex >= setPoints.Length)
            {
                setPointsIndex = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, setPoints[setPointsIndex].transform.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
