using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot22_RocketBoost : MonoBehaviour
{
    public GameObject leftBoost;
    public GameObject rightBoost;
	public GameObject body;
	public float boostSpeed = 1000f;
	private float thrustAmount = 1f;
	
	private bool boosting = false;

	private Rigidbody rb;

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
    }

    // Update is called once per frame
    void Update()
    {
		if ((Input.GetButtonDown(button1))&&(boosting==false))
		{
			leftBoost.transform.Translate(0,thrustAmount, 0);
            rightBoost.transform.Translate(0,thrustAmount, 0);
			boosting = true;
		
			rb.AddForce(transform.forward * boostSpeed, ForceMode.Impulse);


			StartCoroutine(TurnOffBoost());
		}  
        
    }
    
    IEnumerator TurnOffBoost()
    {
		yield return new WaitForSeconds(0.6f);
		leftBoost.transform.Translate(0,-thrustAmount, 0);
        rightBoost.transform.Translate(0,-thrustAmount, 0);
		boosting = false;
	}
}
