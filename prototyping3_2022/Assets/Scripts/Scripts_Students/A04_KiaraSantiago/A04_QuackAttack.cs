using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A04_QuackAttack : MonoBehaviour
{

    public string button;
    public float cooldown = 5.0f;
    public GameObject enemy;

    private bool isReadytoAttack = true;
    private float cdTimer;

    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
        
        //decide if we are player one or two
        if(gameObject.CompareTag("Player1")) //we are player 1
        {
            if(GameObject.FindWithTag("Player2"))
            {
                enemy = GameObject.FindWithTag("Player2");
            }
        }
        else if(gameObject.CompareTag("Player2")) //we are player 2
        {
            if (GameObject.FindWithTag("Player1"))
            {
                enemy = GameObject.FindWithTag("Player1");
            }
        }
        else
        {
            Debug.Log("A04 Quack Attack - could not find enemy");
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

            //throw back enemy
            Transform theirTransform = enemy.GetComponent<Transform>();

            Vector3 theirOGPosition = theirTransform.position;

            theirOGPosition += Vector3.back * 3.0f;

            theirTransform.SetPositionAndRotation(theirOGPosition, Quaternion.identity);

        }
    }
}
