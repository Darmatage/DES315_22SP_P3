using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScottFadoBristow_DelayedHoming : MonoBehaviour
{
    public float Delay = 1.0f;
    public float LaunchSpeed = 2.0f;
    public float HomingSpeed = 2.0f;

    float timer_ = 0;

    GameObject target;

    private Rigidbody physics;
    // Start is called before the first frame update
    void Start()
    {
        timer_ = Delay;

        physics = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        timer_ -= Time.deltaTime;
        if(timer_ <= 0)
        {
            Vector3 TargetPos = new Vector3(0 , 0, 0);
            if (target)
                TargetPos = target.transform.position;

            Vector3 diff = TargetPos - gameObject.transform.position;

            Vector3 ndiff = diff.normalized;

            physics.velocity = ndiff * HomingSpeed;
        }
    }

    public void Launch(GameObject t = null)
    {
        //Due to ordering, this might be called before Start()
        if (!physics)
        {
            physics = gameObject.GetComponent<Rigidbody>();
        }
        physics.velocity = new Vector3(0, LaunchSpeed, 0);

        target = t;

    }
}
