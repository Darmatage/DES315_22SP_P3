using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A19_Weapon2 : MonoBehaviour
{
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
        }
        else if (Input.GetButtonDown(button2) && !hasSpin && onSpinCooldown)
        {
           StartCoroutine(a19_Weapon1.CooldownFeedback());
        }
        if(hasSpin)
        {
            Spin();
        }
    }

    private void Spin()
    {
        transform.Rotate(spinRotation * spinSpeed * Time.deltaTime);

    }
    private IEnumerator SpinCorontine()
    {
        spinDamageCollider.SetActive(true);
        spinTrail.enabled = true;
        yield return new WaitForSeconds(spinTimer);
        spinDamageCollider.SetActive(false);

        hasSpin = false;
        onSpinCooldown = true;
        spinTrail.enabled = false;

        StartCoroutine(SpinCorontineCooldown());

    }
     private IEnumerator SpinCorontineCooldown()
    {
        yield return new WaitForSeconds(spinCooldown);
         hasSpin = false;
        onSpinCooldown = false;

    }
}
