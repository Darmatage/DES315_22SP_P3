using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JirakitJaurisiripipat_KnockBack : MonoBehaviour
{
    [SerializeField]
    private float knockBackForce = 100f;
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();

        if(rb != null)
        {
            Vector3 direction = collision.transform.position - transform.position;
            direction.y = 0;

            rb.AddForce(direction.normalized * knockBackForce, ForceMode.Impulse);

            if(rb.gameObject.transform.parent.tag == "Player1")
            {
                Camera cam = GameObject.FindWithTag("camP1").GetComponent<Camera>();

            }
            else if(rb.gameObject.transform.parent.tag == "Player2")
            {

            }
        }
    }
}
