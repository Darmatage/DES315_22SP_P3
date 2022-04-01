using System.Collections;
using UnityEngine;

public class WonjuJo_BasicWeapon : MonoBehaviour
{
	//NOTE: This script goes on the main playerBot Game Object, and the weapon goes in the public GO slot

	public GameObject weaponThrust;
	public GameObject Projectile;
	public GameObject ProjectileLauncher;

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
	
	//Vector3 ObjectScale;
	//Vector3 ObjectDecreasedScale;

	//float time = 0;
	//float speed = 3.0f;

	//bool CanAttack = false;

	void Start()
	{
		button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;

		//ObjectScale = ProjectileLauncher.transform.localScale;
		//ObjectDecreasedScale = new Vector3(0.00499304f, 0.005272481f, 0.004954814f);
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

		if ((Input.GetButtonDown(button2)) && Time.time > Button2Cooldown)
		{

			Button2Cooldown = Time.time + Button2CooldownRate;

			Instantiate(Projectile, LauncherPosition, transform.rotation);
			//StartCoroutine(RepeatLerp(ObjectDecreasedScale, ObjectScale, Button2Cooldown));
		}
	}

	IEnumerator WithdrawWeapon()
	{
		yield return new WaitForSeconds(0.6f);
		weaponThrust.transform.Translate(0, 0, -thrustAmount);
		weaponOut = false;
	}

	//public IEnumerator RepeatLerp(Vector3 original, Vector3 changed, float t)
 //   {
	//	float i = 0.0f;
	//	float rate = (1.0f / time) * speed;
	//	while(i<1.0f)
 //       {
	//		i += Time.deltaTime * rate;
	//		ProjectileLauncher.transform.localScale = Vector3.Lerp(original, changed, i);
	//		yield return null;
	//	}
 //   }

}
