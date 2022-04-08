using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A20_SnowTurretBehavior : MonoBehaviour
{
    private GameObject snowballPrefab;
    private float rotationSpeed;
    private float turretCooldown;
    private bool turretOn = false;
    private Rigidbody rb;
    public Transform snowballSpawn;
    private float initialSpeed = 10.0f;
    private bool spawning = false;

    A20_Snowball snowman;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.sqrMagnitude > 0.1f)
        {
            transform.localScale = transform.localScale * 1.0002f;
        }
        else if (!spawning)
        {
            StartCoroutine(SpawnSnowballs());
            spawning = true;
        }
    }

    public void Initialize(GameObject snowballPrefab_, Vector3 position, Vector3 forward, A20_Snowball snowman_)
    {
        snowballPrefab = snowballPrefab_;
        transform.position = position;
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.velocity = forward * initialSpeed;
        snowman = snowman_;
    }

    private IEnumerator SpawnSnowballs()
    {
        while (transform.localScale.x > 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,
             transform.eulerAngles.y + 22.5f,
              transform.eulerAngles.z);
            
            A20_SnowballBehavior snowball = Instantiate(snowballPrefab).GetComponent<A20_SnowballBehavior>();
            snowball.Initialize(snowballSpawn.position, transform.forward);

            transform.localScale -= Vector3.one * 0.075f;

            yield return new WaitForSeconds(0.15f);
        }
        snowman.ResetBody();
        Destroy(gameObject);
    }
}
