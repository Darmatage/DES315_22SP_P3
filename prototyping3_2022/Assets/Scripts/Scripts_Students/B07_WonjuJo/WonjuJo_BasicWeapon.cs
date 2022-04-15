using System.Collections;
using UnityEngine;

public class WonjuJo_BasicWeapon : MonoBehaviour
{
	//NOTE: This script goes on the main playerBot Game Object, and the weapon goes in the public GO slot

	public GameObject FrontBarrel;
	public GameObject BackBarrel;
	public GameObject BackProjectileLauncher;
	public GameObject FrontProjectileLauncher;
	public GameObject Projectile;

	//grab axis from parent object
	public string button1;
	public string button2;
	public string button3;
	public string button4; // currently boost in player move script
	
	private float Button2Cooldown = 0f;
	private float Button2CooldownRate = 3f;

	Vector3 FrontLauncherPosition;
	Vector3 BackLauncherPosition;
	
	bool IsOrigianlPosition;

	public Color StartColor;
	public Color EndColor;

	private Renderer BackLauncherRenderer;
	private Renderer FrontLauncherRenderer;

	AudioSource AS;
	public AudioClip FirstWeaponClip;
	public AudioClip BeepSound;
	public AudioClip ChangePositionClip;

	bool CannotAttack = false;

	float StartTime;

	private bool ProjectileCheck;
	private float ProjectileTimer = 0f;
	private float ProjectileLife = 3f;

	void Start()
	{
		button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;

		BackLauncherRenderer = BackBarrel.GetComponent<Renderer>();
		FrontLauncherRenderer = FrontBarrel.GetComponent<Renderer>();

		IsOrigianlPosition = true;

		BackLauncherRenderer.material.color = StartColor;
		FrontLauncherRenderer.material.color = StartColor;

		AS = GetComponent<AudioSource>();

	}

    void Update()
	{
		FrontLauncherPosition = FrontProjectileLauncher.transform.position;
		BackLauncherPosition = BackProjectileLauncher.transform.position;

		if (Input.GetButtonDown(button1))
		{
			if(IsOrigianlPosition) // changed
            {
				AS.PlayOneShot(ChangePositionClip);
				StartCoroutine(WaitForSeconds(1.3f));
				FrontBarrel.SetActive(true);
				BackBarrel.SetActive(false);
				IsOrigianlPosition = false;
            }
            else //original
            {
				AS.PlayOneShot(ChangePositionClip);
				StartCoroutine(WaitForSeconds(1.3f));
				FrontBarrel.SetActive(false);
				BackBarrel.SetActive(true);
				IsOrigianlPosition = true;
			}
			
		}

		if (Input.GetButtonDown(button2) && Time.time > Button2Cooldown)
		{
			StartTime = Time.time;

			if (IsOrigianlPosition) // back
			{
				BackLauncherRenderer.material.color = StartColor;

				Button2Cooldown = Time.time + Button2CooldownRate;

				Instantiate(Projectile, BackLauncherPosition, BackProjectileLauncher.transform.rotation);

				ProjectileCheck = true;

				CannotAttack = true;
			}
			else //front
			{
				FrontLauncherRenderer.material.color = StartColor;

				Button2Cooldown = Time.time + Button2CooldownRate;

				Instantiate(Projectile, FrontLauncherPosition, FrontProjectileLauncher.transform.rotation);

				ProjectileCheck = true;

				CannotAttack = true;
			}

        }

        if (CannotAttack)
		{
			if (IsOrigianlPosition) // changed
			{
				float t = (Time.time - StartTime) / Button2CooldownRate;
				BackLauncherRenderer.material.color = Color.Lerp(EndColor, StartColor, t);
			}
			else //original
			{
				float t = (Time.time - StartTime) / Button2CooldownRate;
				FrontLauncherRenderer.material.color = Color.Lerp(EndColor, StartColor, t);
			}
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

	IEnumerator WaitForSeconds(float time)
    {
		yield return new WaitForSeconds(time);
	}
}