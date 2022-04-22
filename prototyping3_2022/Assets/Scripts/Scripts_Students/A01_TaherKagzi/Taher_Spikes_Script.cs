using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taher_Spikes_Script : MonoBehaviour
{
    //NOTE: This script goes on the main playerBot Game Object, and the weapon goes in the public GO slot

	public GameObject[] spikeHolders;
	public GameObject[] jumpers;
	private float thrustAmount = -0.6f;
	private float timer = 0.0f;
	public float Spike_cd = 5.0f;

	private bool weaponOut = false;
	private bool jumpOut = false;

	//grab axis from parent object
	public string button1;
	public string button2;
	public string button3;
	public string button4; // currently boost in player move script
	public string jumpButton;

	void Start(){
		button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
		button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
		button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
		button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;
		jumpButton = gameObject.transform.parent.GetComponent<playerParent>().jumpInput;
	}

    void Update(){
		//if (Input.GetKeyDown(KeyCode.T)){
		timer += Time.deltaTime;

		if ((Input.GetButtonDown(button1))&&(weaponOut==false) && timer > Spike_cd){

			foreach (var spike in spikeHolders)
			{
				spike.transform.Translate(0, thrustAmount, 0);
			}
			
			weaponOut = true;
			
			StartCoroutine(WithdrawWeapon());
		}

		if ((Input.GetButtonDown(jumpButton)) && (jumpOut == false))
		{

			foreach (var jump in jumpers)
			{
				jump.transform.Translate(-thrustAmount, 0, 0);
			}

			jumpOut = true;

			StartCoroutine(WithdrawJumpers());
		}
	}

	IEnumerator WithdrawWeapon(){
		yield return new WaitForSeconds(0.6f);

		foreach (var spike in spikeHolders)
		{
			spike.transform.Translate(0, -thrustAmount, 0);
		}

		weaponOut = false;
	}

	IEnumerator WithdrawJumpers()
	{
		yield return new WaitForSeconds(0.6f);

		foreach (var jump in jumpers)
		{
			jump.transform.Translate(thrustAmount, 0, 0);
		}

		jumpOut = false;
	}
}
