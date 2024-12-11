using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab;           // The enemy to spawn
    public Transform spawnPoint;             // Where the enemy will spawn
     public int initialSpawnCount = 5;
    public float spawnInterval = 3f;         // Time between spawns
    public int maxEnemies = 10;               // Maximum number of active enemies

    private int currentEnemyCount = 0;       // Tracks the number of active enemies
    private float nextSpawnTime = 0f;        // Tracks when the next enemy should spawn

    
    void Start()
    {
        // Spawn the initial set of enemies
        for (int i = 0; i < initialSpawnCount; i++)
        {
            if (currentEnemyCount < maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    void Update()
    {
        // Check if it's time to spawn a new enemy and if the limit is not exceeded
        if (Time.time >= nextSpawnTime && currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval; // Schedule the next spawn
        }
    }

    private void SpawnEnemy()
    {
        

        // Spawn the enemy and increase the count
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        currentEnemyCount++;

         // Set a random or specific point value for the spawned enemy
    Enemy enemy = newEnemy.GetComponent<Enemy>();
    if (enemy != null)
    {
        enemy.pointValue = Random.Range(5, 20); // Random points between 5 and 20
    }

        // Subscribe to the enemy's destruction event to decrease the count
        newEnemy.GetComponent<Enemy>().OnEnemyDestroyed += HandleEnemyDestroyed;
    }

    private void HandleEnemyDestroyed()
    {
        currentEnemyCount--;
    }
}
