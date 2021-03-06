using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B05_BotFrontWeapon : MonoBehaviour
{
    public GameObject WeaponParent = null;
    public GameObject RightWing = null;
    public GameObject LeftWing = null;
    public ParticleSystem ChargeSystem = null;
    public AudioClip ChargeSound = null;
    public float DashSpeed = 10.0f;
    public float Cooldown = 3.5f;
    public float WeaponActiveTime = 0.5f;

    string button1;
    Rigidbody rb = null;
    AudioSource source = null;
    float timer = 0.0f;
    float weaponActiveTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
        button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        if(ChargeSystem != null)
        {
            ParticleSystem.EmissionModule module = ChargeSystem.emission;
            module.rateOverDistanceMultiplier = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        weaponActiveTimer -= Time.deltaTime;

        if(Input.GetButtonDown(button1) && timer <= 0.0f)
        {
            if(WeaponParent != null)
            {
                WeaponParent.SetActive(true);
            }
            rb.AddForce(transform.forward * DashSpeed, ForceMode.Impulse);
            timer = Cooldown;
            weaponActiveTimer = WeaponActiveTime;
            if (ChargeSystem != null)
            {
                ParticleSystem.EmissionModule module = ChargeSystem.emission;
                module.rateOverDistanceMultiplier = 10;
            }
            if(source != null && ChargeSound != null)
            {
                source.PlayOneShot(ChargeSound);
            }
        }

        if(WeaponParent != null && weaponActiveTimer <= 0.0f)
        {
            WeaponParent.SetActive(false);
            if (ChargeSystem != null)
            {
                ParticleSystem.EmissionModule module = ChargeSystem.emission;
                module.rateOverDistanceMultiplier = 0;
            }
        }
    }
}
