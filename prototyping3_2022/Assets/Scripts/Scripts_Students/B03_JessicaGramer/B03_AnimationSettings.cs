using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class B03_AnimationSettings
{

    public static int IsWalking;
    public static int Jump;
    public static int Idle;

    public static int SheildDefend;

    public static int DrawBow;

    public static int Sheild;
    public static int Daggers;

    static B03_AnimationSettings()
    {
        IsWalking = Animator.StringToHash("IsWalking");
        Jump = Animator.StringToHash("Jump");
        Idle = Animator.StringToHash("Idle");

        SheildDefend = Animator.StringToHash("SheildDefend");

        DrawBow = Animator.StringToHash("DrawBow");
        Sheild = Animator.StringToHash("Sheild");
        Daggers = Animator.StringToHash("Daggers");
    }
}
