using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject closedRooms;
    public List<GameObject> rooms;
    public float waitTime;
    private bool SpawnStairs;
    public GameObject Stairs;
    public float stairsSpawnRadius = 5f; // Radius within which stairs can spawn inside the room
    public LayerMask wallLayer; // Layer mask for walls

    void Start()
    {
        // Check for missing room prefabs
        if (bottomRooms.Length == 0 || topRooms.Length == 0 || leftRooms.Length == 0 || rightRooms.Length == 0)
        {
            Debug.LogError("One or more room arrays are empty in RoomTemplates!");
        }

        if (closedRooms == null)
        {
            Debug.LogError("Closed room prefab is missing in RoomTemplates!");
        }
    }

    void Update()
    {
        if (waitTime <= 0 && SpawnStairs == false)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 1) // Target the last room
                {
                    // Get the position of the last room
                    Vector3 roomPosition = rooms[i].transform.position;

                    // Try to find a valid position for the stairs
                    Vector3 validPosition = FindValidPosition(roomPosition);

                    if (validPosition != Vector3.zero)
                    {
                        // Spawn the stairs at the valid position
                        Instantiate(Stairs, validPosition, Quaternion.identity);
                        SpawnStairs = true;
                    }
                    else
                    {
                        Debug.LogWarning("Failed to find a valid position for the stairs.");
                    }
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    private Vector3 FindValidPosition(Vector3 roomPosition)
    {
        int maxAttempts = 10; // Maximum attempts to find a valid position
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            // Generate a random position within the stairs spawn radius
            Vector3 randomOffset = new Vector3(
                Random.Range(-stairsSpawnRadius, stairsSpawnRadius),
                Random.Range(-stairsSpawnRadius, stairsSpawnRadius),
                0f
            );
            Vector3 randomPosition = roomPosition + randomOffset;

            // Check if the position overlaps with a wall
            Collider2D hitCollider = Physics2D.OverlapCircle(randomPosition, 0.5f, wallLayer);
            if (hitCollider == null) // No wall detected at this position
            {
                return randomPosition; // Return the valid position
            }
        }

        return Vector3.zero; // Return zero if no valid position is found
    }
}