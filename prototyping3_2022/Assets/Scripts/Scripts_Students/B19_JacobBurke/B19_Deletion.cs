using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B19_Deletion : MonoBehaviour
{
    public float Lifetime = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Lifetime >= 0)
            Lifetime -= Time.deltaTime;
        else
            Destroy(gameObject);
    }
}
