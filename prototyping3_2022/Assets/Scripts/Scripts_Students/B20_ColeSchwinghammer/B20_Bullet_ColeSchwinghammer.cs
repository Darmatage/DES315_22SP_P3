using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B20_Bullet_ColeSchwinghammer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
