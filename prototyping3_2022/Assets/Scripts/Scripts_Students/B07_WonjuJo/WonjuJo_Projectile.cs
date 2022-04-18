using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonjuJo_Projectile : MonoBehaviour
{
    private float ProjectileLife = 3f;
    private float ProjectileTimer = 0f;
    float Speed = 15f;
    // Start is called before the first frame update

    Rigidbody rigidBody;

    private AudioSource AS;
    public AudioClip CannonSound;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        AS = GetComponent<AudioSource>();

        AS.PlayOneShot(CannonSound);
    }

    private void Update()
    {
        rigidBody.AddForce(transform.forward * Speed);
 
        ProjectileTimer += Time.deltaTime;
        if (ProjectileTimer >= ProjectileLife)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

}
