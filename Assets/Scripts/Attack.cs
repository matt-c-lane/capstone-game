using UnityEngine;
/*
Attack the base class for attacks. If you are making a new attack, it
is best practice to inherit from MeleeAttack or RangedAttack instead.
Only inherit from Attack if you know what you are doing.
*/
public abstract class Attack : ScriptableObject
{
    public int damage;
    public LayerMask enemyLayer;
    public string damageType;

    public abstract void Execute(Vector2 origin, Vector2 direction, int[] stats);
}
