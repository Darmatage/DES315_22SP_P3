using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B09_Pincher_Object : MonoBehaviour
{
    [SerializeField] GameObject parent;
    public GameObject grabbedObject;
    private Vector3 originalPincherPos;
    private Quaternion originalPincherRot;
    public enum PincherState
    {
        Idle,Open,Close
    }

    public PincherState state;

    // Start is called before the first frame update
    void Start()
    {
        grabbedObject = null;
        originalPincherPos = transform.localPosition;
        originalPincherRot = transform.localRotation;
        state = PincherState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition == originalPincherPos)
        {
            state = PincherState.Idle;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject player2 = collision.gameObject;

        if (player2 != parent)
        {
            if (player2.GetComponent<BotBasic_Move>())
            {
                grabbedObject = player2;
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
                grabbedObject = null;
            }
        }
    }

    public void open()
    {
        Vector3 openPos = new Vector3(originalPincherPos.x * 2.0f, originalPincherPos.y, originalPincherPos.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, openPos, 0.9f);
        transform.localRotation = originalPincherRot;

        state = PincherState.Open;

    }

    public void close()
    {
        if (PincherState.Open == state)
        {
            state = PincherState.Close;
        }
        else
        {
            if (PincherState.Idle == state)
            {
                return;
            }
        }

        if(grabbedObject)
            return;

        transform.localPosition = Vector3.Lerp(transform.localPosition, originalPincherPos, 0.9f);
        transform.localRotation = originalPincherRot;


    }
}
