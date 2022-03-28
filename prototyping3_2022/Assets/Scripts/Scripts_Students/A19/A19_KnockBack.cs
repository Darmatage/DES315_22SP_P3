using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A19_KnockBack : MonoBehaviour
{
    public float knockBackForce;
     private void OnTriggerEnter(Collider other)
    {
        var rb = other.GetComponent<Rigidbody>();
        if(rb)
        {
            var direction =  other.transform.position - gameObject.transform.position;
            rb.AddForce(direction * knockBackForce);
        }
        
	}
}
