using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScottFadoBristow_LaunchRockets : MonoBehaviour
{
    public float Cooldown = 1.0f;

    public float HomingSpeed = 20.0f;

    public Vector3 StartingOffset = new Vector3(0, 2, 0);

    public string buttonID;

    private GameObject opponent;

    public GameObject RocketPrefab;

    private float timer_ = 0;
    // Start is called before the first frame update
    void Start()
    {
        bool IsP1 = true;


        //Get keys

        buttonID = gameObject.transform.parent.GetComponent<playerParent>().action2Input;

        //Check if we are player 1 or player 2
        playerParent pP = gameObject.transform.root.GetComponent<playerParent>();
        if (pP)
            IsP1 = pP.isPlayer1;

        string OppTag = "Player2";

        if (!IsP1)
            OppTag = "Player1";

        //Get the opponent's parent
        //Should only be one
        GameObject[] goa = GameObject.FindGameObjectsWithTag(OppTag);
        if (goa.Length > 0)
        {
            GameObject opP = goa[0];

            //Get the correct child (The bot itself)
            //I don't think any bot overrid this class?
            BotBasic_FallRespawn opFR = opP.transform.GetComponentInChildren<BotBasic_FallRespawn>();

            if (opFR)
                opponent = opFR.gameObject;

        }
    }

    // Update is called once per frame
    void Update()
    {
        timer_ -= Time.deltaTime;

        //ATTACK
        if (Input.GetButtonDown(buttonID) && timer_ <= 0)
        {
            timer_ = Cooldown;
            LaunchRockets();
        }
    }


    void LaunchRockets()
    {
        Vector3 spawnPosition = gameObject.transform.position + StartingOffset;
        GameObject r = Instantiate(RocketPrefab, spawnPosition, Quaternion.identity);
        if(r)
        {
            ScottFadoBristow_DelayedHoming rScript = r.GetComponent<ScottFadoBristow_DelayedHoming>();

            rScript.Launch(opponent);
        }

    }
}
