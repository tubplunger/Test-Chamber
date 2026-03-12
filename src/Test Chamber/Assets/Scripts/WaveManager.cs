using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Transform[] spawnPoints;

    public GameObject basicEnemy;
    public GameObject rangedEnemy;
    public GameObject tankEnemy;

    public Transform player;

    public int currentWave = 0;

    public int enemiesToSpawn;
    public int enemiesSpawned;
    public int enemiesAlive;

    public float arenaMinX = -25f;
    public float arenaMaxX = 25f;
    public float arenaMinY = -25f;
    public float arenaMaxY = 25f;

    public float spawnDelay = 1.5f;

    private float spawnTimer;

    private bool waveActive = false;

    void Update()
    {
        if (!waveActive)
        {
            if (enemiesAlive <= 0 && Input.GetKeyDown(KeyCode.E))
            {
                StartNextWave();
            }
            return;
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnDelay && enemiesSpawned < enemiesToSpawn)
        {
            spawnTimer = 0f;
            SpawnEnemy();
        }

        if (enemiesAlive <= 0 && enemiesSpawned >= enemiesToSpawn)
        {
            waveActive = false;
            Debug.Log("Wave cleared! Press E for next wave.");
        }
    }

    void StartNextWave()
    {
        currentWave++;

        enemiesSpawned = 0;
        enemiesAlive = 0;

        enemiesToSpawn = 5 + (currentWave * 3);

        waveActive = true;

        Debug.Log("Starting Wave " + currentWave);
    }

    void SpawnEnemy()
    {
        Vector2 spawnPos = GetSpawnPosition();

        GameObject enemyPrefab = ChooseEnemyType();

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        HealthSystem health = enemy.GetComponent<HealthSystem>();

        if (health != null)
        {
            health.waveManager = this;
        }

        enemiesSpawned++;
        enemiesAlive++;
    }

    GameObject ChooseEnemyType()
    {
        int roll = Random.Range(0, 100);

        if (roll < 60)
            return basicEnemy;

        if (roll < 85)
            return rangedEnemy;

        return tankEnemy;
    }

    Vector2 GetSpawnPosition()
    {
        int attempts = 10;

        while (attempts > 0)
        {
            attempts--;

            float spawnDistance = 15f; // distance from player
            float angle = Random.Range(0f, Mathf.PI * 2);

            Vector2 offset = new Vector2(
                Mathf.Cos(angle),
                Mathf.Sin(angle)
            ) * spawnDistance;

            Vector2 spawnPosition = (Vector2)player.position + offset;

            float padding = 1.5f;

            spawnPosition.x = Mathf.Clamp(spawnPosition.x, arenaMinX + padding, arenaMaxX - padding);
            spawnPosition.y = Mathf.Clamp(spawnPosition.y, arenaMinY + padding, arenaMaxY - padding);

            Vector3 viewport = Camera.main.WorldToViewportPoint(spawnPosition);

            bool visible =
                viewport.x > 0 &&
                viewport.x < 1 &&
                viewport.y > 0 &&
                viewport.y < 1;

            if (!visible)
                return spawnPosition;
        }

        return (Vector2)player.position + Random.insideUnitCircle * 15f;
    }

    public void EnemyDied()
    {
        enemiesAlive--;
    }
}
