using UnityEngine;

public abstract class Wearable : ScriptableObject
{
    public string wearableName;
    public Sprite wearableIcon;

    public void Equip()
    {
        //Empty
    }
}