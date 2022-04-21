using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SethMerrill_BotScript : MonoBehaviour
{
	public GameObject weapon;
	public GameObject arms;
	private string b1, b2, b3, b4;
	private float timeUntilReady;
	public float rechargeTime;
	public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
		audio = GetComponent<AudioSource>();
		b1 = transform.parent.GetComponent<playerParent>().action1Input;
		b2 = transform.parent.GetComponent<playerParent>().action2Input;
		b3 = transform.parent.GetComponent<playerParent>().action3Input;
		b4 = transform.parent.GetComponent<playerParent>().action4Input;
		timeUntilReady = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeUntilReady > 0.0f)
		{
			timeUntilReady -= Time.deltaTime;
		}
		else
		{
			timeUntilReady = 0.0f;
			weapon.GetComponent<ParticleSystem>().Play();
			if(CheckInputFire() && weapon != null)
			{
				audio.Play();
				arms.GetComponent<SethMerrillArmsScript>().Unhold();
				weapon.GetComponent<ParticleSystem>().Stop();
				weapon.GetComponent<SethMerrillCannonScript>().Fire();
				GetComponent<Rigidbody>().AddForce(transform.forward * -5000.0f);
				timeUntilReady = rechargeTime;
			}
		}
    }
	
	bool CheckInputFire()
	{
		return Input.GetButtonDown(b1);
	}
	
	bool CheckInputArms()
	{
		return Input.GetButtonDown(b2);
	}
	
	private void OnTriggerStay(Collider other)
	{
		if(CheckInputArms() && arms != null)
		{
			arms.GetComponent<SethMerrillArmsScript>().Grab(other.gameObject);
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		arms.GetComponent<SethMerrillArmsScript>().Unhold();
	}
}
