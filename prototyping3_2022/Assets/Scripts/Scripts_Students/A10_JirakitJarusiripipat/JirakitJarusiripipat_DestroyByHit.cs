using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JirakitJarusiripipat_DestroyByHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        Destroy(gameObject);
    }
}
