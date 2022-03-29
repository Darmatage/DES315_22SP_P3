using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanielNunes_DisableCollider : MonoBehaviour
{
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
        //if it is actually dealing damage here
        if (GetComponent<HazardDamage>().damage > 0.0f)
        {
            Transform p = other.transform.parent;

            if (p)
            {
                p = other.transform.parent.root;
                if (p)
                {
                    //if we are player1 and hit player2, or if we're player2 and hit player1
                    if (p.CompareTag("Player2") && GetComponent<HazardDamage>().isPlayer1Weapon ||
                        p.CompareTag("Player1") && GetComponent<HazardDamage>().isPlayer2Weapon)
                    {
                        Debug.Log(other.name);
                        GetComponent<BoxCollider>().enabled = false;
                        Invoke(nameof(EnableCollider), 1.0f);
                    }
                }
            }
        }
    }

    private void EnableCollider()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
}
