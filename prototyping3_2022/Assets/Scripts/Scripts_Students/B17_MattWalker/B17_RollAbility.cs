using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B17_RollAbility : MonoBehaviour
{

    public float RollForce = 2000.0f;

    private float RollDirection;
    private bool IsRolling;

    //grab axis from parent object
    public string button1;
    public string button2;
    public string button3;
    public string button4; // currently boost in player move script
    public string jumpInput;

    // Start is called before the first frame update
    void Start()
    {
        IsRolling = false;


        button1 = gameObject.transform.parent.parent.GetComponent<playerParent>().action1Input;
        button2 = gameObject.transform.parent.parent.GetComponent<playerParent>().action2Input;
        button3 = gameObject.transform.parent.parent.GetComponent<playerParent>().action3Input;
        button4 = gameObject.transform.parent.parent.GetComponent<playerParent>().action4Input;
        jumpInput = gameObject.transform.parent.parent.GetComponent<playerParent>().jumpInput;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(jumpInput))
		{
            IsRolling = true;
            RollDirection = 1.0f;
		}
        else if (Input.GetButton(button1))
        {
            IsRolling = true;
            RollDirection = -1.0f;
        }

        if (IsRolling)
            Roll();

        IsRolling = false;

    }

    private void Roll()
    {
        Rigidbody rb = transform.parent.GetComponent<Rigidbody>();
        rb.AddRelativeTorque(new Vector3(0.0f, 0.0f, RollForce) * RollDirection);
    }
}
