using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMov : MonoBehaviour
{
    public float speed = 3f;
    public float horizontalMove;
    public float verticalMove;
    private Rigidbody rb;
    public AudioSource bellRing;
    public Light myLight;
    public ParticleSystem particles;
    public int numItems;
    
    public float jumpForce = 300.0f;
    private bool grounded = true;
    public float turbo = 2f;

    public GameObject prefabBala;

    void start()
    {
        rb = GetComponent<Rigidbody>();
        bellRing = GetComponent<AudioSource>();
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        transform.Rotate(0, Input.GetAxisRaw("Mouse X"), 0);
        
        Jump();

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject balaAuxiliar = Instantiate(prefabBala, transform.position + transform.forward * 2, Quaternion.identity);
            balaAuxiliar.GetComponent<Rigidbody>().AddForce(transform.forward * 800);
            Destroy(balaAuxiliar, 2);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed * turbo;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = speed / turbo;
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(horizontalMove * speed * Time.deltaTime, 0, verticalMove * speed * Time.deltaTime);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        bellRing.Play();
        if(other.tag == "Item")
        {
            FindObjectOfType<PlayerHealth>().ItemConseguido();
        }
        //numItems++;
        //myLight.intensity = 3;
        //particles.startLifetime = 5;
        
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    myLight.intensity = 1;
    //}

    public void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector2.up * jumpForce);
            grounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }
    
}
