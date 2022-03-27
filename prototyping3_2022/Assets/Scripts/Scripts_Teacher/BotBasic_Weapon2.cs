using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBasic_Weapon2 : MonoBehaviour{
    //NOTE: This script goes on the main playerBot Game Object, and the weapon goes in the public GO slot

	private Animator anim;

	//public GameObject weaponSpike;
	//private float thrustAmount = 3f;
	
	private bool canAttack = true;
	public float coolDown = 1f;

	//grab axis from parent object
	public string button1;
	public string button2;
	public string button3;
	public string button4; // currently boost in player move script

    void Start(){
		button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
		button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
		button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
		button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;
		
		anim = gameObject.GetComponentInChildren<Animator>();
		
    }

    void Update(){
		//if (Input.GetKeyDown(KeyCode.T)){
		if ((Input.GetButtonDown(button1))&&(canAttack==true)){
			//weaponThrust.transform.Translate(0,thrustAmount, 0);
			anim.SetTrigger("Attack");
			canAttack = false;
			StartCoroutine(useWeaponAgain());
		}
    }

	IEnumerator useWeaponAgain(){
		yield return new WaitForSeconds(coolDown);
		//weaponThrust.transform.Translate(0,-thrustAmount, 0);
		canAttack = true;
	}

}
