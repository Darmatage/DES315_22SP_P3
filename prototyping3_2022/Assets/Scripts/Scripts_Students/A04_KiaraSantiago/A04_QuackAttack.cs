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


    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.transform.parent.GetComponent<playerParent>().action2Input;

        anim = gameObject.GetComponentInChildren<Animator>();
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


            anim.SetTrigger("Quack");
            //throw back enemy
            /*Transform theirTransform = enemy.GetComponent<Transform>();

            Vector3 theirOGPosition = theirTransform.position;

            theirOGPosition += Vector3.back * 3.0f;

            theirTransform.SetPositionAndRotation(theirOGPosition, Quaternion.identity);*/

        }
    }
}
