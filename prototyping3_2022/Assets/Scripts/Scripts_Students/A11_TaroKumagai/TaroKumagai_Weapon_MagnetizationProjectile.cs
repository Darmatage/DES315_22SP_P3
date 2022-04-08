using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaroKumagai_MagnetizationProjectile : MonoBehaviour
{
    void OnTriggerEnter(Collision other)
    {
        if (other.gameObject.GetComponent<TaroKumagai_Weapon_BasicProjectile>())
            Physics.IgnoreCollision(GetComponent<Collider>(), other.collider, true);
        else
            Destroy(gameObject);
    }
}
