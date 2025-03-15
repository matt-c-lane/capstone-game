using UnityEngine;

[CreateAssetMenu(fileName = "New Player Class", menuName = "Player/Class")]
public class PlayerClass : ScriptableObject
{
    public string className;
    public string description;

    public PowerPlayer classPower;
}