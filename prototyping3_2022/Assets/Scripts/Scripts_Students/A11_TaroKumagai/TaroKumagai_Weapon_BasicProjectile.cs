using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaroKumagai_Weapon_BasicProjectile : MonoBehaviour
{

	public GameObject projectile;

    public int MaxProjectiles = 2;
    public float ProjectileLifetime = 3f;
    public float ProjectileLaunchForce = 10f;
    public int activeProjectiles = 0;
	//grab axis from parent object
	public string button;


    void Start(){
		button = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        
    }

    void Update()
    {
		//if (Input.GetKeyDown(KeyCode.T)){
		if (Input.GetButtonDown(button) && activeProjectiles < MaxProjectiles)
        {
            Vector3 LaunchPosition = gameObject.transform.position + gameObject.transform.forward * 2;
            GameObject newProjectile = Instantiate(projectile, LaunchPosition, Quaternion.identity);
            activeProjectiles++;
            newProjectile.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * ProjectileLaunchForce, ForceMode.Impulse);
            newProjectile.GetComponent<TaroKumagai_WeaponDamage>().parentRef = this;

            if (gameObject.transform.root.tag == "Player1")
                newProjectile.GetComponent<HazardDamage>().isPlayer1Weapon = true;
            else
                newProjectile.GetComponent<HazardDamage>().isPlayer2Weapon = true;

            StartCoroutine(SetProjectileToExpire(newProjectile, ProjectileLifetime));
        }
    }

	IEnumerator SetProjectileToExpire(GameObject projectile, float projectileLifetime)
    {
		yield return new WaitForSeconds(projectileLifetime);
        Destroy(projectile);
    }

}
