using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueWeapon : MonoBehaviour
{
    public GameObject weaponThrust;
    private float thrustAmount = 5f;
	
    private bool weaponOut = false;

    //grab axis from parent object
    public string button1;

    void Start(){
        button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
    }

    void Update(){
        if ((Input.GetButton(button1))&&(weaponOut==false)){
            weaponThrust.transform.Translate(0,thrustAmount, 0);
            weaponOut = true;
        }
        else
        {
            weaponThrust.transform.Translate(0,-thrustAmount, 0);
            weaponOut = false;
        }
    }
}
