using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B17_LeapAbility : MonoBehaviour
{
    public float Cooldown = 5.0f;
    public float ForwardForce = 10000.0f;
    public float UpwardForce = 3300.0f;

    private float CooldownTimer;
    // Start is called before the first frame update
    void Start()
    {
        CooldownTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Reduce cooldown
        CooldownTimer = Mathf.Clamp(CooldownTimer - Time.deltaTime, 0.0f, Cooldown);

        if (Input.GetKeyDown(KeyCode.R))
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
