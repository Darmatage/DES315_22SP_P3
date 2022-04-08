using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A19_Weapon2 : MonoBehaviour
{
    public A19_SoundManager a19_SoundManager;

    public GameObject spinDamageCollider;
    
    public string button2;
    public Vector3 spinRotation;
    public float spinSpeed;
    public float spinTimer;
    public float spinCooldown;
    public TrailRenderer spinTrail;
    public A19_Weapon1 a19_Weapon1;
    



    [SerializeField]
    private bool hasSpin = false;
    [SerializeField]
    private bool onSpinCooldown = false;

     public Quaternion originalRotationValue; // declare this as a Quaternion



    // Start is called before the first frame update
    void Start()
    {
        button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(button2) && !hasSpin && !onSpinCooldown)
        {
            hasSpin = true;
            StartCoroutine(SpinCorontine());
            a19_Weapon1.CooldownFeedbac2k();
            originalRotationValue = transform.rotation;
            a19_SoundManager.PlaySpinEffect();

        }
        else if (Input.GetButtonDown(button2) && !hasSpin && onSpinCooldown)
        {
        }
        if(hasSpin)
        {
            Spin();
        }
    }

    private void Spin()
    {
        transform.RotateAround(transform.position,Vector3.up,360f * (Time.deltaTime/1f * spinSpeed));

    }
    private IEnumerator SpinCorontine()
    {
        gameObject.GetComponent<BotBasic_Damage>().enabled = false;
        spinDamageCollider.SetActive(true);
        spinTrail.enabled = true;
        yield return new WaitForSeconds(spinTimer);

      //  transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.deltaTime * spinCooldown); 
        spinDamageCollider.SetActive(false);

        hasSpin = false;
        onSpinCooldown = true;
        spinTrail.enabled = false;
        gameObject.GetComponent<BotBasic_Damage>().enabled = true;


        StartCoroutine(SpinCorontineCooldown());

    }
     private IEnumerator SpinCorontineCooldown()
    {
        yield return new WaitForSeconds(spinCooldown);
         hasSpin = false;
        onSpinCooldown = false;
        a19_Weapon1.ResetMaterial(2);


    }
}
