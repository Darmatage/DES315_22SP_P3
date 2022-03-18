using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineAttack : MonoBehaviour
{
    public float fuse_time = 5;
    public int aoe_damage = 5;
    public int direct_damage = 10;

    private float countdown_timer = 0;
    private bool explosion_alive = false;
    private float explosion_lifetime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        countdown_timer = fuse_time;
    }

    // Update is called once per frame
    void Update()
    {
        countdown_timer -= Time.deltaTime;

        if (countdown_timer <= 0.0f)
        {
            Explode();
        }

        if (explosion_alive)
        {
            explosion_lifetime -= Time.deltaTime;

            if (explosion_lifetime <= 0.0f)
            {
                GameObject explosion = this.transform.GetChild(1).gameObject;
                Renderer explosion_renderer = (Renderer)explosion.GetComponent(typeof(Renderer));
                Color explosion_color = explosion_renderer.material.color;
                Color test = explosion_color;
                test.a = 0.0f;
                explosion_renderer.material.color = test;


                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject mine_light = this.transform.GetChild(0).gameObject;
        Renderer mine_light_renderer = (Renderer)mine_light.GetComponent(typeof(Renderer));
        mine_light_renderer.enabled = false;

        GameObject explosion = this.transform.GetChild(1).gameObject;
        Renderer explosion_renderer = (Renderer)explosion.GetComponent(typeof(Renderer));
        Color explosion_color = explosion_renderer.material.color;

        Renderer mine_renderer = (Renderer)this.GetComponent(typeof(Renderer));

        mine_renderer.enabled = false;
        Color test = explosion_color;
        test.a = 0.75f;
        explosion_renderer.material.color = test;

        explosion_alive = true;
    }

    void Explode()
    {
        GameObject mine_light = this.transform.GetChild(0).gameObject;
        Renderer mine_light_renderer = (Renderer)mine_light.GetComponent(typeof(Renderer));
        mine_light_renderer.enabled = false;

        GameObject explosion = this.transform.GetChild(1).gameObject;
        Renderer explosion_renderer = (Renderer)explosion.GetComponent(typeof(Renderer));
        Color explosion_color = explosion_renderer.material.color;

        Renderer mine_renderer = (Renderer) this.GetComponent(typeof(Renderer));

        mine_renderer.enabled = false;
        Color test = explosion_color;
        test.a = 0.75f;
        explosion_renderer.material.color = test;




    }
}

