using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class B03_WeaponEvent : UnityEvent<B03Bot_Weapon.Weapon> { }

[RequireComponent(typeof(B03Bot_Move))]
public class B03Bot_Weapon : MonoBehaviour
{
	//NOTE: This script goes on the main playerBot Game Object, and the weapon goes in the public GO slot
	public enum Weapon
    {
		SHEILD,
		BOW,
		KNIVES,

		NONE
    }

	public Weapon currentWeapon { get; private set; }

	//grab axis from parent object
	public string button1;
	public string button2;
	public string button3;
	public string button4; // currently boost in player move script

	public B03_WeaponEvent ActivateWeapon;
	public UnityEvent SheildDefend;
	[SerializeField] float slowDown = 2f;
	[SerializeField] GameObject KinvesWeapons;
	[SerializeField] GameObject Projectitle;
	[SerializeField] float projectileSpeed;
	[SerializeField] Transform projectileStart;
	[SerializeField] GameObject projectileParticles;

	bool deactivateSheild = false;
	B03Bot_Move movement;

	void Start()
	{
		currentWeapon = Weapon.NONE;

		button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
		button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
		button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
		button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;

		if (ActivateWeapon == null) ActivateWeapon = new B03_WeaponEvent();
		ActivateWeapon.AddListener(WeaponEvent);

		movement = GetComponent<B03Bot_Move>();
	}

	void Update()
	{
		if(Input.GetButtonDown(button1) && currentWeapon == Weapon.NONE)
        {
			SheildDown();
		}
		else if(!Input.GetButton(button1) && currentWeapon == Weapon.SHEILD)
        {
			deactivateSheild = true;
			ActivateWeapon.Invoke(Weapon.NONE);
			movement.moveSpeed *= slowDown;
		}
		else if (Input.GetButton(button2) && currentWeapon != Weapon.BOW)
		{
			ActivateWeapon.Invoke(Weapon.BOW);
		}
		else if (Input.GetButtonDown(button3) && currentWeapon == Weapon.NONE)
		{
			ActivateWeapon.Invoke(Weapon.KNIVES);
			KinvesWeapons.SetActive(true);
		}
		else if (!Input.GetButton(button3) && currentWeapon == Weapon.KNIVES)
		{
			ActivateWeapon.Invoke(Weapon.NONE);
			KinvesWeapons.SetActive(false);
		}
	}

	public void SheildDown()
    {
		deactivateSheild = false;
		ActivateWeapon.Invoke(Weapon.SHEILD);
		movement.moveSpeed /= slowDown;
	}

	void WeaponEvent(Weapon weapon)
    {
		if(currentWeapon == Weapon.SHEILD && weapon == Weapon.NONE && !deactivateSheild)
        {
			SheildDefend.Invoke();
			deactivateSheild = true;
			movement.moveSpeed *= slowDown;
		}

		currentWeapon = weapon;
    }

	public void AnimationEventActivateBow()
	{
		GameObject projectile = Instantiate(Projectitle, projectileStart.position, transform.rotation);
		HazardDamage hazard = projectile.GetComponent<HazardDamage>();

		if (projectile.transform.root.tag == "Player1") { hazard.isPlayer1Weapon = true; }
		if (projectile.transform.root.tag == "Player2") { hazard.isPlayer2Weapon = true; }

		hazard.particlesPrefab = projectileParticles;
		projectile.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);

		ActivateWeapon.Invoke(Weapon.NONE);
	}
}
