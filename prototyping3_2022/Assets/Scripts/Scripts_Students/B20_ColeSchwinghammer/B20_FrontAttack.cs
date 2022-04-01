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

	private bool weaponOut = false;

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
		if (weaponOut)
        {
			transform += (50*Time.deltaTime);
			weaponThrust.transform.Translate(0, 50*Time.deltaTime, 0);
        }
		//else
        //{
			timer += Time.deltaTime;
        //}
		if (transform >= thrustAmount)
        {
			weaponThrust.transform.Translate(0, -transform, 0);
			transform = 0.0f;
			weaponOut = false;
        }
		//if (Input.GetKeyDown(KeyCode.T)){
		if ((Input.GetButtonDown(button1)) && (weaponOut == false) && timer > 3.0f)
		{
			weaponOut = true;
			timer = 0.0f;

			//StartCoroutine(WithdrawWeapon());
		}
	}
	void OnCollisionEnter(Collision other)
	{
		weaponThrust.transform.Translate(0, -transform, 0);
		transform = 0.0f;
		weaponOut = false;
	}

	IEnumerator WithdrawWeapon()
	{
		yield return new WaitForSeconds(0.6f);
		weaponOut = false;
	}
}
