using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyungseobKim_Cannon : MonoBehaviour
{
    public Transform launchPosition;
    public GameObject bulletPrefab;
    private HyungseobKim_Bullet bullet;

    [HideInInspector]
    public string button1;
    [HideInInspector]
    public string button2;
    [HideInInspector]
    public string button3;
    [HideInInspector]
    public string button4;

    [HideInInspector]
    public BotBasic_Damage enemy;

    // Start is called before the first frame update
    void Start()
    {
        button1 = gameObject.transform.parent.parent.GetComponent<playerParent>().action1Input;
        button2 = gameObject.transform.parent.parent.GetComponent<playerParent>().action2Input;
        button3 = gameObject.transform.parent.parent.GetComponent<playerParent>().action3Input;
        button4 = gameObject.transform.parent.parent.GetComponent<playerParent>().action4Input;

        var bots = FindObjectsOfType<BotBasic_Damage>();
        foreach(BotBasic_Damage bot in bots)
        {
            if (bot.gameObject != transform.parent.gameObject)
            {
                enemy = bot;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(button1))
        {
            LaunchBullet();
        }
    }

    private void LaunchBullet()
    {
        // If there is a bullet already launched.
        if (bullet != null)
        {
            // If it is still moving,
            if (bullet.moving)
            {
                // cancel launching.
                return;
            }
            else
            {
                bullet.DestroyBullet();
            }
        }

        // Spawn a bullet.
        bullet = Object.Instantiate(bulletPrefab, launchPosition.position, Quaternion.identity).GetComponent<HyungseobKim_Bullet>();

        // Initialize the bullet.
        bullet.Initialize(this, transform.parent.transform.forward);
    }
}
