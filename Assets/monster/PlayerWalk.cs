using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public float rotationSpeed = 3f;
    public int speed = 3;
    private PlayerAnimations playerAnim;
    private float rotateY;
    private float h, v;
    public Transform groundCheckPosition;
    public float jumpPower = 200f;
    public float radius = 0.3f;
    public LayerMask groundLayer;
    private Rigidbody rb;
    private bool isGrounded, hasJumped;

    void Awake()
    {
        playerAnim = GetComponent<PlayerAnimations>();
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        CheckMovement();
        AnimatePlayer();
        CheckAttack();
        GroundCollisionAndJump();
    }

    private void CheckMovement()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        
        rotateY -=  h * rotationSpeed;

        transform.localRotation = Quaternion.AngleAxis(rotateY, Vector3.up);

        Vector3 moveDirection = new Vector3(0, 0, v).normalized;
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    void AnimatePlayer()
    {
        if(v != 0)
        {
            playerAnim.PlayerWalk(true);
        }
        else
        {
            playerAnim.PlayerWalk(false);
        }
    }

    void CheckAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAnim.PlayerAttack();
        }
    }

    void GroundCollisionAndJump()
    {
        isGrounded = Physics.OverlapSphere(groundCheckPosition.position, radius, groundLayer).Length > 0;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded)
            {
                rb.AddForce(new Vector3(0, jumpPower, 0));
                hasJumped = true;
                playerAnim.Jumped(true);
            }
        }
    }

    void OnCollisionEnter(Collision target)
    {
        if(target.gameObject.tag == "Ground") 
        {
            if (hasJumped)
            {
                hasJumped = false;
                playerAnim.Jumped(false);
            }
        }
    }
}
