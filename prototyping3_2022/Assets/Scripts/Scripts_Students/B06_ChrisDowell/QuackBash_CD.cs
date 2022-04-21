using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuackBash_CD : MonoBehaviour
{



    private float timeHeld = 0;
    [Header("Bot Attributes")]
    private float m_cooldowndt = 0;
    public float m_coolDown = 1;
    public float chargeTime = 2f;
    public float m_launchForce = 1000;
    public float m_minDamage;
    public float m_MaxDamage;

    public float m_chargeSpeed = 3f;
    public float m_chargeTurn = 14;

    private HazardDamage m_weapon;
    private BotBasic_Move m_movement;
    private Rigidbody m_rb;

    [Header("Effects")]
    public float m_scale = 2;
    public AnimationCurve m_interp;
    public AudioSource m_chargeSound;
    public AudioSource m_LaunchSound;
    public ParticleSystem m_chargingFX;
    public ParticleSystem m_LaunchFX;

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
    private bool m_chargeSoundTrigger = false;


    // wiser interface ig
    [System.NonSerialized] public string button1;
    [System.NonSerialized] public string button2;
    [System.NonSerialized] public string button3;
    [System.NonSerialized] public string button4; // currently boost in player move script


    // Start is called before the first frame update
    void Start()
    {
        // stolen and optimized from Jason Wiser's interface
        var parentScript = gameObject.transform.parent.GetComponent<playerParent>();
        button1 = parentScript.action1Input;
        button2 = parentScript.action2Input;
        button3 = parentScript.action3Input;
        button4 = parentScript.action4Input;


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
        if (Input.GetButtonDown(button1) && m_cooldowndt <= 0)
        {
            // first frame
            timeHeld = 0;
            
        }

        if (Input.GetButton(button1) && m_cooldowndt <= 0)
        {
            m_state = DuckState.Charging;
            if (m_chargeSound && m_chargeSoundTrigger == false)
            {
                m_chargeSound.Play();
                m_chargeSoundTrigger = true;
            }
            // holding charges the duck
            timeHeld = Mathf.Clamp(timeHeld += Time.deltaTime, 0, chargeTime);
            float t = timeHeld / chargeTime;
            float eval = m_interp.Evaluate(t);
            transform.localScale = Vector3.Lerp(m_originalScale, m_originalScale * m_scale, eval);
            m_movement.moveSpeed = Mathf.Lerp(m_originalSpeed, m_chargeSpeed, eval);
            m_movement.rotateSpeed = Mathf.Lerp(m_originalTurn, m_chargeTurn, eval);
            m_weapon.damage = Mathf.Lerp(m_minDamage, m_MaxDamage, eval);
            // damage is based on the charge

            if (m_chargingFX)
            {
                m_chargingFX.Play();
            }


        }
        else
        {
            // when release
            if (m_state == DuckState.Charging)
            {


                // Reset movespeed
                m_movement.moveSpeed = 0;
                m_movement.rotateSpeed = 0;
                m_cooldowndt = m_coolDown;
                float t = timeHeld / chargeTime;
                float eval = m_interp.Evaluate(t);


                Invoke("ResetBot", chargeTime * eval);
                // launch bot
                m_rb.AddForce(transform.forward * (m_launchForce * eval), ForceMode.Impulse);
                m_state = DuckState.Fire;

                // fx

                if (m_chargeSound)
                {
                    m_chargeSound.Stop();
                }
                if (m_LaunchSound)
                {
                    m_LaunchSound.Play();
                }

                if (m_chargingFX)
                {
                    m_chargingFX.Stop();
                }

                if (m_LaunchFX)
                {
                    m_LaunchFX.Play();
                }


            }
            else if (m_state == DuckState.Fire)
            {

            }
            else // this is not active
            {
                // revert scale
                m_cooldowndt = Mathf.Clamp(m_cooldowndt - Time.deltaTime, 0, m_coolDown);
                timeHeld = Mathf.Clamp(timeHeld - (Time.deltaTime * 2), 0, chargeTime);
                float t = timeHeld / chargeTime;
                float eval = m_interp.Evaluate(t);
                transform.localScale = Vector3.Lerp(m_originalScale, m_originalScale * m_scale, eval);


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
        m_chargeSoundTrigger = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if it hits something
        ResetBot();
    }
}
