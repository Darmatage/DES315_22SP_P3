using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class B03_AnimationSettings
{
    public static int IsIdle;
    public static int IdleBow;
    public static int IdleSheild;
    public static int IdleKnives;

    public static int IsWalking;
    public static int WalkingBow;
    public static int WalkingSheild;
    public static int WalkingKnives;

    public static int BowAttack;
    public static int SheildDefend;

    public static int DrawBow;
    public static int DrawSheild;
    public static int DrawKnives;

    static B03_AnimationSettings()
    {
        IsIdle = Animator.StringToHash("IsIdle");
        IdleBow = Animator.StringToHash("IdleBow");
        IdleSheild = Animator.StringToHash("IdleSheild");
        IdleKnives = Animator.StringToHash("IdleKnives");

        IsWalking = Animator.StringToHash("IsWalking");
        WalkingBow = Animator.StringToHash("WalkingBow");
        WalkingSheild = Animator.StringToHash("WalkingSheild");
        WalkingKnives = Animator.StringToHash("WalkingKnives");

        BowAttack = Animator.StringToHash("BowAttack");
        SheildDefend = Animator.StringToHash("SheildDefend");

        DrawBow = Animator.StringToHash("DrawBow");
        DrawSheild = Animator.StringToHash("DrawSheild");
        DrawKnives = Animator.StringToHash("DrawKnives");
    }
}
