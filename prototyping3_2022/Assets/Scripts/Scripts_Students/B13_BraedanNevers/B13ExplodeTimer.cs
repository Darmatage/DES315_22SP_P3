using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B13ExplodeTimer : MonoBehaviour
{

    public float ExplodeTimer;


    // Update is called once per frame
    void Update()
    {
        ExplodeTimer -= Time.deltaTime;
        if (ExplodeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
