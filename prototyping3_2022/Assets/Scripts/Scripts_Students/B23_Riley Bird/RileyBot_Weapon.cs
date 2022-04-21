using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RileyBot_Weapon : MonoBehaviour{
    //NOTE: This script goes on the main playerBot Game Object, and the weapon goes in the public GO slot

	public GameObject weaponThrust;
	private float thrustAmount = 1.5f;
	public bool weaponSide = false; // False is left, True is right

	private Rigidbody otherPlayerBody;
	private Transform otherPlayerTransform;
	
	private bool weaponOut = false;
	private bool pushed = false;
	private bool spun = false;

	//grab axis from parent object
	public string button1;
	public string button2;
	public string button3;
	public string button4; // currently boost in player move script

	public AudioSource source;
	public AudioClip hit;
	public AudioClip jump;
	public AudioClip push;


	void Start(){
		if(weaponSide)
        {
			thrustAmount = -1.5f;
        }
		button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
		button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
		button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
		button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;

		if (gameObject.transform.root.tag == "Player1")
		{
			otherPlayerBody = GameObject.FindGameObjectWithTag("Player2").GetComponentInChildren<Rigidbody>();
			otherPlayerTransform = GameObject.FindGameObjectWithTag("Player2").GetComponentInChildren<Transform>();
		}
		else
        {
			otherPlayerBody = GameObject.FindGameObjectWithTag("Player1").GetComponentInChildren<Rigidbody>();
			otherPlayerTransform = GameObject.FindGameObjectWithTag("Player1").GetComponentInChildren<Transform>();
		}
	}

    void Update(){
		//if (Input.GetKeyDown(KeyCode.T)){
		if ((Input.GetButtonDown(button1))&&(weaponOut==false))
		{
			weaponThrust.transform.Translate(0,thrustAmount, 0);
			weaponOut = true;
			if(!weaponSide)
				source.PlayOneShot(hit);
			
			
			StartCoroutine(WithdrawWeapon());
		}
		if((Input.GetButtonDown(button2))&&(pushed==false) && !weaponSide)
        {
			pushed = true;
			Vector3 vec = otherPlayerTransform.position - transform.position;
			vec.Normalize();
			vec.y = 0;
			otherPlayerBody.AddForce((vec * 10), ForceMode.VelocityChange);
			if (!weaponSide)
				source.PlayOneShot(push);
			StartCoroutine(PushAway());
        }
		if ((Input.GetButtonDown(button3)) && (pushed == false) && !weaponSide)
		{
			pushed = true;
			Vector3 vec = transform.position - otherPlayerTransform.position;
			vec.Normalize();
			vec.y = 0;
			otherPlayerBody.AddForce((vec * 10), ForceMode.VelocityChange);
			if (!weaponSide)
				source.PlayOneShot(jump);
			StartCoroutine(PushAway());
		}
		if ((Input.GetButtonDown(button4)) && (spun == false) && !weaponSide)
		{
			spun = true;
			gameObject.GetComponent<BotBasic_Move>().rotateSpeed *= 4;
			StartCoroutine(Spin());
		}
	}

	IEnumerator WithdrawWeapon(){
		yield return new WaitForSeconds(0.6f);
		weaponThrust.transform.Translate(0,-thrustAmount, 0);
		weaponOut = false;
	}

	IEnumerator PushAway()
    {
		yield return new WaitForSeconds(5f);
		pushed = false;
    }

	IEnumerator Spin()
    {
		yield return new WaitForSeconds(2f);
		gameObject.GetComponent<BotBasic_Move>().rotateSpeed /= 4;
		spun = false;
    }

}
