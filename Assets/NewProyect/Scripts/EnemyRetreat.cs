using UnityEngine;
using UnityEngine.AI;

public class EnemyRetreat : MonoBehaviour
{
    public float retreatDistance = 2f; // Distancia máxima del knockback
    public float detectionDistance = 2f; // Distancia de detección del jugador
    public LayerMask enemyLayer; // Capa de enemigos
    public float knockbackSpeed = 5f; // Velocidad del knockback

    public Transform player;
    private NavMeshAgent navMeshAgent;

    private bool isKnockedBack = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player has the tag 'Player'.");
        }

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent not found on the enemy.");
        }
    }

    void Update()
    {
        if (player == null || isKnockedBack)
        {
            return;
        }

        // Detectar clic y distancia al jugador
        if (Vector3.Distance(transform.position, player.position) <= detectionDistance && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
            {
                if (hit.transform == transform)
                {
                    ApplyKnockback();
                }
            }
        }
    }

    void ApplyKnockback()
    {
        isKnockedBack = true;

        // Pausar el NavMeshAgent
        navMeshAgent.enabled = false;

        // Calcular dirección y limitar la distancia
        Vector3 retreatDirection = (transform.position - player.position).normalized;
        Vector3 targetPosition = transform.position + retreatDirection * retreatDistance;

        // Iniciar knockback interpolado
        StartCoroutine(PerformKnockback(targetPosition));
    }

    System.Collections.IEnumerator PerformKnockback(Vector3 targetPosition)
    {
        float elapsed = 0f;
        float duration = retreatDistance / knockbackSpeed; // Duración ajustada según la velocidad

        Vector3 initialPosition = transform.position;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        // Sincronizar NavMeshAgent con la nueva posición
        navMeshAgent.Warp(transform.position);

        // Reactivar el NavMeshAgent
        navMeshAgent.enabled = true;
        isKnockedBack = false;
    }
}
