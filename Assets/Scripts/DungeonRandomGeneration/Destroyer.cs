using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the "SpawnPoint" tag
        if (other.CompareTag("SpawnPoint"))
        {
            Debug.Log("Destroying SpawnPoint: " + other.gameObject.name);
            Destroy(other.gameObject); // Destroy the SpawnPoint GameObject
        }
    }
}