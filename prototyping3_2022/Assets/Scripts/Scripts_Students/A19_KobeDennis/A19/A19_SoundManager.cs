using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A19_SoundManager : MonoBehaviour
{
    public AudioClip groundSlam;
    public AudioClip spin;

    public AudioSource audioSource;
    
    // Random pitch adjustment range.
    public float LowPitchRange = .95f;
    public float HighPitchRange = 1.05f;
    public void PlayGroundSlamEffect()
    {
        float randomPitch = Random.Range(LowPitchRange, HighPitchRange);

        audioSource.pitch = randomPitch;
        audioSource.PlayOneShot(groundSlam);
    }
    public void PlaySpinEffect()
    {
        float randomPitch = Random.Range(LowPitchRange, HighPitchRange);

        audioSource.pitch = randomPitch;
        audioSource.clip = spin;
        audioSource.Play(0);
    }
}
