using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class B09_Pincher_Ability : MonoBehaviour
{
    public GameObject pincher_left;
    public GameObject pincher_right;
    public GameObject grabbedObject;


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

        Debug.Log("Rotation of player: " + transform.eulerAngles);

        //Grab Mechanic
        if (Input.GetButton(button1))
        {
            left.open();
            right.open();
        }
        else
        {
            if (left.grabbedObject && right.grabbedObject)
            {
                GameObject player2 = left.grabbedObject;
                player2.GetComponent<Rigidbody>().isKinematic = true;
                Vector3 dist = transform.localPosition - left.ContactPoint;

                player2.transform.localPosition = new Vector3(transform.position.x, player2.transform.localPosition.y, transform.position.z);
            }
            left.close();
            right.close();
        }
    }
}
