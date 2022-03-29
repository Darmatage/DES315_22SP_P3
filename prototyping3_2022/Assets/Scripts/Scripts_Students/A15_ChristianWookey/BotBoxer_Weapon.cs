using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBoxer_Weapon : MonoBehaviour
{
	//NOTE: This script goes on the main playerBot Game Object, and the weapon goes in the public GO slot

	public GameObject weaponLeftThrust;
	public GameObject weaponRightThrust;

	public float thrustDistance = 2f;
	public float thrustTime = 1f;
	public float cooldownTime = 1f;

	private bool leftWeaponOut = false;
	private bool rightWeaponOut = false;


	//grab axis from parent object
	private string button1;
	private string button2;
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
		if ((Input.GetButtonDown(button1)) && (leftWeaponOut == false))
		{
			weaponLeftThrust.GetComponent<SphereCollider>().enabled = true;
			weaponLeftThrust.transform.Translate(0, 0, thrustDistance);
			leftWeaponOut = true;
			StartCoroutine(WithdrawLeftWeapon());
		}
		if ((Input.GetButtonDown(button2)) && (rightWeaponOut == false))
		{
			weaponRightThrust.GetComponent<SphereCollider>().enabled = true;
			weaponRightThrust.transform.Translate(0, 0, thrustDistance);
			rightWeaponOut = true;
			StartCoroutine(WithdrawRightWeapon());
		}
	}

	IEnumerator WithdrawLeftWeapon()
	{
		yield return new WaitForSeconds(thrustTime);
		weaponLeftThrust.transform.Translate(0, 0, -thrustDistance);
		weaponLeftThrust.GetComponent<SphereCollider>().enabled = false;
		StartCoroutine(CooldownLeftWeapon());
	}

	IEnumerator WithdrawRightWeapon()
	{
		yield return new WaitForSeconds(thrustTime);
		weaponRightThrust.transform.Translate(0, 0, -thrustDistance);
		weaponRightThrust.GetComponent<SphereCollider>().enabled = false;
		StartCoroutine(CooldownRightWeapon());
	}

	IEnumerator CooldownLeftWeapon()
	{
		yield return new WaitForSeconds(cooldownTime);
		leftWeaponOut = false;
	}

	IEnumerator CooldownRightWeapon()
	{
		yield return new WaitForSeconds(cooldownTime);
		rightWeaponOut = false;
	}

}
