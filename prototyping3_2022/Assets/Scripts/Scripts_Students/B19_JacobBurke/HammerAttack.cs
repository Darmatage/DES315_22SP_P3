using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAttack : MonoBehaviour
{
    [HideInInspector]
    public string hammerKey;

    Animator controller;
    Rigidbody rigidbody;
    float maxCharge = 10.0f;
    float currentCharge;

    public GameObject explosion;
    public GameObject hammerTip;

    // Start is called before the first frame update
    void Start()
    {
        hammerKey = transform.parent.gameObject.transform.parent.GetComponent<playerParent>().action2Input;

        controller = GetComponent<Animator>();
        
        rigidbody = transform.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCharge <= maxCharge)
            currentCharge += Time.deltaTime;

        if(Input.GetButtonDown(hammerKey))
        {
            controller.Play("B19_Smash");
            
            rigidbody.AddRelativeForce(new Vector3(0, 2 * currentCharge, -2 * currentCharge), ForceMode.Impulse);
            rigidbody.AddRelativeTorque(new Vector3(-32 * currentCharge, 0, 0), ForceMode.Impulse);

            GameObject tempExplosion = Instantiate(explosion);
            tempExplosion.transform.position = transform.parent.transform.position + transform.parent.transform.forward * 6;
            tempExplosion.transform.localScale = new Vector3(currentCharge / 2, currentCharge / 2, currentCharge / 2);

            hammerTip.GetComponent<HazardDamage>().damage = (int)Mathf.Pow(1.32f, currentCharge);

            currentCharge = 0;
        }

    }
}
