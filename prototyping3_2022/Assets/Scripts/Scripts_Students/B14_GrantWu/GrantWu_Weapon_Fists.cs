using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrantWu_Weapon_Fists : MonoBehaviour
{
	//NOTE: This script goes on the main playerBot Game Object, and the weapon goes in the public GO slot

	public GameObject leftFist;
	public GameObject rightFist;

	public float thrustAmount = 5f;

	private bool leftOut = false;
	private bool rightOut = false;

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
		if ((Input.GetMouseButtonDown(0)) && (leftOut == false))
		{
			leftFist.transform.Translate(0, 0, thrustAmount);
			leftOut = true;
			StartCoroutine(WithdrawWeapon(true));
		}
		if ((Input.GetMouseButtonDown(1)) && (rightOut == false))
		{
			rightFist.transform.Translate(0, 0, thrustAmount);
			rightOut = true;
			StartCoroutine(WithdrawWeapon(false));
		}
	}

	IEnumerator WithdrawWeapon(bool isLeft)
	{
		yield return new WaitForSeconds(0.6f);
		if (isLeft)
		{
			leftFist.transform.Translate(0, 0, -thrustAmount);
			leftOut = false;
		}
		else
        {
			rightFist.transform.Translate(0, 0, -thrustAmount);
			rightOut = false;
		}
	}
}
