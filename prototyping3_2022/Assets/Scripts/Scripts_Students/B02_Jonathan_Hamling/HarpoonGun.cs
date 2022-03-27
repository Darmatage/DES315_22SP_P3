using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonGun : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private GameObject enemy;

    public float hookSpeed;
    public float maxHookDistance;
    public float x_offset;
    public float z_offset;

    LineRenderer lineRenderer;

    bool isHook;
    bool wasEnmyHook;

    float distance;
    Vector3 startPos;

    Rigidbody rb;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        isHook = false;
        wasEnmyHook = false;
        distance = 0;
        rb = GetComponent<Rigidbody>();
        startPos = new Vector3(player.transform.position.x + x_offset, player.transform.position.y, player.transform.position.z + z_offset);
    }

    private void Update()
    {
        startPos = new Vector3(player.transform.position.x + x_offset, player.transform.position.y, player.transform.position.z + z_offset);
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, transform.position);
        if (Input.GetButtonDown("Fire1") && !isHook && !wasEnmyHook)
        {
            StartHook();
        }

        ReturnHook();
        BringEnemyBack();
    }

    private void StartHook()
    {
        isHook = true;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * hookSpeed);
    }

    private void ReturnHook()
    {
        if (isHook)
        {
            distance = Vector3.Distance(transform.position, startPos);
            if (distance > maxHookDistance || wasEnmyHook)
            {
                rb.isKinematic = true;
                transform.position = startPos;
                isHook = false;
            }
        }
    }

    private void BringEnemyBack()
    {
        if (wasEnmyHook)
        {
            Vector3 final = new Vector3(startPos.x, enemy.transform.position.y, startPos.z + z_offset);
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, final, maxHookDistance);
            wasEnmyHook = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player2"))
        {
            wasEnmyHook = true;
            enemy = other.gameObject;
        }
    }
}
