using System;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;      // Prefab to spawn
    public Transform spawnPoint;        // Location where enemy should appear
    public float spawnInterval = 20f;   // Time between spawns
    public float initialDelay = 3f;     // Delay before first spawn

    private GameObject currentEnemy;
    private float timer;
    private bool spawnedInit = false;

    void Start()
    {
        timer = initialDelay;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if (!spawnedInit)
            {
                SpawnEnemy();
                Debug.Log("Enemy Spawned");
                spawnedInit = true;
                timer = spawnInterval;
            }
            else if (currentEnemy == null)
            {
                SpawnEnemy();
                Debug.Log("Enemy Spawned");
                timer = spawnInterval;
            }
            else
            {
                // Enemy still alive, just reset timer to check again in the future
                timer = 1f;
            }
        }
    }

    void SpawnEnemy()
    {
        currentEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
