using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScottFadoBristow_Toggle_Hazard_Damage : HazardDamage
{
    private float startingDamage = 0f;
    public bool DamageOn = true;

    new void Start()
    {
        base.Start();

        startingDamage = damage;

        if (!DamageOn)
            ToggleDamageOff();
    }
    public void ToggleDamageOff()
    {
        
        damage = 0;
    }


    public void ToggleDamageOn()
    {

        damage = startingDamage;
    }


}
