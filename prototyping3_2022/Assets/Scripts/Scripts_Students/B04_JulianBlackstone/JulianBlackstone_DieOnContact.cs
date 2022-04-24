using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JulianBlackstone_DieOnContact : MonoBehaviour
{
    float deathTimer = 0.0f;
    bool shouldKill = false;

    private void Update()
    {
        if (deathTimer > 0)
        {
            deathTimer -= Time.deltaTime;
            return;
        }


        if (shouldKill == true)
        {
            GameObject.Destroy(this.gameObject);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        deathTimer = 0.3f;
        shouldKill = true;
    }
}
