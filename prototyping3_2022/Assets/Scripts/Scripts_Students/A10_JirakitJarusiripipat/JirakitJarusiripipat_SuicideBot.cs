using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class JirakitJarusiripipat_SuicideBot : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public Transform target;
    private Rigidbody rb;
    private bool stop = false;
    private bool oneTime = false;
    [SerializeField]
    private GameObject BlastExplosion;
    public JirakitJarusiripipat_SoundKeeper soundKeeper;
    [HideInInspector]
    public GameObject parent;
    [SerializeField]
    private AudioSource movingSound;
    [SerializeField]
    private AudioSource blastSound;
    [SerializeField]
    private AudioSource countdownSound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        soundKeeper = parent.GetComponentInChildren<JirakitJarusiripipat_SoundKeeper>();
    }
    private void OnDestroy()
    {
        movingSound.Stop();
        countdownSound.Stop();
        blastSound.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if(!stop && !oneTime)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            rb.MovePosition(pos);
            transform.LookAt(target);
            //movingSound.Play();
        }
        else if(stop && !oneTime)
        {
            movingSound.Stop();
            countdownSound.Play();

            oneTime = true;
            StartCoroutine(Boom());
        }
    }
    IEnumerator Boom()
    {
        yield return new WaitForSeconds(2.0f);
        Instantiate(BlastExplosion, transform.position, Quaternion.identity);
        soundKeeper.PlayBotExplosion();
        //blastSound.Play();
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == target.GetComponent<BotBasic_Damage>().gameObject)
        {
            stop = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hazard")
        {
            Destroy(gameObject);
        }
        //else if(other.gameObject.tag == target.gameObject.tag)
        //{
        //    stop = true;
        //}
    }
}
