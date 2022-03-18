using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenThompson_ExplosionBehavior : MonoBehaviour
{
    [SerializeField]
    private float explosiveFade;

    [SerializeField]
    HazardDamage hazard;

    private bool whichPlayer = false; // Player 1

    // Start is called before the first frame update
    void Start()
    {
        if(whichPlayer)
        {
            hazard.isPlayer2Weapon = true;
        }
        else
        {
            hazard.isPlayer1Weapon = true;
        }

        Destroy(gameObject, explosiveFade);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IsPlayer2()
    {
        whichPlayer = true;
    }
}
