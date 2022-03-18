using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JirakitJarusiripipat_CheckMissileRange : MonoBehaviour
{
    public bool isInRange = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.tag == "player2" || other.gameObject.tag == "player1")
        {
            Debug.Log("test");
        }
    }
    private void OnTriggerExit(Collider other)
    {

    }
}
