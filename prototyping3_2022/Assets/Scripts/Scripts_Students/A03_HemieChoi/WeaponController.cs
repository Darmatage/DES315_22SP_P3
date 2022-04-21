using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float rotateSpeed = 1;
    public float shootPower = 5;
    public GameObject throwBall;
    public GameObject display1;
    public GameObject display2;
    public GameObject display3;
    public GameObject display4;
    public GameObject display5;
    public Transform shotPoint;
	public Transform Orca;
    private HazardDamage dmgScrt;
    private bool isDuckAvail = true;
    public int duckOutCount = 0;

    public string button1;
    public string button2;
    public string button3;
    public string button4;

    private float holdDtB2 = 0.0f;
    private float holdDtB3 = 0.0f;

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
        //float horiRot = Input.GetAxis("Horizontal");
        //float verRot = Input.GetAxis("Vertical");

        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, horiRot * rotateSpeed, verRot * rotateSpeed));

        isDuckAvail = duckOutCount == 5 ? false : true;

        switch (duckOutCount)
        {
            case 0:
                display1.SetActive(true);
                display2.SetActive(true);
                display3.SetActive(true);
                display4.SetActive(true);
                display5.SetActive(true);
                break;
            case 1:
                display1.SetActive(false);
                display2.SetActive(true);
                display3.SetActive(true);
                display4.SetActive(true);
                display5.SetActive(true);
                break;
            case 2:
                display1.SetActive(false);
                display2.SetActive(false);
                display3.SetActive(true);
                display4.SetActive(true);
                display5.SetActive(true);
                break;
            case 3:
                display1.SetActive(false);
                display2.SetActive(false);
                display3.SetActive(false);
                display4.SetActive(true);
                display5.SetActive(true);
                break;
            case 4:
                display1.SetActive(false);
                display2.SetActive(false);
                display3.SetActive(false);
                display4.SetActive(false);
                display5.SetActive(true);
                break;
            case 5:
                display1.SetActive(false);
                display2.SetActive(false);
                display3.SetActive(false);
                display4.SetActive(false);
                display5.SetActive(false);
                break;
        }
 

        if ((Input.GetButtonDown(button1)) && isDuckAvail)
        {
            GameObject ball2Throw = Instantiate(throwBall, shotPoint.position, shotPoint.rotation);
            ball2Throw.GetComponent<Rigidbody>().velocity = shotPoint.transform.forward * shootPower;
            //showCount[duckOutCount].SetActive(false);
            display1.SetActive(false);
            ++duckOutCount;
        }
		
		if ((Input.GetButton(button2)))
        {
            //Orca.rotation = Quaternion.Euler(Orca.rotation.eulerAngles + new Vector3(0, rotateSpeed, 0));
            //Orca.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
            if (transform.localScale.magnitude < 1.5)
            {
                transform.localScale += transform.localScale * 0.35f * Time.deltaTime;
                shootPower = 5;
            }
        }
		
		if ((Input.GetButton(button3)))
        {
            //Orca.rotation = Quaternion.Euler(Orca.rotation.eulerAngles + new Vector3(0, rotateSpeed * -1, 0));
            //Orca.transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
            if (transform.localScale.magnitude > 1.0)
                transform.localScale -= transform.localScale * 0.35f * Time.deltaTime;

            holdDtB3 += Time.deltaTime;
            if (holdDtB3 > 0.5f && shootPower > 1)
            {
                --shootPower;
                holdDtB3 = 0.0f;
            }
        }

        //if (Orca.rotation.y < -90.0f)
        //Orca.rotation = Quaternion.Euler(Orca.rotation.eulerAngles.x, -90.0f, Orca.rotation.eulerAngles.z);
        //if (Orca.rotation.y > 90.0f)
        //Orca.rotation = Quaternion.Euler(Orca.rotation.eulerAngles.x, 90.0f, Orca.rotation.eulerAngles.z);

    }

    public void showDuck()
    {
        //showCount[duckOutCount].SetActive(true);
    }
}
