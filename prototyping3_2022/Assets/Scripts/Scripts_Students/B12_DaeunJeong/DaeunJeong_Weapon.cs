using System.Collections;
using UnityEngine;

public class DaeunJeong_Weapon : MonoBehaviour
{
    //NOTE: This script goes on the main playerBot Game Object, and the weapon goes in the public GO slot

    public GameObject weaponThrust;
    private float thrustAmount = 5f;

    private bool weaponOut = false;

    //grab axis from parent object
    public string button1;
    public string button2;
    public string button3;
    public string button4; // currently boost in player move script

    private Rigidbody rb;
    private AudioSource audioSource;
    public AudioClip weaponSFX;
    public AudioClip boostSFX;
    public float boostSpeed = 0.0f;
    public GameObject fireVFX;

    public float WithdrawWeaponTimer = 2.0f;
    public float BoostTimer = 1.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
        button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
        button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;
    }

    void Update()
    {
        // Weapon
        if ((Input.GetButtonDown(button1)) && (weaponOut == false))
        {
            weaponThrust.transform.Translate(0, 0, thrustAmount);
            weaponOut = true;
            StartCoroutine(WithdrawWeapon());
        }

        // BOOST
        if (Input.GetButtonDown(button2))
        {
            StartCoroutine(Boost());
        }
    }

    IEnumerator WithdrawWeapon()
    {
        audioSource.PlayOneShot(weaponSFX);
        yield return new WaitForSeconds(WithdrawWeaponTimer);
        weaponThrust.transform.Translate(0, 0, -thrustAmount);
        weaponOut = false;
    }

    IEnumerator Boost()
    {
        fireVFX.SetActive(true);
        yield return new WaitForSeconds(BoostTimer);
        rb.AddForce(transform.forward * boostSpeed, ForceMode.Impulse);
        audioSource.PlayOneShot(boostSFX);
        fireVFX.SetActive(false);
    }
}