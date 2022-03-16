using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KaiKawashima_PartsManager : MonoBehaviour
{
    public enum Side
    {
        FRONT = 0,
        BACK = 1,
        LEFT = 2,
        RIGHT = 3,
        TOP = 4,
        BOTTOM = 5
    };

    public GameObject PartPrefab;
    public int MaxPartsPerSide = 5;
    public float Damage = 1.0f;

    // 0: front, 1: back, 2: left, 3: right, 4: top, 5: bottom
    private List<List<GameObject>> AllParts;

    // Start is called before the first frame update
    void Start()
    {
        // add list for each side
        AllParts.Add(new List<GameObject>());
        AllParts.Add(new List<GameObject>());
        AllParts.Add(new List<GameObject>());
        AllParts.Add(new List<GameObject>());
        AllParts.Add(new List<GameObject>());
        AllParts.Add(new List<GameObject>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddPart(GameObject part, Side side)
    {
        // if no more parts can be created from that side
        if (AllParts[(int)side].Count > MaxPartsPerSide) return false;

        // add part
        AllParts[((int)side)].Add(part);
        return true;
    }

    public void CollectAllParts()
    {
        foreach (List<GameObject> sides in AllParts)
        {
            foreach (GameObject part in sides)
            {
                part.GetComponent<HazardDamage>().damage = Damage;
                
            }
        }
    }
}
