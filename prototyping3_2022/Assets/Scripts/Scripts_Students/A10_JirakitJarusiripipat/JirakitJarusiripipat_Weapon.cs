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
	private float eachCooldown = 0.5f;
	private float currentEachCooldown;
	private int currentMissileOut;
	private int maximumMissile = 4;
	private GameObject target;
	void Start()
	{
		button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
		button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
		button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
		button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;
		if(GetComponentInParent<playerParent>().isPlayer1)
        {
			target = GameObject.FindGameObjectWithTag("Player2");
		}
		else
        {

			target = GameObject.FindGameObjectWithTag("Player1");
		}
	}

	void Update()
	{
		//if (Input.GetKeyDown(KeyCode.T)){
		if ((Input.GetButtonDown(button2)) && (!missileOut) && missileReady)
		{
			//weaponThrust.transform.Translate(0, thrustAmount, 0);
			missileOut = true;
			StartCoroutine(WithdrawWeapon());
		}
		if(missileOut && currentEachCooldown <= 0.0f && currentMissileOut < maximumMissile)
        {
			GameObject obj = Instantiate(missile, shootPoint[0].transform.position, Quaternion.identity);
            float randomNumberX = Random.Range(-4.0f, 4.0f);
            float randomNumberZ = Random.Range(-4.0f, 4.0f);
            Vector3 velo = CalculateVelocity(new Vector3(target.transform.GetComponentInChildren<BotBasic_Damage>().gameObject.transform.position.x + randomNumberX, target.transform.GetComponentInChildren<BotBasic_Damage>().gameObject.transform.position.y - 2.2f, target.transform.GetComponentInChildren<BotBasic_Damage>().gameObject.transform.position.z + randomNumberZ), transform.position , 1.5f);
			obj.transform.rotation = Quaternion.LookRotation(velo);
			obj.GetComponent<Rigidbody>().velocity = velo;
            currentMissileOut++;
            Debug.Log(currentMissileOut);
            currentEachCooldown = eachCooldown;
			Debug.Log(currentEachCooldown);
		}
		else if(currentEachCooldown > 0.0f)
        {
			currentEachCooldown -= Time.deltaTime;
        }
		if(!missileOut && currentMissileCooldown > 0.0f )
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
			GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * -5000);

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
		//
	}
	IEnumerator WithdrawWeapon()
	{
		yield return new WaitForSeconds(missileCooldown - 4.0f);
		//weaponThrust.transform.Translate(0, -thrustAmount, 0);
		missileOut = false;
		currentMissileCooldown = missileCooldown;
		currentMissileOut = 0;
	}
	public Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
	{
		Vector3 distance = target - origin;
		Vector3 distanceXZ = distance;
		distanceXZ.y = 0f;

		float sy = distance.y;
		float sxz = distanceXZ.magnitude;

		float vxz = sxz / time;
		float vy = sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

		Vector3 result = distanceXZ.normalized;
		result *= vxz;
		result.y = vy;

		return result;
	}
}
