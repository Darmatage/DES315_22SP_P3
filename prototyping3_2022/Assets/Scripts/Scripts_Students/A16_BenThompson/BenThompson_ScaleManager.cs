using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenThompson_ScaleManager : MonoBehaviour
{
    private int currentNumScales;
    private int initialNumScales = 4;

    [SerializeField]
    GameObject[] scales;

    [SerializeField]
    private float respawnTimeAfterLastScale;

    [SerializeField]
    GameObject scalePrefab;

    [SerializeField]
    GameObject spawnLocation;

    private float activeTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentNumScales = initialNumScales;
    }

    // Update is called once per frame
    void Update()
    {
        // If there is an active timer
        if(activeTimer > 0.0f)
        {
            // Decrease the timer
            activeTimer -= Time.deltaTime;

            // Leave the loop
            return;
        }
        else if(currentNumScales == 0 && activeTimer <= 0.0f)
        {
            // Respawn the scales
            RespawnScales();
        }
    }

    // Use scale
    public bool UseScaleMine()
    {
        if (currentNumScales == 0)
            return false;

        // Remove a scale from the bot's back
        scales[currentNumScales - 1].SetActive(false);

        // Decrease the number of scales
        currentNumScales--;

        GameObject mine = Instantiate(scalePrefab, spawnLocation.transform.position, scalePrefab.transform.rotation);
        
        if(transform.parent.parent.tag == "Player2")
        {
            mine.GetComponentInChildren<BenThompson_MineBehavior>().isPlayer2weapon = true;
        }
        
        //scalePrefab.transform.position = spawnLocation.transform.position;

        // If we are now out of scales
        if(currentNumScales == 0)
        {
            // Set the respawn time if we are out of scales
            activeTimer = respawnTimeAfterLastScale;
        }

        return true;
    }

    public int GetNumScales()
    {
        return currentNumScales;
    }

    private void RespawnScales()
    {
        // Make the scales visible again
        foreach(GameObject scale in scales)
        {
            scale.SetActive(true);
        }

        // Reset the current number of scales
        currentNumScales = initialNumScales;
    }
}
