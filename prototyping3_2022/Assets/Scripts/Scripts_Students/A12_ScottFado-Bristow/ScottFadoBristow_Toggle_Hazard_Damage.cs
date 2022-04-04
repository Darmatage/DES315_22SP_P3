using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScottFadoBristow_Toggle_Hazard_Damage : HazardDamage
{
    private float startingDamage = 0f;


    new void Start()
    {
        base.Start();

        startingDamage = damage;
    }
    void ToggleDamage()
    {
        
        damage = 0;
    }


   
}
