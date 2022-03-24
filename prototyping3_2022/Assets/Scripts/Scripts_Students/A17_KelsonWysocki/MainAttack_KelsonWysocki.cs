using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAttack_KelsonWysocki : MonoBehaviour
{
    public GameObject laser;
    public MainAttackDamage_KelsonWysocki hitbox;
    private ParticleSystem laserEffect;
    public float range;
    public float damage;
    public float duration;

    public string button;

    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        laserEffect = laser.GetComponent<ParticleSystem>();
        laserEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(button) && !hitbox.isShooting)
        {
            laserEffect.Play();
            Invoke(nameof(EndAttack), duration - laserEffect.main.startLifetime.constant);
            hitbox.isShooting = true;
        }
    }

    void EndAttack()
    {
        laserEffect.Stop();
        hitbox.isShooting = false;
    }
}
