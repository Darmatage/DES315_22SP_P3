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
	private float Button2CooldownRate = 4f;

	Vector3 LauncherPosition;

	public Color StartColor;
	public Color EndColor;

	public AudioClip CannotLaunch;
	public AudioSource AS;

	private Renderer LauncherRenderer;

	bool CannotAttack = false;

	float StartTime;

	void Start()
	{
		button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;

		if (!CannotLaunch)
			Debug.Log("There is no audio clip in basic weapon");

		if (!AS)
			Debug.Log("There is no audio source in basic weapon");

		LauncherRenderer = Barrel.GetComponent<Renderer>();

		if (!LauncherRenderer)
			Debug.Log("There is no LauncherRenderer");

		LauncherRenderer.material.color = StartColor;
	}

    void Update()
	{
		LauncherPosition = ProjectileLauncher.transform.position;

		if ((Input.GetButtonDown(button1)) && (weaponOut == false) && Time.time > Button1Cooldown)
		{
			Button1Cooldown = Time.time + Button1CooldownRate;
			
			weaponThrust.transform.Translate(0, 0, thrustAmount);
			weaponOut = true;
			StartCoroutine(WithdrawWeapon());
		
		}

		if(CannotAttack)
		{
			float t = (Time.time - StartTime) / Button2CooldownRate;
			LauncherRenderer.material.color = Color.Lerp(EndColor, StartColor, t);
		}

		if (Input.GetButtonDown(button2) && Time.time > Button2Cooldown)
		{
			StartTime = Time.time;

			LauncherRenderer.material.color = StartColor;

			Button2Cooldown = Time.time + Button2CooldownRate;

			Instantiate(Projectile, LauncherPosition, transform.rotation);
			
			CannotAttack = true;
		}
	}

	IEnumerator WithdrawWeapon()
	{
		yield return new WaitForSeconds(0.6f);
		weaponThrust.transform.Translate(0, 0, -thrustAmount);
		weaponOut = false;
	}

}
