using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeanteJames_Weapon1 : MonoBehaviour
{
    public GameObject coilToThrow;
    public float throwSpeed = 3f;

    private GameObject Throwable;
    // Buttons
    string button1;
    string button2;
    string button3;

    // Start is called before the first frame update
    void Start()
    {
        button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
        button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(button2))
        {
            Throw();
        }
    }

    void Throw()
    {
        Vector3 offset = new Vector3(0.0f, 1.25f, 0.5f);
        Throwable = GameObject.Instantiate(coilToThrow, gameObject.transform.position + offset, Quaternion.identity);

        // Add the coil to the main coil set to keep track of all the spawned coils
        Throwable.GetComponent<DeanteJames_CoilBehavior>().AddCoil(Throwable);
        Rigidbody rb = Throwable.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.velocity = (transform.forward + new Vector3(0.0f, 0.65f, 0.0f)) * throwSpeed;
    }
}
