using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenThompson_HatChooser : MonoBehaviour
{
    [SerializeField]
    GameObject[] hats;

    // Start is called before the first frame update
    void Start()
    {
        if(hats.Length > 0)
        {
            // Choose a random hat
            int hatIndex = Random.Range(0, hats.Length);

            // display the hat
            hats[hatIndex].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
