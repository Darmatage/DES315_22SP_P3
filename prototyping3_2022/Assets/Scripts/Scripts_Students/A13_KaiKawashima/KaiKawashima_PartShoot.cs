using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaiKawashima_PartShoot : MonoBehaviour
{
    private KaiKawashima_PartsManager partsManager;
    private KaiKawashima_PartsShield partsShield;
    private GameObject partPrefab;
    private BotBasic_Damage damageStuff;

    public float Damage = 1.0f;
    public float ShotSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        partsManager = gameObject.GetComponentInChildren<KaiKawashima_PartsManager>();
        partPrefab = partsManager.PartPrefab;
        damageStuff = gameObject.GetComponentInParent<BotBasic_Damage>();
        partsShield = gameObject.GetComponent<KaiKawashima_PartsShield>();
    }

    // Update is called once per frame
    void Update()
    {
        // do nothing
    }

    public void Shoot()
    {
        GameObject newObject = GameObject.Instantiate(partPrefab, gameObject.transform.position + gameObject.transform.forward.normalized * 4.0f, Quaternion.identity);
        newObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward.normalized * ShotSpeed;
        newObject.GetComponent<HazardDamage>().damage = Damage;

        if (damageStuff.shieldPowerFront <= 0)
        {
            damageStuff.dmgParticlesFront.SetActive(true);
        }
        
        if (!partsManager.AddPart(newObject, KaiKawashima_PartsManager.Side.FRONT))
        {
            GameObject.Destroy(newObject);
        }

        --damageStuff.shieldPowerFront;
        --partsShield.front;

    }
}
