using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B17_LeechStats : MonoBehaviour
{
    private string thisPlayer;
    private int materialToggle = 0; // for alternating between materials during growth
    private GameHandler gh;
    private Vector3 targetGrowthSize;
    private bool isLeeching;

    public float ScaleIncreaseAmount = 0.2f;
    public float ScaleIncreaseFalloff = 0.8f;
    public float ScaleIncreaseSpeed = 100f;
    public float HealthIncreaseAmount = 7.0f;
    public Material NormalBodyMaterial;
    public Material GrowthBodyMaterial;
    public GameObject BotBody;

    // For weapon cooldown and cooldown effects
    public GameObject MouthWeapon;
    public GameObject UpperJawObject;
    public GameObject LowerJawObject;
    private B17_JawMovement UpperJaw;
    private B17_JawMovement LowerJaw;

    private bool shouldDisableAttack;

    public float cooldown = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        thisPlayer = gameObject.transform.root.tag;
        gh = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
        isLeeching = false;

        UpperJaw = UpperJawObject.GetComponent<B17_JawMovement>();
        LowerJaw = LowerJawObject.GetComponent<B17_JawMovement>();

        shouldDisableAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        // This is in update() so the weapon isn't disabled until the frame after
        // this script detects a weapon hit.  If we do it on the frame of the hit
        // then the BotBasic_Damage script doesn't detect the hit
        if (shouldDisableAttack)
            MouthWeapon.tag = "Untagged";
    }

    void OnTriggerEnter(Collider other)
    {
        Transform otherRoot = other.transform.root;

        if (otherRoot.tag != thisPlayer)
            if (otherRoot.tag == "Player1" || otherRoot.tag == "Player2")
			{
                // Don't let the weapon attack multiple times in quick succession 
                transform.GetComponent<Collider>().enabled = false;
                UpperJaw.SetEnabled(false);
                LowerJaw.SetEnabled(false);
                shouldDisableAttack = true;
                Invoke("EnableWeapon", cooldown);

                // Increase bot stats and size
                isLeeching = true;
                IncreaseStats();
                targetGrowthSize = transform.parent.localScale * (1.0f + ScaleIncreaseAmount);
                StartCoroutine(IncreaseSize(targetGrowthSize));
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
        
        // Reduce the amount the leech grows next time
        if (isLeeching)
		{
            isLeeching = false;
            ScaleIncreaseAmount *= ScaleIncreaseFalloff;
            ScaleIncreaseSpeed *= ScaleIncreaseFalloff;
        }
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

    void EnableWeapon()
	{
        transform.GetComponent<Collider>().enabled = true;

        LowerJaw.SetEnabled(true);
        UpperJaw.SetEnabled(true);
        MouthWeapon.tag = "Hazard";
        shouldDisableAttack = false;
    }
}
