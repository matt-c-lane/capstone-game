using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 -> need bottom door
    // 2 -> need top door
    // 3 -> need left door
    // 4 -> need right door

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;


    void Start()
    {
        GameObject roomManager = GameObject.FindGameObjectWithTag("Rooms");
        
        if (roomManager != null)
        {
            templates = roomManager.GetComponent<RoomTemplates>();
            if (templates == null)
            {
                Debug.LogError("RoomTemplates component is missing on the 'Rooms' GameObject!");
                return;
            }
        }
        else
        {
            Debug.LogError("No GameObject with tag 'Rooms' found in the scene!");
            return;
        }

        Invoke("Spawn", 0.2f);
    }

    void Spawn()
    {
        if (spawned || templates == null) return;

        GameObject selectedRoom = null;

        switch (openingDirection)
        {
            case 1: // Need to spawn a room with BOTTOM door
                if (templates.bottomRooms.Length > 0)
                {
                    rand = Random.Range(0, templates.bottomRooms.Length);
                    selectedRoom = templates.bottomRooms[rand];
                }
                break;
            case 2: // Need to spawn a room with TOP door
                if (templates.topRooms.Length > 0)
                {
                    rand = Random.Range(0, templates.topRooms.Length);
                    selectedRoom = templates.topRooms[rand];
                }
                break;
            case 3: // Need to spawn a room with LEFT door
                if (templates.leftRooms.Length > 0)
                {
                    rand = Random.Range(0, templates.leftRooms.Length);
                    selectedRoom = templates.leftRooms[rand];
                }
                break;
            case 4: // Need to spawn a room with RIGHT door
                if (templates.rightRooms.Length > 0)
                {
                    rand = Random.Range(0, templates.rightRooms.Length);
                    selectedRoom = templates.rightRooms[rand];
                }
                break;
            default:
                Debug.LogError("Invalid openingDirection value: " + openingDirection);
                return;
        }

        if (selectedRoom != null)
        {
            Instantiate(selectedRoom, transform.position, selectedRoom.transform.rotation);
            spawned = true;
        }
        else
        {
            Debug.LogError("No valid room prefab found for direction " + openingDirection);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    if (other.CompareTag("SpawnPoint"))
        {
        RoomSpawner otherSpawn = other.GetComponent<RoomSpawner>();

        if (otherSpawn != null && otherSpawn.spawned == false && spawned == false)
        {
            if (templates != null && templates.closedRooms != null)
            {
                Instantiate(templates.closedRooms, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Closed room prefab is missing from RoomTemplates!");
            }
        }
        spawned = true;
        }
    }
}