using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;

    public Text livesText; // Assign in inspector
    public HealthManager healthManager;

    void Start()
    {
        currentLives = maxLives;
        UpdateLivesText();
    }

    public void LoseLife()
    {
        currentLives--;

        if (currentLives <= 0)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("LevelSelect"); // Load title scene
        }
        else
        {
            // Reset health
            healthManager.currentHealth = healthManager.maxHealth;
            healthManager.UpdateHealthText();
        }

        UpdateLivesText();
    }

    void UpdateLivesText()
    {
        if (livesText != null)
            livesText.text = "Lives: " + currentLives;
    }
}
