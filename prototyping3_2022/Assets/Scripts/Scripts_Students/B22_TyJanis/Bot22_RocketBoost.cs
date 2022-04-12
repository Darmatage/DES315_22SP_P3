using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot22_RocketBoost : MonoBehaviour
{
    public GameObject leftBoost;
    public GameObject rightBoost;
	public GameObject body;
	public GameObject cooldownMeter;
	public GameObject spike;
	public float boostSpeed = 1000f;
	public float cooldownTime = 2.0f;
	AudioSource Audio;
	public AudioClip chargeSound;
	private float nextFireTime = 0.0f;
	private float thrustAmount = 1f;
	
	private bool boosting = false;

	private Rigidbody rb;
	private Renderer cooldownColor;

	//grab axis from parent object
	[HideInInspector]
	public string button1;
	[HideInInspector]
	public string button2;
	[HideInInspector]
	public string button3;
	[HideInInspector]
	public string button4; // currently boost in player move script

    // Start is called before the first frame update
    void Start()
    {
     	button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
		button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
		button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
		button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;

		if (body.GetComponent<Rigidbody>() != null)
		{
			rb = body.GetComponent<Rigidbody>();
		}

		cooldownColor = cooldownMeter.GetComponent<Renderer>();

		Audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {		
		if(Time.time > nextFireTime)
		{
			cooldownColor.material.SetColor("_Color", Color.green);

			if ((Input.GetButton(button1)) && !boosting)
			{
				Audio.PlayOneShot(chargeSound);
				boosting = true;

				leftBoost.transform.Translate(0,thrustAmount, 0);
				rightBoost.transform.Translate(0,thrustAmount, 0);
			
				rb.AddForce(transform.forward * boostSpeed, ForceMode.Impulse);
				StartCoroutine(TurnOffBoost()); 

				nextFireTime = Time.time + cooldownTime;

			}

		}
		else
		{
			cooldownColor.material.SetColor("_Color", Color.red);
		}

		if(!boosting)
			spike.GetComponent<HazardDamage>().damage = 1.0f;
		else
			spike.GetComponent<HazardDamage>().damage = 3.0f;

		//Debug.Log(spike.GetComponent<HazardDamage>().damage);	
        
    }
    
    IEnumerator TurnOffBoost()
    {
		yield return new WaitForSeconds(0.6f);
		leftBoost.transform.Translate(0,-thrustAmount, 0);
        rightBoost.transform.Translate(0,-thrustAmount, 0);
		boosting = false;
	}

}
