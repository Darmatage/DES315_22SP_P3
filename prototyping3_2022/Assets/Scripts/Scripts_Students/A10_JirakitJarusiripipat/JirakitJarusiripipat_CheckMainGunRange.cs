using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JirakitJarusiripipat_CheckMainGunRange : MonoBehaviour
{
    public bool inMainGunRange = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            if(other.gameObject.transform.parent.transform.parent != null)
            {
                if (other.gameObject.transform.parent.transform.parent.tag == "Player1")
                {
                    inMainGunRange = true;
                    Debug.Log("Found Player 1");
                }
                else if (other.gameObject.transform.parent.transform.parent.tag == "Player2")
                {
                    inMainGunRange = true;
                    Debug.Log("Found Player 2");
                }
            }
            
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.transform.parent.transform.parent.tag != null)
            {
                if (other.gameObject.transform.parent.transform.parent.tag == "Player1")
                {
                    inMainGunRange = false;
                }
                else if (other.gameObject.transform.parent.transform.parent.tag == "Player2")
                {
                    inMainGunRange = false;
                }
            }
        }
            
    }
}
