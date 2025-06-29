using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CheckPlayerStringInput : MonoBehaviour
{
    private GameObject[] _enemies;
    private bool _isCorrect = false;
    public String[] correctStrings;// Must be set from -> Scene -> Background Canvas -> Typing Panel
    public InputField inputField; // Must be selected child component not a must but for the sake of simplicity.

    void Start()
    {
        GetComponent<CanvasGroup>().alpha = 0f; // set Alpha of Typing panel
        GetComponent<Image>().color = Color.grey; // set color of Typing panel
    }
    
    public void checkPlayerStringInput( string playerInput)
    {
        GetComponent<Image>().color = Color.grey;
        
        if (correctStrings.Length == 0)
        {
            Debug.Log("ther are no strings in CorrectStrings array");
        }

        foreach (String correctString in correctStrings)
        {
            if (!_isCorrect) _isCorrect = correctString == playerInput;
        }
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

        GameObject.Find("Typing Panel").GetComponent<CanvasGroup>().alpha = 0f;
        inputField.text = "";
        _isCorrect = false;
        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
        foreach (GameObject enemy in _enemies)
        {
            if (!enemy.GetComponent<EnemyMovement>().canMove)
            {
                enemy.GetComponent<EnemyMovement>().canMove = true;
            }
        }
    }
}
