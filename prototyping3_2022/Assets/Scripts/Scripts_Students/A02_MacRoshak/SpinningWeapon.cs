using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningWeapon : MonoBehaviour
{
	//NOTE: This script goes on the main playerBot Game Object, and the weapon goes in the public GO slot

	public GameObject spinnerBlade;
	//private float thrustAmount = 3f;

	public float maxspeed = 15.0f;
	private float weaponAcceleration;
	public float maxDamage = 15;
	private float weaponDecel;

	//private bool weaponOut = false;
	public Rigidbody bladeRigidbody;
	private HazardDamage damageScript;
	//private Transform BladeTransform;

	//grab axis from parent object
	public string button1;
	public string button2;
	public string button3;
	public string button4; // currently boost in player move script

	public float currentDamage = 0.0f;

	void Start()
	{
		button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
		button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
		button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
		button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;

		bladeRigidbody = spinnerBlade.GetComponent<Rigidbody>();
		damageScript = spinnerBlade.GetComponent<HazardDamage>();

		weaponAcceleration = maxspeed/3.0f;
		weaponDecel = maxspeed / 2.0f;

		bladeRigidbody.maxAngularVelocity = maxspeed;
		//bladeTransform = spinnerBlade.GetComponent<Transform>();
	
	}

	void FixedUpdate()
	{
		//if (Input.GetKeyDown(KeyCode.T)){
		if (Input.GetButton(button1))
		{
			if(bladeRigidbody.angularVelocity.magnitude <= maxspeed)
				bladeRigidbody.AddTorque(transform.up * weaponAcceleration, ForceMode.Acceleration);
			//bladeRigidbody.angularVelocity = Vector3.up * 100.0f;
		}
		else
        {
			if(bladeRigidbody.angularVelocity.y > 0.0f)
				bladeRigidbody.AddTorque(transform.up * -weaponDecel, ForceMode.Acceleration);
		}


		/*if(BladeTransform.rotation.x != 0 || BladeTransform.rotation.z != 0)
        {
			BladeTransform.rotation = Vector3.up * BladeTransform.rotation.y;
	    }*/

		currentDamage = bladeRigidbody.angularVelocity.magnitude / 2.5f;
		if(currentDamage < 1.0f)
        {
			currentDamage = 0.0f;
        }
		damageScript.damage = Mathf.Round(currentDamage);


	}

	
}
