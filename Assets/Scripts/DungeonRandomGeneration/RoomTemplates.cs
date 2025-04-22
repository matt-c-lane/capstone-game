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

                    // Generate a random position within a 5-foot radius
                    Vector3 randomOffset = new Vector3(
                        Random.Range(-stairsSpawnRadius, stairsSpawnRadius),
                        Random.Range(-stairsSpawnRadius, stairsSpawnRadius),
                        0f
                    );

                    // Spawn the stairs at the random position within the room
                    Instantiate(Stairs, roomPosition + randomOffset, Quaternion.identity);
                    SpawnStairs = true;
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}