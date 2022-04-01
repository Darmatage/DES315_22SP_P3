using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class B03_ProjectileMovement : MonoBehaviour
{
    [SerializeField] float lifeSpan;
    [SerializeField] float projectileSpeed;

    new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddForce(transform.forward * projectileSpeed);
        Destroy(gameObject, lifeSpan);
    }
}
