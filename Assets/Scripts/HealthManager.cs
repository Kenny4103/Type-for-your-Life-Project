using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public Text healthText; // Assign in inspector
    public Lives livesManager; // Assign in inspector

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    public void TakeDamage()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            livesManager.LoseLife();
        }

        UpdateHealthText();
    }

    public void UpdateHealthText()
    {
        if (healthText != null)
            healthText.text = "Health: " + currentHealth;
    }
}
