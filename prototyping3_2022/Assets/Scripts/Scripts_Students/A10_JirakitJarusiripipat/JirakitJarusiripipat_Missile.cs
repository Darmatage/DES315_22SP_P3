using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JirakitJarusiripipat_Missile : MonoBehaviour
{
    [SerializeField]
    private GameObject splash;
    [SerializeField]
    private GameObject slpashWithoutDamage;
    public GameObject parent;
    private JirakitJarusiripipat_SoundKeeper soundKeeper;
    [SerializeField]
    private GameObject missileIndication;
    private bool goDown = false;
    private bool calculate;
    private Rigidbody rb;
    public GameObject target;
    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(0.5f);
        goDown = true;
    }
    private void Start()
    {
        StartCoroutine(Countdown());

        //parent = GameObject.FindWithTag("Player1");
        soundKeeper = parent.GetComponentInChildren<JirakitJarusiripipat_SoundKeeper>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if(!goDown && !calculate)
        {
            rb.AddForce(new Vector3(0, 15, 0));
        }
        else if(goDown && !calculate)
        {
            float randomNumberX = Random.Range(-4.0f, 4.0f);
            float randomNumberZ = Random.Range(-4.0f, 4.0f);
            Vector3 velo = CalculateVelocity(new Vector3(target.transform.GetComponentInChildren<BotBasic_Damage>().gameObject.transform.position.x + randomNumberX, target.transform.GetComponentInChildren<BotBasic_Damage>().gameObject.transform.position.y - 3.0f, target.transform.GetComponentInChildren<BotBasic_Damage>().gameObject.transform.position.z + randomNumberZ), transform.position, 0.5f);
            transform.rotation = Quaternion.LookRotation(velo);
            GetComponentInChildren<Rigidbody>().velocity = velo;
            Vector3 placeToSpawn = new Vector3(target.transform.GetComponentInChildren<BotBasic_Damage>().gameObject.transform.position.x + randomNumberX, 0, target.transform.GetComponentInChildren<BotBasic_Damage>().gameObject.transform.position.z);
            Instantiate(missileIndication, placeToSpawn, Quaternion.identity);
            calculate = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            GameObject obj = Instantiate(splash, transform.position, Quaternion.identity);
            soundKeeper.Explode();
            Destroy(gameObject);
        }
        else
        {
            soundKeeper.Explode();
            GameObject obj = Instantiate(slpashWithoutDamage, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        float sy = distance.y;
        float sxz = distanceXZ.magnitude;

        float vxz = sxz / time;
        float vy = sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= vxz;
        result.y = vy;

        return result;
    }
}
