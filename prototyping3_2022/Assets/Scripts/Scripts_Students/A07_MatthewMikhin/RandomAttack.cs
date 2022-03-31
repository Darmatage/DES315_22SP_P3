using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform d20;
    [SerializeField] HazardDamage melee;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int rand = Random.Range(0, 21);
            melee.damage = 0.1f  *  rand;
        }
    }
}
