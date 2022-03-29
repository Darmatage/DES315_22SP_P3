using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A20_SnowballBehavior : MonoBehaviour
{
    private Vector3 dir;
    public float speed;
    public float gravityFactor;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
        dir.y -= gravityFactor;
    }

    public void Initialize(Vector3 position, Vector3 direction)
    {
        transform.position = position;
        dir = direction;
    }
}
