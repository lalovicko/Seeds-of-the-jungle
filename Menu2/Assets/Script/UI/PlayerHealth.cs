using UnityEngine;
using TMPro; // Librería necesaria para usar TextMeshPro

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI healthText; // Arrastra tu texto aquí

    [SerializeField] private UIStatemanager uiManager; // NUEVO

    private float currentHealth;
    private bool isDead = false;

    void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthUI(); // Actualiza el texto al empezar

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        // Evitamos que la vida baje de 0 en el texto
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthUI();
        Debug.Log("Vida actual: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Función pequeña para no repetir código
    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Vida: " + currentHealth.ToString();
        }
    }

    void Die()
    {
        isDead = true;

        // Cambiar estado del juego a Dead (para el debug)
        if (uiManager != null)
            uiManager.ChangeState(UIStatemanager.UIState.Dead);

        Time.timeScale = 0f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (GetComponent<PlayerMovement>() != null)
            GetComponent<PlayerMovement>().enabled = false;
    }
}
