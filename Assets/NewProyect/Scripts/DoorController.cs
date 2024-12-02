using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class DoorController : MonoBehaviour
{
    public NavMeshSurface navMeshSurface; // Referencia al NavMeshSurface
    public Animator doorAnimator; // Referencia al Animator
    public bool isOpen = false;

    void Start()
    {
        Debug.Log("DoorController initialized.");
        GetComponent<Collider>().isTrigger = false;
    }

    public void RemoveDoor()
    {
        Debug.Log("RemoveDoor called.");
        gameObject.SetActive(false);
        isOpen = true;

        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
            Debug.Log("NavMesh rebuilt.");
        }
        else
        {
            Debug.LogError("NavMeshSurface is not assigned!");
        }

        if (doorAnimator != null)
        {
            doorAnimator.SetBool("character_nearby", true); // Establecer el parámetro en true
            Debug.Log("character_nearby set to true.");
        }
        else
        {
            Debug.LogError("Animator is not assigned!");
        }
    }
}
