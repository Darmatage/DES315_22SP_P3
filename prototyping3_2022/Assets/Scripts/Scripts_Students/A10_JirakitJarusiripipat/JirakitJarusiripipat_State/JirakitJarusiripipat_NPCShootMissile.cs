using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JirakitJarusiripipat_NPCShootMissile : JirakitJarusiripipat_IState
{
    private JirakitJarusiripipat_Weapon weapon;
    public JirakitJarusiripipat_NPCShootMissile(JirakitJarusiripipat_Weapon missile)
    {
        weapon = missile;
    }
    public void OnEnter()
    {
        weapon.ShootMissile();
        Debug.Log("Enter ShootMissileState");
    }

    public void OnExit()
    {
        Debug.Log("Exit ShootMissileState");
    }

    public void Tick()
    {
        Debug.Log("In ShootMissileState");
    }
}
