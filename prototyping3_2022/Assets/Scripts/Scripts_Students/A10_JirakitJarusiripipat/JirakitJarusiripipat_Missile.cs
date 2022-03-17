using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JirakitJarusiripipat_Missile : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 targetPos;
    public Vector3 originPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velo = CalculateVelocity(targetPos,originPos,15f);
        transform.rotation = Quaternion.LookRotation(velo);
        GetComponent<Rigidbody>().velocity = velo;
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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
    }
}
