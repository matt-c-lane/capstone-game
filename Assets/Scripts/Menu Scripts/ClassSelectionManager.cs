using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ClassSelectionManager : MonoBehaviour
{
    public static PlayerClass selectedClass;
    public static Weapon weaponFirst;
    public static Weapon weaponSecond;

    [Header("UI References")]
    public PlayerClass warrior;
    public PlayerClass mage;
    public PlayerClass thief;
    public Weapon sword;
    public Weapon bow;
    public Weapon spell;

    public void Warrior()
    {
        selectedClass = warrior;
        weaponFirst = sword;
        weaponSecond = bow;
        Accept();
    }
    public void Mage()
    {
        selectedClass = mage;
        weaponFirst = spell;
        weaponSecond = sword;
        Accept();
    }
    public void Thief()
    {
        selectedClass = thief;
        weaponFirst = bow;
        weaponSecond = sword;
        Accept();
    }
    public void Accept()
    {
        
        Debug.Log($"Player is a: {selectedClass}");
        SceneManager.LoadScene("Dungeon Floor 1");
    }
}
