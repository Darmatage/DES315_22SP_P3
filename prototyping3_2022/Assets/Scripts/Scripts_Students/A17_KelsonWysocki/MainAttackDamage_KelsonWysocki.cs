using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAttackDamage_KelsonWysocki : MonoBehaviour
{
    HazardDamage hazardDamage;

    public float totalDamage;
    public float damageDuration;
    public float damageInterval;
    private float intervalTimer;
    private float distanceGone;

    [HideInInspector]
    public bool isShooting = false;

    private void Start()
    {
        hazardDamage = GetComponent<HazardDamage>();
        hazardDamage.damage = (totalDamage * damageInterval) / damageDuration;
    }

    void FixedUpdate()
    {
        if (!isShooting) return;

        distanceGone += (13 / damageInterval) * Time.fixedDeltaTime;
        if (distanceGone >= 1f)
            distanceGone = 0f;
        transform.localPosition = Vector3.Lerp(new Vector3(0f, 0f, 1f), new Vector3(0f, 0f, 14f), distanceGone);
    }

    private void OnTriggerEnter(Collider other)
    {
        distanceGone = 0f;
    }
}
