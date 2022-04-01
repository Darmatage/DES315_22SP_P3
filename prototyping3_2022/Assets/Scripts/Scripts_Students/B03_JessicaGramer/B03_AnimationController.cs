using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class B03_AnimationController : MonoBehaviour
{
    [SerializeField] B03Bot_Weapon weapon;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void BowFire()
    {
        weapon.AnimationEventActivateBow();
    }

    public void SheildDefend()
    {
        if (!animator) return;

        animator.SetBool(B03_AnimationSettings.Sheild, false);
    }

    public void WeaponEvent(B03Bot_Weapon.Weapon weapon)
    {
        if (!animator) return;

        if(weapon == B03Bot_Weapon.Weapon.SHEILD)
        {
            animator.SetBool(B03_AnimationSettings.Sheild, true);
        }
        else if(weapon == B03Bot_Weapon.Weapon.BOW)
        {
            animator.SetTrigger(B03_AnimationSettings.DrawBow);
        }
        else if(weapon == B03Bot_Weapon.Weapon.KNIVES)
        {
            animator.SetBool(B03_AnimationSettings.Daggers, true);
        }
        else
        {
            animator.SetBool(B03_AnimationSettings.Daggers, false);
            animator.SetBool(B03_AnimationSettings.Sheild, false);
        }
    }

    public void MovementEvent(float distance)
    {
        if (!animator) return;

        if (distance != 0) animator.SetBool(B03_AnimationSettings.IsWalking, true);
        else animator.SetBool(B03_AnimationSettings.IsWalking, false);
    }
}
