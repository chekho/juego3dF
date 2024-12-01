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

    public List<string> itemsCollected = null;
    public List<string> itemsToCollect = null;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = normalSpeed; // Establecer la velocidad inicial
        ResetAnimationStates();
    }

    void Update()
    {
        if (itemsCollected.Contains("Lever"))
        {
            var lev = gameObject.transform.Find("mario_right_hand_open.001");
            lev.gameObject.SetActive(true);
            haveLever = true;
        }
        else
        {
            var lev = gameObject.transform.Find("mario_right_hand_open.001");
            lev.gameObject.SetActive(false);
            haveLever = false;

        }
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
            }
            isWalking = agent.velocity.magnitude > 0.1f;
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

    public void CollectItem(string itemName)
    {
        itemsCollected.Add(itemName);
    }
}