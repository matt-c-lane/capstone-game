using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab; 
    [SerializeField]
    private float spawnRadius = 10f; // Maximum distance from the spawn point
    [SerializeField]
    private int minEnemies = 2; // Minimum number of enemies to spawn
    [SerializeField]
    private int maxEnemies = 4; // Maximum number of enemies to spawn

    private int totalEnemiesToSpawn; // Total number of enemies to spawn
    private int enemiesSpawned = 0; // Counter for spawned enemies

void Start()
{
    // Check if the room or its parent is tagged as "PlayerSpawnRoom"
    if (gameObject.CompareTag("PlayerSpawnRoom") || transform.parent.CompareTag("PlayerSpawnRoom"))
    {
        Debug.Log("Spawner is in the Player Spawn Room. No enemies will be spawned.");
        Destroy(gameObject); // Disable the spawner
        return;
    }

    // Randomly determine the total number of enemies to spawn (between minEnemies and maxEnemies)
    totalEnemiesToSpawn = Random.Range(minEnemies, maxEnemies + 1);

    // Spawn the enemies
    SpawnEnemies();
}

    private void SpawnEnemies()
{
    int maxAttempts = 10; // Maximum attempts to find a valid position for each enemy

    while (enemiesSpawned < totalEnemiesToSpawn)
    {
        bool validPositionFound = false;
        Vector2 randomPosition = Vector2.zero;

        // Try to find a valid position within the maximum number of attempts
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            // Generate a random position within the spawn radius
            randomPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

            // Check if the position overlaps with a wall tile
            Collider2D hitCollider = Physics2D.OverlapCircle(randomPosition, 0.5f, LayerMask.GetMask("Wall"));
            if (hitCollider == null) // No wall detected at this position
            {
                validPositionFound = true;
                break;
            }
        }

        // If a valid position is found, spawn the enemy
        if (validPositionFound)
        {
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            enemiesSpawned++;
        }
        else
        {
            Debug.LogWarning("Failed to find a valid position for an enemy after maximum attempts.");
            break; // Exit the loop if no valid position is found
        }
    }

    // Disable the spawner after spawning all enemies
    Destroy(gameObject);
}
}