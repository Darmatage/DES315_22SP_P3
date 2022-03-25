using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScottFadoBristow_SlamWeapon : MonoBehaviour
{
    public GameObject slamTrigger;

    public float AttackCooldown = 2.0f;

    bool canAttack = true;

    public string button1;

    private float timer;

    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown(button1)) && (canAttack == true))
        {
            GameObject s = Instantiate(slamTrigger, transform);

            Vector3 nt = s.transform.localPosition;
            nt.x += 4.0f;
            s.transform.localPosition = nt;

            canAttack = false;
            timer = AttackCooldown;
        }

        if (canAttack == false)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                canAttack = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(count > 0)
        {
            this.GetComponent<HazardDamage>().damage = 0;
        }

        count++;
    }
}
