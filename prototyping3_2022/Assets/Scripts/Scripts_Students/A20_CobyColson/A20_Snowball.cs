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
    private AudioSource audioSrc;
    public AudioClip snowballThrowSfx;
    public AudioClip snowballRollSfx;
    public AudioClip snowmanGrowSfx;
    private bool playThrowAnimation = false;
    private float timePlayedThrowAnimation = 0;
    private float throwAnimationLerpFactor = 0;
    private Quaternion initialRotation;
    private Quaternion targetRotation;
    // Start is called before the first frame update
    void Start()
    {
        activekey1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        activeKey2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
        movement = GetComponent<BotBasic_Move>();
        action1CooldownTimer = 0;

        snowmanTopMiddleHeightDelta = snowmanTopRef.transform.position.y -
        snowmanMiddleRef.transform.position.y;

        audioSrc = GetComponent<AudioSource>();

        string playerNumber = gameObject.transform.root.tag;
        if (playerNumber == "Player1")
        {
            snowballPrefab.GetComponent<HazardDamage>().isPlayer1Weapon = true;
        }
        else if (playerNumber == "Player2")
        {
            snowballPrefab.GetComponent<HazardDamage>().isPlayer2Weapon = true;
        }
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

            audioSrc.clip = snowmanGrowSfx;
            audioSrc.pitch = 1;
            audioSrc.volume = 1;
            audioSrc.Play();
        }

        if (Input.GetButtonDown(activekey1) && snowmanBottomRef.activeSelf && transform.up.y >= 1)
        {
            RollSnowball();
        }
        if (Input.GetButtonDown(activeKey2) && action1CooldownTimer <= 0)
        {
            ThrowSnowball();
        }

        if (playThrowAnimation)
        {
            throwAnimationLerpFactor = Mathf.PingPong(timePlayedThrowAnimation * 8.0f, 1.0f);
            snowmanMiddleRef.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, throwAnimationLerpFactor);
            timePlayedThrowAnimation += Time.deltaTime;
            if (timePlayedThrowAnimation > 0.2f)
            {
                playThrowAnimation = false;
                timePlayedThrowAnimation = 0;
                //snowmanMiddleRef.transform.rotation = snowmanTopRef.transform.rotation;
            }
        }
        else if (snowmanMiddleRef.transform.rotation != snowmanTopRef.transform.rotation)
        {
            snowmanMiddleRef.transform.rotation = Quaternion.Lerp(snowmanMiddleRef.transform.rotation,
             snowmanTopRef.transform.rotation, Time.time);
        }
    }
    
    public void ResetBody()
    {
        bodyResetRequired = true;
    }

    public void PlaySnowballSfx(float volume)
    {
        audioSrc.clip = snowballThrowSfx;
        audioSrc.volume = volume;
        audioSrc.pitch = Random.Range(0.65f, 1.25f);
        audioSrc.Play();
    }

    private void ThrowSnowball()
    {
        A20_SnowballBehavior snowball = Instantiate(snowballPrefab).GetComponent<A20_SnowballBehavior>();
        Vector3 spawnPoint = (snowballSpawnLeft ? snowballSpawnPointLeft.position : snowballSpawnPointRight.position);
        Vector3 spawnDirection = transform.forward + 0.1f * (snowballSpawnLeft ? transform.right : -transform.right);
        snowball.Initialize(spawnPoint, spawnDirection);
        playThrowAnimation = true;
        initialRotation = transform.rotation;
        GameObject targetObj = new GameObject();
        float deltaY = (snowballSpawnLeft ? 45 : -45);
        targetObj.transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y + deltaY,
            transform.eulerAngles.z);
        targetRotation = targetObj.transform.rotation;
        Destroy(targetObj);

        snowballSpawnLeft = !snowballSpawnLeft;
        action1CooldownTimer = action1Cooldown;
        
        PlaySnowballSfx(0.75f);
    }

    private void RollSnowball()
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

        audioSrc.clip = snowballRollSfx;
        audioSrc.pitch = 1;
        audioSrc.volume = 1;
        audioSrc.Play();
    }
}
