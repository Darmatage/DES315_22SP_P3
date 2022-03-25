using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenThompson_FirebreathBehavior : MonoBehaviour
{
    private bool player1Weapon = true;
    private int hitCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayer2()
    {
        player1Weapon = false;
    }

    public void ResetHitCount()
    {
        hitCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hitCount > 0)
        {
            this.GetComponent<HazardDamage>().damage = 0;
        }

        // Prevent multiple collisions
        if (other.gameObject.transform.root.tag == "Player1" && !player1Weapon || other.gameObject.transform.root.tag == "Player2" && player1Weapon)
        {
            hitCount++;
        }
    }
}
