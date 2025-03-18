using UnityEngine;

public abstract class Power : ScriptableObject
{
    public string powerName;
    public string description;

    public abstract void Activate();
    public abstract void Deactivate();
}