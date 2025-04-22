using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable 
{
    void Damage(float damageAmount);

    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }

}
