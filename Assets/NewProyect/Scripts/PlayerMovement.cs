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
    public CanvasController canvasController; 

    private bool isWalking = false;
    private bool picking = false;
    private bool attackLever = false;
    private bool attackLevernt = false;
    public bool haveLever = false;
    private Animator anim;

    public List<string> itemsCollected = null;
    public List<string> itemsToCollect = null;

    private bool canMove = true; // Nueva variable para controlar el movimiento

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = normalSpeed; 
        ResetAnimationStates();
    }

    void Update()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        agent.speed = isSprinting ? sprintSpeed : normalSpeed;

        if (isSprinting && canvasController != null)
        {
            canvasController.DecreaseOxygenTime(3 * Time.deltaTime);
        }

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
            if (Input.GetMouseButtonDown(0) && canMove) // Verificar si el movimiento está permitido
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    agent.SetDestination(hit.point);
                }
            }
            isWalking = agent.velocity.magnitude > 0.1f;
        }

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

    public void StartAttackLever()
    {
        if (haveLever)
        {
            attackLever = true;
            canMove = false; // Desactivar el movimiento temporalmente
            Invoke("EnableMovement", 0.5f); // Volver a activar el movimiento después de 0.5 segundos
        }
    }

    public void StartAttackNoLever()
    {
        if (!haveLever)
        {
            attackLevernt = true;
            canMove = false; // Desactivar el movimiento temporalmente
            Invoke("EnableMovement", 0.5f); // Volver a activar el movimiento después de 0.5 segundos
        }
    }

    void EnableMovement()
    {
        canMove = true;
    }

    public void LookAtTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}
