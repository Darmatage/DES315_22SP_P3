using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaiKawashima_NoDamageWhenNotMoving : MonoBehaviour
{
  private HazardDamage damage;
  private Rigidbody rb;
  // Start is called before the first frame update
  void Start()
  {
    damage = gameObject.GetComponent<HazardDamage>();
    rb = gameObject.GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void Update()
  {
    if (rb.velocity == Vector3.zero) damage.enabled = false;
  }
}
