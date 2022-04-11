using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform d20;
    [SerializeField] HazardDamage melee;
    [SerializeField] MeshRenderer renderer;
    [SerializeField] GameObject tree;
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> clips;
    [SerializeField] ParticleSystem particleSystem;
    ScreenShake shake;

    [SerializeField]enum Clips
    {
        noAction,
        treeGrow
    }

    float timeLeft;
    Color targetColor;
    float attackTimer;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
        shake = GetComponent<ScreenShake>();
    }

    void PlayClip(Clips clip)
    {
        audioSource.clip = clips[(int)clip];
        audioSource.Play();
    }

    void PerformAttack()
    {
        if (renderer.material.color.g > renderer.material.color.r && renderer.material.color.g > renderer.material.color.b)
        {
            //spawn tree
            Instantiate(tree, transform.position + transform.forward * 4 + transform.up * 3, new Quaternion());
            PlayClip(Clips.treeGrow);
            attackTimer = 1;
            return;
        }
        else if (renderer.material.color.r > renderer.material.color.g && renderer.material.color.r > renderer.material.color.b)
        {
            particleSystem.Play();
            shake.TriggerShake(0.3f);
            attackTimer = 1;
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= Time.deltaTime)
        {
            // transition complete
            // assign the target color
            renderer.material.color = targetColor;

            // start a new transition
            targetColor = new Color(Random.value, Random.value, Random.value);
            timeLeft = 1.0f;
        }
        else
        {
            // transition in progress
            // calculate interpolated color
            renderer.material.color = Color.Lerp(renderer.material.color, targetColor, Time.deltaTime / timeLeft);

            // update the timer
            timeLeft -= Time.deltaTime;
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (attackTimer < 0)
            {
                // int rand = Random.Range(0, 21);
                // melee.damage = 0.1f  *  rand;
                
                PerformAttack();
                
            }
            else
            {
                PlayClip(Clips.noAction);
            }


            
        }
        attackTimer -= Time.deltaTime;
        if (attackTimer < -1)
        {
            attackTimer = -1;
        }
    }
}
