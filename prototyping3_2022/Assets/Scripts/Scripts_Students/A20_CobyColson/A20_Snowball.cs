using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A20_Snowball : MonoBehaviour
{
    private string activekey1;
    private string activeKey2;
    public GameObject snowballPrefab;
    public GameObject snowballBigPrefab;
    public GameObject snowmanBottom;
    public GameObject snowmanMiddle;
    public GameObject snowmanTopRef;
    public GameObject snowmanMiddleRef;
    public GameObject snowmanBottomRef;
    
    public Transform snowballSpawnPointLeft;
    public Transform snowballSpawnPointRight;
    private BotBasic_Move movement;
    public float turretRotationSpeed;
    public float turretShootCooldown;
    private bool snowballSpawnLeft = true;
    private enum SnowmanState
    {
        Head,
        HeadMiddle,
        Complete
    }
    SnowmanState snowmanState = SnowmanState.Complete;

    public float action1Cooldown;
    private float action1CooldownTimer = 0.0f;
    private float snowmanTopMiddleHeightDelta;
    private bool bodyResetRequired = false;

    // Start is called before the first frame update
    void Start()
    {
        activekey1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        activeKey2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
        movement = GetComponent<BotBasic_Move>();
        action1CooldownTimer = 0;

        snowmanTopMiddleHeightDelta = snowmanTopRef.transform.position.y -
        snowmanMiddleRef.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        action1CooldownTimer -= Time.deltaTime;

        if (bodyResetRequired && transform.up.y >= 1)
        {
            snowmanMiddleRef.transform.position = new Vector3(snowmanMiddleRef.transform.position.x,
                snowmanMiddleRef.transform.position.y + 1.25f,
                snowmanMiddleRef.transform.position.z);

            snowmanTopRef.transform.position = new Vector3(snowmanTopRef.transform.position.x,
                    snowmanMiddleRef.transform.position.y + snowmanTopMiddleHeightDelta,
                    snowmanTopRef.transform.position.z);
            snowmanBottomRef.SetActive(true);

            bodyResetRequired = false;
        }

        if (Input.GetButtonDown(activekey1) && snowmanBottomRef.activeSelf && transform.up.y >= 1)
        {
            snowmanBottomRef.SetActive(false);

            snowmanMiddleRef.transform.position = new Vector3(snowmanMiddleRef.transform.position.x,
                snowmanMiddleRef.transform.position.y - 1.25f,
                snowmanMiddleRef.transform.position.z);

            snowmanTopRef.transform.position = new Vector3(snowmanTopRef.transform.position.x,
                snowmanMiddleRef.transform.position.y + snowmanTopMiddleHeightDelta,
                snowmanTopRef.transform.position.z);

            GameObject turret;
            turret = Instantiate(snowballBigPrefab) as GameObject;
            A20_SnowTurretBehavior turretBehavior = turret.GetComponent<A20_SnowTurretBehavior>();
            turretBehavior.Initialize(snowballPrefab, transform.position + transform.forward * 3.0f, transform.forward, this);
        }
        if (Input.GetButtonDown(activeKey2) && action1CooldownTimer <= 0)
        {
            A20_SnowballBehavior snowball = Instantiate(snowballPrefab).GetComponent<A20_SnowballBehavior>();
            Vector3 spawnPoint = (snowballSpawnLeft ? snowballSpawnPointLeft.position : snowballSpawnPointRight.position);
            Vector3 spawnDirection = transform.forward + 0.1f * (snowballSpawnLeft ? transform.right : -transform.right);
            snowball.Initialize(spawnPoint, spawnDirection);
            snowballSpawnLeft = !snowballSpawnLeft;
            action1CooldownTimer = action1Cooldown;
        }
    }
    
    public void ResetBody()
    {
        bodyResetRequired = true;
    }
}
