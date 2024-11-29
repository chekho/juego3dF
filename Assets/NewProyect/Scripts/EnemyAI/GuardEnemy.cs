using UnityEngine;
using UnityEngine.AI;

public class GuardEnemy : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 10f;
    public float chaseRadius = 15f;

    private NavMeshAgent agent;
    private Animator animator;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Obtener el componente Animator
    }

    void Update()
{
    // Verificar si el agente está habilitado y colocado en el NavMesh
    if (!agent.isOnNavMesh)
    {
        return; // Salir para evitar llamar a SetDestination
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
        agent.SetDestination(transform.position); // O puede patrullar en su área.
    }

    // Verificar la velocidad del NavMeshAgent para activar la animación de caminar
    if (agent.velocity.sqrMagnitude > 0f)
    {
        SetWalkingAnimation(true); // Activar animación de caminar si está en movimiento
    }
    else
    {
        SetWalkingAnimation(false); // Activar animación de idle si no está en movimiento
    }
}


    void SetWalkingAnimation(bool isWalking)
    {
        animator.SetBool("walkParam", isWalking); // Actualizar el parámetro del Animator
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            animator.SetBool("punchParam", true); // Activar animación de punch
            animator.SetBool("walkParam", false); // Desactivar animación de caminar
            agent.isStopped = true; // Detener el movimiento del NavMeshAgent
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Chasing player!");
            animator.SetBool("punchParam", false); // Desactivar animación de punch
            agent.isStopped = false; // Reanudar el movimiento del NavMeshAgent
            SetWalkingAnimation(isChasing); // Reanudar animación de caminar si está persiguiendo
        }
    }

    // Función para manejar el final del ataque del jugador
    public void PlayerAttackEnd()
    {
        Debug.Log("Player attack animation ended!");
        animator.SetBool("punchParam", false); // Asegurarse de desactivar el parámetro de punch
        agent.isStopped = false; // Detener el movimiento del NavMeshAgent
        SetWalkingAnimation(false); // Asegurarse de que la animación de caminar se detenga
    }
}
