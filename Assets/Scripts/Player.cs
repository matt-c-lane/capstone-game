using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // === Health System ===
    public int currentHealth { get; private set; }
    public int maxHealth { get; private set; } = 100;

    // === Inventory System ===
    private Inventory inventory;

    // === Weapon System ===
    public Weapon equippedWeapon;

    // === Player Class System ===
    private PlayerClass chosenClass;

    // === Movement System ===
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;

    private bool isMoving;
    private Rigidbody2D rb;
    private Vector2 movementInput;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = new Inventory();
        currentHealth = maxHealth;
    }

    void Update()
    {
        // === Handle Movement Input ===
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput = movementInput.normalized; // Prevent diagonal speed boost

        // Set isMoving based on input
        isMoving = movementInput != Vector2.zero;

        // Animation movement
        if (isMoving) {
            animator.SetFloat("moveX", movementInput.x);
            animator.SetFloat("moveY", movementInput.y);
        }
        animator.SetBool("isMoving", isMoving);

        // Sprinting (Shift Key)
        float speedModifier = Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1f;

        rb.linearVelocity = movementInput * moveSpeed * speedModifier;

        // === Handle Attacks ===
        if (Input.GetMouseButtonDown(0) && equippedWeapon != null)
        {
            Vector2 attackDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            equippedWeapon.ExecuteAttack(transform.position, attackDirection);
        }
    }

    // === Health Functions ===
    public void IncreaseHealth(int amount) { currentHealth = Mathf.Min(currentHealth + amount, maxHealth); }
    public void DecreaseHealth(int amount) { currentHealth = Mathf.Max(currentHealth - amount, 0); if (currentHealth == 0) Die(); }
    public void IncreaseMaxHealth(int amount) { maxHealth += amount; currentHealth += amount; }
    public void DecreaseMaxHealth(int amount) { maxHealth = Mathf.Max(maxHealth - amount, 1); currentHealth = Mathf.Min(currentHealth, maxHealth); }
    public void ApplyDamage(int damage, object enemy) { DecreaseHealth(damage); }

    private void Die() { Debug.Log("Player has died!"); }

    // === Inventory Functions ===
    public void AddToInventory(InventoryItem item) { inventory.AddItem(item); }
    public void RemoveFromInventory(string itemName, int amount = 1) { inventory.RemoveItem(itemName, amount); }
    public void ShowInventory() { inventory.PrintInventory(); }

    // === Weapon Functions ===
    public void EquipWeapon(Weapon weapon) { equippedWeapon = weapon; Debug.Log($"Equipped {weapon.weaponName}"); }

    // === Player CLass Functions ===
    public void SetClass(PlayerClass selectedClass) { chosenClass = selectedClass; Debug.Log($"Class set to {selectedClass.name}"); }
}
