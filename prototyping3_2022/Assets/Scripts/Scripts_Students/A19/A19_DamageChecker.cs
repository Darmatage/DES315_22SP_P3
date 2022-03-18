using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A19_DamageChecker : MonoBehaviour
{
    public A19_Weapon1 a19_Weapon1;

    private void Start()
    {

    }
     private void OnTriggerEnter(Collider other)
    {
        
            a19_Weapon1.ResetGroundSlam();
        
	}

}
