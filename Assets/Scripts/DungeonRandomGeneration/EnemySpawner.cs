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
        while (enemiesSpawned < totalEnemiesToSpawn)
        {
            // Generate a random position within the spawn radius
            Vector2 randomPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

            // Spawn the enemy at the random position
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

            // Increment the counter
            enemiesSpawned++;
        }

        // Disable the spawner after spawning all enemies
        Destroy(gameObject);
    }

    // Optional: Visualize the spawn radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}