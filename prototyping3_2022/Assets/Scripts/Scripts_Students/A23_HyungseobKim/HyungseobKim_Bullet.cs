using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class HyungseobKim_Bullet : MonoBehaviour
{
    private HyungseobKim_Cannon cannon;
    private Vector3 direction;
    private Rigidbody enemy;

    private string button2;
    private string button3;
    private string button4;

    [HideInInspector]
    public bool moving = true;
    public float speed;

    // Gravitational Acceleration
    private static float g = 1.0f;

    private Rigidbody rigidbodyComponent;

    // Weapon 2.
    public float explosionRange;
    public float explosionSpeed;

    // Weapon 3.
    public float pullRange;
    public float pullSpeed;

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

        if (Input.GetButtonDown(button2))
        {
            // If the enemy exists.
            if (enemy != null)
            {
                // If the enemy is in range.
                Vector3 toEnemy = enemy.position - transform.position;

                // Push the enemy.
                if (toEnemy.sqrMagnitude < explosionRange)
                {
                    enemy.AddForce(toEnemy.normalized * explosionSpeed, ForceMode.Impulse);
                }
            }

            DestroyBullet();
            return;
        }

        if (Input.GetButtonDown(button3))
        {
            // If the enemy exists.
            if (enemy != null)
            {
                // If the enemy is in range.
                Vector3 toEnemy = transform.position - enemy.position;

                // Push the enemy.
                if (toEnemy.sqrMagnitude < pullRange)
                {
                    enemy.AddForce(toEnemy.normalized * pullSpeed, ForceMode.Impulse);
                }
            }

            DestroyBullet();
            return;
        }

        if (Input.GetButtonDown(button4))
        {
            cannon.transform.parent.transform.position = transform.position;
            DestroyBullet();
            return;
        }
    }

    public void Initialize(HyungseobKim_Cannon Cannon, Vector3 Direction)
    {
        cannon = Cannon;
        direction = Direction.normalized;

        button2 = cannon.button2;
        button3 = cannon.button3;
        button4 = cannon.button4;

        if (Cannon.enemy != null)
        {
            enemy = Cannon.enemy.GetComponent<Rigidbody>();
        }
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
