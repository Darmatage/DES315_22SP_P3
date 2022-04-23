using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingAttack : MonoBehaviour
{
    //public GameObject target;
    public Rigidbody attack_body;
    public Collider attack_collider;
    public float fuse_time = 5.0f;

    public GameObject explosion_prefab;

    public float turn_speed = 1.0f;
    public float move_speed = 5.0f;

    private Transform local_transform;
    private GameObject parent;

    private bool explosion_alive = false;
    private GameObject target;
    private bool no_target = false;
    private float explosion_lifetime = 0.1f;
    private string player_tag;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
        local_transform = GetComponent<Transform>();

        GameObject[] targets = new GameObject[1];

        GameObject test = this.transform.parent.gameObject;
        GameObject test2 = this.transform.parent.transform.parent.gameObject;


        if (this.transform.parent.transform.parent.tag == "Player1")
        {
            targets = GameObject.FindGameObjectsWithTag("Player2");
            player_tag = "Player1";
        }

        else if (this.transform.parent.transform.parent.tag == "Player2")
        {
            targets = GameObject.FindGameObjectsWithTag("Player1");
            player_tag = "Player2";
        }



        target = targets[0].transform.GetChild(0).gameObject;

        if (!target)
        {
            Debug.Log("Please set the tracking Target");
            no_target = true;
        }

        this.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!attack_body)
            return;

        timer += Time.deltaTime;

        if (timer >= fuse_time)
            Explode();

        attack_body.velocity = local_transform.forward * move_speed;


        if (target)
        {
            var target_rotation = Quaternion.LookRotation(target.transform.position - local_transform.position);
            attack_body.MoveRotation(Quaternion.RotateTowards(local_transform.rotation, target_rotation, turn_speed));
        }



        if (explosion_alive == true)
        {
            explosion_lifetime -= Time.deltaTime;

            if (explosion_lifetime <= 0.0f)
                Destroy(this.gameObject);


        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (player_tag == "Player1")
        {
            if (collision.gameObject.CompareTag("Player2"))
            {
                if (explosion_alive == false)
                {
                    GameObject explosion_object = Instantiate(explosion_prefab, this.transform.position, Quaternion.identity);
                    // explosion_collider = explosion_object.GetComponent<Collider>();
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

        else if (player_tag == "Player2")
        {
            if (collision.gameObject.CompareTag("Player1"))
            {
                if (explosion_alive == false)
                {
                    GameObject explosion_object = Instantiate(explosion_prefab, this.transform.position, Quaternion.identity);
                    // explosion_collider = explosion_object.GetComponent<Collider>();
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
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (player_tag == "Player1")
        {
            if (other.gameObject.CompareTag("Player2"))
            {
                if (explosion_alive == false)
                {
                    GameObject explosion_object = Instantiate(explosion_prefab, this.transform.position, Quaternion.identity);
                    // explosion_collider = explosion_object.GetComponent<Collider>();
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

        else if (player_tag == "Player2")
        {
            if (other.gameObject.CompareTag("Player1"))
            {
                if (explosion_alive == false)
                {
                    GameObject explosion_object = Instantiate(explosion_prefab, this.transform.position, Quaternion.identity);
                    // explosion_collider = explosion_object.GetComponent<Collider>();
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
    }*/

    /*private void OnCollisionEnter(Collision collision)
    {
        if (player_tag == "Player1")
        {
            if (collision.gameObject.CompareTag("Player2"))
            {
                if (explosion_alive == false)
                {
                    GameObject explosion_object = Instantiate(explosion_prefab, this.transform.position, Quaternion.identity);
                    // explosion_collider = explosion_object.GetComponent<Collider>();
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

        else if (player_tag == "Player2")
        {
            if (collision.gameObject.CompareTag("Player1"))
            {
                if (explosion_alive == false)
                {
                    GameObject explosion_object = Instantiate(explosion_prefab, this.transform.position, Quaternion.identity);
                    // explosion_collider = explosion_object.GetComponent<Collider>();
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

    }*/

    void Explode()
    {
        if (explosion_alive == false)
        {
            GameObject explosion_object = Instantiate(explosion_prefab, this.transform.position, Quaternion.identity);
            // explosion_collider = explosion_object.GetComponent<Collider>();
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

