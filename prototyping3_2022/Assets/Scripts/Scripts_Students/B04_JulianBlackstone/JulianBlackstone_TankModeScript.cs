using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JulianBlackstone_TankModeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            BotBasic_Move myMove = GetComponent<BotBasic_Move>();

            myMove.moveSpeed = 2;
            myMove.rotateSpeed = 250;

        }
        else
        {
            BotBasic_Move myMove = GetComponent<BotBasic_Move>();

            myMove.moveSpeed = 15;
            myMove.rotateSpeed = 120;
        }
    }
}
