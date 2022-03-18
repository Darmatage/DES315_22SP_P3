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

    [Header("Firebreath Stats")]
    [SerializeField]
    private float firebreathLength;
    
    [SerializeField]
    private float firebreathCooldown;

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

    [Header("Scales")]
    [SerializeField]
    private BenThompson_ScaleManager scaleManager;

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
        // Use a mine
        if(Input.GetKeyDown(KeyCode.P))
        {
            scaleManager.UseScaleMine();
        }

        // If there is an active cooldown
        if(activeCooldown > 0.0f)
        {
            // Decrease the cooldown
            activeCooldown -= Time.deltaTime;

            // Leave the loop
            return;
        }

        // If the firebreath ability can be used
        if((Input.GetButtonDown(button1)) && (weaponOut == false))
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

                    // Start the process of ending the fire breath attack
                    StartCoroutine(EndFireBreath());

                    break;
            }

            
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

                break;

            case 3:

                // We no longer have a valid range that is out
                currentRangeOut = -1;

                // Deactivate the 3 scale range fire breath
                firebreath3scales.SetActive(false);

                break;

            case 2:

                // We no longer have a valid range that is out
                currentRangeOut = -1;

                // Deactivate the 2 scale range fire breath
                firebreath2scales.SetActive(false);

                break;

            case 1:

                // We no longer have a valid range that is out
                currentRangeOut = -1;

                // Deactivate the 1 scale range fire breath
                firebreath1scale.SetActive(false);

                break;
        }

        // We are no longer active with a weapon
        weaponOut = false;

        // Set a cooldown for the firebreath ability
        activeCooldown = firebreathCooldown;
    }
}
