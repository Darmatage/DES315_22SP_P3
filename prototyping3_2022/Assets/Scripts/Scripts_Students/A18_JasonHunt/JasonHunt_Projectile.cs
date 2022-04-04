using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JasonHunt_Projectile : MonoBehaviour
{

    public Vector3 direction;
    public int projectileSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += (direction * projectileSpeed* Time.deltaTime);
    }
}
