using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAttack_KelsonWysocki : MonoBehaviour
{
    public GameObject laser;
    private BotBasic_Move move;
    public string button;
    public float offset;
    public float cooldown;

    public float resetSpeedTime;
    private float resetSpeedCounter = 0f;

    private float startTime;

    private bool canShoot = true;
    private bool resetSpeed = false;

    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        move = GetComponent<BotBasic_Move>();
    }

    // Update is called once per frame
    void Update()
    {
        resetSpeedCounter += Time.deltaTime;

        if (Input.GetButtonDown(button))
            startTime = Time.time;

        if (Input.GetButton(button))
        {
            resetSpeedCounter = 0f;
            resetSpeed = false;
            if (canShoot)
            {
                GameObject projectile1 = Instantiate(laser, transform);
                projectile1.GetComponent<MainAttackLifetime_KelsonWysocki>().parent = transform;
                Vector3 newPos = projectile1.transform.position;
                newPos += transform.forward * offset;
                projectile1.transform.position = newPos;

                projectile1.GetComponent<MainAttackLifetime_KelsonWysocki>().startPos = newPos;
                canShoot = false;

                Invoke(nameof(Reset), cooldown);
            }

            float change = Time.time - startTime;
            move.moveSpeed = Mathf.Lerp(20f, 0f, change / 0.25f);
            move.rotateSpeed = Mathf.Lerp(250f, 150f, change / 0.25f);
        }

        if (!resetSpeed && resetSpeedCounter >= resetSpeedTime)
        {
            resetSpeed = true;
            startTime = Time.time;
        }

        if (resetSpeed)
        {
            float change = Time.time - startTime;
            move.moveSpeed = Mathf.Lerp(1f, 20f, change / 0.25f);
            move.rotateSpeed = Mathf.Lerp(150f, 250f, change / 0.25f);
        }
    }

    private void Reset()
    {
        canShoot = true;
    }
}
