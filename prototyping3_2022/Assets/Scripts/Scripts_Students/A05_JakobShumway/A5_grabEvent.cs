using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A5_grabEvent : MonoBehaviour
{
    public AudioSource stabSfx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void grabBoolTrue()
    {
        GetComponentInParent<A5_stingAttack>().isGrabbing = true;
    }

    public void grabBoolFalse()
    {
        GetComponentInParent<A5_stingAttack>().isGrabbing = false;
    }

    public void clawBoolTrue()
    {
        GetComponentInParent<A5_stingAttack>().isClawing = true;
        GetComponentInChildren<HazardDamage>().damage = 1;
    }

    public void clawBoolFalse()
    {
        GetComponentInParent<A5_stingAttack>().isClawing = false;
        GetComponentInChildren<HazardDamage>().damage = 0;
    }

    public void playStabSfx()
    {
        stabSfx.pitch = Random.Range(.5f, 1.5f);
        stabSfx.Play();
    }
}
