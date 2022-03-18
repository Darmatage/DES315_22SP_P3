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

    // Start is called before the first frame update
    void Start()
    {
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
        if (!isAttacking && Input.GetKeyDown(KeyCode.E))
        {
            halberdAnim.Play("Slash");
        }

        if (!isAttacking && Input.GetKeyDown(KeyCode.R))
        {
            halberdAnim.Play("Swing");
        }

        if (!isAttacking && Input.GetKeyDown(KeyCode.F))
        {
            halberdAnim.Play("Poke");
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
