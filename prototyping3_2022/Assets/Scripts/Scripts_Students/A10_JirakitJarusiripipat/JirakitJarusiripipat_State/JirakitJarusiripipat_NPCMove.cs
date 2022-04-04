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
    public JirakitJarusiripipat_NPCMove(JirakitJarusiripipat_BotA10NPC bot, NavMeshAgent nav, JirakitJarusiripipat_CheckMainGunRange gun, JirakitJarusiripipat_Weapon weapon)
    {
        _navMeshAgent = nav;
        _botA10NPC = bot;
        _mainGun = gun;
        _weapon = weapon;
    }

    void JirakitJarusiripipat_IState.Tick()
    {
        //if(!_mainGun.inMainGunRange)
        //{
        //    //_navMeshAgent.SetDestination(_weapon.target.GetComponent<BotBasic_Damage>().gameObject.transform.position);
        //    _navMeshAgent.destination = _weapon.target.GetComponent<BotBasic_Damage>().gameObject.transform.position;
        //    Debug.Log("In MoveState");

        //}
    }

    void JirakitJarusiripipat_IState.OnEnter()
    {
        Debug.Log("Enter MoveState");
        _navMeshAgent.enabled = true;
        _speed = _navMeshAgent.speed;

    }

    void JirakitJarusiripipat_IState.OnExit()
    {
        Debug.Log("Out MoveState");
        _navMeshAgent.enabled = false;
    }
}
