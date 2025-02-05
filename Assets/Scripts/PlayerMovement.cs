using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Normal movement speed
    public float sprintSpeed = 8f; // Sprint speed
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


        // Prevent diagonal movement (optional)
        if (moveInput.x != 0) moveInput.y = 0;
    }
    void FixedUpdate()
    {
        // Check if the player is holding the Shift key
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;


        // Apply movement to Rigidbody2D
        rb.linearVelocity = moveInput * currentSpeed;
    }
}