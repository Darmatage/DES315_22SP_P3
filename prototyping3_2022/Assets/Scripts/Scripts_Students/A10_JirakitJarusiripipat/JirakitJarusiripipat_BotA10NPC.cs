using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class JirakitJarusiripipat_BotA10NPC : MonoBehaviour
{
    private JirakitJarusipipat_StateMachine _stateMachine;
    private JirakitJarusiripipat_Weapon _weapon;
    private JirakitJarusiripipat_CheckMainGunRange _mainGun;
    public static bool isMoving;

    private BotBasic_Move basicMove;

    public float timeUntilJump = 2;
    public float jumpTimer = 0;
    //private Jirakit
    void At(JirakitJarusiripipat_IState to, JirakitJarusiripipat_IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);


    // Start is called before the first frame update
    void Start()
    {
        basicMove = gameObject.GetComponent<BotBasic_Move>();
        var navMeshAgent = GetComponent<NavMeshAgent>();
        _stateMachine = new JirakitJarusipipat_StateMachine();
        _mainGun = GetComponentInChildren<JirakitJarusiripipat_CheckMainGunRange>();
        _weapon = GetComponent<JirakitJarusiripipat_Weapon>();


        var moveToward = new JirakitJarusiripipat_NPCMove(this, navMeshAgent, _mainGun, _weapon, transform);
        var shootMissile = new JirakitJarusiripipat_NPCShootMissile(_weapon);
        var shootMainGun = new JirakitJarusiripipat_NPCShootMainGun(_weapon);
        var spawnBot = new JirakitJarusiripipat_NPCSpawnBot(_weapon);
        var idle = new JirakitJarusiripipat_NPCIdle();
        //var flee = new JirakitJarusiripipat_NPCIdle();



        At(moveToward, idle, StopMove());
        At(idle, moveToward, Move());
        At(shootMainGun, moveToward, Move());
        _stateMachine.AddAnyTransition(shootMissile, () => _weapon.missileReady);
        _stateMachine.AddAnyTransition(shootMainGun, () => _weapon.currentBulletCooldown <= 0.0f);
        _stateMachine.AddAnyTransition(spawnBot, () => _weapon.currentBotCooldown <= 0.0f);
        //_stateMachine.AddAnyTransition(moveToward, () => isMoving);
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
    Func<bool> Move() => () => isMoving;
    Func<bool> StopMove() => () => !isMoving;
    //Func<bool> NotInRange() => () => !_mainGun.inMainGunRange;
    //Func<bool> InRange() => () => _mainGun.inMainGunRange;
    //Func<bool> Move() => () => dist < 3;
    //Func<bool> StopMove() => () isMoving = true;
    // Update is called once per frame
    private void Update()
    {
        _stateMachine.Tick();
        Vector3 lookVector = _weapon.target.GetComponentInChildren<BotBasic_Damage>().gameObject.transform.position - transform.position;
        lookVector.y = transform.position.y;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 9 * Time.deltaTime);
        Debug.Log("IsMoving " + isMoving);
        //transform.LookAt(_weapon.target.GetComponentInChildren<BotBasic_Damage>().gameObject.transform);
        //Determine if the AI is flipped
        if (Vector3.Angle(transform.up, Vector3.up) > 20f)
        {
            jumpTimer += Time.deltaTime;

            if (jumpTimer > timeUntilJump)
                Jump();
        }
        else
            jumpTimer = 0;
    }
    public void Jump()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        if (basicMove.isGrounded == true)
        {
            rb.AddForce(rb.centerOfMass + new Vector3(0f, basicMove.jumpSpeed * 10, 0f), ForceMode.Impulse);
        }

        if ((basicMove.isTurtled == true) && (basicMove.canFlip == true))
        {
            rb.AddForce(rb.centerOfMass + new Vector3(basicMove.jumpSpeed / 2, 0, basicMove.jumpSpeed / 2), ForceMode.Impulse);
            transform.Rotate(150f, 0, 0);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            // canFlip = false;
            // canFlipGate = true;
        }

        else if (basicMove.canFlip == true)
        {
            Vector3 betterEulerAngles = new Vector3(gameObject.transform.parent.eulerAngles.x, transform.eulerAngles.y, gameObject.transform.parent.eulerAngles.z);
            transform.eulerAngles = betterEulerAngles;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
