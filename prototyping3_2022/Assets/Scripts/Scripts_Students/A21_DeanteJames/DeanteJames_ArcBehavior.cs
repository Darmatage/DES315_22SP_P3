using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeanteJames_ArcBehavior : MonoBehaviour
{
    public bool player1 = false;
    public GameObject musicManager;
    public AudioClip zap;

    // Start is called before the first frame update
    void Start()
    {
        musicManager = GameObject.Find("MusicManager");
        if (player1)
        {
            gameObject.GetComponent<HazardDamage>().isPlayer1Weapon = true;
        }
        else
        {
            gameObject.GetComponent<HazardDamage>().isPlayer2Weapon = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Make it so anything but the ground collisions is ignored for the
    // arc and the coil
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Ground"))
        { return; }
        if (musicManager && !collision.gameObject.name.Contains("Coil") && !collision.gameObject.name.Contains("BotA21"))
        {
            musicManager.GetComponent<AudioSource>().PlayOneShot(zap);
        }

        Physics.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider>());
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Transform child = null;
    //    if (other.transform.childCount > 0)
    //        child = other.transform.GetChild(0);

    //    if (!other.gameObject.name.Contains("BotA21"))
    //        return;

    //    if (child != null)
    //    {
    //        if (!other.transform.GetChild(0).name.Contains("BotA21"))
    //            return;
    //    }

    //    gameObject.GetComponent<Collider>().isTrigger = !(gameObject.GetComponent<Collider>().isTrigger);
    //    Physics.IgnoreCollision(other, gameObject.GetComponent<Collider>());
    //}
}
