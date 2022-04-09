using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScottFadoBristow_ExplodeOnCollide : MonoBehaviour
{
    public GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(Explosion)
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
