using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JirakitJarusiripipat_TankBullet : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem bulletEffect;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Hit");

        //if (collision.gameObject.transform.parent.tag == "Player1")
        //{
        //    //collision.gameObject.GetComponent<BotBasic_Damage>();
        //}
        //else if (collision.gameObject.transform.parent.tag == "Player2")
        //{

        //}
        //bulletEffect.Stop();
        //Destroy(bulletEffect.gameObject);
        Destroy(gameObject);

    }
}
