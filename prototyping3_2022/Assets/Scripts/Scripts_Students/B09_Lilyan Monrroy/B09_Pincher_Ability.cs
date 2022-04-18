using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class B09_Pincher_Ability : MonoBehaviour
{
    public GameObject pincher_left;
    public GameObject pincher_right;
    public GameObject saw;
    public GameObject player2Input; //Breakout key ->
    public GameObject player1Input; //Breakout key D

    public GameObject coolDownEffect;

    private GameObject player2Canvas;

    [SerializeField] private Animator sawBlade;
    private GameObject grabbedObject;
    private GameObject parent;
    private float sawBaseZ = 0.0f;
    private float targetZ = 0.0f;
    private float elapsedTime;
    private Transform grabbedObjecParentTransform;
    private int breakCount;
    private int maxBreakCount;
    private KeyCode breakOutKey;

    [SerializeField] private AudioClip[] clinkAudioClips;
    private AudioSource pincherAudioSource;
    public bool playPincherSFX;

    [SerializeField] private AudioClip[] pushAudioClips;
    private AudioSource pushAudioSource;

    [SerializeField] private AudioClip sawAudioClip;
    private AudioSource sawAudioSource;

    //Not able to use claw cool down
    private float cooldown;
    private float maxCooldown;

    //Button B press cool down.
    private float CooldownTime;
    private float cooldownUntilNextPress;

    [SerializeField] private Transform coolDownTransform;

    //grab axis from parent object
    private string button1;
    private string button2;
    private string button3;
    private string button4;

    // Start is called before the first frame update
    void Start()
    {
        button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
        button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
        button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;

        targetZ = Mathf.Abs(1.50f - sawBaseZ);

        maxCooldown = 3.0f;
        maxBreakCount = 5;
        CooldownTime = 0.5f;

        parent = pincher_left.transform.parent.gameObject;
        pincherAudioSource = parent.GetComponent<AudioSource>();
        sawAudioSource = saw.GetComponent<AudioSource>();

        if (gameObject.transform.parent.GetComponent<playerParent>().isPlayer1)
        {
            breakOutKey = KeyCode.RightArrow;
        }
        else
        {
            breakOutKey = KeyCode.D;
        }
    }

    // Update is called once per frame
    void Update()
    {
        B09_Pincher_Object left = pincher_left.GetComponent<B09_Pincher_Object>();
        B09_Pincher_Object right = pincher_right.GetComponent<B09_Pincher_Object>();

        if (left == null || right == null)
            return;

        if (breakCount >= maxBreakCount && cooldown > 0.0f)
        {
            if (grabbedObject)
            {
                gameObject.GetComponent<Rigidbody>().AddForce(-1 * transform.forward * 4000);
            }
            
            ResetGrabbedObject();
            
            left.open();
            right.open();



            cooldown -= Time.deltaTime;



            return;
        }
        else
        {
            if (cooldown <= 0.0f && breakCount >= maxBreakCount)
            {
                breakCount = 0;
            }
        }


        if (left.grabbedObject == null && right.grabbedObject == null)
        {
            ResetGrabbedObject();
        }

        if (grabbedObject)
        {
            if (player2Canvas == null)
            {
                //Determine which breakout key to use.
                if (breakOutKey == KeyCode.RightArrow)
                {
                    player2Canvas = Instantiate(player2Input);
                }
                else
                {
                    player2Canvas = Instantiate(player1Input);
                }

                player2Canvas.SetActive(true);
                player2Canvas.transform.SetParent(grabbedObject.transform);
            }

            player2Canvas.transform.localPosition = new Vector3(0.0f,2.0f,0.0f);
            player2Canvas.transform.rotation = Quaternion.identity;

            if (Input.GetKeyDown(breakOutKey) && cooldownUntilNextPress < Time.time)
            {
                cooldownUntilNextPress = Time.time + CooldownTime;

                ++breakCount;

                if (breakCount >= maxBreakCount)
                {
                    cooldown = maxCooldown;
                    GameObject swirlEffect = Instantiate(coolDownEffect, coolDownTransform);
                    swirlEffect.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
                    Destroy(swirlEffect,maxCooldown);

                    pushAudioSource = parent.AddComponent<AudioSource>(); ;
                    pushAudioSource.clip = pushAudioClips[Random.Range(0, 3)];
                    pushAudioSource.pitch = Random.Range(1.10f, 1.50f);
                    pushAudioSource.volume = Random.Range(0.65f, 1.00f);
                    pushAudioSource.Play();
                    Destroy(pushAudioSource,maxCooldown);
                }
            }
        }

        //Grab Mechanic
        if (Input.GetButton(button1))
        {
            ResetGrabbedObject();

            left.open();
            right.open();

        }
        else
        {
            if ((left.grabbedObject != null && left.state == B09_Pincher_Object.PincherState.Close) && (right.grabbedObject != null && right.state == B09_Pincher_Object.PincherState.Close))
            {
                sawBlade.SetBool("Spinning", true);

                if (sawAudioSource.isPlaying == false)
                {
                    sawAudioSource.clip = sawAudioClip;
                    sawAudioSource.pitch = pincherAudioSource.pitch = Random.Range(0.95f, 1.05f);
                    sawAudioSource.volume = Random.Range(0.35f, .45f);
                    sawAudioSource.loop = true;
                    sawAudioSource.Play();
                }

                if (grabbedObject == null)
                {
                    Debug.Log("Copied changed transform");
                    grabbedObject = left.grabbedObject;
                    grabbedObjecParentTransform = grabbedObject.transform.parent;
                    elapsedTime = 0;
                    sawBaseZ = saw.transform.localPosition.z;
                    playPincherSFX = true;
                }
                else
                {

                    elapsedTime += Time.deltaTime;

                    float newPositionZ = sawBaseZ + Mathf.Abs(Mathf.Sin(elapsedTime * 2.0f) * targetZ);

                    Debug.Log(newPositionZ);

                    saw.transform.localPosition = new Vector3(0, saw.transform.localPosition.y, newPositionZ);
                }

                grabbedObject.GetComponent<BotBasic_Move>().isGrabbed = true;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                grabbedObject.transform.SetParent(transform);
            }


            left.close();
            right.close();
        }

        if (playPincherSFX)
        {
            pincherAudioSource.clip = clinkAudioClips[Random.Range(0, 3)];
            pincherAudioSource.Play();
            pincherAudioSource.pitch = Random.Range(1.10f, 1.50f);
            playPincherSFX = false;
        }
    }

    private void ResetGrabbedObject()
    {
        if (grabbedObject)
        {
            grabbedObject.GetComponent<BotBasic_Move>().isGrabbed = false;
            grabbedObject.transform.SetParent(grabbedObjecParentTransform);
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObjecParentTransform = null;
            grabbedObject = null;

            saw.transform.localPosition = new Vector3(0, saw.transform.localPosition.y, sawBaseZ);

            Destroy(player2Canvas);
        }

        sawBlade.SetBool("Spinning", false);
        sawAudioSource.Stop();
    }
}
