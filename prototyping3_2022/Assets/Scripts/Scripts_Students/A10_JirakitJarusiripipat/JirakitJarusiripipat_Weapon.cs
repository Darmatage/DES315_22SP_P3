using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JirakitJarusiripipat_Weapon : MonoBehaviour
{
	private JirakitJarusiripipat_SoundKeeper soundKeeper;

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
	private GameObject mainGun;
	[SerializeField]
	private Material normalMainGun;
	[SerializeField]
	private Material cooldownMainGun;
	[SerializeField]
	private Transform mainGunBarrel;
	[Header("Missle Launcher")]
    [SerializeField]
    Transform[] shootPoint;
    [SerializeField]
    private GameObject missile;
	public GameObject weaponThrust;
	private float thrustAmount = 3f;
	private Animator animator;
	[SerializeField]
	private Material normalMissile;
	[SerializeField]
	private Material cooldownMissile;
	[SerializeField]
	private GameObject missileBarrel;
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
	private float holdTime = 1.0f;
	private float currentHoldTime;
	private bool holding = false;
	[SerializeField]
	private GameObject missileIndication;
	[Header("Suicide Bot")]
	[SerializeField]
	private float botCooldown = 7.0f;
	private float currentBotCooldown;
	private bool botOut = false;
	[SerializeField]
	private GameObject suicideBot;
	[SerializeField]
	private Transform botSpawn;
	[SerializeField]
	private GameObject catTail;
	[SerializeField]
	private Material normalCatTail;
	[SerializeField]
	private Material cooldownCatTail;
	[Header("UI")]
	[SerializeField]
	private Image missileBarImage;
	[SerializeField]
	private Image barBG;
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
		soundKeeper = GetComponent<JirakitJarusiripipat_SoundKeeper>();
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		//if (Input.GetKeyDown(KeyCode.T)){
		float dist;
		if (target != null)
        {
			dist = Vector3.Distance(target.transform.position, transform.position);

		}
		if ((Input.GetButtonDown(button2)) && (!missileOut) && missileReady)
		{
			missileOut = true;
			//weaponThrust.transform.Translate(0, thrustAmount, 0);
			StartCoroutine(WithdrawWeapon());
			holding = true;
			currentHoldTime = holdTime;
			animator.SetTrigger("Shoot");
			barBG.gameObject.SetActive(true);
			missileBarImage.fillAmount = 1.0f;

		}
		if (currentHoldTime > 0.0f && (missileOut) && missileReady)
        {
			currentHoldTime -= Time.deltaTime;
        }
		else
        {
			holding = false;

		}
		if (missileOut && currentEachCooldown <= 0.0f && currentMissileOut < maximumMissile && !holding)
        {
			GameObject obj = Instantiate(missile, shootPoint[currentMissileOut].transform.position, Quaternion.identity);
            float randomNumberX = Random.Range(-4.0f, 4.0f);
            float randomNumberZ = Random.Range(-4.0f, 4.0f);
            Vector3 velo = CalculateVelocity(new Vector3(target.transform.GetComponentInChildren<BotBasic_Damage>().gameObject.transform.position.x + randomNumberX, target.transform.GetComponentInChildren<BotBasic_Damage>().gameObject.transform.position.y - 3.0f, target.transform.GetComponentInChildren<BotBasic_Damage>().gameObject.transform.position.z + randomNumberZ), transform.position , 0.5f);
			obj.transform.rotation = Quaternion.LookRotation(velo);
			obj.GetComponentInChildren<Rigidbody>().velocity = velo;
            currentMissileOut++;
            currentEachCooldown = eachCooldown;
			soundKeeper.PlayLauncher();
			obj.GetComponentInChildren<JirakitJarusiripipat_Missile>().parent = gameObject;
			Vector3 placeToSpawn = new Vector3(target.transform.GetComponentInChildren<BotBasic_Damage>().gameObject.transform.position.x + randomNumberX, 0, target.transform.GetComponentInChildren<BotBasic_Damage>().gameObject.transform.position.z);
			Instantiate(missileIndication, placeToSpawn, Quaternion.identity);
			missileBarrel.GetComponent<Renderer>().material = cooldownMissile;
		}
		else if(currentEachCooldown > 0.0f)
        {
			currentEachCooldown -= Time.deltaTime;
        }
		if(!missileOut && currentMissileCooldown > 0.0f )
        {
			currentMissileCooldown -= Time.deltaTime;
			//uiDisplay += Time.deltaTime;
			missileBarImage.fillAmount = currentMissileCooldown / missileCooldown;
			missileReady = false;
		}
		else if(!missileOut && currentMissileCooldown <= 0.0f)
        {
			missileReady = true;
			missileBarrel.GetComponent<Renderer>().material = normalMissile;
			barBG.gameObject.SetActive(false);
		}

		if((Input.GetButtonDown(button1) && mainGunReady))
        {
			mainGunReady = false;
			GameObject obj = Instantiate(tankBullet, mainGunBarrel.position, Quaternion.identity);
			obj.GetComponentInChildren<Rigidbody>().AddForce(mainGunBarrel.forward * bulletSpeed);
			currentBulletCooldown = bulletCooldown;
			GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * -5000);
			soundKeeper.PlayMainGun();
			mainGun.GetComponent<Renderer>().material = cooldownMainGun;

		}
		if (currentBulletCooldown > 0.0f)
		{
			currentBulletCooldown -= Time.deltaTime;
			mainGunReady = false;
		}
		else if (currentBulletCooldown <= 0.0f)
		{
			mainGunReady = true;
			mainGun.GetComponent<Renderer>().material = normalMainGun;
		}

        if (Input.GetButtonDown(button3) && !botOut)
        {
			GameObject obj = Instantiate(suicideBot, botSpawn.transform.position, Quaternion.identity);
			obj.GetComponent<JirakitJarusiripipat_SuicideBot>().target = target.GetComponentInChildren<BotBasic_Damage>().gameObject.transform;
			Debug.Log(obj.GetComponent<JirakitJarusiripipat_SuicideBot>().target);
            botOut = true;
            currentBotCooldown = botCooldown;
			obj.GetComponent<JirakitJarusiripipat_SuicideBot>().parent = gameObject;
			catTail.GetComponentInChildren<Renderer>().material = cooldownCatTail;

        }
        if (currentBotCooldown > 0.0f && botOut)
        {
            currentBotCooldown -= Time.deltaTime;
        }
		else if(currentBotCooldown <= 0.0f)
        {
			botOut = false;
			catTail.GetComponentInChildren<Renderer>().material = normalCatTail;
		}
    }
	IEnumerator WithdrawWeapon()
	{
		yield return new WaitForSeconds(missileCooldown+1 - 4.0f);
		//weaponThrust.transform.Translate(0, -thrustAmount, 0);
		missileOut = false;
		currentMissileCooldown = missileCooldown;
		currentMissileOut = 0;
		//animator.SetTrigger("Idle");
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
