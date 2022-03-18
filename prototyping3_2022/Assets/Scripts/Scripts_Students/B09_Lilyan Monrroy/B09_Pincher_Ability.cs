using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B09_Pincher_Ability : MonoBehaviour
{
    public GameObject pinchers;

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

    }

    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetButtonDown(button1))
        {
            GameObject player2 = other.gameObject;;
            
            if (player2.CompareTag("Player2"))
            {
                //Logic to grab here.
            }
        }
    }
}
