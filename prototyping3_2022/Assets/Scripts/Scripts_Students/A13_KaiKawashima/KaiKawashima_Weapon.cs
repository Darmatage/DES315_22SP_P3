using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaiKawashima_Weapon : MonoBehaviour
{
    public KaiKawashima_PartsManager partsManager;
    public float cooldown1 = 1.0f;
    private float timer1 = 1.0f;
    private string button1;
    public KaiKawashima_PartShoot partShoot;
    public float cooldown2 = 1.0f;
    private float timer2 = 1.0f;
    private string button2;

    // Start is called before the first frame update
    void Start()
    {
        button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
    }

    // Update is called once per frame
    void Update()
    {
        timer1 -= Time.deltaTime;

        if (Input.GetButtonDown(button1) && timer1 < 0.0f)
        {
            timer1 = cooldown1;
            partsManager.CollectAllParts();
        }

        timer2 -= Time.deltaTime;

        if (Input.GetButtonDown(button2) && timer2 < 0.0f)
        {
            timer2 = cooldown2;
            partShoot.Shoot();
        }
    }
}
