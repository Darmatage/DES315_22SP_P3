using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScottFadoBristow_SlamWeapon : MonoBehaviour
{
    public GameObject slamTrigger;

    public float AttackCooldown = 2.0f;

    public Vector3 HitboxOffset;

    bool canAttack = true;

    public string button1;

    private float timer;

    private Animator aniPlayer;


    // Start is called before the first frame update
    void Start()
    {
        button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        aniPlayer = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown(button1)) && (canAttack == true))
        {
            aniPlayer.Play("ScottFadoBristow_Crab_Slam");

            //SPawn Hitbox now called Via animation event
            //SpawnHitbox();

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


    void SpawnHitbox()
    {

        GameObject s = Instantiate(slamTrigger, transform);

        Vector3 nt = s.transform.localPosition;
        nt += HitboxOffset;
        s.transform.localPosition = nt;

    }

    
}
