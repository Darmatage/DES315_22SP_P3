using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JirakitJarusiripipat_Missile : MonoBehaviour
{
    [SerializeField]
    private GameObject splash;
    [HideInInspector]
    public GameObject parent;
    private JirakitJarusiripipat_SoundKeeper soundKeeper;
    private void Start()
    {

        //parent = GameObject.FindWithTag("Player1");
        soundKeeper = parent.GetComponentInChildren<JirakitJarusiripipat_SoundKeeper>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            GameObject obj = Instantiate(splash, transform.position, Quaternion.identity);
            soundKeeper.Explode();
            Destroy(gameObject);
        }
        else
        {
            soundKeeper.Explode();
            Destroy(gameObject);
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == 8)
    //    {
    //        GameObject obj = Instantiate(splash, transform.position, Quaternion.identity);
    //        soundKeeper.Explode();
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        soundKeeper.Explode();
    //        Destroy(gameObject);
    //    }
    //}
}
