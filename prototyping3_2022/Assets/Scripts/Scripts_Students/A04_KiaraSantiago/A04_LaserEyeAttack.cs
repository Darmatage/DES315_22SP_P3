using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A04_LaserEyeAttack : MonoBehaviour
{
    public GameObject leftEyePrefab;
    public GameObject rightEyePrefab;

    private GameObject curLeft;
    private GameObject curRight;

    public string button;

    public float lengthOfAttack = 4f;
    public float cooldown = 1.0f;
    private bool readytoattack = true;
    private float cdtimer;

    private Vector3 leftOGpos;
    private Vector3 rightOGpos;

    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        leftOGpos = leftEyePrefab.transform.position;
        rightOGpos = rightEyePrefab.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 recentLeft = leftOGpos + transform.position + transform.forward * 3.0f;
        Vector3 recentRight = rightOGpos + transform.position + transform.forward * 3.0f;


        if(!readytoattack)
        {
            cdtimer -= Time.deltaTime;

            if(cdtimer <= 0)
            {
                readytoattack = true;
            }
        }

        if (Input.GetButtonDown(button) && readytoattack)
        {
            readytoattack = false;
            cdtimer = cooldown;

            curLeft = Instantiate(leftEyePrefab, recentLeft, Quaternion.identity);
            curRight = Instantiate(rightEyePrefab, recentRight, Quaternion.identity);


            Rigidbody left = curLeft.GetComponent<Rigidbody>();
            Rigidbody right = curRight.GetComponent<Rigidbody>();

            Vector3 tarLeft = transform.forward * lengthOfAttack + transform.up * -lengthOfAttack;
            Vector3 tarRight = transform.forward * lengthOfAttack + transform.up * -lengthOfAttack;

            left.AddRelativeForce(tarLeft, ForceMode.VelocityChange);
            right.AddRelativeForce(tarRight, ForceMode.VelocityChange);
            
            
        }

    } 
}
