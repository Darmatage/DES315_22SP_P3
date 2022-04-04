using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeanteJames_CoilBehavior : MonoBehaviour
{
    public bool planted = false;
    public const int maxCoils = 3;
    private Vector3 origPosition;
    static HashSet<GameObject> coilHolder = new HashSet<GameObject>();
    public GameObject line;

    // adjacency matrix
    public int[,] attachedCoils = new int[maxCoils, maxCoils];
    private int ID = 0;

    // -1 no attached coil if it is a valid ID then it is being attached to something
    public int newCoilAttached = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (newCoilAttached != -1)
        {
            List<GameObject> coils = coilHolder.ToList();
            //Vector3 avgPos = gameObject.transform.position;
            //GameObject arc = GameObject.Instantiate(line, avgPos, Quaternion.identity);
            //List<Vector2> points = new List<Vector2>();
            //points.Add(gameObject.transform.position);
            //points.Add(coils[newCoilAttached].transform.position);
            //arc.GetComponent<EdgeCollider2D>().SetPoints(points);

            coils[newCoilAttached].GetComponent<DeanteJames_CoilBehavior>().newCoilAttached = -1;
            newCoilAttached = -1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // make the planter able to pass through the planted coils
        if (planted)
        {
            origPosition = transform.position;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            if (!collision.gameObject.name.Contains("BotA21"))
                return;

            gameObject.GetComponent<Collider>().isTrigger = !(gameObject.GetComponent<Collider>().isTrigger);
        }

        if (!collision.gameObject.name.Contains("Ground"))
        { return; }

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.drag = 8.0f;
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

            gameObject.GetComponent<Collider>().isTrigger = !(gameObject.GetComponent<Collider>().isTrigger);
            transform.position = origPosition;
        }
    }

    public void AddCoil(GameObject coil)
    {
        coilHolder.Add(coil);

        // set IDs
        int count = 0;
        foreach (var item in coilHolder)
        {
            item.GetComponent<DeanteJames_CoilBehavior>().ID = count++;
        }
    }

    public void attachCoil(GameObject coil)
    {
        newCoilAttached = coil.GetComponent<DeanteJames_CoilBehavior>().ID;
        attachedCoils[ID, newCoilAttached] = 1;
    }
}
