using UnityEngine;
using UnityEngine.AI;

public class RightTurningEnemy : MonoBehaviour
{
    public float detectionRange = 5f; // Rango de detección para los caminos a la derecha
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // Establecer un destino inicial aleatorio o específico
        SetRandomDestination();
    }

    void Update()
    {
        // Detectar el camino a la derecha
        Vector3 rightDirection = transform.right;
        Vector3 forwardDirection = transform.forward;
        
        bool rightPathClear = Physics.Raycast(transform.position, rightDirection, detectionRange);
        bool forwardPathClear = Physics.Raycast(transform.position, forwardDirection, detectionRange);
        
        // Si el camino a la derecha está claro y el camino hacia adelante no lo está, girar a la derecha
        if (rightPathClear && !forwardPathClear)
        {
            Vector3 newDirection = transform.position + rightDirection * detectionRange;
            agent.SetDestination(newDirection);
        }
        else if (!agent.hasPath)
        {
            // Si no hay camino, establecer un nuevo destino
            SetRandomDestination();
        }
    }

    void SetRandomDestination()
    {
        // Establecer un destino aleatorio en el NavMesh
        Vector3 randomDirection = Random.insideUnitSphere * detectionRange;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, detectionRange, 1);
        Vector3 finalPosition = hit.position;
        agent.SetDestination(finalPosition);
    }
}
