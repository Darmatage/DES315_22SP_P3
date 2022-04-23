using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B20_FrontAttack : MonoBehaviour
{
	//NOTE: This script goes on the main playerBot Game Object, and the weapon goes in the public GO slot

	public GameObject weaponThrust;
	//private float thrustAmount = 5*4f;
	//private float transform = 0.0f;
	private float timer = 1.0f;
	float cooldown = 1.0f;
	AudioSource audio;
	private bool weaponOut = false;

	//grab axis from parent object
	public string button1;
	GameObject bullet;
	public GameObject fake;

	void Start()
	{
		button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
		audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
	}

	void Update()
	{
		cooldown -= Time.deltaTime;
		//if (!bullet)
        //{
			//weaponOut = false;
        //}

		if (weaponOut == false)
		{
			fake.GetComponent<MeshRenderer>().enabled = true;
		}
		else if (weaponOut == true)
        {
			fake.GetComponent<MeshRenderer>().enabled = false;
		}

		if ((Input.GetButtonDown(button1)) && ((weaponOut == false) && !bullet))
        {
			cooldown = timer;
			audio.Play();
			ShootBullet();
        }

		if (cooldown < 0.0f)
        {
			weaponOut = false;
			if (bullet)
				Destroy(bullet);
        }
	}

	void ShootBullet()
	{
		weaponOut = true;
		Quaternion.Euler(weaponThrust.transform.rotation.x, Quaternion.identity.y, Quaternion.identity.z);
		bullet = Instantiate(weaponThrust, gameObject.transform.position + gameObject.transform.forward.normalized * 2.5f, Quaternion.identity);
		bullet.GetComponent<Rigidbody>().velocity = gameObject.transform.forward.normalized*25.0f;
    }


	IEnumerator WithdrawWeapon()
	{
		yield return new WaitForSeconds(0.6f);
		weaponOut = false;
	}
}
