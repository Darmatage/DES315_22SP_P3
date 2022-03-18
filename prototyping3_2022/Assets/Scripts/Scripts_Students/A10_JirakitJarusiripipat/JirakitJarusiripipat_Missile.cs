using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JirakitJarusiripipat_Missile : MonoBehaviour
{
    [SerializeField]
    private GameObject splash;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            GameObject obj = Instantiate(splash,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
