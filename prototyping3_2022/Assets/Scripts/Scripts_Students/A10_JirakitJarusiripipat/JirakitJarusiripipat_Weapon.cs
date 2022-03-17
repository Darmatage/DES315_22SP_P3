using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JirakitJarusiripipat_Weapon : MonoBehaviour
{
	[HideInInspector]
	//grab axis from parent object
	public string button1;
	[HideInInspector]
	public string button2;
	[HideInInspector]
	public string button3;
	[HideInInspector]
	public string button4; // currently boost in player move script
	[Header("TankMainGun")]
	[SerializeField]
	private GameObject tankBullet;
	[SerializeField]
	private float bulletCooldown;
	[SerializeField]
	private float bulletSpeed;
	private float currentBulletCooldown;
	private bool mainGunReady = true;
	[SerializeField]
	private Transform mainGunBarrel;
	[Header("Missle Launcher")]
    [SerializeField]
    Transform[] shootPoint;
    [SerializeField]
    private GameObject missile;
	public GameObject weaponThrust;
	private float thrustAmount = 3f;
	[SerializeField]
	private float missileCooldown;
	private float currentMissileCooldown;
	private bool missileOut = false;
	private bool missileReady = true;
	private bool weaponOut = false;
	


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
		if ((Input.GetButtonDown(button2)) && (!missileOut) && missileReady)
		{
			//weaponThrust.transform.Translate(0, thrustAmount, 0);
			missileOut = true;
			GameObject obj = Instantiate(missile, shootPoint[0].transform.position, Quaternion.identity);
			obj.GetComponent<JirakitJarusiripipat_Missile>().originPos = shootPoint[0].transform.position ;
			obj.GetComponent<JirakitJarusiripipat_Missile>().targetPos = transform.position + new Vector3(1, 1, 4);
			
			StartCoroutine(WithdrawWeapon());
		}
		if(!missileOut && currentMissileCooldown > 0.0f)
        {
			currentMissileCooldown -= Time.deltaTime;
			missileReady = false;
		}
		else if(!missileOut && currentMissileCooldown <= 0.0f)
        {
			missileReady = true;
		}

		if((Input.GetButtonDown(button1) && mainGunReady))
        {
			mainGunReady = false;
			GameObject obj = Instantiate(tankBullet, mainGunBarrel.position, Quaternion.identity);
			obj.GetComponent<Rigidbody>().AddForce(mainGunBarrel.forward * bulletSpeed);
			currentBulletCooldown = bulletCooldown;

		}
		if (currentBulletCooldown > 0.0f)
		{
			currentBulletCooldown -= Time.deltaTime;
			mainGunReady = false;
		}
		else if (currentBulletCooldown <= 0.0f)
		{
			mainGunReady = true;
		}

	}

	IEnumerator WithdrawWeapon()
	{
		yield return new WaitForSeconds(missileCooldown - 4.0f);
		//weaponThrust.transform.Translate(0, -thrustAmount, 0);
		missileOut = false;
		currentMissileCooldown = missileCooldown;
	}
}
