using UnityEngine;

public class MouseOverHighlight : MonoBehaviour
{
    public Material highlightMaterial; // Material brillante
    private Material originalMaterial; // Material origina
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            originalMaterial = rend.material;
        }
    }

    void OnMouseEnter()
    {
        if (rend != null && highlightMaterial != null)
        {
            rend.material = highlightMaterial;
        }
    }

    void OnMouseExit()
    {
        if (rend != null && originalMaterial != null)
        {
            rend.material = originalMaterial;
        }
    }
}
