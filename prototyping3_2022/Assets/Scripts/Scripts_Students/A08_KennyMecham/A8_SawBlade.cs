using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A8_SawBlade : MonoBehaviour
{
  public KeyCode activation_key;
  public GameObject pivot_object;
  public Vector3 pivot_axis;
  public float active_time;
  public float cooldown_time;
  public float rotation_amount;

  private float active_timer_;
  private float cooldown_timer_;
  private Vector3 starting_local_pos_;
  private Quaternion starting_local_rot_;
  // Start is called before the first frame update
  void Start()
  {
    starting_local_pos_ = transform.localPosition;
    starting_local_rot_ = transform.localRotation;
  }

  // Update is called once per frame
  void Update()
  {
    CheckForActivation();
    UpdateActiveTimer();
    UpdateCooldownTimer();
  }

  void CheckForActivation()
  {
    if (Input.GetKeyDown(activation_key) && cooldown_timer_ == 0)
    {
      active_timer_ = active_time;
      cooldown_timer_ = cooldown_time + active_time;
    }
  }  

  void UpdateActiveTimer()
  {
    if(active_timer_ > 0)
    {
      active_timer_ -= Time.deltaTime;

      // reset the saw to it's original position and rotation
      if(active_timer_ <= 0)
      {
        transform.localPosition = starting_local_pos_;
        transform.localRotation = starting_local_rot_;
        active_timer_ = 0;
      }
      // update the saw position and rotation
      else
      {
        //transform.RotateAround(pivot_object.transform.position, pivot_axis, 1);
        transform.RotateAround(pivot_object.transform.position, pivot_axis, Time.deltaTime / active_time * rotation_amount);
      }
    }
  }

  void UpdateCooldownTimer()
  {
    if(cooldown_timer_ > 0)
    {
      cooldown_timer_ -= Time.deltaTime;

      if(cooldown_timer_ < 0)
      {
        cooldown_timer_ = 0;
      }
    }
  }
}
