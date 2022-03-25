using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaroKumagai_WeaponDamage : HazardDamage
{
    public TaroKumagai_Weapon_BasicProjectile parentRef;
    public GameObject Explosion;


	void OnCollisionEnter(Collision other)
    {
        if (isAPlayer(other.gameObject))
            Physics.IgnoreCollision(GetComponent<Collider>(), other.collider, true);


        //if (isAPlayer(other.gameObject) || isReactiveProjectile(other.gameObject))
        //{
        //    SpawnParticlesHere = other.contacts[0].point;
        //    //make particles
        //    GameObject damageParticles = Instantiate(particlesPrefab, SpawnParticlesHere, other.transform.rotation);
        //    StartCoroutine(destroyParticles(damageParticles));
        //    
        //}

        if (isReactiveProjectile(other.gameObject))
        {
            // Get the midpoint of the collision
            Vector3 CollisionEpicenter = gameObject.transform.position + other.gameObject.transform.position;
            CollisionEpicenter.x /= 2;
            CollisionEpicenter.y /= 2;
            CollisionEpicenter.z /= 2;

            // Create Explosion Hitbox and effect
            parentRef.CreateExplosion(CollisionEpicenter);

            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        parentRef.activeProjectiles.Remove(gameObject);
    }

    bool isAPlayer(GameObject someObject)
    {
        return someObject.transform.root.tag == "Player1" || someObject.transform.root.tag == "Player2";
    }
    bool isOpposingPlayer(GameObject someObject)
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
