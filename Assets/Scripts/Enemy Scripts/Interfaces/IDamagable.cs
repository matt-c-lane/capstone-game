using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable 
{
    void Damage(int damage, string attack, int[] stats);

    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }

}
