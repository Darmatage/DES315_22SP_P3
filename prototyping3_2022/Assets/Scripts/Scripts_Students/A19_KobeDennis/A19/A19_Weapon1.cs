using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A19_Weapon1 : MonoBehaviour
{
    public A19_SoundManager a19_SoundManager;
    public GameObject dustPartilce;
    public GameObject damageCollider;
    public BotBasic_Move botBasic_Move;
    public Rigidbody rb;
    public float groundSlamCooldownTimer;
    public bool isGroundSlam = false;
    public float gravityScale = 10.0f;
    public MeshRenderer botSpot1aterial;
    public MeshRenderer botSpot2aterial;

    public Material cooldowwMaterial;


    //grab axis from parent object
	public string button1;


    private bool groundSlamCooldown = false;

    private Material orignalMaterial;

   
    // Start is called before the first frame update
    void Start()
    {
        orignalMaterial = botSpot1aterial.material;
        
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
            CooldownFeedback();


            

		}
        else if(Input.GetButtonDown(button1) && groundSlamCooldown)
        {
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
        a19_SoundManager.PlayGroundSlamEffect();

                Instantiate(dustPartilce, transform.position,  Quaternion.Euler(90, 0, 0) );
        isGroundSlam = false;
        damageCollider.SetActive(false);

    }

    private IEnumerator GroundSlamCourtine()
    {
        yield return new WaitForSeconds(groundSlamCooldownTimer);
        groundSlamCooldown = false;
        ResetMaterial(1);



    }
    public void ResetMaterial(int id)
    {
        if(id == 1)
         botSpot1aterial.material = orignalMaterial;

        if(id == 2)
         botSpot2aterial.material = orignalMaterial;
    }

    public void CooldownFeedback()
    {
        botSpot1aterial.material = cooldowwMaterial;

    }
     public void CooldownFeedbac2k()
    {
        botSpot2aterial.material = cooldowwMaterial;

    }
}
