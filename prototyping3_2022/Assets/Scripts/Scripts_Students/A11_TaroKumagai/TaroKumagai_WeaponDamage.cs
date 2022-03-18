using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaroKumagai_WeaponDamage : HazardDamage
{
    public TaroKumagai_Weapon_BasicProjectile parentRef;
	void OnCollisionEnter(Collision other)
    {
        if (isAPlayer(other.gameObject) || isReactiveProjectile(other.gameObject))
        {
            SpawnParticlesHere = other.contacts[0].point;
            //make particles
            GameObject damageParticles = Instantiate(particlesPrefab, SpawnParticlesHere, other.transform.rotation);
            StartCoroutine(destroyParticles(damageParticles));
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        parentRef.activeProjectiles.Remove(gameObject);
    }

    bool isAPlayer(GameObject someObject)
    {
        return someObject.gameObject != parentRef.gameObject && (someObject.transform.root.tag == "Player1" || someObject.transform.root.tag == "Player2");
    }

    bool isReactiveProjectile(GameObject someObject)
    {
        var projectile = someObject.gameObject.GetComponent<TaroKumagai_WeaponDamage>();

        if (projectile == null)
            return false;

        if (projectile.parentRef == parentRef)
            return true;
        else
            return false;

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