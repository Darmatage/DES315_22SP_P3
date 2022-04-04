using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A20_Snowball : MonoBehaviour
{
    private string activekey1;
    private string activeKey2;
    public GameObject snowballPrefab;
    public GameObject snowmanBottom;
    public GameObject snowmanMiddle;
    public GameObject snowmanTopRef;
    public GameObject snowmanMiddleRef;
    
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
    // Start is called before the first frame update
    void Start()
    {
        activekey1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        activeKey2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
        movement = GetComponent<BotBasic_Move>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(activekey1))
        {
            if (snowmanState >= SnowmanState.HeadMiddle)
            {
                GameObject turret;
                GameObject bodyPart;
                if (snowmanState >= SnowmanState.Complete)
                {
                    turret = Instantiate(snowmanBottom) as GameObject;
                    bodyPart = gameObject.transform.Find("SnowmanBase").gameObject;

                    snowmanTopRef.transform.position = new Vector3(snowmanTopRef.transform.position.x,
                     snowmanTopRef.transform.position.y - 2,
                      snowmanTopRef.transform.position.z);

                    snowmanMiddleRef.transform.position = new Vector3(snowmanMiddleRef.transform.position.x,
                     snowmanMiddleRef.transform.position.y - 2,
                      snowmanMiddleRef.transform.position.z);
                }
                else
                {
                    turret = Instantiate(snowmanMiddle) as GameObject;
                    bodyPart = gameObject.transform.Find("SnowmanMiddle").gameObject;
                    snowmanTopRef.transform.position = new Vector3(snowmanTopRef.transform.position.x,
                     snowmanTopRef.transform.position.y - 1.4f,
                      snowmanTopRef.transform.position.z);
                }
                bodyPart.SetActive(false);
                movement.moveSpeed -= 1.5f;

                A20_SnowTurretBehavior turretBehavior = turret.AddComponent<A20_SnowTurretBehavior>();
                turretBehavior.Initialize(snowballPrefab, turretRotationSpeed, turretShootCooldown, transform.position + transform.forward * 5.0f);
                snowmanState--;
            }
        }
        if (Input.GetButtonDown(activeKey2))
        {
            StartCoroutine(SpawnSnowball());
        }
    }

    private IEnumerator SpawnSnowball()
    {
        A20_SnowballBehavior snowball = Instantiate(snowballPrefab).GetComponent<A20_SnowballBehavior>();
        Vector3 spawnPoint = (snowballSpawnLeft ? snowballSpawnPointLeft.position : snowballSpawnPointRight.position);
        Vector3 spawnDirection = transform.forward + 0.1f * (snowballSpawnLeft ? transform.right : -transform.right);
        snowball.Initialize(spawnPoint, spawnDirection);
        snowballSpawnLeft = !snowballSpawnLeft;
        yield return new WaitForSeconds(3);
        Destroy(snowball.gameObject);
    }
}
