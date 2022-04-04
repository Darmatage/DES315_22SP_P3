using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A19_Gliding : MonoBehaviour
{
    public GameObject glideEffect;
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
        Vector3 gravity = gravityWorld * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
    public void UpdateGlidingEffect()
    {
        if(isGliding)
        {
            glideEffect.SetActive(true);
        }
        else
        {
            glideEffect.SetActive(false);
        }
    }

    public void Gliding()
    {
        if(!botBasic_Move.isGrounded)
        {
            if(a19_Weapon1.isGroundSlam && Input.GetButton(botBasic_Move.pJump))
                a19_Weapon1.isGroundSlam = false;
                
            if( !a19_Weapon1.isGroundSlam && Input.GetButton(botBasic_Move.pJump) && rb.velocity.y <= 0f)
            {
                
                isGliding = true;
                rb.velocity = new Vector3(rb.velocity.x, -glideSpeed, rb.velocity.z);
                UpdateGlidingEffect();
            }
            else
            {
                isGliding = false;
                UpdateGlidingEffect();
            }
        }
        else
        {
            isGliding = false;
            UpdateGlidingEffect();
        }
    }
}
