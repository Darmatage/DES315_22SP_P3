using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B17_LeapAbility : MonoBehaviour
{
    public float Cooldown = 5.0f;
    public float ForwardForce = 10000.0f;
    public float UpwardForce = 3300.0f;

    private float CooldownTimer;

    //grab axis from parent object
    public string button1;
    public string button2;
    public string button3;
    public string button4; // currently boost in player move script

    // Start is called before the first frame update
    void Start()
    {
        CooldownTimer = 0.0f;

        button1 = gameObject.transform.parent.parent.GetComponent<playerParent>().action1Input;
        button2 = gameObject.transform.parent.parent.GetComponent<playerParent>().action2Input;
        button3 = gameObject.transform.parent.parent.GetComponent<playerParent>().action3Input;
        button4 = gameObject.transform.parent.parent.GetComponent<playerParent>().action4Input;
    }

    // Update is called once per frame
    void Update()
    {
        // Reduce cooldown
        CooldownTimer = Mathf.Clamp(CooldownTimer - Time.deltaTime, 0.0f, Cooldown);

        if (Input.GetButtonDown(button2))
        {
            if (CooldownTimer <= 0.0f)
			{
                CooldownTimer = Cooldown;
                Leap();
			}
        }
    }

    private void Leap()
    {
        Rigidbody rb = transform.parent.GetComponent<Rigidbody>();
        rb.AddRelativeForce(new Vector3(0.0f, UpwardForce, ForwardForce));
    }
}
