using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotB15_Weapon : MonoBehaviour{
    //NOTE: This script goes on the main playerBot Game Object, and the weapon goes in the public GO slot

    public GameObject FrontWeapon;
    public GameObject BackWeapon;

    public GameObject RightWeapon;
    public GameObject LeftWeapon;

    public GameObject UnderWeapon;


    public GameObject BottomSmoke;
    //public AudioSource SoundEffect;
    //public AudioSource WoopSoundEffect;
    private float thrustAmount = 3.0f;

    private bool frontWeaponOut = false;
	private bool backWeaponOut = false;
    public bool rightWeaponOut = false;
    public bool leftWeaponOut = false;

    public bool underWeaponOut = false;

    private bool emergencyEject = true;
    private Rigidbody rb;

	//grab axis from parent object
	public string button1;
	public string button2;
	public string button3;
	public string button4; // currently boost in player move script

    void Start(){
        rb = GetComponent<Rigidbody>();
		button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
		button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
		button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
		button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;

        BottomSmoke.SetActive(false);
    }

    void Update(){
		//if (Input.GetKeyDown(KeyCode.T)){
		if ((Input.GetButtonDown(button1))&&(frontWeaponOut==false))
        {
            FrontWeapon.transform.localScale = new Vector3(3, 3, 3);
            FrontWeapon.transform.Translate(0, thrustAmount, 0);

            BackWeapon.transform.localScale = new Vector3(3,3,3);
            BackWeapon.transform.Translate(0, thrustAmount, 0);

            RightWeapon.transform.localScale = new Vector3(3, 3, 3);
            RightWeapon.transform.Translate(0, thrustAmount, 0);

            LeftWeapon.transform.localScale = new Vector3(3, 3, 3);
            LeftWeapon.transform.Translate(0, thrustAmount, 0);

            //SoundEffect.PlayOneShot(SoundEffect.clip);
            frontWeaponOut = true;
            backWeaponOut = true;
            rightWeaponOut = true;
            leftWeaponOut = true;
			StartCoroutine(WithdrawFrontWeapon());
        }
        if ((Input.GetButtonDown(button2)) && (backWeaponOut == false))
        {
            UnderWeapon.transform.localScale = new Vector3(3, 3, 3);
            UnderWeapon.transform.Translate(0, thrustAmount, 0);
            //SoundEffect.PlayOneShot(SoundEffect.clip);
            underWeaponOut = true;
            StartCoroutine(WithdrawUnderWeapon());
        }
        if (Input.GetButtonDown(button3) && (emergencyEject == true))
        {
            rb.AddForce(rb.centerOfMass + new Vector3(/*Random.Range(0, 200)*/0, 200, 0/* Random.Range(0, 200)*/), ForceMode.Impulse);
            //WoopSoundEffect.Play();
            StartCoroutine(EmergencyCooldown());
            emergencyEject = false;
        }
    }

	IEnumerator WithdrawFrontWeapon(){
		yield return new WaitForSeconds(0.6f);
        FrontWeapon.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        FrontWeapon.transform.Translate(0, -thrustAmount, 0);
        BackWeapon.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        BackWeapon.transform.Translate(0, -thrustAmount, 0);
        RightWeapon.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        RightWeapon.transform.Translate(0, -thrustAmount, 0);
        LeftWeapon.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        LeftWeapon.transform.Translate(0, -thrustAmount, 0);

        frontWeaponOut = false;
	}

    IEnumerator WithdrawUnderWeapon()
    {
        yield return new WaitForSeconds(0.6f);
        UnderWeapon.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        UnderWeapon.transform.Translate(0, -thrustAmount, 0);
        underWeaponOut = false;
    }

    IEnumerator EmergencyCooldown()
    {
        BottomSmoke.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        BottomSmoke.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        emergencyEject = true;
    }
}
