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

    public BotBasic_Damage DamageStuff;
    public GameObject PartPrefab;
    public int MaxPartsPerSide = 5;
    public float Damage = 1.0f;
    public float PartSpeed = 3.0f;

    private bool IsCollecting = false;
    private float CollectingTimer = 0.0f;
    // 0: front, 1: back, 2: left, 3: right, 4: top, 5: bottom
    private List<List<GameObject>> AllParts = new List<List<GameObject>>();


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

        DamageStuff = gameObject.GetComponentInParent<BotBasic_Damage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CollectingTimer > 0.0f) CollectingTimer -= Time.deltaTime;
        else IsCollecting = false;
    }


    public bool AddPart(GameObject part, Side side)
    {
        // if no more parts can be created from that side
        if (AllParts[(int)side].Count > MaxPartsPerSide) return false;
        if (DamageStuff.shieldPowerFront <= 0) return false;

        // add part
        AllParts[((int)side)].Add(part);

        return true;
    }

    public void CollectAllParts()
    {
        IsCollecting = true;
        CollectingTimer = 1.0f;
        foreach (List<GameObject> sides in AllParts)
        {
            foreach (GameObject part in sides)
            {
                part.GetComponent<HazardDamage>().damage = Damage;
                Rigidbody rb = part.GetComponent<Rigidbody>();
                rb.velocity = Vector3.Normalize(transform.position - part.transform.position) * PartSpeed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < 6; ++i)
        {
            foreach (GameObject part in AllParts[i])
            {
                if (part == other.gameObject && IsCollecting)
                {
                    AllParts[i].Remove(part);
                    GameObject.Destroy(other.gameObject);

                    if (i == (int)Side.TOP)
                    { 
                        ++DamageStuff.shieldPowerTop;
                        DamageStuff.dmgParticlesTop.SetActive(false);
                    }
                    if (i == (int)Side.BOTTOM) 
                    {
                        ++DamageStuff.shieldPowerBottom;
                    }
                    if (i == (int)Side.LEFT)
                    {
                        ++DamageStuff.shieldPowerLeft;
                        DamageStuff.dmgParticlesLeft.SetActive(false);
                    }
                    if (i == (int)Side.RIGHT)
                    {
                        ++DamageStuff.shieldPowerRight;
                        DamageStuff.dmgParticlesRight.SetActive(false);
                    }
                    if (i == (int)Side.FRONT)
                    {
                        ++DamageStuff.shieldPowerFront;
                        DamageStuff.dmgParticlesFront.SetActive(false);
                    }
                    if (i == (int)Side.BACK)
                    {
                        ++DamageStuff.shieldPowerBack;
                        DamageStuff.dmgParticlesBack.SetActive(false);
                    }


                    return;
                }
            }
        }
    }
}
