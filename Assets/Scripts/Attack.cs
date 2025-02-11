using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public int damage;
    public LayerMask enemyLayer;

    public abstract void Execute(Vector2 origin, Vector2 direction);
}
