using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAttack_KelsonWysocki : MonoBehaviour
{
    public GameObject laser;
    private ParticleSystem laserEffect;
    public float range;
    public float damage;
    public float duration;

    public string button;

    private bool shooting = false;

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
        if (Input.GetButtonDown(button) && !shooting)
        {
            laserEffect.Play();
            Invoke(nameof(EndAttack), duration - 3.5f);
            shooting = true;
        }
    }

    void EndAttack()
    {
        laserEffect.Stop();
        shooting = false;
    }
}
