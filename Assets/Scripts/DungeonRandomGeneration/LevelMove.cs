using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class LevelMove : MonoBehaviour
{
    public int sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
 
    // Level move zoned enter, if collider is a player
    // Move game to another scene
    public void OnTriggerEnter2D(Collider2D other) {
        print("Trigger Entered");
        
        if(other.CompareTag("Player")  && !other.isTrigger) { 
            // Player entered, so move level
            print("Switching Scene to " + sceneToLoad);
            playerStorage.initialValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}