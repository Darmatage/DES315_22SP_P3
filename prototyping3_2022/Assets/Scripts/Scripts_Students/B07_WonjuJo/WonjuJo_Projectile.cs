using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonjuJo_Projectile : MonoBehaviour
{
    private float ProjectileLife = 2f;
    private float ProjectileTimer = 0f;
    private float Speed = 10f;
    // Start is called before the first frame update
    
    Rigidbody rigidBody;

    public AudioClip CannonSound;
    private AudioSource AS;

    public ParticleSystem PS;
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        AS = GetComponent<AudioSource>();
        
        AS.PlayOneShot(CannonSound);
    }

    private void Update()
    {
        rigidBody.AddForce(-transform.forward * Speed);

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
