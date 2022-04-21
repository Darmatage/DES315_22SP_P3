using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JasonHunt_Projectile : MonoBehaviour
{

    public Vector3 direction;
    public int projectileSpeed;
    public bool stuck = false;
    public GameObject Home;
    public bool returning = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!stuck)
        {
            gameObject.transform.position += (direction * projectileSpeed * Time.deltaTime);
        }
        else if (returning)
        {
            //get normalized vector from projectile to home (player object)
            Vector3 dir = Home.transform.position - gameObject.transform.position;
            dir.Normalize();

            //send objects back to the player with a 30% speed increase
            gameObject.transform.position += (dir * projectileSpeed * 1.3f * Time.deltaTime);
            GetComponent<JasonHunt_SelfDestruct>().enabled = true;
        }    
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.transform.root.tag == "Player1" || collision.gameObject.transform.root.tag == "Player2")
        {
            Destroy(this.gameObject);
        }
    }

    public void StopProjectile()
    {
        stuck = true;
    }


}
