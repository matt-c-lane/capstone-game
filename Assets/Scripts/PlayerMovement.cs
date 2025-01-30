using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from player
        moveInput.x = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        moveInput.y = Input.GetAxisRaw("Vertical");   // W/S or Up/Down
        moveInput = moveInput.normalized; // Prevent diagonal speed boost
    }

    void FixedUpdate()
    {
        // Apply movement to Rigidbody2D
        rb.linearVelocity = moveInput * moveSpeed;
    }
}