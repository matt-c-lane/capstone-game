using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoomSpawn : MonoBehaviour
{
    public GameObject[] roomPrefabs;  // Array to store different room prefabs
    public Transform spawnPoint;      // Position where the room will be spawned

    void Start()
    {
        SpawnRandomRoom();
    }

    void SpawnRandomRoom()
    {
        if (roomPrefabs.Length > 0)  // Ensure array is not empty
        {
            int randIndex = Random.Range(0, roomPrefabs.Length);  // Pick a random room
            GameObject spawnedRoom = Instantiate(roomPrefabs[randIndex], spawnPoint.position, Quaternion.identity);

            // Assign the PlayerSpawnRoom tag to the spawned room
            spawnedRoom.tag = "PlayerSpawnRoom";
            Debug.Log("Randomly spawned room tagged as PlayerSpawnRoom.");
        }
        else
        {
            Debug.LogWarning("No rooms assigned in the array!");
        }
    }
}