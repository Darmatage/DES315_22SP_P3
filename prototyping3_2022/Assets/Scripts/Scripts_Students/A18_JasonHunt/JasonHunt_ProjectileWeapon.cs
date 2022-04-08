using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JasonHunt_ProjectileWeapon : MonoBehaviour
{
	//NOTE: This script goes on the main playerBot Game Object, and the weapon goes in the public GO slot

	public GameObject projectileDisc;
	public GameObject projectileDisplay;
	public GameObject emitter;
	private float thrustAmount = 3f;

	private bool weaponReloaded = true;

	//grab axis from parent object
	public string button1;
	public string button2;
	public string button3;
	public string button4; // currently boost in player move script

	void Start()
	{
		button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
		button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
		button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
		button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;

	}

	void Update()
	{
		//if (Input.GetKeyDown(KeyCode.T)){
		if ((Input.GetButtonDown(button1)) && (weaponReloaded == true))
		{
			//push display back into body
			projectileDisplay.transform.Translate(0, 0, -1);

			//projectileDisc.transform.Translate(0, thrustAmount, 0);
			GameObject newProjectile = Instantiate(projectileDisc, emitter.transform.position, Quaternion.identity);

			//calculate direction and apply to projectile script
			Vector3 direction = emitter.transform.position - gameObject.transform.position;
			direction.Normalize();

			newProjectile.GetComponent<JasonHunt_Projectile>().direction = direction;
			newProjectile.GetComponent<JasonHunt_Projectile>().Home = this.gameObject;

			weaponReloaded = false;
			StartCoroutine(RespawnWeapon());
		}

		if ((Input.GetButtonDown(button2)))
		{
			GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Hazard");
			foreach (GameObject proj in projectiles)
            {
				if (proj.name.Contains("JasonHunt_Projectile"))
                {
					if (proj.GetComponent<JasonHunt_Projectile>().stuck == true)
                    {
						proj.GetComponent<JasonHunt_Projectile>().returning = true;
                    }
                }
            }
		}
	}

	IEnumerator RespawnWeapon()
	{
		yield return new WaitForSeconds(2.0f);
		projectileDisplay.transform.Translate(0, 0, 1);
		weaponReloaded = true;
	}

}