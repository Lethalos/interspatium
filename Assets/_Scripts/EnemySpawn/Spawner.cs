using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnRadius = 10f;
    private EnemyPoolHandler enemyPoolHandler;
    private Camera mainCamera;

    void Start()
    {
        enemyPoolHandler = FindObjectOfType<EnemyPoolHandler>();
        mainCamera = Camera.main;

        // Example of spawning enemies
        InvokeRepeating("SpawnEnemies", 0f, 5f);
    }

    private void SpawnEnemies()
    {
        SpawnEnemy(EnemyType.Ranged);
        SpawnEnemy(EnemyType.Melee);
    }

    void SpawnEnemy(EnemyType type)
    {
        if (type == EnemyType.Ranged)
        {
            RangedEnemy enemy = enemyPoolHandler.GetRangedEnemy();
            enemy.transform.position = GetRandomSpawnPositionOutsideCamera();
            enemy.gameObject.SetActive(true);
        }
        else if (type == EnemyType.Melee)
        {
            MeleeEnemy enemy = enemyPoolHandler.GetMeleeEnemy();
            enemy.transform.position = GetRandomSpawnPositionOutsideCamera();
            enemy.gameObject.SetActive(true);
        }
    }

    Vector3 GetRandomSpawnPositionOutsideCamera()
    {
        Vector3 spawnPosition;
        bool positionFound = false;

        while (!positionFound)
        {
            spawnPosition = transform.position + (Random.insideUnitSphere * spawnRadius);
            spawnPosition.y = 0; // Ensure the spawn position is on the ground

            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(spawnPosition);
            if (viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1)
            {
                positionFound = true;
                return spawnPosition;
            }
        }

        return transform.position; // Default fallback position
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
