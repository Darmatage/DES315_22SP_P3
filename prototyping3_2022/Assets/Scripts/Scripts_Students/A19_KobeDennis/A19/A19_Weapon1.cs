using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A19_Weapon1 : MonoBehaviour
{
    public GameObject dustPartilce;
    public GameObject damageCollider;
    public BotBasic_Move botBasic_Move;
    public Rigidbody rb;
    public float groundSlamCooldownTimer;
    public bool isGroundSlam = false;
    public float gravityScale = 10.0f;
    public MeshRenderer botBodyMaterial;
    public Material cooldowwMaterial;


    //grab axis from parent object
	public string button1;


    private bool groundSlamCooldown = false;

    private Material orignalMaterial;

   
    // Start is called before the first frame update
    void Start()
    {
        orignalMaterial = botBodyMaterial.material;
        groundSlamCooldown = false;
        button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T)){
		if (Input.GetButtonDown(button1) && !isGroundSlam && !groundSlamCooldown){

            //Trying to slam while grounded
            if(botBasic_Move.isGrounded)
            {
                return;
            }
            isGroundSlam = true;
            groundSlamCooldown = true;
            damageCollider.SetActive(true);
			Vector3 gravity = -9.81f * gravityScale * Vector3.up;
            rb.AddForce(gravity, ForceMode.Acceleration);
            StartCoroutine(GroundSlamCourtine());

            

		}
        else if(Input.GetButtonDown(button1) && groundSlamCooldown)
        {
            StartCoroutine(CooldownFeedback()); 
        }
        

        if(isGroundSlam)
        {
            if(botBasic_Move.isGrounded)
            {
                ResetGroundSlam();
            }
        }
    }

    public void ResetGroundSlam()
    {
                Instantiate(dustPartilce, transform.position,  Quaternion.Euler(90, 0, 0) );

        isGroundSlam = false;
        damageCollider.SetActive(false);

    }

    private IEnumerator GroundSlamCourtine()
    {
        yield return new WaitForSeconds(groundSlamCooldownTimer);
        groundSlamCooldown = false;


    }

    public IEnumerator CooldownFeedback()
    {
        botBodyMaterial.material = cooldowwMaterial;
        yield return new WaitForSeconds(0.2f);
        botBodyMaterial.material = orignalMaterial;

    }
}
