using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JirakitJarusiripipat_SuicideBot : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public Transform target;
    private Rigidbody rb;
    private bool stop = false;
    private bool oneTime = false;
    [SerializeField]
    private GameObject BlastExplosion;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!stop && !oneTime)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            rb.MovePosition(pos);
            transform.LookAt(target);
        }
        else if(stop && !oneTime)
        {
            oneTime = true;
            StartCoroutine(Boom());
        }
    }
    IEnumerator Boom()
    {
        yield return new WaitForSeconds(2.0f);
        Instantiate(BlastExplosion, transform.position, Quaternion.identity);
        Debug.Log("Boommmmmmmmmmmmmmmm!");
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == target.GetComponent<BotBasic_Damage>().gameObject)
        {
            stop = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hazard")
        {
            Destroy(gameObject);
        }
        //else if(other.gameObject.tag == target.gameObject.tag)
        //{
        //    stop = true;
        //}
    }
}
