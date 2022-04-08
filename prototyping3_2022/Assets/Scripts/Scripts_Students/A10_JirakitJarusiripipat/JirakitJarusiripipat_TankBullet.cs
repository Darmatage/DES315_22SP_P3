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
        bulletEffect.gameObject.SetActive(true);
        bulletEffect.Play();
        Destroy(gameObject);

    }
}
