using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamianRouseWeapon : MonoBehaviour
{
  [Header("Parts of the Spear")]
  public GameObject forward_;
  public GameObject up_;
  public GameObject right_;
  public GameObject spearTip_;
  private GameObject owner_;
  public GameObject dashHitBox_;
  public GameObject swipeHitBox_;

  [Header("Move 1 Settings")]
  public int moveOneDamage_ = 4;
  public float moveOneRange_ = 4f;
  public float moveOneSpeed_ = 4f;

  [Header("Move 2 Settings")]
  public int moveTwoDamage_ = 6;
  public float moveTwoTurnSpeedMult_ = 4f;
  public float moveTwoDashForce_ = 400f;
  public float moveTwoWait1_ = 4f;
  public float moveTwoWait2_ = 4f;
  public float moveTwoWait3_ = 4f;
  public float moveTwoWait4_ = 4f;

  [Header("Move 3 Settings")]
  public int moveThreeDamage_ = 4;
  public float moveThreeWait1_ = 4f;
  public float moveThreeWait2_ = 4f;
  public float moveThreeWait3_ = 4f;
  public float moveThreeWait4_ = 4f;
  public float slashSpin_ = -4f;

  //Misc for lerping
  private Vector3 startPos_;
  private Quaternion startRot_;
  private Vector3 currentVec_;
  private Quaternion currentQuat_;
  private Vector3 targetVec_;
  private Quaternion targetQuat_;


  [Header("Debug")]
  public int hitsLeft_ = 0;

  //Lerp Stuff
  private bool lerping_ = false;
  private float lerpTimer_;
  private float lerpDuration_;
  private Func<int> lerpFunction_ = () => { return 1; };
  private Func<int> Endfunc_ = () => { return 1; };

  // Start is called before the first frame update
  void Start()
  {
    startPos_ = transform.localPosition;
    startRot_ = transform.localRotation;
  }

  // Update is called once per frame
  void Update()
  {
    Use();
    /*
     float botMove = Input.GetAxisRaw(pVertical) * moveSpeed * Time.deltaTime;
		float botRotate = Input.GetAxisRaw(pHorizontal) * rotateSpeed * Time.deltaTime;
		
		if (isGrabbed == false){
			transform.Translate(0,0, botMove);
			transform.Rotate(0, botRotate, 0);
		}
     */
  }

  //Called By Manager
  //-----------------------------------------
  public void TransferData(GameObject owner)
  {
    owner_ = owner;
  }

  public void Act(int move)
  {
    if (move == 1)
      MoveOne();
    else if (move == 2)
      MoveTwo();
    else if (move == 3)
      MoveThree();
    else
      owner_.GetComponent<DamianRouseManager>().isAttacking = false;
  }

  //Move One
  //------------------------------
  public void MoveOne()
  {
    SetHitDamage(spearTip_, moveOneDamage_);
    AddHits(spearTip_, 1);
    targetVec_ = (forward_.transform.localPosition - spearTip_.transform.localPosition) * moveOneRange_ + startPos_;
    SetValues(moveOneSpeed_, MoveOneStep1, MoveOneStep1E);
  }

  public int MoveOneStep1()
  {
    transform.localPosition = Vector3.Lerp(startPos_, targetVec_, lerpTimer_ / lerpDuration_);
    return 1;
  }

  public int MoveOneStep1E()
  {
    //targetVec_ = (forward_.transform.localPosition - spearTip_.transform.localPosition) * moveOneRange_ + startPos_;
    SetValues(moveOneSpeed_, MoveOneStep2, MoveEnd);
    return 1;
  }

  public int MoveOneStep2()
  {
    transform.localPosition = Vector3.Lerp(targetVec_, startPos_ , lerpTimer_ / lerpDuration_);
    return 1;
  }

  //Move Two
  //------------------------------

  public void MoveTwo()
  {
    targetVec_ = -(right_.transform.localPosition - spearTip_.transform.localPosition) * 1f + startPos_;
    SetValues(moveTwoWait1_, MoveTwoStep1, MoveTwoStep1E);
  }

  public int MoveTwoStep1()
  {
    transform.localPosition = Vector3.Lerp(startPos_, targetVec_, lerpTimer_ / lerpDuration_);
    return 1;
  }

  public int MoveTwoStep1E()
  {
    BotBasic_Move BBM = owner_.GetComponent<BotBasic_Move>();
    BBM.rotateSpeed = owner_.GetComponent<DamianRouseManager>().defaultTurn_ * moveTwoTurnSpeedMult_;
    SetValues(moveTwoWait2_, MoveTwoStep2, MoveTwoStep2E);
    return 1;
  }

  public int MoveTwoStep2()
  {
    //wait or whatever
    return 1;
  }

  public int MoveTwoStep2E()
  {
    
    SetHitDamage(dashHitBox_, moveTwoDamage_);
    AddHits(dashHitBox_, 1);

    owner_.GetComponent<BotBasic_Move>().rotateSpeed = 0;
    owner_.GetComponent<Rigidbody>().AddForce(transform.forward * moveTwoDashForce_, ForceMode.Impulse);

    SetValues(moveTwoWait3_, MoveTwoStep3, MoveTwoStep3E);
    return 1;
  }

  public int MoveTwoStep3()
  {
    //wait
    //transform.localPosition = Vector3.Lerp(startPos_, -(right_.transform.localPosition - spearTip_.transform.localPosition) * 2f + startPos_, lerpTimer_ / lerpDuration_);
    return 1;
  }

  public int MoveTwoStep3E()
  {
    SetValues(moveTwoWait4_, MoveTwoStep4, MoveEnd);
    return 1;
  }

  public int MoveTwoStep4()
  {
    transform.localPosition = Vector3.Lerp(currentVec_, startPos_, lerpTimer_ / lerpDuration_);
    return 1;
  }

  //Move Three
  //------------------------------

  public void MoveThree()
  {
    Vector3 vec = -(right_.transform.localPosition - spearTip_.transform.localPosition) * 1f;
    vec += (forward_.transform.localPosition - spearTip_.transform.localPosition) * 2f;
    vec += (up_.transform.localPosition - spearTip_.transform.localPosition) * 1f;

    targetVec_ = vec + startPos_;
    SetValues(moveThreeWait1_, MoveThreeStep1, MoveThreeStep1E);
  }

  public int MoveThreeStep1()
  {
    transform.localPosition = Vector3.Lerp(startPos_, targetVec_, lerpTimer_ / lerpDuration_);

    transform.localRotation = Quaternion.Slerp(startRot_, Quaternion.Euler(-45,-45,1), lerpTimer_ / lerpDuration_);
    return 1;
  }

  public int MoveThreeStep1E()
  {
    SetValues(moveThreeWait2_, MoveThreeStep2, MoveThreeStep2E);
    return 1;
  }

  public int MoveThreeStep2()
  {
    //wait or whatever
    return 1;
  }

  public int MoveThreeStep2E()
  {
    SetHitDamage(swipeHitBox_, moveTwoDamage_);
    AddHits(swipeHitBox_, 1);

    Vector3 vec = (forward_.transform.localPosition - spearTip_.transform.localPosition) * 3f;
    vec += -(up_.transform.localPosition - spearTip_.transform.localPosition) * 1f;

    targetVec_ = vec + startPos_;
    SetValues(moveThreeWait3_, MoveThreeStep3, MoveThreeStep3E);
    return 1;
  }

  public int MoveThreeStep3()
  {
    //Vector3 vec = -(right_.transform.localPosition - spearTip_.transform.localPosition) * 1f;
    transform.localPosition = Vector3.Lerp(currentVec_, targetVec_, lerpTimer_ / lerpDuration_);

    transform.localRotation = Quaternion.Slerp(currentQuat_, Quaternion.Euler(5, 45, 1), lerpTimer_ / lerpDuration_);


    return 1;
  }

  public int MoveThreeStep3E()
  {
    SetValues(moveThreeWait4_, MoveThreeStep4, MoveEnd);
    
    return 1;
  }

  public int MoveThreeStep4()
  {
    transform.localPosition = Vector3.Lerp(currentVec_, startPos_, lerpTimer_ / lerpDuration_);
    transform.localRotation = Quaternion.Slerp(currentQuat_, startRot_, lerpTimer_ / lerpDuration_);
    
    owner_.transform.Rotate(0, Time.deltaTime * slashSpin_, 0);
    
    return 1;
  }

  //Generic Move End
  //------------------------------

  public int MoveEnd()
  {
    ClearHits();
    owner_.GetComponent<DamianRouseManager>().FinishMove();
    return 1;
  }

  //Collision Hits
  //------------------------------
  public void SetHitDamage(GameObject on, int amount)
  {
    HazardDamage hd = on.GetComponent<HazardDamage>();

    if(hd)
    {
      hd.damage = amount;
    }
  }

  public void AddHits(GameObject go, int hits)
  {
    hitsLeft_ += hits;
    go.GetComponent<BoxCollider>().enabled = true;
  }

  public void Hit(GameObject go, HazardDamage hazard)
  {
    --hitsLeft_;

    if (hitsLeft_ < 1)
    {
      go.GetComponent<BoxCollider>().enabled = false;
      //hazard.damage = 0;
    }
  }

  public void ClearHits()
  {
    hitsLeft_ = 0;
    spearTip_.GetComponent<BoxCollider>().enabled = false;
    dashHitBox_.GetComponent<BoxCollider>().enabled = false;
    swipeHitBox_.GetComponent<BoxCollider>().enabled = false;
  }


  //Lerp Stuff
  //-------------------------------------
  public void SetValues(float lerpDuration, Func<int> lerpFunction = null, Func<int> Endfunc = null)
  {
    lerpTimer_ = 0;
    lerping_ = true;
    lerpDuration_ = lerpDuration;

    lerpFunction_ = lerpFunction;
    Endfunc_ = Endfunc;
  }

  public bool Use()
  {
    //if not lerping, return false
    if (!lerping_)
      return false;

    lerpTimer_ += Time.deltaTime;
    if (lerpTimer_ > lerpDuration_)
      lerpTimer_ = lerpDuration_;

    lerpFunction_();
    if(lerpTimer_ >= lerpDuration_)
    {
      currentVec_ = transform.localPosition;
      currentQuat_ = transform.localRotation;
      lerping_ = false;
      Endfunc_();
    }
    return true;
  }
}
