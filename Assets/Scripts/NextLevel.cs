using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string nextSceneName = "Room_2";
    public string playerTag = "Player";
    public KeyCollect keyCollect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && keyCollect.keyCollected)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
