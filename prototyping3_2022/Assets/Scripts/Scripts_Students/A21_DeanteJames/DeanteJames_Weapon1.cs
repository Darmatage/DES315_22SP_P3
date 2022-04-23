using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeanteJames_Weapon1 : MonoBehaviour
{
    // Prefabs set in editor
    public GameObject coilToThrow;
    public GameObject arc;
    public float throwSpeed = 3f;

    private GameObject Throwable = null;

    // Buttons
    string button1;
    string button2;
    string button3;

    [SerializeField]
    public float cooldownAllDeployed = 0.25f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
        button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;

        if (gameObject.transform.root.tag == "Player1")
        {
            coilToThrow.GetComponent<DeanteJames_CoilBehavior>().player1 = true;
            arc.GetComponent<DeanteJames_ArcBehavior>().player1 = true;
        }
        else
        {
            coilToThrow.GetComponent<DeanteJames_CoilBehavior>().player1 = false;
            arc.GetComponent<DeanteJames_ArcBehavior>().player1 = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
            return;
        }

        if (Input.GetButtonDown(button2))
        {
            Throw();

            if (Throwable.GetComponent<DeanteJames_CoilBehavior>().areMaxCoilsPlanted())
            {
                timer = cooldownAllDeployed;
            }

        }
    }

    void Throw()
    {
        Vector3 offset = new Vector3(0.0f, 0.5f, 0.5f);
        Throwable = GameObject.Instantiate(coilToThrow, gameObject.transform.position + offset, Quaternion.identity);

        // Add the coil to the main coil set to keep track of all the spawned coils
        Throwable.GetComponent<DeanteJames_CoilBehavior>().AddCoil(Throwable);
        Rigidbody rb = Throwable.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.velocity = (transform.forward + new Vector3(0.0f, 0.65f, 0.0f)) * throwSpeed;
    }
}
