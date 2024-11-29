using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigEnemy : MonoBehaviour
{
    public GameObject item;
    public int velocity;
    public int vida = 5;

    void Update()
    {
        transform.LookAt(item.transform);
        GetComponent<Rigidbody>().velocity = transform.right * velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bala")
        {
            Destroy(collision.collider.gameObject);
            vida--;
            if (vida == 0)
                Destroy(gameObject);
        }

        if(collision.collider.tag == "Player")
        {
            //SceneManager.LoadScene(0);
            FindObjectOfType<PlayerHealth>().RecibirGolpe();
        }
    }
}
