using UnityEngine;
using UnityEngine.AI;

public class GuardEnemy : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 10f;
    public float chaseRadius = 15f;
    public CanvasController canvasController;
    public float oxygenReductionTime = 60f; // Variable pública para configurar el tiempo de reducción de oxígeno desde el editor
    public AudioSource audioSource;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    void Update()
    {
        if (!agent.isOnNavMesh)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer < detectionRadius)
        {
            isChasing = true;

        }

        if (isChasing && distanceToPlayer < chaseRadius)
        {
            agent.SetDestination(player.position);

        }
        else
        {
            isChasing = false;
            agent.SetDestination(transform.position);
            audioSource.Play();
        }

        if (agent.velocity.sqrMagnitude > 0f)
        {
            SetWalkingAnimation(true);
        }
        else
        {
            SetWalkingAnimation(false);
        }
    }

    void SetWalkingAnimation(bool isWalking)
    {
        animator.SetBool("walkParam", isWalking);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
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
            audioSource.Play();
            SetWalkingAnimation(isChasing);
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
            canvasController.DecreaseOxygenTime(oxygenReductionTime); // Usar la variable configurada en el editor
        }
    }
}
