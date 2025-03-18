using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject[] objects;

    private void Start()
    {
        if (objects == null || objects.Length == 0)
        {
            Debug.LogError("SpawnObject: No objects assigned to spawn!");
            return;
        }

        int rand = Random.Range(0, objects.Length);
        
        if (objects[rand] != null)
        {
            GameObject instance = Instantiate(objects[rand], transform.position, Quaternion.identity);
            instance.transform.parent = transform;
        }
        else
        {
            Debug.LogError("SpawnObject: Attempted to spawn a null object! Check the objects array.");
        }
    }
}
