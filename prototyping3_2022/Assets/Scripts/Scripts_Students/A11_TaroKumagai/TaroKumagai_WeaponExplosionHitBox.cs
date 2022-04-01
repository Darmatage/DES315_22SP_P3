using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaroKumagai_WeaponExplosionHitBox : HazardDamage
{
    public TaroKumagai_Weapon_BasicProjectile parentRef;
    public float explosionLifeTime = 1f;

	void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ground") || other.gameObject.tag.Equals("Hazard"))
            Physics.IgnoreCollision(GetComponent<Collider>(), other.collider, true);
    }


    private void Start()
    {
        StartCoroutine(destroySelf(explosionLifeTime));
    }
    private void OnDestroy()
    {
        parentRef.activeExplosions.Remove(gameObject);
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

    IEnumerator destroySelf(float lifetime)
    {
		yield return new WaitForSeconds(lifetime);
		Destroy(gameObject);
	}

}
