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
	private float effectiveSpinDragCo;
	public float bulletSpeed = 30f;
	public float bulletLifetime = .5f;
	public float fireRate = 50f;
	public float spreadFactor = 5f;
	public float DPS = 1f;

	private float fireTimer = 0f;

	public float holdMin = .5f;
	private float minTimer = 0f;
	public float windupTime = 1f;
	public float overheatTime = 3f;

	public GameObject barPrefab;
	public GameObject bar;
	public B18_Firebar barGraphic;

	private float heatTimer = 0f;

	//grab axis from parent object
	[HideInInspector]
	public string button1;
	[HideInInspector]
	public string button2;

	void Start()
	{
		button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
		button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;

		bar = Instantiate(barPrefab);
		bar.GetComponent<B18_FollowScript>().follow = transform;
		barGraphic = bar.GetComponent<B18_Firebar>();

		barGraphic.SetWindup(windupTime / overheatTime);

		bulletPool.SetParent(null);

		effectiveSpinDragCo = spinDragCo;
	}

	private bool didOverheat = false;
	private float damageCounter = 0;

	void Update()
	{
		var appliedAcceleration = 0f;

		//is shooting?
		if ((Input.GetButton(button1)) && !didOverheat && minTimer < .01f)
		{
			heatTimer += Time.deltaTime;

			if(heatTimer > windupTime)
            {
				appliedAcceleration += spinAcceleration;
            }
			else
            {
				appliedAcceleration += spinAcceleration * (heatTimer/windupTime);
			}

			if (heatTimer > windupTime)
            {
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
						damageCounter += DPS / fireRate;
						bulletDamage.damage = 0;
						while(damageCounter >= 1)
                        {
							damageCounter--;
							bulletDamage.damage++;

						}

						if (gameObject.transform.parent.GetComponent<playerParent>().isPlayer1)
							bulletDamage.isPlayer1Weapon = true;
						else
							bulletDamage.isPlayer2Weapon = true;
					}

					Destroy(bulletObj, bulletLifetime);
				}

				var rb = GetComponent<Rigidbody>();
				var myDamage = GetComponent<BotBasic_Damage>();
				if(rb != null)
				{
					var helper = .8f;
					if(myDamage.knockBackSpeed < .01)
					{
						helper = 0;
					}

					rb.AddTorque(-transform.right * helper, ForceMode.Impulse);
				}
            }
            else
            {
				fireTimer += Time.deltaTime;
				fireTimer = Mathf.Clamp(fireTimer, 0, holdMin);
			}

			if(heatTimer>overheatTime)
            {
				didOverheat = true;
            }


		}
        else
        {
			fireTimer = 0;

			heatTimer -= Time.deltaTime;
			if(heatTimer < windupTime)
            {
				didOverheat = false;
            }
        }

		if(Input.GetButtonUp(button1) && (heatTimer > windupTime))
		{
			minTimer = holdMin;
        }



		if (heatTimer > windupTime)
		{
			effectiveSpinDragCo = .001f;
		}
		else
		{
			effectiveSpinDragCo = spinDragCo * (1- heatTimer / windupTime);
		}
		minTimer -= Time.deltaTime;
		minTimer = Mathf.Clamp(minTimer, 0, holdMin);

		heatTimer = Mathf.Clamp(heatTimer, 0, overheatTime);

		barGraphic.SetHeatBar(heatTimer / overheatTime);

		//handle spinning stuff
		appliedAcceleration -= effectiveSpinDragCo * spinSpeed;
		spinSpeed += appliedAcceleration;
		if (spinSpeed < 0) spinSpeed = 0;
		weaponBarrels.transform.Rotate(0, spinSpeed * Time.deltaTime, 0);

		SetBarColor();
	}

	private void SetBarColor()
    {
		if(heatTimer < windupTime)
        {
			//blue to red
			barGraphic.SetColor(new Color(heatTimer / windupTime, 1 - heatTimer / windupTime, 1 - heatTimer / windupTime));
        }
		else
        {
			//red
			var colorToSet = new Color(1, 0, 0);		

			//if did overheat or heatTimer < holdmin then blinking yellow
			if(didOverheat || minTimer > .01f)
            {
				if(Mathf.Cos(heatTimer * 30) > 0)
                {
					colorToSet = new Color(1, 1, 0);
				}
            }

			barGraphic.SetColor(colorToSet);
		}
    }
}
