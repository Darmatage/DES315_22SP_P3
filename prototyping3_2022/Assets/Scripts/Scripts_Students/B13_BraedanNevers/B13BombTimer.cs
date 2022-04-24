using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B13BombTimer : MonoBehaviour
{
    // Start is called before the first frame update
    public float explodeTimer;
    public GameObject Explosion;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        explodeTimer -= Time.deltaTime;
        if(explodeTimer <= 0)
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
