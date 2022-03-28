using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B05_BotSideWeapons : MonoBehaviour
{


    public bool Button2 = true;
    public GameObject WeaponAnchor = null;
    public GameObject WeaponPrefab = null;
    public float MaxAngle = 75.0f;
    public float AttackTime = 0.2f;
    public float CooldownTime = 0.3f;


    string button2;
    string button3;
    bool attacking = false;
    bool coolingDown = false;
    float timer = 0.0f;
    GameObject currWeapon = null;

    // Start is called before the first frame update
    void Start()
    {
        button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
        button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
    }

    // Update is called once per frame
    void Update()
    {
        if(((Input.GetButtonDown(button2) && Button2) || (Input.GetButtonDown(button3) && !Button2)) && !attacking && !coolingDown)
        {
            attacking = true;
            timer = 0.0f;
            currWeapon = Instantiate(WeaponPrefab, WeaponAnchor.transform);
            currWeapon.transform.localPosition = Vector3.right * currWeapon.transform.localScale.x / 2.0f;
            Vector3 angle = WeaponAnchor.transform.localEulerAngles;
            angle.y = -MaxAngle;
            WeaponAnchor.transform.localEulerAngles = angle;
        }

        if(attacking)
        {
            timer += Time.deltaTime;

            Vector3 angle = WeaponAnchor.transform.localEulerAngles;
            angle.y = Mathf.Lerp(-MaxAngle, MaxAngle, timer / AttackTime);
            WeaponAnchor.transform.localEulerAngles = angle;

            if (timer > AttackTime)
            {
                Destroy(currWeapon);
                coolingDown = true;
                attacking = false;
                timer = CooldownTime;
            }
        }

        if(coolingDown)
        {
            timer -= Time.deltaTime;
            if(timer <= 0.0f)
            {
                coolingDown = false;
            }
        }
    }
}
