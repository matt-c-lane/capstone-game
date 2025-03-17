using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Image healthBar;
    public Image manaBar;
    public Image staminaBar;
    public Image weaponIcon;

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
}
