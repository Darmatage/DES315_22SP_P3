using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B13BotWeapon : MonoBehaviour
{
	//NOTE: This script goes on the main playerBot Game Object, and the weapon goes in the public GO slot
	public float AttackOneCooldown = 5.0f;
	public float AttackTwoCooldown = 1.0f;

	public GameObject boxBomb;

	private Animator anime;
	private bool CanAttackBomb = true;
	private bool CanAttackArm = true;
	public GameObject roboArm;

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
		roboArm.tag = "Untagged";	

		anime = GetComponentInChildren<Animator>();
	}

	void Update()
	{
		if ((Input.GetButtonDown(button1)) && (CanAttackBomb == true))
		{
			anime.SetTrigger("Attack");
			CanAttackBomb = false;
			StartCoroutine(WaitForPlaceBomb());
		}
		if ((Input.GetButtonDown(button2)) && (CanAttackArm == true))
		{
			roboArm.tag = "Hazard";
			anime.SetTrigger("Smack");
			CanAttackArm = false;
			StartCoroutine(WaitForArmSmack());
		}
	}

	IEnumerator WaitForPlaceBomb()
	{
		yield return new WaitForSeconds(AttackOneCooldown);
		boxBomb.SetActive(true);
		CanAttackBomb = true;
	}
	IEnumerator WaitForArmSmack()
	{
		yield return new WaitForSeconds(AttackTwoCooldown);
		roboArm.tag = "Untagged";
		CanAttackArm = true;
	}
}
