using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingAttack : MonoBehaviour
{
    public Transform target;
    public Rigidbody attack_body;

    public float turn_speed = 1.0f;
    public float move_speed = 10.0f;

    private Transform local_transform;
    private GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        if (!target)
            Debug.Log("Please set the rocket Target");

        local_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!attack_body)
            return;

        attack_body.velocity = local_transform.forward * move_speed;

        var target_rotation = Quaternion.LookRotation(target.position - local_transform.position);
        attack_body.MoveRotation(Quaternion.RotateTowards(local_transform.rotation, target_rotation, turn_speed));
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }


}
