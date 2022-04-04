using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class JirakitJarusiripipat_BotA10NPC : MonoBehaviour
{
    private JirakitJarusipipat_StateMachine _stateMachine;
    private JirakitJarusiripipat_Weapon _weapon;
    private bool isPlayer1;
    private JirakitJarusiripipat_CheckMainGunRange _mainGun;
    //private Jirakit
    void At(JirakitJarusiripipat_IState to, JirakitJarusiripipat_IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);


    // Start is called before the first frame update
    void Start()
    {
        var navMeshAgent = GetComponent<NavMeshAgent>();
        _stateMachine = new JirakitJarusipipat_StateMachine();
        _mainGun = GetComponent<JirakitJarusiripipat_CheckMainGunRange>();
        _weapon = GetComponent<JirakitJarusiripipat_Weapon>();
        if (transform.parent.tag == "Player1")
        {
            isPlayer1 = true;
        }
        else
        {
            isPlayer1 = false;
        }

        var moveToward = new JirakitJarusiripipat_NPCMove(this, navMeshAgent, _mainGun, _weapon);
        var shootMissile = new JirakitJarusiripipat_NPCShootMissile(_weapon);
        var shootMainGun = new JirakitJarusiripipat_NPCShootMainGun(_weapon);
        var spawnBot = new JirakitJarusiripipat_NPCSpawnBot(_weapon);
        var idle = new JirakitJarusiripipat_NPCIdle();


        
        //At(moveToward, idle, NotInRange());
        //At(idle, moveToward, InRange());
        _stateMachine.AddAnyTransition(shootMissile, () => _weapon.missileReady);
        _stateMachine.AddAnyTransition(shootMainGun, () => _weapon.currentBulletCooldown <= 0.0f);
        _stateMachine.AddAnyTransition(spawnBot, () => _weapon.currentBotCooldown <= 0.0f);
        _stateMachine.SetState(idle);
        At(shootMissile, idle, MissileIsNotReady());
        At(shootMainGun, idle, MainGunIsNotReady());
        At(spawnBot, idle, BotIsNotReady());
        //At(shootMissile, moveToward, MissileIsNotReady());
        //At(shootMainGun, moveToward, MainGunIsNotReady());
        //At(spawnBot, moveToward, BotIsNotReady());
    }

    Func<bool> MissileIsNotReady() => () => !_weapon.missileReady;
    Func<bool> MainGunIsNotReady() => () => _weapon.currentBulletCooldown > 0.0f;
    Func<bool> BotIsNotReady() => () => _weapon.currentBotCooldown <= 0.0f;
    Func<bool> NotInRange() => () => !_mainGun.inMainGunRange;
    //Func<bool> InRange() => () => _mainGun.inMainGunRange;
    // Update is called once per frame
    private void Update()
    {
        //transform.LookAt(_weapon.target.transform);
        _stateMachine.Tick();
        //Vector3 lookVector = _weapon.target.transform.position - transform.position;
        //lookVector.y = transform.position.y;
        //Quaternion rot = Quaternion.LookRotation(lookVector);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
        transform.LookAt(_weapon.target.GetComponentInChildren<BotBasic_Damage>().gameObject.transform);
    }
    //public float Speed = 1f;

    //private Coroutine LookCoroutine;

    //public void StartRotating()
    //{
    //    if (LookCoroutine != null)
    //    {
    //        StopCoroutine(LookCoroutine);
    //    }

    //    LookCoroutine = StartCoroutine(LookAt());
    //}

    //private IEnumerator LookAt()
    //{
    //    Quaternion lookRotation = Quaternion.LookRotation(_weapon.target.transform.position - transform.position);

    //    float time = 0;

    //    Quaternion initialRotation = transform.rotation;
    //    while (time < 1)
    //    {
    //        transform.rotation = Quaternion.Slerp(initialRotation, lookRotation, time);

    //        time += Time.deltaTime * Speed;

    //        yield return null;
    //    }
    //}
}
