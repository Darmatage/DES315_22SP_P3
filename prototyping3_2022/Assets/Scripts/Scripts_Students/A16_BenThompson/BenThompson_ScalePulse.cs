using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenThompson_ScalePulse : MonoBehaviour
{
    private bool upPulse = true;
    private float pulseTimer = 0.0f;

    [SerializeField]
    private float totalPulseTime;

    [SerializeField]
    private Material downPulseColor;

    [SerializeField]
    private Material upPulseColor;

    [SerializeField]
    private MeshRenderer meshRend;

    [SerializeField]
    private BenThompson_Weapons weapon;

    [SerializeField]
    private float waitTime = 1.0f;
    private float waitTimer = 0.0f;

    [SerializeField]
    ParticleSystem particles;


    // Start is called before the first frame update
    void Start()
    {
        pulseTimer = totalPulseTime / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(weapon.GetMineTime() > 0.0f)
        {
            // Set the material to the down pulse color
            meshRend.material = downPulseColor;

            // On exit we will be in the upPulse phase
            upPulse = true;

            pulseTimer = totalPulseTime / 2;

            waitTimer = 0.0f;

            particles.Stop();
        }
        else
        {
            if(waitTimer > 0.0f)
            {
                waitTimer -= Time.deltaTime;
                return;
            }

            if (upPulse)
            {
                meshRend.material.Lerp(downPulseColor, upPulseColor, 1 - pulseTimer / (totalPulseTime / 2));

                particles.startColor = meshRend.material.color;
                particles.Play();

                pulseTimer -= Time.deltaTime;

                if (pulseTimer <= 0.0f)
                {
                    pulseTimer = totalPulseTime / 2;
                    upPulse = false;
                    waitTimer = waitTime;
                }
            }
            else
            {
                meshRend.material.Lerp(upPulseColor, downPulseColor, 1 - pulseTimer / (totalPulseTime / 2));
                particles.startColor = meshRend.material.color;
                particles.Play();

                pulseTimer -= Time.deltaTime;

                if (pulseTimer <= 0.0f)
                {
                    pulseTimer = totalPulseTime / 2;
                    upPulse = true;
                    waitTimer = waitTime;
                }
            }
        }
    }
}
