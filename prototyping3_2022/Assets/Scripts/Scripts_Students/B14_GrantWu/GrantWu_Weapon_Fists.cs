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

	private Rigidbody myrb; // this rigidbody

	// other game object stuff
	private GameObject othergo;
	private float othermovespeed;
	private float otherrotatespeed;
	private float otherjumpspeed;
	private AudioSource audiosource;

	void Start()
	{
		button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
		button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
		button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
		button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;

		myrb = gameObject.GetComponent<Rigidbody>();
		audiosource = gameObject.GetComponent<AudioSource>();

		// Grab other game object based on player tag
		if (gameObject.transform.parent.CompareTag("Player1"))
			othergo = GameObject.FindWithTag("Player2") ? GameObject.FindWithTag("Player2").transform.GetChild(0).gameObject : null;
		else if (gameObject.transform.parent.CompareTag("Player2"))
			othergo = GameObject.FindWithTag("Player1") ? GameObject.FindWithTag("Player1").transform.GetChild(0).gameObject : null;

		// Store other's original values here
		if (othergo)
		{
			BotBasic_Move othermovement = othergo.GetComponent<BotBasic_Move>();
			othermovespeed = othermovement.moveSpeed;
			otherrotatespeed = othermovement.rotateSpeed;
			otherjumpspeed = othermovement.jumpSpeed;
		}
		else
			Debug.LogWarning("Other Game Object doesn't exist");
	}

	void Update()
	{
		//if (Input.GetKeyDown(KeyCode.T)){
		if ((Input.GetMouseButtonDown(0) || Input.GetButtonDown(button1)) && (leftOut == false))
		{
			myrb.constraints = RigidbodyConstraints.FreezeAll;
			leftFist.transform.Translate(0, 0, thrustAmount);
			leftOut = true;
			StartCoroutine(WithdrawWeapon(true));
		}
		if ((Input.GetMouseButtonDown(1) || Input.GetButtonDown(button2)) && (rightOut == false))
		{
			myrb.constraints = RigidbodyConstraints.FreezeAll;
			rightFist.transform.Translate(0, 0, thrustAmount);
			rightOut = true;
			StartCoroutine(WithdrawWeapon(false));
		}
	}

	IEnumerator WithdrawWeapon(bool isLeft)
	{
		yield return new WaitForSeconds(0.5f);
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
		myrb.constraints = RigidbodyConstraints.None;
	}

    private void OnCollisionEnter(Collision other)
    {
		if (other.gameObject.transform.parent.CompareTag("Player1") || other.gameObject.transform.parent.CompareTag("Player2"))
        {
			BotBasic_Move other_movement = other.gameObject.GetComponent<BotBasic_Move>();
			Rigidbody other_rb = other.gameObject.GetComponent<Rigidbody>();

			other_rb.constraints = RigidbodyConstraints.FreezeAll;
			other_movement.moveSpeed = 0f;
			other_movement.rotateSpeed = 0f;
			other_movement.jumpSpeed = 0f;
			audiosource.Play();
			Debug.Log("Stunned");
			StartCoroutine(ReleaseStun(other_rb, other_movement));
		}
    }

	IEnumerator ReleaseStun(Rigidbody other_rb, BotBasic_Move other_movement)
    {
		yield return new WaitForSeconds(1f);
		other_movement.moveSpeed = othermovespeed;
		other_movement.rotateSpeed = otherrotatespeed;
		other_movement.jumpSpeed = otherjumpspeed;
		other_rb.constraints = RigidbodyConstraints.None;
    }
}
