using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomEnemy : MonoBehaviour
{
    public CanvasController canvasController;
    public float oxygenReductionTime = 60f; // Variable pública para configurar el tiempo de reducción de oxígeno desde el editor

    public float patrolRange = 10f; // Radio de patrulla
    public Transform centrePoint; // Punto central del área de patrulla
    public AudioSource audio;

    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!agent.isOnNavMesh)
        {
            return;
        }

        // Patrulla aleatoria
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, patrolRange, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                agent.SetDestination(point);
            }
        }

        SetWalkingAnimation(agent.velocity.sqrMagnitude > 0f);
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    void SetWalkingAnimation(bool isWalking)
    {
        animator.SetBool("walkParam", isWalking);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            audio.Play();
            animator.SetBool("punchParam", true);
            animator.SetBool("walkParam", false);
            agent.isStopped = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Chasing player!");
            animator.SetBool("punchParam", false);
            agent.isStopped = false;
            SetWalkingAnimation(agent.velocity.sqrMagnitude > 0f);
        }
    }

    public void PlayerAttackEnd()
    {
        Debug.Log("Player attack animation ended!");
        animator.SetBool("punchParam", false);
        agent.isStopped = false;
        SetWalkingAnimation(false);

        if (canvasController != null)
        {
            canvasController.DecreaseOxygenTime(oxygenReductionTime);
        }
    }
}
