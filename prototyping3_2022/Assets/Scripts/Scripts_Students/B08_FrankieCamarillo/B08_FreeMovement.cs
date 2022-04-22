using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B08_FreeMovement : BotBasic_Move
{
	//private Rigidbody rb;

	


	private string thisPlayer;
	private Transform otherPlayer;

	private new void Start()
    {
		if (gameObject.GetComponent<Rigidbody>() != null)
		{
			rb = gameObject.GetComponent<Rigidbody>();
		}

		parentName = this.transform.parent.gameObject.name;

		thisPlayer = gameObject.transform.root.tag;

		if (thisPlayer == "Player1")
        {
			otherPlayer = GameObject.FindWithTag("Player2").transform.GetChild(0).gameObject.transform;
		}
		else
        {
			otherPlayer = GameObject.FindWithTag("Player1").transform.GetChild(0).gameObject.transform;
		}



		pVertical = gameObject.transform.parent.GetComponent<playerParent>().moveAxis;
		pHorizontal = gameObject.transform.parent.GetComponent<playerParent>().rotateAxis;
		pJump = gameObject.transform.parent.GetComponent<playerParent>().jumpInput;
		button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;

		// pVertical = "p1Vertical";
		// pHorizontal = "p1Horizontal";
		// pJump = "p1Jump";
		// button4 = "p1Fire4";
	}

    new void Update()
	{
		float botMove = Input.GetAxisRaw(pVertical) * moveSpeed * Time.deltaTime;
		float botStrafe = Input.GetAxisRaw(pHorizontal) * moveSpeed * Time.deltaTime;


		transform.LookAt(otherPlayer);

		

		//gameObject.transform.root.tag;
		

		if (isGrabbed == false)
		{
			transform.Translate(botStrafe, 0, botMove);
			transform.Rotate(0, /* change this */0, 0);
		}

		// JUMP
		isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
		isTurtled = Physics.CheckSphere(turtleCheck.position, 0.4f, groundLayer);
		if (Input.GetButtonDown(pJump))
		{
			if (isGrounded == true)
			{
				rb.AddForce(rb.centerOfMass + new Vector3(0f, jumpSpeed * 10, 0f), ForceMode.Impulse);
			}

			//flip cooldown logic
			// if ((isTurtled == true) && (canFlip == false)){
			// canFlipGate = false;	
			// }

			if ((isTurtled == true) && (canFlip == true))
			{
				rb.AddForce(rb.centerOfMass + new Vector3(jumpSpeed / 2, 0, jumpSpeed / 2), ForceMode.Impulse);
				transform.Rotate(flipSpeed, 0, 0);
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
				// canFlip = false;
				// canFlipGate = true;
			}

			else if (canFlip == true)
			{
				Vector3 betterEulerAngles = new Vector3(gameObject.transform.parent.eulerAngles.x, transform.eulerAngles.y, gameObject.transform.parent.eulerAngles.z);
				transform.eulerAngles = betterEulerAngles;
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			}
		}
	}
}
