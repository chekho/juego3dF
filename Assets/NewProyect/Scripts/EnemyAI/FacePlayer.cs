using UnityEngine;
using UnityEngine.AI;

public class BlockPlayer : MonoBehaviour
{
    public Transform player;
    public float stopDistance = 2f; // Distancia mínima al jugador
    public float predictionTime = 1f; // Tiempo de predicción para calcular la futura posición del jugador
    public float safeDistance = 1.5f; // Distancia mínima para no chocar

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Calcular la futura posición del jugador
        Vector3 futurePosition = player.position + player.forward * predictionTime;
        Vector3 directionToPlayer = futurePosition - transform.position;

        // Calcular la posición objetivo para bloquear al jugador manteniendo distancia
        Vector3 targetPosition = player.position + player.forward * stopDistance;

        // Solo moverse si el enemigo está delante del jugador, no detrás, y manteniendo distancia
        if (Vector3.Dot(directionToPlayer.normalized, player.forward) > 0 && Vector3.Distance(transform.position, player.position) > safeDistance)
        {
            agent.SetDestination(targetPosition);
        }
        else if (Vector3.Distance(transform.position, player.position) <= safeDistance)
        {
            // Detenerse si está demasiado cerca
            agent.isStopped = true;
        }
        else
        {
            // Reanudar movimiento si está a una distancia segura
            agent.isStopped = false;
        }
    }
}
