using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamianRouseHitBox : MonoBehaviour
{
  public GameObject weapon_;
  private DamianRouseWeapon weaponScript_;

    // Start is called before the first frame update
    void Start()
    {
    weaponScript_ = weapon_.GetComponent<DamianRouseWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  void OnCollisionEnter(Collision other)
  {
    weaponScript_.Hit();
  }
}
