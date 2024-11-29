using UnityEngine;
using UnityEngine.AI;

public class PauseArea : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PauseGame();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        // Desactivar los comportamientos de los enemigos y otros objetos con la etiqueta "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.isStopped = true;
            }

            MonoBehaviour[] scripts = enemy.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                if (!(script is NavMeshAgent)) // Evita deshabilitar el propio NavMeshAgent
                {
                    script.enabled = false;
                }
            }
        }

        // Pausar otros elementos del juego si es necesario
    }

    void ResumeGame()
    {
        // Reactivar los comportamientos de los enemigos y otros objetos con la etiqueta "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.isStopped = false;
            }

            MonoBehaviour[] scripts = enemy.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                if (!(script is NavMeshAgent)) // Evita habilitar el propio NavMeshAgent
                {
                    script.enabled = true;
                }
            }
        }

        // Reanudar otros elementos del juego si es necesario
    }
}
