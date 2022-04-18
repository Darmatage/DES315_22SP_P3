using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreddyMartin_HammerSwing : MonoBehaviour
{
    public GameObject Hammer;
    public ParticleSystem SlamParticles;
    public ParticleSystem TrailParticles;
    public float CooldownTime = 2f;
    public float SlamTime = 0.5f;
    public float RecoverTime = 0.75f;

    [HideInInspector]
    public string attackButton;

    AudioSource AS;
    float cooldownTimer = 0f;
    float slamTimer = 0f;
    float recoverTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();

        attackButton = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (cooldownTimer <= 0f && Input.GetButtonDown(attackButton))
        {
            cooldownTimer = CooldownTime;
            slamTimer = SlamTime;
            recoverTimer = 0f;

            if (TrailParticles)
            {
                TrailParticles.Play();
            }
        }

        if (slamTimer > 0f)
        {
            slamTimer -= Time.deltaTime;

            float angle = Mathf.Cos((slamTimer / SlamTime) * (Mathf.PI / 2f)) * 90f;
            Hammer.transform.localRotation = Quaternion.Euler(angle, 0f, 0f);

            if (slamTimer <= 0f)
            {
                recoverTimer = RecoverTime;

                AS.Play();

                if (SlamParticles)
                {
                    SlamParticles.Play();
                }
            }
        }

        if (recoverTimer > 0f)
        {
            recoverTimer -= Time.deltaTime;

            float angle = 90f - ((Mathf.Cos((recoverTimer / RecoverTime) * Mathf.PI) + 1f) * 45f);
            Hammer.transform.localRotation = Quaternion.Euler(angle, 0f, 0f);
        }
    }
}
