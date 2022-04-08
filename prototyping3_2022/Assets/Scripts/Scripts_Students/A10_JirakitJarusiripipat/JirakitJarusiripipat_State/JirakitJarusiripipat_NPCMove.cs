using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JirakitJarusiripipat_NPCMove : JirakitJarusiripipat_IState
{
    private readonly JirakitJarusiripipat_BotA10NPC _botA10NPC;
    private NavMeshAgent _navMeshAgent;
    private JirakitJarusiripipat_CheckMainGunRange _mainGun;
    private float _speed = 9f;
    private JirakitJarusiripipat_Weapon _weapon;
    private float currentCountdown = 0.0f;
    private float countdown = 3.0f;
    private Transform transform;
    public JirakitJarusiripipat_NPCMove(JirakitJarusiripipat_BotA10NPC bot, NavMeshAgent nav, JirakitJarusiripipat_CheckMainGunRange gun, JirakitJarusiripipat_Weapon weapon, Transform t)
    {
        _navMeshAgent = nav;
        _botA10NPC = bot;
        _mainGun = gun;
        _weapon = weapon;
        transform = t;
    }

    void JirakitJarusiripipat_IState.Tick()
    {
        _navMeshAgent.SetDestination(_weapon.target.GetComponentInChildren<BotBasic_Damage>().gameObject.transform.position);
        //_navMeshAgent.destination = _weapon.target.GetComponent<BotBasic_Damage>().gameObject.transform.position;
        Debug.Log("In MoveState");
        float dist = Vector3.Distance(_weapon.GetComponent<BotBasic_Damage>().gameObject.transform.position, transform.position);
        if (currentCountdown < 0.0f || dist < 2)
        {
            JirakitJarusiripipat_BotA10NPC.isMoving = false;
        }
        else
        {
            currentCountdown -= Time.deltaTime;
        }

    }

    void JirakitJarusiripipat_IState.OnEnter()
    {
        Debug.Log("Enter MoveState");
        _navMeshAgent.enabled = true;
        _speed = _navMeshAgent.speed;
        currentCountdown = countdown;
        JirakitJarusiripipat_BotA10NPC.isMoving = true;
    }

    void JirakitJarusiripipat_IState.OnExit()
    {
        Debug.Log("Out MoveState");
        _navMeshAgent.enabled = false;
    }
}
