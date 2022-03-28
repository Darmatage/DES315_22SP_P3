using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class B03_WeaponEvent : UnityEvent<B03Bot_Weapon.Weapon> { }

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

	private float thrustAmount = 3f;
	private bool weaponOut = false;

	public Weapon currentWeapon { get; private set; }

	//grab axis from parent object
	public string button1;
	public string button2;
	public string button3;
	public string button4; // currently boost in player move script

	public B03_WeaponEvent ActivateWeapon;
	public UnityEvent SheildDefend;
	[SerializeField] GameObject KinvesWeapons;

	bool deactivateSheild = false;

	void Start()
	{
		currentWeapon = Weapon.NONE;

		button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
		button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
		button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
		button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;

		if (ActivateWeapon == null) ActivateWeapon = new B03_WeaponEvent();
		ActivateWeapon.AddListener(WeaponEvent);
	}

	void Update()
	{
		if(Input.GetButtonDown(button1) && currentWeapon == Weapon.NONE)
        {
			deactivateSheild = false;
			ActivateWeapon.Invoke(Weapon.SHEILD);
        }
		else if(!Input.GetButton(button1) && currentWeapon == Weapon.SHEILD)
        {
			deactivateSheild = true;
			ActivateWeapon.Invoke(Weapon.NONE);
        }
		else if(Input.GetButtonDown(button2) && currentWeapon == Weapon.NONE)
        {
			ActivateWeapon.Invoke(Weapon.BOW);
        }
		else if (!Input.GetButton(button2) && currentWeapon == Weapon.BOW)
		{
			ActivateWeapon.Invoke(Weapon.NONE);
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

	void WeaponEvent(Weapon weapon)
    {
		if(currentWeapon == Weapon.SHEILD && weapon == Weapon.NONE && !deactivateSheild)
        {
			SheildDefend.Invoke();
        }

		currentWeapon = weapon;
    }

	void AnimationEventActivateBow()
    {

    }
}
