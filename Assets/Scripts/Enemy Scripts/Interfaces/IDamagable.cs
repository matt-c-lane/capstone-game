using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable 
{
    void Damage(int damage, DamageType damageType, int[] stats);

    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }

}
