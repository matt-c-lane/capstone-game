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
        if (waitTime <= 0 && SpawnStairs == false){
            for (int i = 0; i < rooms.Count; i++){
                if (i == rooms.Count-1) {
                    Instantiate(Stairs, rooms[i].transform.position, Quaternion.identity);
                    SpawnStairs = true;
                }
            }
        }
        else {
            waitTime -= Time.deltaTime;
        }
    }
}

