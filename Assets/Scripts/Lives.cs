using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;

    public Text livesText; // Assign in inspector
    public HealthManager healthManager;

    public CanvasGroup gameOverPanel; // Assign in inspector (the Game Over panel)
    public float gameOverDelay = 3f;  // Time before loading LevelSelect

    void Start()
    {
        currentLives = maxLives;
        UpdateLivesText();

        // Make sure the Game Over panel is hidden at start
        if (gameOverPanel != null)
        {
            gameOverPanel.alpha = 0f;
            gameOverPanel.interactable = false;
            gameOverPanel.blocksRaycasts = false;
        }
    }

    public void LoseLife()
    {
        if (currentLives <= 0)
            return; // Prevent lives from going negative or duplicate game over

        currentLives--;

        if (currentLives <= 0)
        {
            Debug.Log("Game Over");

            // Show Game Over panel and begin delay
            if (gameOverPanel != null)
            {
                gameOverPanel.alpha = 1f;
                gameOverPanel.interactable = true;
                gameOverPanel.blocksRaycasts = true;
            }

            StartCoroutine(LoadLevelSelectAfterDelay());
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

    System.Collections.IEnumerator LoadLevelSelectAfterDelay()
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene("LevelSelect");
    }
}
