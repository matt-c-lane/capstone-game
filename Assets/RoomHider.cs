using UnityEngine;

public class RoomHider : MonoBehaviour
{
    // Called when another collider enters the trigger collider attached to this GameObject
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object that collided has the "Player" tag
        if (collision.CompareTag("Player"))
        {
            // Destroy this GameObject (and its sprite)
            Destroy(gameObject);
        }
    }
}