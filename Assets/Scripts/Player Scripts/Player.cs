using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStats
{
    Body,
    Mind,
    Luck
}

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

    // === Player Subsystems ===
    public UIManager uiManager; //UI for the player
    public PlayerLevelManager leveler;
    public PlayerHealthManager healther;
    public PlayerManaManager manaer;

    // === Armor System ===
    public int armor = 1; //Protects against physical attacks, should never be zero
    public int shield = 1; //Protects against magical attacks, should never be zero
    public Wearable wearable; //Armor or clothes worn by player

    // === Stamina System ===
    public int currentStamina { get; private set; }
    public int maxStamina { get; private set; } = 10; //Player's max stamina, used by weapons

    // === Inventory System ===
    private Inventory inventory; //Player's inventory

    // === Weapon System ===
    public Weapon equippedWeapon; //Weapon the player is currently using
    public Weapon offWeapon; //Weapon the player has in the off-hand

    // === Player Class System ===
    public PlayerClass chosenClass; //Class the player chose
    private PowerPlayer classPower; //Power the player's class gives

    // === Movement System ===
    public float moveSpeed = 5f; //Speed of the player
    public float sprintMultiplier = 1.5f; //Multiplies moveSpeed while player is sprinting

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
        SetClass(chosenClass);
        EquipWeapon(equippedWeapon);

        // === Setup ===
        rb = GetComponent<Rigidbody2D>();
        inventory = new Inventory();

        leveler = new PlayerLevelManager(this);
        healther = new PlayerHealthManager(this);
        manaer = new PlayerManaManager(this);
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
        // === Swap Weapon ===
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwapWeapon();
        }
        // === Activate Class Power ===
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseClassPower();
        }
    }

    // === Stats Functions ===
    public void ModBody(int amount) { bodyMod += amount; }
    public void ModMind(int amount) { mindMod += amount; }
    public void ModLuck(int amount) { luckMod += amount; }

    // === Player Stats Functions ===
    private void SetAllStats(int body, int luck, int mind) { _body = body; _luck = luck; _mind = mind; }

    public void Defeat() { Die(); }
    private void Die() { Debug.Log("Player has died!"); }

    // === Inventory Functions ===
    public void AddToInventory(InventoryItem item) { inventory.AddItem(item); }
    public void RemoveFromInventory(string itemName, int amount = 1) { inventory.RemoveItem(itemName, amount); }
    public void ShowInventory() { inventory.PrintInventory(); }

    // === Weapon Functions ===
    public void EquipWeapon(Weapon weapon) { equippedWeapon = weapon; uiManager.UpdateWeapon(equippedWeapon); }
    private void SwapWeapon() { (equippedWeapon, offWeapon) = (offWeapon, equippedWeapon); uiManager.UpdateWeapon(equippedWeapon); }

    // === Player Class Functions ===
    public void SetClass(PlayerClass selectedClass) { chosenClass = selectedClass; classPower = chosenClass.classPower; }
    private void UseClassPower() { classPower.Activate(this); }

    public IEnumerator ClassPowerDuration(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            uiManager.UpdateClassTimer(ClassTimer.Active, elapsedTime, duration);
            yield return null;
        }

        classPower.Deactivate();
    }
    
    public IEnumerator ClassPowerCooldown(float cooldown)
    {
        float elapsedTime = 0f;

        while (elapsedTime < cooldown)
        {
            elapsedTime += Time.deltaTime;
            uiManager.UpdateClassTimer(ClassTimer.Cooldown, elapsedTime, cooldown);
            yield return null;
        }

        classPower.EndCooldown();
    }
}
