using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaroKumagai_Weapon_BasicProjectile : MonoBehaviour
{

	public GameObject projectile;
	public GameObject explosion;

    public int MaxProjectiles = 2;
    public float ProjectileLifetime = 3f;
    public float ProjectileLaunchForce = 10f;
    public float ProjectileMagnetizationForce = 3f;
    public float ProjectileTravelTime = 0.5f;

    public LinkedList<GameObject> activeProjectiles = new LinkedList<GameObject>();
    public LinkedList<GameObject> activeExplosions = new LinkedList<GameObject>();
	//grab axis from parent object
	public string ButtonLaunch = "INVALID_KEY";
	public string ButtonMagnetize = "INVALID_KEY";
    private bool magentizing = false;


    private void Awake()
    {
        ButtonLaunch = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        ButtonMagnetize = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
    }

    void Start()
    {
		
    }

    void Update()
    {

        if (Input.GetButtonDown(ButtonLaunch) && activeProjectiles.Count < MaxProjectiles)
        {
            Vector3 LaunchPosition = gameObject.transform.position + gameObject.transform.forward * 2;
            GameObject newProjectile = Instantiate(projectile, LaunchPosition, Quaternion.identity);
            activeProjectiles.AddLast(newProjectile);
            newProjectile.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * ProjectileLaunchForce, ForceMode.Impulse);
            newProjectile.GetComponent<TaroKumagai_WeaponDamage>().parentRef = this;

            if (gameObject.transform.root.tag == "Player1")
                newProjectile.GetComponent<HazardDamage>().isPlayer1Weapon = true;
            else
                newProjectile.GetComponent<HazardDamage>().isPlayer2Weapon = true;

            StartCoroutine(SetProjectileToStop(newProjectile, ProjectileTravelTime));
            StartCoroutine(SetProjectileToExpire(newProjectile, ProjectileLifetime));
        }

        if (Input.GetButtonDown(ButtonMagnetize))
        {
            foreach (var projectile in activeProjectiles)
                projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if (Input.GetButtonUp(ButtonMagnetize))
        {
            foreach (var projectile in activeProjectiles)
                projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if (Input.GetButton(ButtonMagnetize))
        {
            if(activeProjectiles.Count > 1)
                magentizing = true;
            else
                magentizing = false;

            Vector3 midpoint = new Vector3();

            foreach(var projectile in activeProjectiles)
            {
                midpoint.x += projectile.transform.position.x;
                midpoint.y += projectile.transform.position.y;
                midpoint.z += projectile.transform.position.z;
            }

            if (activeProjectiles.Count != 0)
            {
                midpoint.x /= activeProjectiles.Count;
                midpoint.y /= activeProjectiles.Count;
                midpoint.z /= activeProjectiles.Count;
            }

            foreach (var projectile in activeProjectiles)
            {
                Vector3 direction = (midpoint - projectile.transform.position).normalized;
                projectile.GetComponent<Rigidbody>().AddForce(direction * ProjectileMagnetizationForce, ForceMode.Force);
            }
        }
        else
            magentizing = false;
    }

    public void CreateExplosion(Vector3 position)
    {
        if (activeExplosions.Count > 0)
            return;

        GameObject explosionRef = Instantiate(explosion, position, Quaternion.identity);
        explosionRef.GetComponent<TaroKumagai_WeaponExplosionHitBox>().parentRef = this;

        activeExplosions.AddLast(explosionRef);
    }


    IEnumerator SetProjectileToStop(GameObject projectile, float projectileTravelTime)
    {
        yield return new WaitForSeconds(projectileTravelTime);

        if(projectile && !magentizing)
            projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;

    }

    IEnumerator SetProjectileToExpire(GameObject projectile, float projectileLifetime)
    {
		yield return new WaitForSeconds(projectileLifetime);

        if (projectile)
            Destroy(projectile);
    }

}
