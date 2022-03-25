using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineAttack : MonoBehaviour
{
    public float fuse_time = 5;
    public int aoe_damage = 5;
    public int direct_damage = 10;
    public GameObject explosion_prefab;
    public int player_number = 0;

    private float countdown_timer = 0;
    private bool explosion_alive = false;
    private float explosion_lifetime = 0.1f;
    private GameObject explosion_object;

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
            Explode();

        if (explosion_alive)
        {
            explosion_lifetime -= Time.deltaTime;

            if (explosion_lifetime <= 0.0f)
                Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (explosion_alive == false)
        {
            GameObject explosion_object = Instantiate(explosion_prefab, this.transform.position, Quaternion.identity);
            explosion_object.transform.parent = this.transform;

            GameObject mine_light = this.transform.GetChild(0).gameObject;
            Renderer mine_light_renderer = (Renderer)mine_light.GetComponent(typeof(Renderer));
            mine_light_renderer.enabled = false;

            Renderer mine_renderer = (Renderer)this.GetComponent(typeof(Renderer));
            mine_renderer.enabled = false;
            explosion_alive = true;
        }
    }

    void Explode()
    {
        if (explosion_alive == false)
        {
            GameObject explosion_object = Instantiate(explosion_prefab, this.transform.position, Quaternion.identity);
            explosion_object.transform.parent = this.transform;

            GameObject mine_light = this.transform.GetChild(0).gameObject;
            Renderer mine_light_renderer = (Renderer)mine_light.GetComponent(typeof(Renderer));
            mine_light_renderer.enabled = false;

            Renderer mine_renderer = (Renderer)this.GetComponent(typeof(Renderer));
            mine_renderer.enabled = false;
            explosion_alive = true;
        }


    }
}

