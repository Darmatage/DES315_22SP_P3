using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamianRouseManager : MonoBehaviour
{
  [Header("Weapon Settings")]
  public GameObject weaponPrefab_;
  public DamianRouseWeapon weaponScript_;
  public Vector3 weaponOffset_;

  [Header("State")]
  public bool isAttacking;
  public bool isArmored_ = true;
  public bool isPlayer_ = true;

  [Header("DetachArmor Settings")]
  public GameObject armorParent_;
  public float moveMultiplier_;
  public float turnMultiplier_;

  //[Header("Stats")]
  [HideInInspector]
  public float defaultSpeed_;
  [HideInInspector]
  public float defaultTurn_;

  private BotBasic_Move BBM_;
  private BotBasic_Damage BBD_;

  [HideInInspector]
  public string button1;
  [HideInInspector]
  public string button2;
  [HideInInspector]
  public string button3;
  [HideInInspector]
  public string button4;

  // Start is called before the first frame update
  void Start()
  {
    //Get the buttons
    button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
    button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
    button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
    button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;

    //Instantiate Weapom
    GameObject weapon = Instantiate(weaponPrefab_, transform.position + weaponOffset_, transform.rotation);
    weapon.transform.SetParent(gameObject.transform);
    weaponScript_ = weapon.GetComponent<DamianRouseWeapon>();
    weaponScript_.TransferData(gameObject);

    //Get Other Bot Component
    BBM_ = gameObject.GetComponent<BotBasic_Move>();
    BBD_ = gameObject.GetComponent<BotBasic_Damage>();

    //Record defaults
    defaultSpeed_ = BBM_.moveSpeed;
    defaultTurn_ = BBM_.rotateSpeed;

    SetDefaultCamToBehind();
    if(!isPlayer_)
      BBM_.enabled = false;
  }

  void SetDefaultCamToBehind()
  {
    //Get the button that manages this
    GameObject button = null;
    if (gameObject.transform.root.tag == "Player1")
      button = GameObject.Find("ButtonCamRotateP1");
    if (gameObject.transform.root.tag == "Player2")
      button = GameObject.Find("ButtonCamRotateP2");

    //Use Toggle if button exists
    if (button != null)
    {
      ButtonCamerasToggle toggle = button.GetComponent<ButtonCamerasToggle>();
      if (toggle)
        toggle.SwitchCameras();
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (!isPlayer_)
      return;

    if (Input.GetButtonDown(button1))
    {
      UseMove(1);
    }

    else if (Input.GetButtonDown(button2))
    {
      UseMove(2);
    }

    else if (Input.GetButtonDown(button3))
    {
      UseMove(3);
    }

    else if (Input.GetButtonDown(button4))
    {
      UseMove(4);
    }
  }

  public void UseMove(int move)
  {
    if (!isAttacking)
    {
      if (move == 1)
      {
        isAttacking = true;
        BBM_.moveSpeed *= 0.7f;
        BBM_.rotateSpeed = 0;
        weaponScript_.Act(move);
      }
      else if (move == 2)
      {
        isAttacking = true;
        BBM_.moveSpeed = 0;
        BBM_.rotateSpeed = 0;
        weaponScript_.Act(move);
      }
      else if (move == 3)
      {
        isAttacking = true;
        BBM_.moveSpeed *= 0.5f;
        BBM_.rotateSpeed = 0;
        weaponScript_.Act(move);
      }
    }

    if (move == 4)
    {
      if (isArmored_)
        DetachArmor();
    }
  }

  public void DetachArmor()
  {
    isArmored_ = false;

    //Update stats to match less armor
    defaultSpeed_ *= moveMultiplier_;
    defaultTurn_ *= turnMultiplier_;
    BBM_.moveSpeed = defaultSpeed_;
    BBM_.rotateSpeed = defaultTurn_;

    //Unparent all armor pieces and send them flying
    while (armorParent_.transform.childCount != 0)
    {
      Transform t = armorParent_.transform.GetChild(0);
      t.SetParent(null);
      t.gameObject.GetComponent<BoxCollider>().enabled = true;
      Rigidbody childRigidBody = t.gameObject.AddComponent<Rigidbody>();
      childRigidBody.AddForce((t.position - armorParent_.transform.position) * 21f, ForceMode.Impulse);
      childRigidBody.mass = 0.1f;
    }

    //Remove the armor holder for good measure
    armorParent_.transform.SetParent(null);

    //Affect weapon stats
    weaponScript_.moveOneDamage_ = 1;
    weaponScript_.moveOneSpeed_ /= 1.5f;
    weaponScript_.moveTwoDamage_ = 3;

    //Get rid of all shield
    BBD_.shieldPowerFront = 0;
    BBD_.shieldPowerBack = 0;
    BBD_.shieldPowerLeft = 0;
    BBD_.shieldPowerRight = 0;
    BBD_.shieldPowerTop = 0;
    BBD_.shieldPowerBottom = 0;
  }

  public void FinishMove()
  {
    isAttacking = false;
    BBM_.moveSpeed = defaultSpeed_;
    BBM_.rotateSpeed = defaultTurn_;
  }
}
