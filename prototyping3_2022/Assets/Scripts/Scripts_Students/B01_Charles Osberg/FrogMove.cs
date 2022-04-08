using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMove : MonoBehaviour
{
	public float moveSpeed = 10;
	public float rotateSpeed = 100;
	public float jumpSpeed = 7f;
	private float flipSpeed = 150f;

	private float jumpAccumulation = 0.0f;
	private float jumpAccumulationRate = 1.0f;
	
	private Rigidbody rb;
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

	//grab axis from parent object
	public string pVertical;
	public string pHorizontal;
	public string pJump;
	
    void Start(){
		if (gameObject.GetComponent<Rigidbody>() != null){
			rb = gameObject.GetComponent<Rigidbody>();
		}

		pVertical = gameObject.transform.parent.GetComponent<playerParent>().moveAxis;
		pHorizontal = gameObject.transform.parent.GetComponent<playerParent>().rotateAxis;
		pJump = gameObject.transform.parent.GetComponent<playerParent>().jumpInput;
		
		// pVertical = "p1Vertical";
		// pHorizontal = "p1Horizontal";
		// pJump = "p1Jump";
		// button4 = "p1Fire4";
    }

    void Update()
    {
	    float botMove = Input.GetAxisRaw(pVertical) * moveSpeed * Time.deltaTime;
	    float botRotate = Input.GetAxisRaw(pHorizontal) * rotateSpeed * Time.deltaTime;

	    if (isGrabbed == false)
	    {
		    transform.Translate(0, 0, botMove);
		    transform.Rotate(0, botRotate, 0);
	    }

	    // JUMP
	    isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
	    isTurtled = Physics.CheckSphere(turtleCheck.position, 0.4f, groundLayer);
	    if (Input.GetButtonDown(pJump))
	    {
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
			    Vector3 betterEulerAngles = new Vector3(gameObject.transform.parent.eulerAngles.x,
				    transform.eulerAngles.y, gameObject.transform.parent.eulerAngles.z);
			    transform.eulerAngles = betterEulerAngles;
			    GetComponent<Rigidbody>().velocity = Vector3.zero;
			    GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		    }
	    }
	    
	    if (Input.GetButtonUp(pJump))
	    {
		    if (isGrounded)
		    {
			    var jumpForce = transform.up * jumpSpeed * 10.0f * jumpAccumulation;
			    var baseRotation = transform.rotation.eulerAngles;
			    baseRotation.z = 0;
			    baseRotation.x = -45;
			    jumpForce = Quaternion.Euler(baseRotation) * jumpForce;
			    jumpForce = Vector3.Reflect(jumpForce, transform.forward);
			    rb.AddForce(rb.centerOfMass + jumpForce, ForceMode.Impulse);
		    }
		    jumpAccumulation = 0.0f;
	    }
	    else if(Input.GetButton(pJump))
	    {
		    if (isGrounded)
		    {
			    jumpAccumulation = Mathf.Clamp01(jumpAccumulation + Time.deltaTime * jumpAccumulationRate);
		    }
		    else
		    {
			    jumpAccumulation = 0.0f;
			    rb.AddForce((transform.up * -1 + rb.centerOfMass) * 10.0f, ForceMode.Acceleration);
		    }
	    }
    }
    
}
