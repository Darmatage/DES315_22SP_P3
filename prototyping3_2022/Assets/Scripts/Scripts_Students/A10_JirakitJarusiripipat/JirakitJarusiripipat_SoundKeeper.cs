using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JirakitJarusiripipat_SoundKeeper : MonoBehaviour
{
    [SerializeField]
    private AudioSource mainGunSound;
    [SerializeField]
    private AudioSource launcherSound;
    [SerializeField]
    private AudioSource missileHitSound;
    [SerializeField]
    private AudioSource explodeSound;
    [SerializeField]
    private AudioSource botMovingSound;
    
    public void PlayMainGun()
    {
        mainGunSound.Play();
    }
    public void PlayLauncher()
    {
        launcherSound.Play();
    }
    public void MissileHit()
    {
        missileHitSound.Play();
    }
    public void Explode()
    {
        explodeSound.Play();
    }
    public void BotMoving()
    {
        botMovingSound.Play();
    }
}
