using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A20_SnowTurretBehavior : MonoBehaviour
{
    private GameObject snowballPrefab;
    private float rotationSpeed;
    private float turretCooldown;
    private bool turretOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        if (turretOn == false)
        {
            StartCoroutine(SpawnSnowball());
            turretOn = true;
        }
    }

    public void Initialize(GameObject snowballPrefab_, float rotationSpeed_, float turretCooldown_, Vector3 position)
    {
        snowballPrefab = snowballPrefab_;
        rotationSpeed = rotationSpeed_;
        turretCooldown = turretCooldown_;
        transform.position = position;
    }

    private IEnumerator SpawnSnowball()
    {
        while (true)
        {
            A20_SnowballBehavior snowball = Instantiate(snowballPrefab).GetComponent<A20_SnowballBehavior>();
            snowball.Initialize(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z),
             transform.forward);
            yield return new WaitForSeconds(turretCooldown);
            Destroy(snowball.gameObject);
        }
    }
}
