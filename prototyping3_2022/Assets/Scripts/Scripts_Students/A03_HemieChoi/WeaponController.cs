using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float rotateSpeed = 1;
    public float shootPower = 5;
    public GameObject throwBall;
    public Transform shotPoint;
    private HazardDamage dmgScrt;

    public string button1;
    public string button2;
    public string button3;
    public string button4; // currently boost in player move script

    // Start is called before the first frame update
    void Start()
    {
        button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
        button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
        button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;

        dmgScrt = throwBall.GetComponent<HazardDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        float horiRot = Input.GetAxis("Horizontal");
        float verRot = Input.GetAxis("Vertical");

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, horiRot * rotateSpeed, verRot * rotateSpeed));

        if ((Input.GetButtonDown(button1)))
        {
            GameObject ball2Throw = Instantiate(throwBall, shotPoint.position, shotPoint.rotation);
            ball2Throw.GetComponent<Rigidbody>().velocity = shotPoint.transform.up * shootPower;
        }
    }
}
