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
  public KaiKawashima_PartsShield PartShield;
  public int MaxPartsPerSide = 5;
  public float Damage = 1.0f;
  public float PartSpeed = 3.0f;

  private bool IsCollecting = false;
  private float CollectingTimer = 0.0f;
  // 0: front, 1: back, 2: left, 3: right, 4: top, 5: bottom
  private List<List<GameObject>> AllParts = new List<List<GameObject>>();

  private GameHandler handlerForShiled = null;
  private bool isPlayer1 = true;
  private string thisPlayer;

  // Start is called before the first frame update
  void Start()
  {
    if (GameObject.FindWithTag("GameHandler") != null)
    {
      handlerForShiled = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
    }

    thisPlayer = gameObject.transform.root.tag;
    isPlayer1 = thisPlayer == "Player1";

    // add list for each side
    AllParts.Add(new List<GameObject>());
    AllParts.Add(new List<GameObject>());
    AllParts.Add(new List<GameObject>());
    AllParts.Add(new List<GameObject>());
    AllParts.Add(new List<GameObject>());
    AllParts.Add(new List<GameObject>());

    DamageStuff = gameObject.GetComponentInParent<BotBasic_Damage>();
    PartShield = gameObject.GetComponentInParent<KaiKawashima_PartsShield>();
  }

  // Update is called once per frame
  void Update()
  {
    if (CollectingTimer > 0.0f) CollectingTimer -= Time.deltaTime;
    else IsCollecting = false;

    if (IsCollecting)
    {
      foreach (List<GameObject> sides in AllParts)
      {
        foreach (GameObject part in sides)
        {
          part.GetComponent<HazardDamage>().enabled = true;
          part.GetComponent<HazardDamage>().damage = Damage;
          part.GetComponent<BoxCollider>().isTrigger = true;
          Rigidbody rb = part.GetComponent<Rigidbody>();
          rb.velocity = Vector3.Normalize(transform.position - part.transform.position) * PartSpeed;
        }
      }
    }
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
    if (handlerForShiled != null)
    {
      if (isPlayer1)
      {
        handlerForShiled.p1Shields = 6;
      }
      else
      {
        handlerForShiled.p2Shields = 6;
      }
    }

    IsCollecting = true;
    CollectingTimer = 1.0f;
    DamageStuff.shieldPowerLeft = 3;
    DamageStuff.shieldPowerRight = 3;
    DamageStuff.shieldPowerTop = 3;
    DamageStuff.shieldPowerBottom = 3;
    DamageStuff.shieldPowerFront = 5;
    DamageStuff.shieldPowerBack = 3;
    PartShield.top = 3;
    PartShield.bottom = 3;
    PartShield.front = 5;
    PartShield.back = 3;
    PartShield.left = 3;
    PartShield.right = 3;
    DamageStuff.dmgParticlesTop.SetActive(false);
    DamageStuff.dmgParticlesLeft.SetActive(false);
    DamageStuff.dmgParticlesRight.SetActive(false);
    DamageStuff.dmgParticlesFront.SetActive(false);
    DamageStuff.dmgParticlesBack.SetActive(false);

  }

  private void OnTriggerEnter(Collider other)
  {
    for (int i = 0; i < 6; ++i)
    {
      int size = AllParts[i].Count;
      for (int j = 0; j < size; ++j)
      //foreach (GameObject part in AllParts[i])
      {
        if (AllParts[i][j] == other.gameObject && IsCollecting)
        {
          //AllParts[i].Remove(part);
          AllParts[i].RemoveAt(j);
          GameObject.Destroy(other.gameObject);
          --j;
          --size;
        }
      }
    }
  }
}
