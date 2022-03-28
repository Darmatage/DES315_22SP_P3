using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B09_Pincher_Object : MonoBehaviour
{
    [SerializeField] GameObject parent;
    public GameObject grabbedObject;
    [SerializeField] float openVelocity;
    [SerializeField] float closeVelocity;
    private Vector3 originalPincherPos;
    private Quaternion originalPincherRot;
    public Vector3 ContactPoint;


    // Start is called before the first frame update
    void Start()
    {
        grabbedObject = null;
        originalPincherPos = transform.localPosition;
        originalPincherRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject player2 = collision.gameObject;

        if (player2 != parent)
        {
            if (player2.GetComponent<BotBasic_Move>())
            {
                grabbedObject = player2;
                player2.GetComponent<BotBasic_Move>().isGrabbed = true;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        GameObject player2 = collision.gameObject;

        if (player2 != parent)
        {
            if (player2.GetComponent<BotBasic_Move>())
            {
                player2.GetComponent<BotBasic_Move>().isGrabbed = false;
                grabbedObject = null;
            }
        }
    }

    public void open()
    {
        Vector3 openPos = new Vector3(originalPincherPos.x * 2.0f, originalPincherPos.y, originalPincherPos.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, openPos, 0.9f);
        transform.localRotation = originalPincherRot;
    }

    public void close()
    {
        if(grabbedObject)
            return;

        transform.localPosition = Vector3.Lerp(transform.localPosition, originalPincherPos, 0.9f);
        transform.localRotation = originalPincherRot;
    }
}
