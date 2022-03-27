using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamianRouseWeapon : MonoBehaviour
{
  public GameObject forward_;
  public GameObject spearTip_;
  private GameObject owner_;
  public bool lunging;

  private int hitsLeft_;

  public float moveOneRange_ = 4f;
  public float moveOneSpeed_ = 4f;
  private float timer_;
  private bool moveOneActive_ = false;
  private float moveOneTimer_;
  private bool moveOneReturning_ = false;

  private Vector3 start_;

  //public Animator stab_;

  // Start is called before the first frame update
  void Start()
  {
    start_ = transform.localPosition;
  }

  // Update is called once per frame
  void Update()
  {
    MoveOneUpdate();
    /*
     float botMove = Input.GetAxisRaw(pVertical) * moveSpeed * Time.deltaTime;
		float botRotate = Input.GetAxisRaw(pHorizontal) * rotateSpeed * Time.deltaTime;
		
		if (isGrabbed == false){
			transform.Translate(0,0, botMove);
			transform.Rotate(0, botRotate, 0);
		}
     */

    if (lunging)
    {
      GetComponent<Rigidbody>().AddForce(transform.forward * 200, ForceMode.Impulse);
      lunging = false;
    }
  }

  public void MoveOneUpdate()
  {
    if (!moveOneActive_)
      return;

    if (!moveOneReturning_)
    {
      moveOneTimer_ += Time.deltaTime;
      if (timer_ < moveOneTimer_)
      {
        moveOneTimer_ = timer_;
        moveOneReturning_ = true;
      }
    }
    else
    {
      moveOneTimer_ -= Time.deltaTime;
      if (moveOneTimer_ < 0)
      {
        moveOneTimer_ = 0;
        moveOneActive_ = false;
        moveOneReturning_ = false;
      }
    }

    transform.localPosition = Vector3.Lerp(start_, (forward_.transform.localPosition - spearTip_.transform.localPosition) * moveOneRange_ + start_, moveOneTimer_ / timer_);

    if (!moveOneActive_)
    {
      moveOneTimer_ = 0;
      moveOneActive_ = false;
      ClearHits();
      owner_.GetComponent<DamianRouseManager>().FinishMove();
    }
  }

  public void TransferData(GameObject owner)
  {
    owner_ = owner;
  }

  public void Act(int move)
  {
    if (move == 1)
      MoveOne();
    else
      owner_.GetComponent<DamianRouseManager>().isAttacking = false;
  }

  public void MoveOne()
  {
    AddHits(1);
    moveOneActive_ = true;
    moveOneTimer_ = 0;
    timer_ = moveOneSpeed_;
    //stab_.enabled = true;
  }

  public void AddHits(int hits)
  {
    hitsLeft_ += hits;
    spearTip_.GetComponent<BoxCollider>().enabled = true;
  }

  public void Hit()
  {
    --hitsLeft_;

    if (hitsLeft_ < 1)
      spearTip_.GetComponent<BoxCollider>().enabled = false;
  }

  public void ClearHits()
  {
    hitsLeft_ = 0;
    spearTip_.GetComponent<BoxCollider>().enabled = false;
  }
}
