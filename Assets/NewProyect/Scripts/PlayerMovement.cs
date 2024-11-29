using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public float normalSpeed = 5f;
    public float sprintSpeed = 15f;
    public float turnSpeed = 5f;

    void Start() 
    { 
        agent = GetComponent<NavMeshAgent>(); 
        agent.speed = normalSpeed; // Establecer la velocidad inicial
    }

    void Update() 
    { 
        // Detectar si la tecla Shift está presionada
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            agent.speed = sprintSpeed;
        }
        else
        {
            agent.speed = normalSpeed;
        }

        // Moverse con el ratón
        if (Input.GetMouseButtonDown(0)) 
        { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hit; 

            if (Physics.Raycast(ray, out hit)) 
            {
                agent.SetDestination(hit.point); 
            } 
        } 

        // Moverse con las flechas del teclado
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            move += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            move += Vector3.back;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move += Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            move += Vector3.right;
        }

        if (move != Vector3.zero)
        {
            // Cancela el destino anterior
            agent.ResetPath();

            // Mueve al agente
            agent.Move(move * Time.deltaTime * agent.speed);

            // Gira al agente en la dirección del movimiento de forma suave
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }
    }
}
