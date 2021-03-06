using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamianRouseAI : MonoBehaviour
{
  [Header("BotSlot")]
  public int botNumber_;
  public bool debug_ = false;
  public bool NOTINARENA_ = false;

  [Header("Decision Data")]
  public float distanceBetweenFrontAndEnemy_;
  
  public float angleBetweenFrontAndEnemy_;
  public float pastAngleBetweenFrontAndEnemy_;
  public float predictedEnemyAngle_;
  public int futureSense_ = 5;

  public int moving_ = 0;

  public float timeUntilJump_ = 2;
  public float jumpTimer_ = 0;

  public float shield_ = 0;

  public State state_ = State.Passive;

  [Header("Obstacle Detection Data")]
  public float obstacleDetectRange_ = 4f;
  public bool obstacleFrontLeft_ = false;
  public bool obstacleFrontRight_ = false;
  public bool obstacleBackLeft_ = false;
  public bool obstacleBackRight_ = false;

  [Header("Flanking Data")]
  public bool flanked_ = false;
  public float timeUntilFlanked_ = 3f;
  public float flankedTimer_ = 0;
  public float delayUntilFlankingIsPossible_ = 3f;

  [Header("Current Movement")]
  public float move_;
  public float rotation_;

  //Other Bot Components
  private DamianRouseManager DRM_;
  private BotBasic_Move BBM_;
  private BotBasic_Damage BBD_;

  //other bot
  private GameObject enemy_;

  //For navigation
  private List<Collider> hazards_ = new List<Collider>();
  private List<Collider> arena_ = new List<Collider>();

  [Header("Misc")]
  private GameHandler GH_;
  public float startTimer_ = 500f;
  public float timer_ = 0;
  
  public enum State { Passive, Defensive, Neutral, Aggro};

  void Start()
  {
    //Get Other Bot Component
    DRM_ = gameObject.GetComponent<DamianRouseManager>();
    BBM_ = gameObject.GetComponent<BotBasic_Move>();
    BBD_ = gameObject.GetComponent<BotBasic_Damage>();

    //Find what player this is and enemy gameobject head
    GameObject toplevel = null;
    if (gameObject.transform.root.tag == "Player1")
    {
      toplevel = GameObject.Find("PLAYER2_SLOT");
      botNumber_ = 1;
    }
    else if (gameObject.transform.root.tag == "Player2")
    {
      toplevel = GameObject.Find("PLAYER1_SLOT");
      botNumber_ = 2;
    }

    if(toplevel == null)
    {
      NOTINARENA_ = true;
      return;
    }

    //Fetch the enemy from root
    for (int i = 0; i != toplevel.transform.childCount; ++i)
    {
      Transform t = toplevel.transform.GetChild(i);

      if (t && t.gameObject.GetComponent<BotBasic_Damage>() != null)
      {
        enemy_ = t.gameObject;
        break;
      }
    }

    if (enemy_ == null)
    {
      NOTINARENA_ = true;
      return;
    }

    //Get all the arena gameObjects
    toplevel = GameObject.Find("SETTING_ARENA1");
    if (toplevel != null)
    {
      Collider[] colList = toplevel.transform.GetComponentsInChildren<Collider>();
      foreach (Collider c in colList)
        arena_.Add(c);
    }

    //Get all the hazard gameObjects
    toplevel = GameObject.Find("HAZARDS");
    if (toplevel != null)
    {
      Collider[] colList2 = toplevel.transform.GetComponentsInChildren<Collider>();
      foreach (Collider c in colList2)
        hazards_.Add(c);
    }

    GameObject gh = GameObject.Find("GameHandler");
    if (gh != null)
    {
      GH_ = gh.GetComponent<GameHandler>();
      startTimer_ = GH_.gameTime;
    }
    else
      NOTINARENA_ = true;

    shield_ += BBD_.shieldPowerFront;
    shield_ += BBD_.shieldPowerBack;
    shield_ += BBD_.shieldPowerLeft;
    shield_ += BBD_.shieldPowerRight;
    shield_ += BBD_.shieldPowerTop;
    shield_ += BBD_.shieldPowerBottom;
  }

  // Update is called once per frame
  void Update()
  {
    //"Get" the arena timer
    timer_ += Time.deltaTime / 2f;

    //Do nothing outside an arena
    if (NOTINARENA_)
      return;

    GetIntel();
    Act();
    Move();
  }

  //INTEL
  //------------------------------------------------------
  void GetIntel()
  {
    //Get 2D x and z position vectors
    Vector2 self = new Vector2(transform.position.x, transform.position.z);
    Vector2 enemy = new Vector2(enemy_.transform.position.x, enemy_.transform.position.z);
    Vector2 targetDir = enemy - self;
    Vector2 forwardDir = new Vector2(transform.forward.x, transform.forward.z);// - self;

    //Get distance between 2D positions
    distanceBetweenFrontAndEnemy_ = Vector2.Distance(self,enemy);
    
    //Get Angle between front and enemy
    angleBetweenFrontAndEnemy_ = Vector2.SignedAngle(targetDir, forwardDir);
    float absABFE = Mathf.Abs(angleBetweenFrontAndEnemy_);

    //Use angle to determine if player has flanked AI
    if (flanked_ && absABFE < 15f)
      flanked_ = false;
    else if (!flanked_ && absABFE > 15 && absABFE > Mathf.Abs(pastAngleBetweenFrontAndEnemy_))
    {
      flankedTimer_ += Time.deltaTime;

      if (flankedTimer_ > timeUntilFlanked_)
      {
        flanked_ = true;
      }
    }
    else if (!flanked_ && absABFE > 50)
      flanked_ = true;
    else
      flankedTimer_ = 0;

    //Cannot be flanked before a certain time
    //if (timer_ < delayUntilFlankingIsPossible_)
    //  flanked_ = false;

    //Predict where player will be
    predictedEnemyAngle_ = (angleBetweenFrontAndEnemy_ - pastAngleBetweenFrontAndEnemy_) * futureSense_ + angleBetweenFrontAndEnemy_;
    //Update Past
    pastAngleBetweenFrontAndEnemy_ = angleBetweenFrontAndEnemy_;

    //Determine if the AI is flipped
    if(Vector3.Angle(transform.up, Vector3.up) > 20f)
    {
      jumpTimer_ += Time.deltaTime;

      if (jumpTimer_ > timeUntilJump_)
        Jump();
    }
    else
      jumpTimer_ = 0;

    //Getting shot from a distance
    float currentShield = 0;
    currentShield += BBD_.shieldPowerFront;
    currentShield += BBD_.shieldPowerBack;
    currentShield += BBD_.shieldPowerLeft;
    currentShield += BBD_.shieldPowerRight;
    currentShield += BBD_.shieldPowerTop;
    currentShield += BBD_.shieldPowerBottom;

    if(shield_ + 0.01 < currentShield || shield_ - 0.01 > currentShield)
    {
      shield_ = currentShield;
      if (distanceBetweenFrontAndEnemy_ > 10f)
        state_ = State.Aggro;
    }

    //Make rays from 4 spots on AI
    Ray frontLeft = new Ray(transform.position - transform.right + transform.forward * 2, transform.forward);
    Ray frontRight = new Ray(transform.position + transform.right + transform.forward * 2, transform.forward);
    Ray backLeft = new Ray(transform.position - transform.right - transform.forward * 2, -transform.forward);
    Ray backRight = new Ray(transform.position + transform.right - transform.forward * 2, -transform.forward);

    RaycastHit hit;

    obstacleFrontLeft_ = false;
    obstacleFrontRight_ = false;
    obstacleBackLeft_ = false;
    obstacleBackRight_ = false;

    //Check if rays collide with hazards
    foreach (Collider c in hazards_)
    {
      if (!obstacleFrontLeft_ && c.Raycast(frontLeft, out hit, obstacleDetectRange_))
        obstacleFrontLeft_ = true;

      if (!obstacleFrontRight_ && c.Raycast(frontRight, out hit, obstacleDetectRange_))
        obstacleFrontRight_ = true;

      if (!obstacleBackLeft_ && c.Raycast(backLeft, out hit, obstacleDetectRange_))
        obstacleBackLeft_ = true;

      if (!obstacleBackRight_ && c.Raycast(backRight, out hit, obstacleDetectRange_))
        obstacleBackRight_ = true;
    }

    //Draw rays
    if (debug_)
    {
      if (obstacleFrontLeft_)
        Debug.DrawRay(frontLeft.origin, frontLeft.direction * obstacleDetectRange_, Color.red);
      else
        Debug.DrawRay(frontLeft.origin, frontLeft.direction * obstacleDetectRange_, Color.green);

      if (obstacleFrontRight_)
        Debug.DrawRay(frontRight.origin, frontRight.direction * obstacleDetectRange_, Color.red);
      else
        Debug.DrawRay(frontRight.origin, frontRight.direction * obstacleDetectRange_, Color.green);

      if (obstacleBackLeft_)
        Debug.DrawRay(backLeft.origin, backLeft.direction * obstacleDetectRange_, Color.red);
      else
        Debug.DrawRay(backLeft.origin, backLeft.direction * obstacleDetectRange_, Color.green);

      if (obstacleBackRight_)
        Debug.DrawRay(backRight.origin, backRight.direction * obstacleDetectRange_, Color.red);
      else
        Debug.DrawRay(backRight.origin, backRight.direction * obstacleDetectRange_, Color.green);
    }
  }

  //Decide on what to do based on intel
  public void Act()
  {
    //If armored, allow
    if(DRM_.isArmored_)
    {
      if (timer_ / startTimer_  > 0.5f ||
           (botNumber_ == 1 && GameHandler.p1Health < 11) ||
           (botNumber_ == 2 && GameHandler.p2Health < 11)
         )
      {
        DRM_.UseMove(4);
        state_ = State.Aggro;
      }
    }

    switch(state_)
    {
      case State.Passive:
        PassiveAct();
        break;
      case State.Defensive:
        DefensiveAct();
        break;
      case State.Neutral:
        NeutralAct();
        break;
      case State.Aggro:
        AggroAct();
        break;
    }
  }

  void PassiveAct()
  {
    LookTowardsEnemy();
    moving_ = -1;
    MaintainDistance(DRM_.weaponScript_.moveOneRange_ + 4f, DRM_.weaponScript_.moveOneRange_ + 7f, 0);
    if (timer_ > delayUntilFlankingIsPossible_)
      state_ = State.Defensive;
  }

  void DefensiveAct()
  {
    LookTowardsEnemy();
    MaintainDistance(DRM_.weaponScript_.moveOneRange_ + 1f, DRM_.weaponScript_.moveOneRange_ + 7f, 0);
    HandleFlanking(1);

    float absABFE = Mathf.Abs(angleBetweenFrontAndEnemy_);

    //Only do if not attacking
    if (!DRM_.isAttacking)
    {
      if (distanceBetweenFrontAndEnemy_ < DRM_.weaponScript_.moveOneRange_ + 3f && absABFE < 15f)
        DRM_.UseMove(1);
    }
  }

  void NeutralAct()
  {
    moving_ = 0;
    LookTowardsEnemy();
    HandleFlanking(1);

    float absABFE = Mathf.Abs(angleBetweenFrontAndEnemy_);

    //Only do if not attacking
    if (!DRM_.isAttacking)
    {
      if (distanceBetweenFrontAndEnemy_ < DRM_.weaponScript_.moveOneRange_ + 2f && absABFE < 15f)
        DRM_.UseMove(1);
    }
  }

  void AggroAct()
  {
    LookTowardsEnemy();
    MaintainDistance(DRM_.weaponScript_.moveOneRange_, DRM_.weaponScript_.moveOneRange_ + 3f, 0);
    HandleFlanking(1);

    float absABFE = Mathf.Abs(angleBetweenFrontAndEnemy_);

    //Only do if not attacking
    if (!DRM_.isAttacking)
    {
      if (distanceBetweenFrontAndEnemy_ < DRM_.weaponScript_.moveOneRange_ + 2f && absABFE < 15f)
        DRM_.UseMove(1);
    }
  }

  void HandleFlanking(int attacks = 0)
  {
    //What to be done if flanked
    if (flanked_)
    {
      if (attacks != 0)
      {
        if (DRM_.isAttacking == false)
        {
          if (Random.Range(0, 5) == 4)
            DRM_.UseMove(2);
          else
          {
            if (angleBetweenFrontAndEnemy_ < 0)
              DRM_.UseMove(3);
            else
              DRM_.UseMove(2);
          }
        }
      }
    }
    if (flankedTimer_ > 0.3f)
    {
      moving_ = -1;
    }
  }

  void LookTowardsEnemy()
  {
    //Get Rot speed
    rotation_ = BBM_.rotateSpeed * Time.deltaTime;
    float angleToUse = angleBetweenFrontAndEnemy_;
    //Use predicted angle if attacking
    if (DRM_.isAttacking == true)
    {
      angleToUse = predictedEnemyAngle_;
    }

    float absABFE = Mathf.Abs(angleToUse);

    //turn to look right at player
    if (absABFE > 1)
    {
      if (absABFE - rotation_ < 0)
        rotation_ = Mathf.Abs(absABFE - rotation_);

      if (angleToUse < 0)
        rotation_ *= -1;
    }
    else
      rotation_ = 0;
  }

  void MaintainDistance(float min, float max, float variation)
  {
    if (distanceBetweenFrontAndEnemy_ > max)
      moving_ = 1;
    else if (distanceBetweenFrontAndEnemy_ < min + variation)
      moving_ = -1;
    //else
    //  moving_ = 0;
  }

  //MOVEMENT After Acting
  //-----------------------------
  public void Move()
  {
    move_ = BBM_.moveSpeed * Time.deltaTime * moving_;

    //Move if not grabbed
    if (BBM_.isGrabbed == false)
    {
      AvoidObstacles();
      transform.Translate(0, 0, move_);
      transform.Rotate(0, rotation_, 0);
    }
  }

  //Basically strafes around hazards
  void AvoidObstacles()
  {
    float botMove = BBM_.moveSpeed * Time.deltaTime;
    bool avoiding = true;

    if (obstacleFrontLeft_ && obstacleFrontRight_)
    {
      if (angleBetweenFrontAndEnemy_ < 0)
        botMove = -botMove;
    }
    else if (obstacleFrontLeft_)
    {
      //nothing
    }
    else if (obstacleFrontRight_)
      botMove = -botMove;
    else if (obstacleBackLeft_ && obstacleBackRight_)
    {
      if (angleBetweenFrontAndEnemy_ < 0)
        botMove = -botMove;
    }
    else if (obstacleBackLeft_)
    {
      //nothing
    }
    else if (obstacleBackRight_)
      botMove = -botMove;
    else
      avoiding = false;

    if (avoiding)
    {
      transform.Translate(botMove, 0, 0);
      move_ = 0;
      //rotation_ = 0;
    }
  }

  //From Basic Move, just flip AI upright
  public void Jump()
  {
    Rigidbody rb = gameObject.GetComponent<Rigidbody>();

    if (BBM_.isGrounded == true)
    {
      rb.AddForce(rb.centerOfMass + new Vector3(0f, BBM_.jumpSpeed * 10, 0f), ForceMode.Impulse);
    }

    if ((BBM_.isTurtled == true) && (BBM_.canFlip == true))
    {
      rb.AddForce(rb.centerOfMass + new Vector3(BBM_.jumpSpeed / 2, 0, BBM_.jumpSpeed / 2), ForceMode.Impulse);
      transform.Rotate(150f, 0, 0);
      GetComponent<Rigidbody>().velocity = Vector3.zero;
      GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
      // canFlip = false;
      // canFlipGate = true;
    }

    else if (BBM_.canFlip == true)
    {
      Vector3 betterEulerAngles = new Vector3(gameObject.transform.parent.eulerAngles.x, transform.eulerAngles.y, gameObject.transform.parent.eulerAngles.z);
      transform.eulerAngles = betterEulerAngles;
      GetComponent<Rigidbody>().velocity = Vector3.zero;
      GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
  }


}
