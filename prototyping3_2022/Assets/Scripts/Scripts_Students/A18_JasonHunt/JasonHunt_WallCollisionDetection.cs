using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JasonHunt_WallCollisionDetection : MonoBehaviour
{
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
        if (collision.gameObject.name.Contains("CubeWall"))
        {
            gameObject.GetComponentInParent<JasonHunt_Projectile>().StopProjectile();
        }
    }
}
