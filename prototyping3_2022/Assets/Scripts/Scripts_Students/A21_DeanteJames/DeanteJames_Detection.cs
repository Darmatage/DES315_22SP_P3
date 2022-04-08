using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeanteJames_Detection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // This script figures out if the coil is within the radius of another coil
    // and if it is the radius bool does not get to false but if it leaves the 
    // On trigger exit procs and in radius is set to false
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("AttachRadius"))
        {
            DeanteJames_CoilBehavior dj = gameObject.transform.parent.GetComponent<DeanteJames_CoilBehavior>();
            dj.attachCoil(other.gameObject.transform.parent.gameObject);
            dj.setInRadius(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("AttachRadius"))
        {
            DeanteJames_CoilBehavior dj = gameObject.transform.parent.GetComponent<DeanteJames_CoilBehavior>();
            dj.detachCoil();
            dj.setInRadius(false);
        }
    }
}
