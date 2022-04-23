using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A8_KnifeBlade : MonoBehaviour
{
  public GameObject start_point;
  public GameObject end_point;
  public GameObject bot;

  public float active_time;
  public float cooldown_time;
  private string activation_key;

  private float active_timer_ = 0;
  private float cooldown_timer_ = 0;
  private Vector3 local_start_pos_;
  // Start is called before the first frame update
  void Start()
  {
    activation_key = bot.GetComponentInParent<playerParent>().action2Input;
    local_start_pos_ = transform.localPosition;
    ChildrenSetActive(false);
  }

  // Update is called once per frame
  void Update()
  {
    if(cooldown_timer_ == 0 && Input.GetButtonDown(activation_key))
    {
      cooldown_timer_ = active_time + cooldown_time;
      active_timer_ = active_time;
      ChildrenSetActive(true);
    }

    if(active_timer_ > 0)
    {
      UpdateKnifeMovement();
    }

    if(cooldown_timer_ > 0)
    {
      UpdateCooldown();
    }
  }

  void UpdateKnifeMovement()
  {
    active_timer_ -= Time.deltaTime;

    if(active_timer_ <= 0)
    {
      transform.localPosition = local_start_pos_;
      active_timer_ = 0;
      ChildrenSetActive(false);
    }
    else
    {
      transform.position = transform.position + (end_point.transform.position - start_point.transform.position) * (Time.deltaTime / active_time);
    }
  }

  void UpdateCooldown()
  {
    cooldown_timer_ -= Time.deltaTime;

    if (cooldown_timer_ < 0)
    {
      cooldown_timer_ = 0;
    }
  }

  void ChildrenSetActive(bool active)
  {
    foreach(Transform child in transform)
    {
      child.gameObject.SetActive(active);
    }
  }
}
