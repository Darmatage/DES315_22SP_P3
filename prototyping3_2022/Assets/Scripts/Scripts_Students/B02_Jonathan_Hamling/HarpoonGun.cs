//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class HarpoonGun : MonoBehaviour
//{
//    [SerializeField]
//    private GameObject player;
//    private GameObject enemy;
//    [SerializeField]
//    private GameObject harpoon;

//    private string button1;
//    private string button2;
//    private string button3;
//    private string button4;

//    public float hookSpeed;
//    public float maxHookDistance;
//    public float x_offset;
//    public float z_offset;

//    LineRenderer lineRenderer;

//    bool isHook;
//    bool wasEnmyHook;

//    float distance;
//    Vector3 startPos;

//    Rigidbody rb;

//    public void Start()
//    {
//        button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
//        button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
//        button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
//        button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;

//        rb.isKinematic = true;
//    }

//    private void Awake()
//    {
//        lineRenderer = GetComponent<LineRenderer>();
//        isHook = false;
//        wasEnmyHook = false;
//        distance = 0;
//        rb = GetComponent<Rigidbody>();
//        startPos = new Vector3(player.transform.position.x + x_offset, player.transform.position.y, player.transform.position.z + z_offset);
//    }

//    private void Update()
//    {
//        startPos = new Vector3(player.transform.position.x + x_offset, player.transform.position.y, player.transform.position.z + z_offset);
//        lineRenderer.SetPosition(0, startPos);
//        lineRenderer.SetPosition(1, harpoon.transform.position);
//        if (Input.GetButtonDown(button1) /*&& !isHook && !wasEnmyHook*/)
//        {
//            Debug.Log("Object Fired");
//            StartHook();
//        }

//        ReturnHook();
//        BringEnemyBack();
//    }

//    private void StartHook()
//    {
//        isHook = true;
//        rb.isKinematic = false;
//        rb.AddForce(harpoon.transform.forward * hookSpeed);
//    }

//    private void ReturnHook()
//    {
//        if (isHook)
//        {
//            distance = Vector3.Distance(harpoon.transform.position, startPos);

//            if (distance > maxHookDistance || wasEnmyHook)
//            {
//                rb.isKinematic = true;
//                harpoon.transform.position = startPos;
//                isHook = false;
//            }
//        }
//    }

//    private void BringEnemyBack()
//    {
//        if (wasEnmyHook)
//        {
//            Vector3 final = new Vector3(startPos.x, enemy.transform.position.y, startPos.z + z_offset);
//            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, final, maxHookDistance);
//            wasEnmyHook = false;
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if ((other.gameObject.tag.Equals("Player2") && !this.gameObject.tag.Equals("Player2")) 
//            || (other.gameObject.tag.Equals("Player1") && !this.gameObject.tag.Equals("Player1")))
//        {
//            wasEnmyHook = true;
//            enemy = other.gameObject;
//        }
//    }
//}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonGun : MonoBehaviour
{
    //NOTE: This script goes on the main playerBot Game Object, and the weapon goes in the public GO slot

    [SerializeField]
    private GameObject harpoon;
    [SerializeField]
    private GameObject orca;
    GameObject enemy;
    [SerializeField]
    private float launchSpeed = 3f;

    [SerializeField]
    private Material chain;

    GameObject newHarpoon = null;

    float distance;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private float minDistance;

    private bool weaponOut = false;
    private bool isHook;
    private bool isLaunched;
    private bool isAttacked;
    private bool isRecharge = false;

    private Animator anim;

    Rigidbody rb;
    private Vector3 startPos;
    private Vector3 staticHook_offset;

    //grab axis from parent object
    private string button1;
    private string button2;
    private string button3;
    private string button4; // currently boost in player move script

    void Start()
    {
        button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
        button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
        button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;

        isLaunched = false;
        isAttacked = false;

        anim = orca.GetComponent<Animator>();
    }

    void Update()
    {
        startPos = transform.position;

        if (Input.GetButtonDown(button1) && !isLaunched && !isRecharge)
        {
            Debug.Log("Object Fired");
            StartHook();
        }

        ReturnHook();
        BringEnemyBack();

        if (isLaunched)
        {
            if (newHarpoon.GetComponent<ColliderHarpoon>().falseHook)
            {
                StartCoroutine(Recharge());

                newHarpoon.transform.position = new Vector3(0, -999, 0);
                newHarpoon.GetComponent<ColliderHarpoon>().falseHook = false;
                newHarpoon.GetComponent<LineRenderer>().enabled = false;

                isHook = false;
                newHarpoon.GetComponent<ColliderHarpoon>().isHooked = false;
            }

            newHarpoon.GetComponent<LineRenderer>().SetPosition(0, startPos);
            newHarpoon.GetComponent<LineRenderer>().SetPosition(1, newHarpoon.transform.position);
            newHarpoon.GetComponent<LineRenderer>().material = chain;

            isHook = newHarpoon.GetComponent<ColliderHarpoon>().isHooked;
            enemy = newHarpoon.GetComponent<ColliderHarpoon>().enemy;
            newHarpoon.GetComponent<ColliderHarpoon>().player = this.gameObject;
        }

        Debug.Log(isRecharge);
    }

    private void StartHook()
    {
        if (newHarpoon)
        {
            Destroy(newHarpoon.gameObject, .1f);
            newHarpoon = null;
        }

        isLaunched = true;
        newHarpoon = Instantiate(harpoon, harpoon.transform.position, harpoon.transform.rotation);
        newHarpoon.AddComponent<LineRenderer>();
        
        newHarpoon.SetActive(true);
        
        rb = newHarpoon.GetComponent<Rigidbody>();  
        rb.isKinematic = false;
        rb.AddForce(transform.forward * launchSpeed);
    }

    private void ReturnHook()
    {
        if (isLaunched)
        {
            distance = Vector3.Distance(newHarpoon.transform.position, startPos);

            if (isHook)
            {
                isLaunched = false;
            }
            else if (distance > maxDistance)
            {
                newHarpoon.GetComponent<LineRenderer>().enabled = false;

                isLaunched = false;
            }
        }
    }

    private void BringEnemyBack()
    {
        if (isHook)
        { 
            StartCoroutine(hookStage());
        }
    }

    private void sineWave()
    {

    }

    private IEnumerator hookStage()
    {
        Vector3 hookpos = enemy.transform.position;

        newHarpoon.GetComponent<LineRenderer>().SetPosition(0, startPos);
        newHarpoon.GetComponent<LineRenderer>().SetPosition(1, hookpos);

        newHarpoon.transform.position = new Vector3(0, -999, 0);

        Vector3 final = new Vector3(startPos.x, startPos.y + 2f, startPos.z + 3.5f);
        enemy.transform.position = Vector3.Lerp(enemy.transform.position, final, .008f);

        isAttacked = !isAttacked;
        Debug.Log("ATTACKING");
        anim.SetBool("Attack", true);

        yield return new WaitForSeconds(6.0f);

        newHarpoon.GetComponent<LineRenderer>().enabled = false;

        anim.SetBool("Attack", false);

        isHook = false;
        newHarpoon.GetComponent<ColliderHarpoon>().isHooked = false;

        StartCoroutine(Recharge());
    }

    private IEnumerator Recharge()
    {
        isRecharge = true;
        yield return new WaitForSeconds(5.0f);

        isRecharge = false;
    }

}

