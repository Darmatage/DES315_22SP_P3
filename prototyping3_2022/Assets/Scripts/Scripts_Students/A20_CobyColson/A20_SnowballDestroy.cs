using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A20_SnowballDestroy : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if (other.gameObject.GetComponent<A20_SnowballBehavior>() != null)
        {
            other.gameObject.GetComponent<HazardDamage>().damage = 0;
        }
	}
}
