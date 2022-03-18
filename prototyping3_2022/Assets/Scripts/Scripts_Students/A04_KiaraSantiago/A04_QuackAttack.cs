using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A04_QuackAttack : MonoBehaviour
{

    public string button;
    public float cooldown = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(button))
        {

        }
    }
}
