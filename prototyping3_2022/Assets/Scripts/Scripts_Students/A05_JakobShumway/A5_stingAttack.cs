using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A5_stingAttack : MonoBehaviour
{
    [HideInInspector]
    public string atkKey;
    public GameObject tailJoint;

    //public Animation tailSting;

    private Animator tailAni;


    // Start is called before the first frame update
    void Start()
    {
        atkKey = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        tailAni = tailJoint.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(atkKey))
        {
            tailAni.Play("botA5_scorpionSting");
        }
    }
}
