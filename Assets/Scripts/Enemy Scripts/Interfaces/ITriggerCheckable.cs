using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ITriggerCheckable 
{
   
    bool IsAggroed { get; set; }
    bool IsWithinStrickingDistance { get; set; }

    void SetAggroStatus(bool isAggroed);
    void SetStrikingDistanceBool(bool isWithinStrikingDistance);

}
