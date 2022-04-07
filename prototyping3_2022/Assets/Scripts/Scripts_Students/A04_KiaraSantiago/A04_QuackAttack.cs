using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A04_QuackAttack : MonoBehaviour
{
    private Animator anim;
    public string button;
    
    public float cooldown = 5.0f;

    private bool isReadytoAttack = true;
    private float cdTimer;

    public AudioSource quack;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.transform.parent.GetComponent<playerParent>().action2Input;

        anim = gameObject.GetComponentInChildren<Animator>();

        if (gameObject.transform.root.tag == "Player1") 
        {
            //we are player one - get player two
            target = GameObject.FindWithTag("Player2").transform.GetChild(0).gameObject;
            Debug.Log("A04 -got player 2 enemy");
        }
        else if (gameObject.transform.root.tag == "Player2") 
        {
            //we are player two - get player one
            target = GameObject.FindWithTag("Player1").transform.GetChild(0).gameObject;
            Debug.Log("A04 -got player 1 enemy");
        }
    }


    // Update is called once per frame
    void Update()
    {

        if(!isReadytoAttack)
        {
            cdTimer -= Time.deltaTime;

            if(cdTimer <= 0)
            {
                isReadytoAttack = true;
            }
        }


        if(Input.GetButtonDown(button) && isReadytoAttack)
        {
            isReadytoAttack = false;
            cdTimer = cooldown;
            //play sound
            quack.PlayOneShot(quack.clip);

            anim.SetTrigger("Quack");
            //throw back enemy
            //Transform theirTransform = target.GetComponent<Transform>();
            Rigidbody theirs = target.GetComponent<Rigidbody>();

            //theirs.AddRelativeForce(Vector3.back * 3.0f);
            theirs.AddForce(Vector3.back * 30.0f, ForceMode.VelocityChange);

            //Vector3 theirOGPosition = theirTransform.position;

           // theirOGPosition += Vector3.back * 3.0f;

            //theirTransform.SetPositionAndRotation(theirOGPosition, Quaternion.identity);

        }
    }
}
