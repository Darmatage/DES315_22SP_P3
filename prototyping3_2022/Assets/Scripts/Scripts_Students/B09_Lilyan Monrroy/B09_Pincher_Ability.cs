using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class B09_Pincher_Ability : MonoBehaviour
{
    public GameObject pincher_left;
    public GameObject pincher_right;
    public GameObject saw;
    [SerializeField] private Animator sawBlade;
    private GameObject grabbedObject;
    private float thrustAmount = 1.85f;

    private Transform grabbedObjecParentTransform;


    //grab axis from parent object
    private string button1;
    private string button2;
    private string button3;
    private string button4;

    // Start is called before the first frame update
    void Start()
    {
        button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
        button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
        button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;

    }

    // Update is called once per frame
    void Update()
    {
        B09_Pincher_Object left = pincher_left.GetComponent<B09_Pincher_Object>();
        B09_Pincher_Object right = pincher_right.GetComponent<B09_Pincher_Object>();

        if (left == null || right == null)
            return;

        if (left.grabbedObject == null && right.grabbedObject == null)
        {
            ResetGrabbedObject();
        }

        //Grab Mechanic
        if (Input.GetButton(button1))
        {
            ResetGrabbedObject();

            left.open();
            right.open();

        }
        else
        {
            if ((left.grabbedObject != null && left.state == B09_Pincher_Object.PincherState.Close) && (right.grabbedObject != null && right.state == B09_Pincher_Object.PincherState.Close))
            {
                sawBlade.SetBool("Spinning", true);

                if (grabbedObject == null)
                {
                    Debug.Log("Copied changed transform");
                    grabbedObject = left.grabbedObject;
                    grabbedObjecParentTransform = grabbedObject.transform.parent;

                    saw.transform.Translate(0, thrustAmount, 0);
                }

                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                grabbedObject.transform.SetParent(transform);

            }

            left.close();
            right.close();
        }
    }

    private void ResetGrabbedObject()
    {
        if (grabbedObject)
        {
            Debug.Log("Set parent transform");
            grabbedObject.transform.SetParent(grabbedObjecParentTransform);
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObjecParentTransform = null;
            grabbedObject = null;

            saw.transform.Translate(0,  -1 * thrustAmount,0);
        }

        sawBlade.SetBool("Spinning", false);

    }
}
