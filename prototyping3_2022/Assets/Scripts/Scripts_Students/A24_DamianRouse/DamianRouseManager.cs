using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamianRouseManager : MonoBehaviour
{
  public GameObject weaponPrefab_;
  private DamianRouseWeapon weaponScript_;

  public Vector3 weaponOffset_;

  public bool isAttacking;

  private float defaultSpeed_;
  private float defaultTurn_;

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
    button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
    button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
    button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
    button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;

    GameObject weapon = Instantiate(weaponPrefab_, transform.position + weaponOffset_, transform.rotation);
    weapon.transform.SetParent(gameObject.transform);

    weaponScript_ = weapon.GetComponent<DamianRouseWeapon>();
    weaponScript_.TransferData(gameObject);

    defaultSpeed_ = gameObject.GetComponent<BotBasic_Move>().moveSpeed;
    defaultTurn_ = gameObject.GetComponent<BotBasic_Move>().rotateSpeed;
  }

  // Update is called once per frame
  void Update()
  {
    if(!isAttacking)
    {
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

  }

  public void UseMove(int move)
  {
    if (move == 1)
    {
      isAttacking = true;
      gameObject.GetComponent<BotBasic_Move>().moveSpeed = 5;
      gameObject.GetComponent<BotBasic_Move>().rotateSpeed = 0;
      weaponScript_.Act(move);
    }
  }

  public void FinishMove()
  {
    isAttacking = false;
    gameObject.GetComponent<BotBasic_Move>().moveSpeed = defaultSpeed_;
    gameObject.GetComponent<BotBasic_Move>().rotateSpeed = defaultTurn_;
  }
}
