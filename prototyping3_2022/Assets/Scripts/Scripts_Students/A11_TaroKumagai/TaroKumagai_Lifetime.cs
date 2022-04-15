using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaroKumagai_Lifetime : MonoBehaviour
{
    public  float LifeTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (LifeTime > 0.0f)
            StartCoroutine(ExpireIn(LifeTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ExpireIn(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);

        Destroy(gameObject);
    }
}
