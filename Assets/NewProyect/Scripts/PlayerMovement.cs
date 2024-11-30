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

    private bool isWalking = false;
    private bool picking = false;
    private bool attackLever = false;
    private bool attackLevernt = false;
    public bool haveLever = false;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = normalSpeed; // Establecer la velocidad inicial
        ResetAnimationStates();
    }

    void Update()
    {
        // Detectar si la tecla Shift está presionada
        agent.speed = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? sprintSpeed : normalSpeed;



        // Movimiento con teclado
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            move += Vector3.forward;
            isWalking = true;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            move += Vector3.back;
            isWalking = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move += Vector3.left;
            isWalking = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            move += Vector3.right;
            isWalking = true;
        }

        if (move != Vector3.zero)
        {
            agent.ResetPath();
            agent.Move(move * Time.deltaTime * agent.speed);

            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }
        else
        {
            // Movimiento con el ratón
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    agent.SetDestination(hit.point);
                    isWalking = true;
                }
            }
            isWalking = agent.velocity.magnitude > 0.1f;
        }

        // Detectar si el agente está caminando
        //

        // Simulación de ataque
        if (Input.GetMouseButtonDown(1)) // Clic derecho para atacar
        {
            if (haveLever)
            {
                attackLever = true;
                attackLevernt = false;
            }
            else
            {
                attackLever = false;
                attackLevernt = true;
            }
        }

        // Actualizar animaciones
        ChooseAnimation();
    }

    void ChooseAnimation()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("picking", picking);

        if (attackLever)
        {
            anim.SetTrigger("AttackLever");
            attackLever = false;
        }

        if (attackLevernt)
        {
            anim.SetTrigger("AttackNoLever");
            attackLevernt = false;
        }
    }

    void ResetAnimationStates()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("picking", false);
        anim.ResetTrigger("AttackNoLever");
        anim.ResetTrigger("AttackLever");
    }
}