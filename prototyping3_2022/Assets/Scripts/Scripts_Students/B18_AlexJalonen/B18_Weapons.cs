using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B18_Weapons : MonoBehaviour
{
	//NOTE: This script goes on the main playerBot Game Object, and the weapon goes in the public GO slot

	public GameObject weaponBarrels;
	public Transform bulletSpawnPos;
	public Transform bulletPool;
	public GameObject bulletPrefab;

	private float spinSpeed = 0f;
	public float spinAcceleration = 100f;
	public float spinDragCo = .2f;
	public float bulletSpeed = 30f;
	public float bulletLifetime = .5f;
	public float fireRate = 50f;
	public float spreadFactor = 5f;
	public float DPS = 1f;

	private float fireTimer = 0f;

	//grab axis from parent object
	[HideInInspector]
	public string button1;
	[HideInInspector]
	public string button2;

	void Start()
	{
		button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
		button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;

		bulletPool.SetParent(null);
	}

	void Update()
	{
		var appliedAcceleration = 0f;

		//is shooting?
		if ((Input.GetButton(button1)))
		{
			appliedAcceleration += spinAcceleration;

			fireTimer += Time.deltaTime;

			while(fireTimer > (1/ fireRate))
            {
				fireTimer -= (1 / fireRate);
				var bulletObj = Instantiate(bulletPrefab, bulletSpawnPos);
				bulletObj.transform.SetParent(bulletPool);

				var bulletRB = bulletObj.GetComponent<Rigidbody>();
				if(bulletRB != null)
				{
					var r = Random.insideUnitCircle;
					var f = transform.forward;
					var rx = transform.right * r.x * spreadFactor;
					var ry = transform.up * r.y * spreadFactor;

					var direction = f * bulletSpeed + rx + ry;

					bulletRB.velocity = (direction) + GetComponent<Rigidbody>().velocity;
				}

				var bulletDamage = bulletObj.AddComponent<HazardDamage>();
				if(bulletDamage != null)
                {
					bulletDamage.damage = DPS/ fireRate;

					if (gameObject.transform.parent.GetComponent<playerParent>().isPlayer1)
						bulletDamage.isPlayer1Weapon = true;
					else
						bulletDamage.isPlayer2Weapon = true;
                }

				Destroy(bulletObj, bulletLifetime);
            }


		}


		//handle spinning stuff
		appliedAcceleration -= spinDragCo * spinSpeed;
		spinSpeed += appliedAcceleration;
		if (spinSpeed < 0) spinSpeed = 0;
		weaponBarrels.transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
	}
}
