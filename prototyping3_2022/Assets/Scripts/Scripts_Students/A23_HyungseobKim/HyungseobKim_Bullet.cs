using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class HyungseobKim_Bullet : MonoBehaviour
{
    private HyungseobKim_Cannon cannon;
    private Vector3 direction;

    private string button2;
    private string button3;
    private string button4;

    [HideInInspector]
    public bool moving = true;
    public float speed;

    // Gravitational Acceleration
    private static float g = 1.0f;

    private Rigidbody rigidbodyComponent;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            direction.y -= g * Time.deltaTime;

            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            rigidbodyComponent.velocity = Vector3.zero;
            rigidbodyComponent.angularVelocity = Vector3.zero;
        }
    }

    public void Initialize(HyungseobKim_Cannon Cannon, Vector3 Direction)
    {
        cannon = Cannon;
        direction = Direction.normalized;

        button2 = cannon.button2;
        button3 = cannon.button3;
        button4 = cannon.button4;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (moving == false)
        {
            return;
        }

        if (collision.transform.parent == cannon.transform.parent)
        {
            return;
        }

        moving = false;
        transform.parent = collision.transform;
    }
}
