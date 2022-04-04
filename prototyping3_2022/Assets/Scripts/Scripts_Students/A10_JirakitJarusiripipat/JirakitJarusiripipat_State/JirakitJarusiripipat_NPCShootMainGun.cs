using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JirakitJarusiripipat_NPCShootMainGun : JirakitJarusiripipat_IState
{
    JirakitJarusiripipat_Weapon mainGun;
    public JirakitJarusiripipat_NPCShootMainGun(JirakitJarusiripipat_Weapon weapon)
    {
        mainGun = weapon;
    }
    public void OnEnter()
    {
        mainGun.ShootMainGun();
        Debug.Log("Enter ShootMainGunState");
    }

    public void OnExit()
    {
        Debug.Log("Out ShootMainGunState");
    }

    public void Tick()
    {
        Debug.Log("In ShootMainGunState");
    }
}
