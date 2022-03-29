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
    [SerializeField]
    private AudioSource countdownSound;
    [SerializeField]
    private AudioSource botExplosion;
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
        if(!botMovingSound.isPlaying)
        {
            botMovingSound.Play();
        }
    }
    public void StopBotMoving()
    {
        if (botMovingSound.isPlaying)
        {
            botMovingSound.Stop();
        }
    }
    public void PlayCountdownSound()
    {
        countdownSound.Play();
    }
    public void StopCountdownSound()
    {
        countdownSound.Stop();
    }
    public void PlayBotExplosion()
    {
        botExplosion.Play();
    }
}
