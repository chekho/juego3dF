using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    //private string _Walk_Animation = "Walk";
    private string walkParameter = "WalkParam";
    private string attackParameter = "AttackParam";
    private string jumpParameter = "JumpParam";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void WalkAnim()
    {
        anim.Play("Walk");
    }

    public void PlayerAttack()
    {
        anim.SetBool(attackParameter, true);
    }

    public void PlayerAttackEnd()
    {
        anim.SetBool(attackParameter, false);
    }

    public void PlayerWalk(bool walk)
    {
        anim.SetBool(walkParameter, walk);
    }

    public void Jumped(bool hasJumped)
    {
        anim.SetBool(jumpParameter, hasJumped);
    }
}
