using UnityEngine;
using UnityEngine.UI;

public enum ClassTimer
{
    Active,
    Cooldown
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public UIPlayerStats uiStats;
    private bool statsActive = false;

    public Image healthBar;
    public Image manaBar;
    public Image weaponIcon;
    public Image classTimer;
    public Image expBar;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update() {
        if (Input.GetKeyDown("escape")) ToggleStats();
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void UpdateMana(float currentMana, float maxMana)
    {
        manaBar.fillAmount = currentMana / maxMana;
    }

    public void UpdateExp(float currentExp, float maxExp)
    {
        expBar.fillAmount = currentExp / maxExp;
    }

    public void UpdateWeapon(Weapon weapon)
    {
        weaponIcon.sprite = weapon.weaponIcon;
    }
    
    public void UpdateClassTimer(ClassTimer mode, float timeElapsed, float timeTotal)
    {
        if (mode == ClassTimer.Active) { classTimer.fillAmount = 1 - (timeElapsed / timeTotal); }
        else if (mode == ClassTimer.Cooldown) { classTimer.fillAmount = timeElapsed / timeTotal; }
    }

    public void UpdateStats(int b, int m, int l)
    {
        uiStats.UpdateStats(b, m, l);
    }

    private void ToggleStats()
    {
        statsActive = !statsActive;
        uiStats.GetComponent<Canvas>().enabled = statsActive;
    }
}
