using UnityEngine;

public class CheckPlayerStringInput : MonoBehaviour
{
    private GameObject[] enemies;

    void Start()
    {
        GetComponent<CanvasGroup>().alpha = 0f;
    }
    
    public void checkPlayerStringInput( string playerInput)
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (true)/// implement this texts to check against if player got it right
        {
            onSuccessEnteredText();
        }
    }

    void onSuccessEnteredText()
    {
        GameObject.Find("Typing Panel").GetComponent<CanvasGroup>().alpha = 0f;
        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
        foreach (GameObject enemy in enemies)
        {
            if (!enemy.GetComponent<EnemyMovement>().canMove)
            {
                enemy.GetComponent<EnemyMovement>().canMove = true;
            }
        }
    }
}
