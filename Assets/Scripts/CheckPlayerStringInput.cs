using System;
using UnityEngine;
using UnityEngine.UI;

public class CheckPlayerStringInput : MonoBehaviour
{
    private GameObject[] _enemies;
    private bool _isCorrect = false;
    private GameObject _taggingEnemy;

    public string[] correctStrings;
    public InputField inputField;
    public Text promptText; // assign in inspector

    private string currentTargetString;

    void Start()
    {
        GetComponent<CanvasGroup>().alpha = 0f;
        GetComponent<Image>().color = Color.grey;

        SelectRandomString();
    }

    void SelectRandomString()
    {
        if (correctStrings.Length > 0)
        {
            currentTargetString = correctStrings[UnityEngine.Random.Range(0, correctStrings.Length)];

            if (promptText != null)
                promptText.text = "Type: " + currentTargetString;
        }
        else
        {
            Debug.LogWarning("No correct strings provided in the array.");
        }
    }

    public void SetTaggedEnemy(GameObject enemy)
    {
        _taggingEnemy = enemy;
        SelectRandomString(); // Choose new string each time an enemy tags you
    }

    public void checkPlayerStringInput(string playerInput)
    {
        GetComponent<Image>().color = Color.grey;

        _isCorrect = playerInput.Equals(currentTargetString, StringComparison.Ordinal);

        _enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (_isCorrect)
        {
            OnSuccessEnteredText();
        }
        else
        {
            GetComponent<Image>().color = Color.red;
            inputField.text = "";
        }
    }

    void OnSuccessEnteredText()
    {
        GetComponent<CanvasGroup>().alpha = 0f;
        inputField.text = "";
        _isCorrect = false;

        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;

        foreach (GameObject enemy in _enemies)
        {
            if (enemy != null && !enemy.GetComponent<EnemyMovement>().canMove)
            {
                enemy.GetComponent<EnemyMovement>().canMove = true;
            }
        }

        if (_taggingEnemy != null)
        {
            Destroy(_taggingEnemy);
            _taggingEnemy = null;
        }
    }
}
