using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public TextMeshProUGUI healthText; // Referencia al TextMeshPro para mostrar la vida
    public Canvas healthCanvas; // Referencia al canvas de vida
    private Camera mainCamera; // Referencia a la cámara principal
    public CanvasController canvasController; // Referencia al CanvasController

    void Start()
    {
        currentHealth = maxHealth;
        mainCamera = Camera.main; // Obtener la cámara principal
        UpdateHealthText();
    }

    void Update()
    {
        // Hacer que el canvas siga al enemigo
        if (healthCanvas != null)
        {
            healthCanvas.transform.position = transform.position + new Vector3(0, 2, 0); // Ajusta el offset según sea necesario

            // Hacer que el canvas siempre tenga la misma orientación que la cámara
            if (mainCamera != null)
            {
                healthCanvas.transform.rotation = mainCamera.transform.rotation;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // Asegurarse de que la salud no sea negativa
        UpdateHealthText();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = $"HP: {currentHealth}";
        }
    }

    void Die()
    {
        // Aumentar el puntaje en 1000 puntos
        if (canvasController != null)
        {
            canvasController.IncreaseScore(1000);
        }

        // Aquí puedes añadir la lógica para lo que pasa cuando el enemigo muere
        Destroy(gameObject);
    }
}
