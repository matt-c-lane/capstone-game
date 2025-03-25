using UnityEngine;

public abstract class Equipable : MonoBehaviour
{
    public string equipableName;
    public Sprite equipableIcon;

    public abstract void Equip();
    public abstract void Unequip();
}