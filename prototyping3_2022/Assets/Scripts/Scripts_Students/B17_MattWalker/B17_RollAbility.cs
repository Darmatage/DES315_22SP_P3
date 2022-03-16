using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B17_RollAbility : MonoBehaviour
{

    public float RollForce = 2000.0f;

    private float RollDirection;
    private bool IsRolling;



    // Start is called before the first frame update
    void Start()
    {
        IsRolling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
		{
            IsRolling = true;
            RollDirection = 1.0f;
		}
        else if (Input.GetKey(KeyCode.E))
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
