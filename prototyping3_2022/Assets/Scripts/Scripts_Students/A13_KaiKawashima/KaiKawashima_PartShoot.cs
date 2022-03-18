using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaiKawashima_PartShoot : MonoBehaviour
{
    private KaiKawashima_PartsManager partsManager;
    private GameObject partPrefab;

    public float Damage = 1.0f;
    public float ShotSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        partsManager = gameObject.GetComponentInChildren<KaiKawashima_PartsManager>();
        partPrefab = partsManager.PartPrefab;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        GameObject newObject = GameObject.Instantiate(partPrefab, gameObject.transform.position + gameObject.transform.forward.normalized * 4.0f, Quaternion.identity);
        newObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward.normalized * ShotSpeed;
        newObject.GetComponent<HazardDamage>().damage = Damage;
        
        if (!partsManager.AddPart(newObject, KaiKawashima_PartsManager.Side.FRONT))
        {
            GameObject.Destroy(newObject);
        }

    }
}
