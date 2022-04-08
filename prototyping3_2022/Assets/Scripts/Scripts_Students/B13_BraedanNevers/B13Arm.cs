using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B13Arm : MonoBehaviour
{
    public GameObject bombToSpawn;
    public GameObject bombToHold;
    public GameObject bombInBox;

    public void Start()
    {
        bombToHold.SetActive(false);
        bombInBox.SetActive(true);
    }
    public void GrabBombFromBox()
    {
        bombInBox.SetActive(false);
        bombToHold.SetActive(true);
    }

    public void DropBomb()
    {
        Instantiate(bombToSpawn, bombToHold.transform.position, bombToHold.transform.rotation);
        bombToHold.SetActive(false);
    }
}
