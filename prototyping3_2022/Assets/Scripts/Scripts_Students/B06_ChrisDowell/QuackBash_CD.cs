using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuackBash_CD : MonoBehaviour
{
    private float timeHeld = 0;
    public float chargeTime = 2f;
    public float m_launchForce = 1000;
    public float m_minDamage;
    public float m_MaxDamage;
    public float m_scale;

    private HazardDamage m_weapon;
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


    private Vector3 m_originalScale;
    private float m_originalSpeed;
    private float m_originalTurn;
    private float m_originalDamage;
    public float m_chargeSpeed = 3f;
    public float m_chargeTurn = 14;

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
        m_weapon = GetComponentInChildren<HazardDamage>();
        m_originalSpeed = m_movement.moveSpeed;
        m_originalTurn = m_movement.rotateSpeed;
        m_originalDamage = m_weapon.damage;
        m_originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(button1))
        {
            m_state = DuckState.Charging;
            // holding charges the duck
            timeHeld = Mathf.Clamp(timeHeld += Time.deltaTime, 0, chargeTime);
            float t = timeHeld / chargeTime;
            float eval = m_interp.Evaluate(t);

            m_movement.moveSpeed = Mathf.Lerp(m_originalSpeed, m_chargeSpeed, eval);
            m_movement.rotateSpeed = Mathf.Lerp(m_originalTurn, m_chargeTurn, eval);
            m_weapon.damage = Mathf.Lerp(m_minDamage, m_MaxDamage, eval);
            transform.localScale = Vector3.Lerp(m_originalScale, m_originalScale * m_scale, eval);
            // damage is based on the charge
        }
        else
        {
            // when release
            if (m_state == DuckState.Charging)
            {
                // Reset movespeed
                m_movement.moveSpeed = 0;
                m_movement.rotateSpeed = 0;

                Invoke("ResetBot", chargeTime);
                // launch bot
                m_rb.AddForce(transform.forward * m_launchForce, ForceMode.Impulse);
                m_state = DuckState.Fire;
                timeHeld = 0;

            }
            else if (m_state == DuckState.Fire)
            {

            }

        }
        
    }

    private void ResetBot()
    {
        m_movement.moveSpeed = m_originalSpeed;
        m_movement.rotateSpeed = m_originalTurn;
        m_weapon.damage = m_originalDamage;
        transform.localScale = m_originalScale;
        m_state = DuckState.NotActive;
    }
}
