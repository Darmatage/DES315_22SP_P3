using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonjuJo_FirstWeapon : MonoBehaviour
{
    public float forceMagnitude = 2;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.rigidbody != null && collision.gameObject.tag != "Player")
        {
            collision.rigidbody.AddForce(new Vector3(20f, 0f, 0f), ForceMode.Impulse);
        }
    }
}
