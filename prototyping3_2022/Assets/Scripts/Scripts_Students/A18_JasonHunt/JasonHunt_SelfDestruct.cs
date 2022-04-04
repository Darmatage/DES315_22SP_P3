using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JasonHunt_SelfDestruct : MonoBehaviour
{

    public float timeToDeath;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        timeToDeath -= Time.deltaTime;
        if (timeToDeath <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
