using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaroKumagai_WeaponDamage : HazardDamage
{
    public TaroKumagai_Weapon_BasicProjectile parentRef;
	void OnCollisionEnter(Collision other)
    {
        if (other.gameObject != parentRef.gameObject)
        {
            SpawnParticlesHere = other.contacts[0].point;
            //make particles
            GameObject damageParticles = Instantiate(particlesPrefab, SpawnParticlesHere, other.transform.rotation);
            StartCoroutine(destroyParticles(damageParticles));
            Destroy(gameObject);
            parentRef.activeProjectiles--;
        }
    }


    IEnumerator destroyParticles(GameObject particles)
    {
		yield return new WaitForSeconds(0.5f);
		Destroy(particles);
	}

}

	//NOTE: this script is just damage
	//hazard object movement is managed by their button
	//reporting damage is done by the damage script on the bots 