using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScottFadoBristow_DisableTriggerOnHit : MonoBehaviour
{

    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (count > 0)
        {
            gameObject.GetComponent<HazardDamage>().damage = 0;
        }

        count++;
    }
}
