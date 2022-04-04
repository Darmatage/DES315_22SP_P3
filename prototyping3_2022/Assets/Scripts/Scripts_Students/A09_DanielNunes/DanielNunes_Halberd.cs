using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanielNunes_Halberd : MonoBehaviour
{
    [SerializeField]
    GameObject halberdPrefab;

    private Animator halberdAnim;

    //how high above the player is the halberd?
    [SerializeField]
    private float halberdTopOffset;

    public bool isAttacking;
    public bool lunging;

    //grab axis from parent object
    public string button1;
    public string button2;
    public string button3;
    public string button4; // currently boost in player move script

    // Start is called before the first frame update
    void Start()
    {
        button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
        button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
        button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;

        GameObject halberd = Instantiate(halberdPrefab, transform);

        //center the halberd to the bot, and offset it upward
        halberd.transform.position = new Vector3(transform.position.x, transform.position.y + halberdTopOffset, transform.position.z);

        //make the halberd a child of the bot
        //halberd.transform.parent = gameObject.transform;

        halberdAnim = halberd.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking && Input.GetButtonDown(button1))
        {
            halberdAnim.Play("Slash");
        }
        
        if (!isAttacking && Input.GetButtonDown(button2))
        {
            halberdAnim.Play("Poke");
        }

        if (!isAttacking && Input.GetButtonDown(button3))
        {
            halberdAnim.Play("Swing");
        }

        if (!isAttacking && Input.GetButtonDown(button4))
        {
            halberdAnim.Play("SwingR");
        }

    }

    private void FixedUpdate()
    {
        if (lunging)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * 200, ForceMode.Impulse);
            lunging = false;
        }
    }
}
