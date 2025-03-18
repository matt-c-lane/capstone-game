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

    public Image healthBar;
    public Image manaBar;
    public Image staminaBar;
    public Image weaponIcon;
    public Image classTimer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void UpdateMana(float currentMana, float maxMana)
    {
        manaBar.fillAmount = currentMana / maxMana;
    }

    public void UpdateStamina(float currentStamina, float maxStamina)
    {
        staminaBar.fillAmount = currentStamina / maxStamina;
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
}
