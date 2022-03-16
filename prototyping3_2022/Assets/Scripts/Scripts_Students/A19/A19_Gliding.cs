using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A19_Gliding : MonoBehaviour
{
    public BotBasic_Move botBasic_Move;
    public A19_Weapon1 a19_Weapon1;
    public Rigidbody rb;
    public bool isGliding = false;
    private float gravityWorld = -9.81f;
    public float glideSpeed = 1f;
    private float gravityScale = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Gliding();
    }
    private void  FixedUpdate()
    {
        if(botBasic_Move.isGrounded == false)
        {
            Vector3 gravity = gravityWorld * gravityScale * Vector3.up;
            rb.AddForce(gravity, ForceMode.Acceleration);
        }
    }

    public void Gliding()
    {
        if(!botBasic_Move.isGrounded)
        {
            if(Input.GetButton(botBasic_Move.pJump) && rb.velocity.y <= 0f && !a19_Weapon1.isGroundSlam)
            {
                isGliding = true;
                rb.velocity = new Vector3(rb.velocity.x, -glideSpeed, rb.velocity.z);
            }
            else
            {
                isGliding = false;
            }
        }
    }
}
