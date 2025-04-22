using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable 
{
<<<<<<< Updated upstream
    void Damage(float damageAmount);
=======
    void Damage(float damageAmount, DamageType physical, int[] stats);
>>>>>>> Stashed changes

    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }

}
