using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceRadius : MonoBehaviour
{
    // Start is called before the first frame update
    public bool working = false;
    public float timer = 0.3f;
    private void OnTriggerStay(Collider other)
    {
        if (!working)
        {
            return;
        }
        if (other.gameObject != transform.parent.gameObject)
        {
            if(other.GetComponent<Rigidbody>() == null)
            {
                return;
            }
            other.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(other.transform.position - transform.position) * 1000);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            working = false;
            return;
        }

        timer -= Time.deltaTime;
    }
}
