using System.Collections;
using UnityEngine;

public class WonjuJo_BasicWeapon : MonoBehaviour
{
	//NOTE: This script goes on the main playerBot Game Object, and the weapon goes in the public GO slot

	public GameObject weaponThrust;
	public GameObject Projectile;
	public GameObject ProjectileLauncher;
	public GameObject Barrel;

	private float thrustAmount = 3.5f;

	private bool weaponOut = false;

	//grab axis from parent object
	public string button1;
	public string button2;
	public string button3;
	public string button4; // currently boost in player move script

	private float Button1Cooldown = 0f;
	private float Button1CooldownRate = 2f;
	
	private float Button2Cooldown = 0f;
	private float Button2CooldownRate = 3f;

	Vector3 LauncherPosition;

	public Color StartColor;
	public Color EndColor;

	private Renderer LauncherRenderer;

	AudioSource AS;
	public AudioClip FirstWeaponClip;
	public AudioClip BeepSound;

	bool CannotAttack = false;

	float StartTime;

	private bool ProjectileCheck;
	private float ProjectileTimer = 0f;
	private float ProjectileLife = 3f;

	void Start()
	{
		button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;

		LauncherRenderer = Barrel.GetComponent<Renderer>();

		if (!LauncherRenderer)
			Debug.Log("There is no LauncherRenderer");

		LauncherRenderer.material.color = StartColor;

		AS = GetComponent<AudioSource>();
	}

    void Update()
	{
		LauncherPosition = ProjectileLauncher.transform.position;

		if ((Input.GetButtonDown(button1)) && (weaponOut == false) && Time.time > Button1Cooldown)
		{
			Button1Cooldown = Time.time + Button1CooldownRate;

			AS.PlayOneShot(FirstWeaponClip);

			weaponThrust.transform.Translate(0, 0, thrustAmount);
			weaponOut = true;
			StartCoroutine(WithdrawWeapon());
			
		}

		if (Input.GetButtonDown(button2) && Time.time > Button2Cooldown)
		{
			StartTime = Time.time;

			LauncherRenderer.material.color = StartColor;

			Button2Cooldown = Time.time + Button2CooldownRate;

			Instantiate(Projectile, LauncherPosition, transform.rotation);
			
			ProjectileCheck = true;

			CannotAttack = true;
        }

        if (CannotAttack)
		{
			float t = (Time.time - StartTime) / Button2CooldownRate;
			LauncherRenderer.material.color = Color.Lerp(EndColor, StartColor, t);
		}

		if(ProjectileCheck)
        {
			ProjectileTimer += Time.deltaTime;
			if (ProjectileTimer > 1f && ProjectileTimer <= ProjectileLife)
			{
				ProjectileTimer -= Time.deltaTime;
				if (Input.GetButtonDown(button2))
				{
					AS.PlayOneShot(BeepSound);
					ProjectileTimer = 0;
				}
			}
		}


	}

	IEnumerator WithdrawWeapon()
	{
		yield return new WaitForSeconds(0.6f);
		weaponThrust.transform.Translate(0, 0, -thrustAmount);
		weaponOut = false;
	}

	IEnumerator WaitForSeconds(float time)
    {
		yield return new WaitForSeconds(time);

	}
}