using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B18_Movement : MonoBehaviour
{
	public float moveSpeed = 10;
	public float rotateSpeed = 100;
	public float jumpSpeed = 7f;
	private float flipSpeed = 150f;
	public float boostSpeed = 10f;

	private Rigidbody rb;
	private Animator anim;
	public Transform groundCheck;
	public Transform turtleCheck;
	public LayerMask groundLayer;
	//public Collider[] isGrounded;
	public bool isGrounded;
	public bool isTurtled;

	// flip cooldown logic
	public bool canFlip = true;
	// private bool canFlipGate = true;
	// private float flipTimer = 0;
	// public float flipTime = 1f;

	public bool isGrabbed = false;
	public bool isAnchored = false;

	//grab axis from parent object
	public string parentName;
	public string pVertical;
	public string pHorizontal;
	public string pJump;
	public string button2;
	public string button3;
	public string button4; 

	// Start is called before the first frame update
	void Start()
    {
		if (gameObject.GetComponent<Rigidbody>() != null)
		{
			rb = gameObject.GetComponent<Rigidbody>();
		}

		if (gameObject.GetComponent<Animator>() != null)
		{
			anim = gameObject.GetComponent<Animator>();
		}

		parentName = this.transform.parent.gameObject.name;
		pVertical = gameObject.transform.parent.GetComponent<playerParent>().moveAxis;
		pHorizontal = gameObject.transform.parent.GetComponent<playerParent>().rotateAxis;
		pJump = gameObject.transform.parent.GetComponent<playerParent>().jumpInput;
		button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
		button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
		button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;
	}

    // Update is called once per frame
    void Update()
    {
		// JUMP
		isGrounded = Physics.CheckSphere(groundCheck.position, 0.8f, groundLayer);
		isTurtled = Physics.CheckSphere(turtleCheck.position, 0.4f, groundLayer);

		if(isGrounded && !isAnchored && Input.GetButtonDown(button2))
        {
			Anchor();
        }
        else if(isAnchored && Input.GetButtonDown(button2))
        {
			UnAnchor();
        }

		float botStrafe = (-Input.GetAxisRaw(button3) + Input.GetAxisRaw(button4)) * moveSpeed * Time.deltaTime;
		float botMove = Input.GetAxisRaw(pVertical) * moveSpeed * Time.deltaTime;
		float botRotate = Input.GetAxisRaw(pHorizontal) * rotateSpeed * Time.deltaTime;

		if (isGrabbed == false && isAnchored == false)
		{
			transform.Translate(botStrafe, 0, 0);
			transform.Translate(0, 0, botMove);
			transform.Rotate(0, botRotate, 0);
		}


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

	private void Anchor()
    {
		anim.SetTrigger("MoveFeetDown");
		isAnchored = true;
    }

	private void UnAnchor()
    {
		anim.SetTrigger("MoveFeetUp");
		isAnchored = false;
    }
}
