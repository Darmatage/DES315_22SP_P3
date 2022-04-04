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

    // damage support
    private HazardDamage hazardScript;
    private float damage;
    public float MaxScaleAffectingRollDamage = 3.5f;
    public float cooldown = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        IsRolling = false;

        button1 = gameObject.transform.parent.parent.GetComponent<playerParent>().action1Input;
        button2 = gameObject.transform.parent.parent.GetComponent<playerParent>().action2Input;
        button3 = gameObject.transform.parent.parent.GetComponent<playerParent>().action3Input;
        button4 = gameObject.transform.parent.parent.GetComponent<playerParent>().action4Input;
        jumpInput = gameObject.transform.parent.parent.GetComponent<playerParent>().jumpInput;

        hazardScript = transform.GetChild(0).GetComponent<HazardDamage>();
        damage = hazardScript.damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(button3))
		{
            IsRolling = true;
            RollDirection = 1.0f;
		}
        else if (Input.GetButton(button4))
        {
            IsRolling = true;
            RollDirection = -1.0f;
        }
        else
            IsRolling = false;

        if (IsRolling)
        {
            UpdateDamage();
            Roll();
        }
        else
            hazardScript.damage = 0.0f;
    }

    private void Roll()
    {
        Rigidbody rb = transform.parent.GetComponent<Rigidbody>();
        rb.AddRelativeTorque(new Vector3(0.0f, 0.0f, RollForce) * RollDirection);
    }

    public bool GetIsRolling()
	{
        return IsRolling;
	}

    private void UpdateDamage()
	{
        // the roll damage increases when this bot exceeds its normal scale
        float scaleRatio = transform.parent.localScale.magnitude - 1.0f;
        scaleRatio = Mathf.Clamp(scaleRatio, 0.0f, MaxScaleAffectingRollDamage);

        // the roll damage increases when the bot rolls faster
        Rigidbody rb = transform.parent.GetComponent<Rigidbody>();
        float velocityRatio = rb.angularVelocity.magnitude / rb.maxAngularVelocity;

        if (velocityRatio >= 2.0f)
            print("cool");

        hazardScript.damage = damage * scaleRatio * velocityRatio;
    }
}
