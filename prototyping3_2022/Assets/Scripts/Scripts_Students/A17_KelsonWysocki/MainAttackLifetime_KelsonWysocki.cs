using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAttackLifetime_KelsonWysocki : MonoBehaviour
{
    [HideInInspector]
    public Transform parent;
    [HideInInspector]
    public Vector3 startPos;

    public float velocity;
    public float range;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = (parent.forward * velocity);
        timer += Time.deltaTime;

        if (timer >= range / velocity)
            Destroy(gameObject);
    }
}
