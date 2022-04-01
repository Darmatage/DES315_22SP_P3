using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A19_DamageChecker : HazardDamage
{
    public A19_Weapon1 a19_Weapon1;

    private void Start()
    {

    }
     private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<BotBasic_Damage>())
            a19_Weapon1.ResetGroundSlam();
        
	}
        
}
