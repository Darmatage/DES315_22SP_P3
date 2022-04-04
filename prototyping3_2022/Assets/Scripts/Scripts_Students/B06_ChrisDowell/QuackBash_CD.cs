using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuackBash_CD : MonoBehaviour
{
    private float timeHeld = 0;
    public float chargeTime = 2f;
    public float m_launchForce;
    public float m_MaxDamage;

    private BotBasic_Move m_movement;
    private Rigidbody m_rb;
    public AnimationCurve m_interp;

    private enum DuckState
    {
        NotActive,
        Charging,
        Fire
    };

    private DuckState m_state;


    private float m_originalSpeed;

    // wiser interface ig
    [System.NonSerialized] public string button1;
    [System.NonSerialized] public string button2;
    [System.NonSerialized] public string button3;
    [System.NonSerialized] public string button4; // currently boost in player move script


    // Start is called before the first frame update
    void Start()
    {
        // stolen from Jason Wiser's interface
        button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
        button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
        button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;


        m_movement = GetComponent<BotBasic_Move>();
        m_rb = GetComponent<Rigidbody>();
        m_originalSpeed = m_movement.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(button1))
        {
            // holding charges the duck
            timeHeld = Mathf.Clamp(timeHeld += Time.deltaTime, 0, chargeTime);
            float t = timeHeld / chargeTime;
            float eval = m_interp.Evaluate(t);

            // release launches the duck

            // damage is based on the charge
        }
    }
}
