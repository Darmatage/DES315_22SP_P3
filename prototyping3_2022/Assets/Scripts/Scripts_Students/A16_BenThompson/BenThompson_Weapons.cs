using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenThompson_Weapons : MonoBehaviour
{
    [Header("Firebreath Prefabs")]
    [SerializeField]
    private GameObject firebreath4scales;

    [SerializeField]
    private GameObject firebreath3scales;

    [SerializeField]
    private GameObject firebreath2scales;

    [SerializeField]
    private GameObject firebreath1scale;

    [SerializeField]
    private ParticleSystem firebreathCough;

    [SerializeField]
    private GameObject tailArt;

    [Header("Firebreath Stats")]
    [SerializeField]
    private float firebreathLength;
    
    [SerializeField]
    private float firebreathCooldown;

    [SerializeField]
    private float firebreathDamage;

    [SerializeField]
    private float coughCooldown;

    [Header("Button Bindings")]
    //grab axis from parent object
    public string button1;
    public string button2;
    public string button3;
    public string button4; // currently boost in player move script

    // Is the weapon currently out?
    private bool weaponOut = false;

    int firebreathRange = 4;
    int currentRangeOut = -1;

    private float activeCooldown = 0.0f;
    private float mineCooldown = 0.0f;
    private float coughTimer = 0.0f;

    [Header("Scales")]
    [SerializeField]
    private BenThompson_ScaleManager scaleManager;

    [SerializeField]
    private float scalePlacementCooldown;

    // Start is called before the first frame update
    void Start()
    {
        button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
        button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
        button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
        button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;

        if(gameObject.transform.root.tag == "Player2")
        {
            firebreath4scales.GetComponent<BenThompson_FirebreathBehavior>().SetPlayer2();
            firebreath3scales.GetComponent<BenThompson_FirebreathBehavior>().SetPlayer2();
            firebreath2scales.GetComponent<BenThompson_FirebreathBehavior>().SetPlayer2();
            firebreath1scale.GetComponent<BenThompson_FirebreathBehavior>().SetPlayer2();
            firebreath4scales.GetComponent<HazardDamage>().isPlayer2Weapon = true;
            firebreath3scales.GetComponent<HazardDamage>().isPlayer2Weapon = true;
            firebreath2scales.GetComponent<HazardDamage>().isPlayer2Weapon = true;
            firebreath1scale.GetComponent<HazardDamage>().isPlayer2Weapon = true;
        }
        else
        {
            firebreath4scales.GetComponent<HazardDamage>().isPlayer1Weapon = true;
            firebreath3scales.GetComponent<HazardDamage>().isPlayer1Weapon = true;
            firebreath2scales.GetComponent<HazardDamage>().isPlayer1Weapon = true;
            firebreath1scale.GetComponent<HazardDamage>().isPlayer1Weapon = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If firebreath is attempted to be used but can't play the cough
        if (Input.GetButtonDown(button1) && activeCooldown > 0.0f)
        {
            // If we can cough and the firebreath ability isn't about to be ready
            if (coughTimer <= 0.0f && activeCooldown - coughCooldown > 0.0f)
            {
                // Play the cough
                firebreathCough.Play();

                // Reset the cough cooldown
                coughTimer = coughCooldown;
            }
        }


        // Use a mine
        if (Input.GetButtonDown(button2) && mineCooldown <= 0.0f)
        {
            if(scaleManager.UseScaleMine())
            {
                mineCooldown = scalePlacementCooldown;
            }
            
        }


        // If the firebreath ability can be used
        if ((Input.GetButtonDown(button1)) && (weaponOut == false) && activeCooldown <= 0.0f)
        {
            // Get the current scale value
            firebreathRange = scaleManager.GetNumScales();

            // Activate the fire breath ability for the correct number of scales
            switch(firebreathRange)
            {
                // If the user has 4 scales
                case 4:
                    
                    // Activate the fire breath for the 4 scale range
                    firebreath4scales.SetActive(true);

                    // Set the current range that is out as 4
                    currentRangeOut = 4;

                    // Indicate that a weapon is currently out
                    weaponOut = true;

                    tailArt.transform.localEulerAngles = new Vector3(-72.088f, tailArt.transform.localEulerAngles.y, tailArt.transform.localEulerAngles.z);

                    // Start the process of ending the fire breath attack
                    StartCoroutine(EndFireBreath());

                    break;

                // User has 3 scales
                case 3:

                    // Activate the fire breath for the 3 scale range
                    firebreath3scales.SetActive(true);

                    // Set the current range that is out as 3
                    currentRangeOut = 3;

                    // Indicate that a weapon is currently out
                    weaponOut = true;

                    tailArt.transform.localEulerAngles = new Vector3(-72.088f, tailArt.transform.localEulerAngles.y, tailArt.transform.localEulerAngles.z);

                    // Start the process of ending the fire breath attack
                    StartCoroutine(EndFireBreath());

                    break;

                // User has 2 scales
                case 2:

                    // Activate the fire breath for the 2 scale range
                    firebreath2scales.SetActive(true);

                    // Set the current range that is out as 2
                    currentRangeOut = 2;

                    // Indicate that a weapon is currently out
                    weaponOut = true;

                    tailArt.transform.localEulerAngles = new Vector3(-72.088f, tailArt.transform.localEulerAngles.y, tailArt.transform.localEulerAngles.z);

                    // Start the process of ending the fire breath attack
                    StartCoroutine(EndFireBreath());

                    break;

                // User has 1 scale
                case 1:

                    // Activate the fire breath for the 1 scale range
                    firebreath1scale.SetActive(true);

                    // Set the current range that is out as 1
                    currentRangeOut = 1;

                    // Indicate that a weapon is currently out
                    weaponOut = true;

                    tailArt.transform.localEulerAngles = new Vector3(-72.088f, tailArt.transform.localEulerAngles.y, tailArt.transform.localEulerAngles.z);

                    // Start the process of ending the fire breath attack
                    StartCoroutine(EndFireBreath());

                    break;

                case 0:
                    // If firebreath is attempted to be used but can't play the cough
                    if (Input.GetButtonDown(button1))
                    {
                        // If we can cough and the firebreath ability isn't about to be ready
                        if (coughTimer <= 0.0f)
                        {
                            // Play the cough
                            firebreathCough.Play();

                            // Reset the cough cooldown
                            coughTimer = coughCooldown;
                        }
                    }
                    break;
            }

            
        }

        // If there is an active cooldown
        if (activeCooldown > 0.0f)
        {
            // Decrease the cooldown
            activeCooldown -= Time.deltaTime;

            if(activeCooldown <= 0.0f)
            {
                tailArt.transform.localEulerAngles = new Vector3(-37.311f, tailArt.transform.localEulerAngles.y, tailArt.transform.localEulerAngles.z);
            }
            else
            {
                if (scaleManager.GetRespawnTimer() <= 0.0f)
                {
                    tailArt.transform.localEulerAngles = Vector3.Lerp(new Vector3(-72.088f, tailArt.transform.localEulerAngles.y, tailArt.transform.localEulerAngles.z),
                                                             new Vector3(-37.311f, tailArt.transform.localEulerAngles.y, tailArt.transform.localEulerAngles.z),
                                                             1.0f - activeCooldown / firebreathCooldown);
                }
            }
        }

        if (mineCooldown > 0.0f)
        {
            mineCooldown -= Time.deltaTime;
        }

        if(coughTimer > 0.0f)
        {
            coughTimer -= Time.deltaTime;
        }
    }

    IEnumerator EndFireBreath()
    {
        // How long does the attack last?
        yield return new WaitForSeconds(firebreathLength);      // Originally 0.6f
        
        // Deactivate the correct firebreath object
        switch(currentRangeOut)
        {
            case 4:
                
                // We no longer have a valid range that is out
                currentRangeOut = -1;

                // Deactivate the 4 scale range fire breath
                firebreath4scales.SetActive(false);

                firebreath4scales.GetComponent<BenThompson_FirebreathBehavior>().ResetHitCount();
                firebreath4scales.GetComponent<HazardDamage>().damage = firebreathDamage;

                break;

            case 3:

                // We no longer have a valid range that is out
                currentRangeOut = -1;

                // Deactivate the 3 scale range fire breath
                firebreath3scales.SetActive(false);

                firebreath3scales.GetComponent<BenThompson_FirebreathBehavior>().ResetHitCount();
                firebreath3scales.GetComponent<HazardDamage>().damage = firebreathDamage;

                break;

            case 2:

                // We no longer have a valid range that is out
                currentRangeOut = -1;

                // Deactivate the 2 scale range fire breath
                firebreath2scales.SetActive(false);

                firebreath2scales.GetComponent<BenThompson_FirebreathBehavior>().ResetHitCount();
                firebreath2scales.GetComponent<HazardDamage>().damage = firebreathDamage;

                break;

            case 1:

                // We no longer have a valid range that is out
                currentRangeOut = -1;

                // Deactivate the 1 scale range fire breath
                firebreath1scale.SetActive(false);

                firebreath1scale.GetComponent<BenThompson_FirebreathBehavior>().ResetHitCount();
                firebreath1scale.GetComponent<HazardDamage>().damage = firebreathDamage;

                break;
        }

        // We are no longer active with a weapon
        weaponOut = false;

        // Set a cooldown for the firebreath ability
        activeCooldown = firebreathCooldown;
    }   

    public float GetMineTime()
    {
        return mineCooldown;
    }
}
