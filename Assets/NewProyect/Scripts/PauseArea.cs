using UnityEngine;
using UnityEngine.AI;

public class PauseArea : MonoBehaviour
{
    private CanvasController canvasController;

    void Start()
    {
        canvasController = FindObjectOfType<CanvasController>();
    }

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

        // Pausar el gasto de oxígeno y la actualización de puntaje
        if (canvasController != null)
        {
            canvasController.PauseOxygen();
            canvasController.PauseScore();
        }
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

        // Reanudar el gasto de oxígeno y la actualización de puntaje
        if (canvasController != null)
        {
            canvasController.ResumeOxygen();
            canvasController.ResumeScore();
        }
    }
}
