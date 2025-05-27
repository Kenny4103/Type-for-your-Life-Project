using System;
using UnityEngine;

public class KeyCollect : MonoBehaviour
{
    public string playerTag = "Player";
    public Boolean keyCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            Destroy(gameObject);
            keyCollected = true;
        }
    }
}
