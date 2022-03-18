using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JirakitJarusiripipat_DestroyByTime : MonoBehaviour
{
    public float timeToDestroy = 0.0f;
    private void Start()
    {
        
    }
    private void Update()
    {
        if(timeToDestroy <= 0.0f)
        {
            Destroy(gameObject);
        }
        else
        {
            timeToDestroy -= Time.deltaTime;
        }
    }
}
