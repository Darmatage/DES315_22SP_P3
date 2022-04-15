using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B08_SwingAttack : MonoBehaviour
{
    public float WindUp = 2.0f;
    private float CurrentWindUp = 0.0f;
    public float SwingVelocity = 25.0f;
    public float StartingRotation = 0.0f;

    public Transform Weapon;
    public Rigidbody WeaponBody;

    private GameObject Parent;

    // Start is called before the first frame update
    void Start()
    {
        if (!Weapon)
            Debug.Log("Weapon was not properly set");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
