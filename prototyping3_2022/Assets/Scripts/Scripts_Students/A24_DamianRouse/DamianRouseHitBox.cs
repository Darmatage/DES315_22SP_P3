using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamianRouseHitBox : MonoBehaviour
{
  public GameObject weapon_;
  private DamianRouseWeapon weaponScript_;
  private HazardDamage hazardScript_;

  // Start is called before the first frame update
  void Start()
  {
    weaponScript_ = weapon_.GetComponent<DamianRouseWeapon>();
    hazardScript_ = gameObject.GetComponent<HazardDamage>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.GetComponent< BotBasic_Damage>()!= null)
      weaponScript_.Hit(gameObject, hazardScript_);
  }
}
