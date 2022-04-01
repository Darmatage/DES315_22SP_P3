using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A5_stingAttack : MonoBehaviour
{
    [HideInInspector]
    public string atkKey;
    public GameObject tailJoint;
    private Animator tailAni;

    [HideInInspector]
    public string clawKey;

    [HideInInspector]
    public string grabKey;
    public GameObject leftClaw;
    public GameObject rightClaw;
    private Animator leftClawAni;
    private Animator rightClawAni;

    [HideInInspector]
    public bool isClawing = false;

    [HideInInspector]
    public bool isGrabbing = false;

    // Start is called before the first frame update
    void Start()
    {
        clawKey = gameObject.transform.parent.GetComponent<playerParent>().action1Input;

        atkKey = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
        tailAni = tailJoint.GetComponent<Animator>();

        grabKey = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
        leftClawAni = leftClaw.GetComponent<Animator>();
        rightClawAni = rightClaw.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown(clawKey) && !isGrabbing)
        {
            leftClawAni.Play("botA5_clawHitLeft");
            rightClawAni.Play("botA5_clawHit");
        }

        if (Input.GetButtonDown(atkKey))
            tailAni.Play("botA5_scorpionSting");

        if (Input.GetButtonDown(grabKey) && !isGrabbing && !isClawing)
        {
            leftClawAni.Play("botA5_clawGrabLeft");
            rightClawAni.Play("botA5_clawGrab");
        }
    }
}
