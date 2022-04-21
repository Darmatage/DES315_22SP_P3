using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScottFadoBristow_LaunchRockets : MonoBehaviour
{
    public float Cooldown = 1.0f;

    public float HomingSpeed = 20.0f;

    public Vector3 StartingOffset = new Vector3(0, 2, 0);

    public GameObject SiloA;
    private bool aFired = false;
    public GameObject SiloB;
    private bool bFired = false;

    public Material SiloEmptyMat;
    public Material SiloReadyMat;

    private Renderer SAMesh, SBMesh;

    public string buttonID;

    private GameObject opponent;

    public GameObject RocketPrefab;


    public AudioClip LaunchNoise;

    private AudioSource audioSource;

    private float timerA_ = 0;
    private float timerB_ = 0;
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

        SAMesh = SiloA.GetComponentInChildren<Renderer>();
        SBMesh = SiloB.GetComponentInChildren<Renderer>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timerA_ -= Time.deltaTime;
        timerB_ -= Time.deltaTime;

        //ATTACK
        if (Input.GetButtonDown(buttonID) )
        {
            if (timerA_ <= 0)
            {
                timerA_ = Cooldown;
                LaunchRockets(true);
            }
            else if(timerB_ <= 0)
            {
                timerB_ = Cooldown;
                LaunchRockets(false);
            }
        }


        if(timerA_ <= 0)
            SAMesh.material = SiloReadyMat;
        if (timerB_ <= 0)
            SBMesh.material = SiloReadyMat;

    }


    void LaunchRockets(bool A = true)
    {
        Vector3 spawnPosition;
        if (A)
        {
            spawnPosition = SiloA.transform.position + StartingOffset;
            SAMesh.material = SiloEmptyMat;
        }
        else
        {
            spawnPosition = SiloB.transform.position + StartingOffset;
            SBMesh.material = SiloEmptyMat;
        }

        GameObject r = Instantiate(RocketPrefab, spawnPosition, Quaternion.identity);
        if(r)
        {
            ScottFadoBristow_DelayedHoming rScript = r.GetComponent<ScottFadoBristow_DelayedHoming>();

            rScript.Launch(opponent);
        }

        if (audioSource)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(LaunchNoise);
        }

    }
}
