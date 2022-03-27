using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanielNunes_AnimationEvents : MonoBehaviour
{
    [HideInInspector]
    public bool endOfSlash;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAttacking(int true_false)
    {
        DanielNunes_Halberd h = transform.parent.GetComponent<DanielNunes_Halberd>();

        if (h)
        {
            if (true_false != 0)
            {
                h.isAttacking = true;
                transform.parent.GetComponent<BotBasic_Move>().moveSpeed = 5;
            }
            else
            {
                h.isAttacking = false;
                transform.parent.GetComponent<BotBasic_Move>().moveSpeed = 10;
            }
        }
    }

    public void FreezePosition(int true_false)
    {
        if (true_false != 0)
        {
            transform.parent.GetComponent<BotBasic_Move>().moveSpeed = 0;
            transform.parent.GetComponent<BotBasic_Move>().rotateSpeed = 0;
        }
        else
        {
            transform.parent.GetComponent<BotBasic_Move>().moveSpeed = 10;
            transform.parent.GetComponent<BotBasic_Move>().rotateSpeed = 100;
        }
    }

    public void SetLunging()
    {
        transform.parent.GetComponent<DanielNunes_Halberd>().lunging = true;
        transform.parent.GetComponent<BotBasic_Move>().rotateSpeed = 0;
    }

    public void RestoreRotation()
    {
        transform.parent.GetComponent<BotBasic_Move>().rotateSpeed = 100;
    }

    public void SetDamage(float damage)
    {
        transform.Find("Art").Find("Blade").GetComponent<HazardDamage>().damage = damage;
    }

    public void EndOfSlash()
    {
        endOfSlash = true;
    }
}
