using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B17_LeechStats : MonoBehaviour
{
    private string thisPlayer;
    private int materialToggle = 0; // for alternating between materials during growth
    private GameHandler gh;

    public float ScaleIncreaseAmount = 1.1f;
    public float ScaleIncreaseSpeed = 100f;
    public float HealthIncreaseAmount = 7.0f;
    public Material NormalBodyMaterial;
    public Material GrowthBodyMaterial;
    public GameObject BotBody;

    //private bool IsLeeching;
    //public float cooldown = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        thisPlayer = gameObject.transform.root.tag;
        gh = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
        //IsLeeching = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Transform otherRoot = other.transform.root;
        
        if (otherRoot.tag != thisPlayer)
            if (otherRoot.tag == "Player1" || otherRoot.tag == "Player2")
			{
                //IsLeeching = true;
                IncreaseStats();
                StartCoroutine(IncreaseSize(transform.parent.localScale * ScaleIncreaseAmount));
			}
    }

    IEnumerator IncreaseSize(Vector3 targetScale)
	{
        while (transform.parent.localScale.magnitude < targetScale.magnitude)
		{
            // Make the bot flicker between red and normal color while growing
            switch (materialToggle)
			{
                case 0:
                    BotBody.GetComponent<Renderer>().material = GrowthBodyMaterial;
                    materialToggle = 1;
                    break;
                case 1:
                    BotBody.GetComponent<Renderer>().material = NormalBodyMaterial;
                    materialToggle = 0;
                    break;
                default:
                    BotBody.GetComponent<Renderer>().material = NormalBodyMaterial;
                    materialToggle = 0;
                    break;
            }

            transform.parent.localScale += transform.parent.localScale * ScaleIncreaseSpeed * Time.deltaTime;
            yield return null;
        }

        // End with the normal material
        BotBody.GetComponent<Renderer>().material = NormalBodyMaterial;
        //IsLeeching = false;
    }

    void IncreaseStats()
	{
        if (gameObject.transform.root.tag == "Player1")
		{
            //gh.p1Health += HealthIncreaseAmount;
        }
        else
		{
            //gh.p2Health += HealthIncreaseAmount;
        }
	}
}
