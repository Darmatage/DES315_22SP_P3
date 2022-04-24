using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B03_Feedback : MonoBehaviour
{
    [SerializeField] ParticleSystem footstepParticles;
    [SerializeField] bool isFootstepParticlesOn;
    [SerializeField] AudioSource footstepAudio;
    [SerializeField] bool isFootstepAudioOn;
    [SerializeField] ParticleSystem sheildParticles;
    [SerializeField] bool isSheildDefendParticlesOn;
    [SerializeField] AudioSource sheildAudio;
    [SerializeField] bool isSheildDefendSoundOn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SheildDefend()
    {
        if (isSheildDefendParticlesOn && sheildParticles != null) sheildParticles.Play();
        if (isSheildDefendSoundOn && sheildAudio != null) sheildAudio.Play();
    }

    void AnimationFootstep()
    {
        if (isFootstepParticlesOn && footstepParticles != null) footstepParticles.Play();
        if (isFootstepAudioOn && footstepAudio != null) footstepAudio.Play();
    }
}
