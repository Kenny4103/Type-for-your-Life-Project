using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 20f;
    public float initialDelay = 3f;
    public float spawnRadius = 5f; // max distance enemy can be before respawn

    private GameObject currentEnemy;
    private float timer;
    public bool spawnedInit = false;

    void Start()
    {
        timer = initialDelay;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        bool shouldRespawn =
            currentEnemy == null ||
            Vector2.Distance(spawnPoint.position, currentEnemy.transform.position) > spawnRadius;

        if (timer <= 0f && shouldRespawn)
        {
            SpawnEnemy();
            Debug.Log("Enemy Spawned");
            timer = spawnInterval;
            spawnedInit = true;
        }
    }

    void SpawnEnemy()
    {
        currentEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
