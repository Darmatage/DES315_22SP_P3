using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A5_stingAniEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void a5_testStuff()
    {
        print("test");
    }

    public void stingLaunch()
    {
        Rigidbody launchMe = GetComponentInParent<Rigidbody>();
        launchMe.AddForce(new Vector3(0, 20, 0), ForceMode.Impulse);
        launchMe.AddRelativeTorque(new Vector3(64, 0, 0), ForceMode.Impulse);
    }
}
