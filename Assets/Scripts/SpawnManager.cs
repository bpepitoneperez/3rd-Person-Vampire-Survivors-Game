using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public gameManager gameManager;

    public GameObject[] enemies;
    public GameObject[] powerups;

    private float zSpawnRange = 12.0f;
    private float xSpawnRange = 16.0f;
    private float zPowerupRange = 5.0f;
    private float ySpawnEnemy = 0.75f;
    private float ySpawnPowerup = 0.2f;
    private bool startedSpawning;
    private bool initialSpawn;

    public float powerupSpawnTime = 8.0f;
    public float enemySpawnTime = 6.0f;
    public float startDelay = 1.0f;
    public int enemiesToSpawn = 2;

    private float lastEnemySpawnTime = 0f;
    private float lastPowerupSpawnTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (startedSpawning && !initialSpawn && gameManager.isGameActive)
        {
            initialSpawn = true;
            SpawnRandomEnemy();
        }

        if (startedSpawning && initialSpawn && gameManager.isGameActive)
        {
            lastEnemySpawnTime += Time.deltaTime;
            lastPowerupSpawnTime += Time.deltaTime;

            if (lastEnemySpawnTime >= enemySpawnTime)
            {
                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    SpawnRandomEnemy();
                }
                lastEnemySpawnTime = 0f;
            }

            if (lastPowerupSpawnTime >= powerupSpawnTime)
            {
                SpawnRandomPowerup();
                lastPowerupSpawnTime = 0f;
            }
        }

        //if (startedSpawning && gameManager.isGameActive)
        //{
        //    startedSpawning = false;
        //    SpawnRandomEnemy();
        //}
    }

    public void StartSpawning()
    {
        startedSpawning = true;
    }

    void SpawnRandomEnemy()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        float randomZ = Random.Range(-zSpawnRange, zSpawnRange);
        int randomIndex = Random.Range(0, enemies.Length);

        Vector3 spawnPos = new Vector3(randomX, ySpawnEnemy, randomZ);

        Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].transform.rotation);
    }

    void SpawnRandomPowerup()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        float randomZ = Random.Range(-zPowerupRange, zPowerupRange);

        int randomIndex = Random.Range(0, powerups.Length);

        Vector3 spawnPos = new Vector3(randomX, ySpawnPowerup, randomZ);

        Instantiate(powerups[randomIndex], spawnPos, powerups[randomIndex].transform.rotation);
    }
}
