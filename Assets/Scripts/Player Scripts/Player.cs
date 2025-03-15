using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // === Player Stats ===
    [SerializeField] private int _body = 1; // Don't access backing field directly, use body instead
    private int bodyMod;
    public int body { get{return _body+bodyMod;} private set{_body = value;} } //Used to make physical attacks
    [SerializeField] private int _mind = 1; // Don't access backing field directly, use mind instead
    private int mindMod;
    public int mind { get{return _mind+mindMod;} private set{_mind = value;} } //Used to make magical attacks
    [SerializeField] private int _luck = 1; // Don't access backing field directly, use luck instead
    private int luckMod;
    public int luck { get{return _luck+luckMod;} private set{_luck = value;} } //Used for crit chance

    // === Armor System ===
    public int armor = 1; //Protects against physical attacks, should never be zero
    public int shield = 1; //Protects against magical attacks, should never be zero
    public Wearable wearable; //Armor or clothes worn by player

    // === Health System ===
    public int currentHealth { get; private set; }
    public int maxHealth { get; private set; } = 10; //Player's max health

    // === Stamina System ===
    public int currentStamina { get; private set; }
    public int maxStamina { get; private set; } = 10; //Player's max stamina, used by weapons

    // === Magic System ===
    public int currentMana { get; private set; }
    public int maxMana { get; private set; } = 10; //Player's max mana, used by spells

    // === Progression System ===
    public int lvl { get; private set; } = 1; //Player's level
    private int exp = 0; //Player's XP
    private int expNextLvl = 100; //Amount of XP the player needs to reach the next level
    private int expNextMod = 10; //Amount expNextLvl increases by every time the player levels up

    // === Inventory System ===
    private Inventory inventory; //Player's inventory

    // === Weapon System ===
    public Weapon equippedWeapon; //Weapon the player is currently using
    public Weapon offWeapon; //Weapon the player has in the off-hand

    // === Player Race & Class System ===
    private PlayerClass chosenClass; //Class the player chose
    private PowerPlayer classPower; //Power the player's class gives

    // === Movement System ===
    public float moveSpeed = 5f; //Speed of the player
    public float sprintMultiplier = 1.5f; //Multiplies moveSpeed while player is sprinting

    // === UI System ===
    public UIManager uiManager;

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
        currentMana = maxMana;
    }

    void Update()
    {
        // === Handle Movement Input ===
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput = movementInput.normalized;

        //prevent Diaginal movement, for now
        //if (movementInput.x != 0) { movementInput.y = 0; }

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
            equippedWeapon.ExecuteAttack(transform.position, attackDirection, new int[] {body, mind, luck});
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwapWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseClassPower();
        }
    }

    // === Stats Functions ===
    public void ModBody(int amount) { bodyMod += amount; }
    public void ModMind(int amount) { mindMod += amount; }
    public void ModLuck(int amount) { luckMod += amount; }

    // === Health Functions ===
    public void IncreaseHealth(int amount) { currentHealth = Mathf.Min(currentHealth + amount, maxHealth); }
    public void DecreaseHealth(int amount) { currentHealth = Mathf.Max(currentHealth - amount, 0); if (currentHealth <= 0) Die(); }
    public void IncreaseMaxHealth(int amount) { maxHealth += amount; currentHealth += amount; }
    public void DecreaseMaxHealth(int amount) { maxHealth = Mathf.Max(maxHealth - amount, 1); currentHealth = Mathf.Min(currentHealth, maxHealth); }
    public void Damage(int damage) { DecreaseHealth(damage); uiManager.UpdateHealth(currentHealth, maxHealth); Debug.Log($"Player took {damage} damage!"); }

    private void Die() { Debug.Log("Player has died!"); }

    // === Inventory Functions ===
    public void AddToInventory(InventoryItem item) { inventory.AddItem(item); }
    public void RemoveFromInventory(string itemName, int amount = 1) { inventory.RemoveItem(itemName, amount); }
    public void ShowInventory() { inventory.PrintInventory(); }

    // === Progression Fuctions ===
    private void IncreaseLvl(int amount) { lvl += amount; }
    private void CalcExpNext() { expNextLvl += expNextMod; }
    private void GainLvl() { exp -= expNextLvl; IncreaseLvl(1); CalcExpNext(); }
    public void GainExp(int amount) { exp += amount; if(exp > expNextLvl) GainLvl(); }

    // === Weapon Functions ===
    public void EquipWeapon(Weapon weapon) { equippedWeapon = weapon; uiManager.UpdateWeapon(equippedWeapon); }
    private void SwapWeapon() { (equippedWeapon, offWeapon) = (offWeapon, equippedWeapon); uiManager.UpdateWeapon(equippedWeapon); }

    // === Player Class Functions ===
    public void SetClass(PlayerClass selectedClass) { chosenClass = selectedClass; classPower = chosenClass.classPower; }
    private void UseClassPower() { classPower.Activate(this); }

    // === Player Stats Functions ===
    private void SetAllStats(int body, int luck, int mind) { _body = body; _luck = luck; _mind = mind; }
}
