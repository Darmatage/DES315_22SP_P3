using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JasonHunt_SFX : MonoBehaviour
{
    public AudioSource HitWall;
    public AudioSource HitEnemy;
    public AudioSource Shoot;
    public AudioSource Return;


    public void PlayHitWall()
    {
        HitWall.Play();
    }
    public void PlayHitEnemy()
    {
        HitEnemy.Play();
    }

    public void PlayShoot()
    {
        Shoot.Play();
    }

    public void PlayReturn()
    {
        Return.Play();
    }
}

