using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeanteJames_CoilBehavior : MonoBehaviour
{
    public bool player1 = false;

    static private int oldestCoil = 0;
    private bool planted = false;
    private bool inRadius = false;
    public const int maxCoils = 3;

    // List of all the created coils in the game
    static List<GameObject> coilHolder = new List<GameObject>();
    public GameObject lr;

    // all the arcs that are spawned off of this coil
    private List<GameObject> arcSpawned = new List<GameObject>();
    // adjacency matrix
    static public int[,] attachedCoils = new int[maxCoils, maxCoils];
    private int ID;

    // -1 no attached coil if it is a valid ID then it is being attached to something
    public int newCoilAttached = -1;

    // Start is called before the first frame update
    void Start()
    {
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
        // try to attach this coil to another if the ID of another coil is given
        // Do not attempt a connection if the ID and new Coil being attached is the same
        // and only connect if the coil is within the radius of the other coil
        if (newCoilAttached != -1 && newCoilAttached != ID && inRadius)
        {
            // if one way is connected do not connect the other way
            if (attachedCoils[ID, newCoilAttached] == 1 || attachedCoils[newCoilAttached, ID] == 1)
                return;

            // only connect the coils if they are both tocuhing the ground

            if (coilHolder[newCoilAttached].GetComponent<DeanteJames_CoilBehavior>().planted == false
                || planted == false)
                return;


            Vector3 avgPos = (gameObject.transform.position + coilHolder[newCoilAttached].transform.position)/2.0f;
            avgPos += new Vector3(0.0f, 0.6f, 0.0f);
            // Then we have a z = z case
            // we ned to rotate on the z axis
            Vector3 dist = coilHolder[newCoilAttached].transform.position - gameObject.transform.position;
            Vector3 diff = transform.position - coilHolder[newCoilAttached].transform.position;
            GameObject spawned = null;
            if (diff.z + Mathf.Epsilon <= 4.0f || diff.z + Mathf.Epsilon >= -4.0f)
            {
                float angle = Mathf.Atan2(dist.x, dist.z) * Mathf.Rad2Deg;
                spawned = GameObject.Instantiate(lr, avgPos, Quaternion.AngleAxis(angle, Vector3.back));
                Vector3 newVec = spawned.transform.eulerAngles;
                newVec.x = 90.0f;
                spawned.transform.eulerAngles = newVec;

            }
            else
            {
                spawned = GameObject.Instantiate(lr, avgPos, Quaternion.AngleAxis(90.0f, Vector3.right));
            }

            // Make sure the arc that connects the two coils are known by the two coils
            arcSpawned.Add(spawned);
            coilHolder[newCoilAttached].GetComponent<DeanteJames_CoilBehavior>().arcSpawned.Add(spawned);

            Vector3 scale = spawned.transform.localScale;
            scale.y = dist.magnitude / 2.0f;
            spawned.transform.localScale = scale;

            // Once the two coils are attached we don't need to attach them
            // again from the other coil
            attachedCoils[ID, newCoilAttached] = 1;
            attachedCoils[newCoilAttached, ID] = 1;
            coilHolder[newCoilAttached].GetComponent<DeanteJames_CoilBehavior>().newCoilAttached = -1;
            newCoilAttached = -1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // make the planter able to pass through the planted coils
        if (planted)
        {
            //origPosition = transform.position;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().useGravity = false;

            if (collision.gameObject.name.Contains("BotA21") || collision.gameObject.name.Contains("Arc") || 
                collision.gameObject.transform.root.tag.Contains("Player"))
            {
                Physics.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider>());
            }

        }

        if (!collision.gameObject.name.Contains("Ground"))
        { return; }

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        rb.drag = 10.0f;
        planted = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (planted)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (!collision.gameObject.name.Contains("BotA21") || 
                !collision.transform.GetChild(0).name.Contains("BotA21"))
                return;

            //gameObject.GetComponent<Collider>().isTrigger = !(gameObject.GetComponent<Collider>().isTrigger);
        }
    }

    // Destroys all arcs attached to this coil
    public void OnDestroy()
    {
        foreach (var item in arcSpawned)
        {
            if (item != null)
            {
                GameObject.Destroy(item);
            }
        }
        arcSpawned.Clear();
    }

    public void AddCoil(GameObject coil)
    {
        if (maxCoils == coilHolder.Count)
        {
            GameObject.Destroy(coilHolder[oldestCoil]);
            coilHolder[oldestCoil] = coil;
            ResetAdjacencyMatrix(oldestCoil);
            ++oldestCoil;

            if (oldestCoil == 3)
            {
                oldestCoil = 0;
            }
        }
        else
            coilHolder.Add(coil);

        // set IDs
        int count = 0;
        foreach (var item in coilHolder)
        {
            if (item == null)
                continue;

            item.GetComponent<DeanteJames_CoilBehavior>().ID = count++;
        }
    }

    private static void ResetAdjacencyMatrix(int coil)
    {
        for (int i = 0; i < coilHolder.Count; ++i)
        {

            attachedCoils[coil, i] = -1;
            attachedCoils[i, coil] = -1;
        }
    }

    public void attachCoil(GameObject coil)
    {
        newCoilAttached = coil.GetComponent<DeanteJames_CoilBehavior>().ID;
    }

    public void detachCoil()
    {
        newCoilAttached = -1;
    }

    public bool areMaxCoilsPlanted()
    {
        if (maxCoils == coilHolder.Count)
            return true;

        return false;
    }

    public void setInRadius(bool rad)
    {
        inRadius = rad;
    }

    public bool isPlanted()
    {
        return planted;
    }
}
