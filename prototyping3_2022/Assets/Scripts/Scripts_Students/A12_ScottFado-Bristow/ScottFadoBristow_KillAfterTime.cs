using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScottFadoBristow_KillAfterTime : MonoBehaviour
{

    public float KillTime = 1.0f;

    private float timer_;

    // Start is called before the first frame update
    void Start()
    {
        timer_ = KillTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer_ -= Time.deltaTime;
        if(timer_ <= 0)
        {
            Destroy(gameObject);
        }
    }
}
