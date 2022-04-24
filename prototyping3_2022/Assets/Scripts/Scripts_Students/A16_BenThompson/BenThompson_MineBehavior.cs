using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenThompson_MineBehavior : MonoBehaviour
{
    [SerializeField]
    private float explosionTime;

    private bool collided = false;

    [SerializeField]
    GameObject explosionPrefab;

    public bool isPlayer2weapon = false;

    [SerializeField]
    private float defaultTimeToExplode;

    private float countdown;

    // Start is called before the first frame update
    void Start()
    {
        countdown = defaultTimeToExplode;
    }

    // Update is called once per frame
    void Update()
    {
        if(countdown > 0.0f)
        {
            countdown -= Time.deltaTime;
        }
        else if(countdown <= 0.0f && collided == false)
        {
            StartCoroutine(Explode());
            collided = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collided == true)
            return;

        if (other.transform.parent == null)
            return;

        if (other.transform.parent.tag != "Player1" && other.transform.parent.tag != "Player2")
            return;

        if (other.transform.parent.tag == "Player1" && isPlayer2weapon == false)
        {
            return;
        }
        else if (other.transform.parent.tag == "Player2" && isPlayer2weapon == true)
        {
            return;
        }

        if(collided == false)
        {
            StartCoroutine(Explode());
            collided = true;
        }
    }

    IEnumerator Explode()
    {
        // How long does before the explosion?
        yield return new WaitForSeconds(explosionTime);      // Originally 0.6f

        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;

        if(isPlayer2weapon)
        {
            // Inform the system that it is player 2's explosion
            explosion.GetComponent<BenThompson_ExplosionBehavior>().IsPlayer2();
        }
        

        // Destroy the mine
        Destroy(transform.parent.gameObject);
    }
}
