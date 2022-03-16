using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A19_Weapon1 : MonoBehaviour
{
    public BotBasic_Move botBasic_Move;
     public Rigidbody rb;
    public bool isGroundSlam = false;
    public float gravityScale = 10.0f;

    //grab axis from parent object
	public string button1;
	public string button2;
	public string button3;
	public string button4; // currently boost in player move script

   
    // Start is called before the first frame update
    void Start()
    {
        button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
		button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
		button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
		button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T)){
		if (Input.GetButtonDown(button1) && rb.velocity.y <= 0f && !isGroundSlam){
			Vector3 gravity = -9.81f * gravityScale * Vector3.up;
            rb.AddForce(gravity, ForceMode.Acceleration);

		}

        if(botBasic_Move.isGrounded)
        {
            isGroundSlam = true;
        }
    }
}
