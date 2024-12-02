using UnityEngine;
using UnityEngine.AI;

public class EnemyRetreat : MonoBehaviour
{
    public float retreatDistance = 2f; // Distancia máxima del knockback
    public float detectionDistance = 2f; // Distancia de detección del jugador
    public LayerMask enemyLayer; // Capa de enemigos
    public float knockbackSpeed = 5f; // Velocidad del knockback
    public int damageAmount = 10; // Cantidad de daño al golpear

    public Transform player;
    private NavMeshAgent navMeshAgent;
    private PlayerMovement playerMovement; // Referencia al PlayerMovement del jugador
    private EnemyHealth enemyHealth; // Referencia al EnemyHealth del enemigo

    private bool isKnockedBack = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player has the tag 'Player'.");
        }
        else
        {
            playerMovement = player.GetComponent<PlayerMovement>(); // Obtener el PlayerMovement del jugador
            if (playerMovement == null)
            {
                Debug.LogError("PlayerMovement script not found on the player.");
            }
        }

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent not found on the enemy.");
        }

        if (enemyHealth == null)
        {
            Debug.LogError("EnemyHealth script not found on the enemy.");
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
                    bool knockbackApplied = ApplyKnockback();
                    if (knockbackApplied)
                    {
                        PlayPlayerAttackAnimation(); // Reproducir la animación de golpe del jugador
                        playerMovement.LookAtTarget(transform.position); // Hacer que el jugador mire al enemigo
                        enemyHealth.TakeDamage(damageAmount); // Reducir la vida del enemigo
                    }
                }
            }
        }
    }

    bool ApplyKnockback()
    {
        if (isKnockedBack) return false;

        isKnockedBack = true;

        // Pausar el NavMeshAgent
        navMeshAgent.enabled = false;

        // Calcular dirección y limitar la distancia
        Vector3 retreatDirection = (transform.position - player.position).normalized;
        Vector3 targetPosition = transform.position + retreatDirection * retreatDistance;

        // Iniciar knockback interpolado
        StartCoroutine(PerformKnockback(targetPosition));
        return true;
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

    void PlayPlayerAttackAnimation()
    {
        // Reproducir la animación de golpe del jugador
        if (playerMovement != null)
        {
            if (playerMovement.haveLever)
            {
                playerMovement.StartAttackLever();
                damageAmount = 10;
            }
            else
            {
                playerMovement.StartAttackNoLever();
                damageAmount = 5;
            }
        }
    }
}
