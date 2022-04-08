using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B20_FrontAttack : MonoBehaviour
{
	//NOTE: This script goes on the main playerBot Game Object, and the weapon goes in the public GO slot

	public GameObject weaponThrust;
	private float thrustAmount = 5*4f;
	private float transform = 0.0f;
	private float timer = 3.0f;
	float cooldown = 3.0f;

	private bool weaponOut = false;

	//grab axis from parent object
	public string button1;
	GameObject bullet;

	void Start()
	{
		button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
	}

	void Update()
	{
		cooldown -= Time.deltaTime;

		if ((Input.GetButtonDown(button1)) && (weaponOut == false))
        {
			cooldown = timer;
			ShootBullet();
        }

		if (cooldown < 0.0f)
        {
			weaponOut = false;
			if (bullet)
				Destroy(bullet);
        }

		/*if (weaponOut)
        {
			transform += (50*Time.deltaTime);
			//weaponThrust.transform.Translate(0, 50*Time.deltaTime, 0);
			projectile.velocity = weaponThrust.transform.forward;
		}
		//else
        //{
			timer += Time.deltaTime;
        //}
		if (transform >= thrustAmount)
        {
			//weaponThrust.transform.Translate(0, -transform, 0);
			transform = 0.0f;
			weaponOut = false;
        }
		//if (Input.GetKeyDown(KeyCode.T)){
		if ((Input.GetButtonDown(button1)) && (weaponOut == false) && timer > 3.0f)
		{
			weaponOut = true;
			timer = 0.0f;

			//StartCoroutine(WithdrawWeapon());
		}*/
	}

	void ShootBullet()
	{
		weaponOut = true;
		Quaternion.Euler(weaponThrust.transform.rotation.x, Quaternion.identity.y, Quaternion.identity.z);
		bullet = Instantiate(weaponThrust, gameObject.transform.position + gameObject.transform.forward.normalized * 2.5f, Quaternion.identity);
		bullet.GetComponent<Rigidbody>().velocity = gameObject.transform.forward.normalized*50.0f;
    }


	IEnumerator WithdrawWeapon()
	{
		yield return new WaitForSeconds(0.6f);
		weaponOut = false;
	}
}
