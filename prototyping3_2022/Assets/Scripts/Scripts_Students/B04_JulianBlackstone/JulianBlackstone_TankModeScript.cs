using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JulianBlackstone_TankModeScript : MonoBehaviour
{
    [SerializeField]
    private GameObject leg1 = null;
    [SerializeField]
    private GameObject leg2 = null;
    [SerializeField]
    private GameObject leg3 = null;
    [SerializeField]
    private GameObject leg4 = null;    
    [SerializeField]
    private GameObject mace = null;


    //grab axis from parent object
    public string button1;
    public string button2;
    public string button3;
    public string button4; // currently boost in player move script

    void Start()
    {
        button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
        button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
        button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp(button1))
        {
            Vector3 newPos = transform.position;
            newPos.y += 2;
            transform.position = newPos;
        }

        if (Input.GetButtonDown(button1))
        {
            Vector3 newPos = transform.position;
            newPos.y -= 2;
            transform.position = newPos;
        }

        if (Input.GetButton(button1))
        {
            BotBasic_Move myMove = GetComponent<BotBasic_Move>();

            leg1.SetActive(false);
            leg2.SetActive(false);
            leg3.SetActive(false);
            leg4.SetActive(false);
            mace.SetActive(true);

            myMove.moveSpeed = 2;
            myMove.rotateSpeed = 250;

        }
        else
        {
            BotBasic_Move myMove = GetComponent<BotBasic_Move>();


            leg1.SetActive(true);
            leg2.SetActive(true);
            leg3.SetActive(true);
            leg4.SetActive(true);
            mace.SetActive(false);


            myMove.moveSpeed = 15;
            myMove.rotateSpeed = 120;
        }
    }
}
