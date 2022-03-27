using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScottFadoBristow_DisableTriggerOnHit : MonoBehaviour
{
    public float Timeout = 0.25f;
    private int count = 0;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = Timeout;
    }

    // Update is called once per frame
    void Update()
    {
       

        if(timer <= 0)
        {
            DisableDamage();
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (count > 0)
        {
            DisableDamage();
        }
        
        count++;
    }


    private void DisableDamage()
    {
        gameObject.GetComponent<HazardDamage>().damage = 0;
    }
}
