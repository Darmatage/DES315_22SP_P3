using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanielNunes_AnimationEvents : MonoBehaviour
{
    [HideInInspector]
    public bool endOfSlash;

    private BoxCollider coll;

    [SerializeField]
    private AudioSource poke;
    [SerializeField]
    public AudioSource pound;
    [SerializeField]
    private AudioSource swoosh;

    // Start is called before the first frame update
    void Start()
    {
        coll = transform.Find("Art").Find("Blade").GetComponent<BoxCollider>();
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

    public void SetBladeTrigger(int b)
    {
        if (b != 0)
        {
            coll.isTrigger = true;
        }
        else
        {
            coll.isTrigger = false;
        }
    }

    public void SetTrail(int b)
    {
        if (b != 0)
        {
            coll.GetComponent<TrailRenderer>().emitting = true;
        }
        else
        {
            coll.GetComponent<TrailRenderer>().emitting = false;
        }
    }

    public void PlayPound()
    {
        pound.Play();
    }

    public void PlayPoke()
    {
        poke.Play();
    }

    public void PlaySwoosh()
    {
        swoosh.Play();
    }
}
