using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaiKawashima_PartsShield : MonoBehaviour
{
  private KaiKawashima_PartsManager partsManager;
  private BotBasic_Damage damageManager;
  private GameObject partPrefab;

  [HideInInspector]
  public float front = 5.0f;
  [HideInInspector]
  public float back = 5.0f;
  [HideInInspector]
  public float left = 5.0f;
  [HideInInspector]
  public float right = 5.0f;
  [HideInInspector]
  public float top = 5.0f;
  [HideInInspector]
  public float bottom = 5.0f;
  private string thisPlayer;

  public AudioSource audioSource;
  public AudioClip[] audioClips;

  // Start is called before the first frame update
  void Start()
  {
    damageManager = GetComponent<BotBasic_Damage>();
    partsManager = gameObject.GetComponentInChildren<KaiKawashima_PartsManager>();
    partPrefab = partsManager.PartPrefab;

    thisPlayer = gameObject.transform.root.tag;

    front = damageManager.shieldPowerFront;
    back = damageManager.shieldPowerBack;
    left = damageManager.shieldPowerLeft;
    right = damageManager.shieldPowerRight;
    top = damageManager.shieldPowerTop;
    bottom = damageManager.shieldPowerBottom;

    HazardDamage hazard = partPrefab.GetComponent<HazardDamage>();
    hazard.isPlayer1Weapon = thisPlayer == "Player1";
    hazard.isPlayer2Weapon = thisPlayer == "Player2";
  }

  void PlayHitSound()
  {
    int num = Random.Range(0, audioClips.Length);
    audioSource.clip = audioClips[num];
    audioSource.pitch = Random.Range(0.8f, 1.3f);
    audioSource.Play();
  }

  // Update is called once per frame
  void Update()
  {
    if (front > damageManager.shieldPowerFront)
    {
      PlayHitSound();
      for (float i = damageManager.shieldPowerFront; i < front; ++i)
      {
        GameObject newObject = GameObject.Instantiate(partPrefab, gameObject.transform.position + gameObject.transform.forward.normalized * 4.0f, Quaternion.identity);
        if (!partsManager.AddPart(newObject, KaiKawashima_PartsManager.Side.FRONT))
        {
          GameObject.Destroy(newObject);
        }
      }
    }
    front = damageManager.shieldPowerFront;
    if (back > damageManager.shieldPowerBack)
    {
      PlayHitSound();
      for (float i = damageManager.shieldPowerBack; i < back; ++i)
      {
        GameObject newObject = GameObject.Instantiate(partPrefab, gameObject.transform.position + gameObject.transform.forward.normalized * -4.0f, Quaternion.identity);
        if (!partsManager.AddPart(newObject, KaiKawashima_PartsManager.Side.BACK))
        {
          GameObject.Destroy(newObject);
        }
      }
    }
    back = damageManager.shieldPowerBack;
    if (left > damageManager.shieldPowerLeft)
    {
      PlayHitSound();
      for (float i = damageManager.shieldPowerLeft; i < left; ++i)
      {
        GameObject newObject = GameObject.Instantiate(partPrefab, gameObject.transform.position + Vector3.Cross(gameObject.transform.forward.normalized, gameObject.transform.up.normalized) * 2.0f, Quaternion.identity);
        if (!partsManager.AddPart(newObject, KaiKawashima_PartsManager.Side.LEFT))
        {
          GameObject.Destroy(newObject);
        }
      }
    }
    left = damageManager.shieldPowerLeft;
    if (right > damageManager.shieldPowerRight)
    {
      PlayHitSound();
      for (float i = damageManager.shieldPowerRight; i < right; ++i)
      {
        GameObject newObject = GameObject.Instantiate(partPrefab, gameObject.transform.position + Vector3.Cross(gameObject.transform.forward.normalized, gameObject.transform.up.normalized) * -2.0f, Quaternion.identity);
        if (!partsManager.AddPart(newObject, KaiKawashima_PartsManager.Side.RIGHT))
        {
          GameObject.Destroy(newObject);
        }
      }
    }
    right = damageManager.shieldPowerRight;
    if (top > damageManager.shieldPowerTop)
    {
      PlayHitSound();
      for (float i = damageManager.shieldPowerTop; i < top; ++i)
      {
        GameObject newObject = GameObject.Instantiate(partPrefab, gameObject.transform.position + gameObject.transform.up.normalized * 1.0f, Quaternion.identity);
        if (!partsManager.AddPart(newObject, KaiKawashima_PartsManager.Side.TOP))
        {
          GameObject.Destroy(newObject);
        }
      }
    }
    top = damageManager.shieldPowerTop;
    if (bottom > damageManager.shieldPowerBottom)
    {
      PlayHitSound();
      for (float i = damageManager.shieldPowerBottom; i < bottom; ++i)
      {
        GameObject newObject = GameObject.Instantiate(partPrefab, gameObject.transform.position + gameObject.transform.up.normalized * -1.0f, Quaternion.identity);
        if (!partsManager.AddPart(newObject, KaiKawashima_PartsManager.Side.BOTTOM))
        {
          GameObject.Destroy(newObject);
        }
      }
    }
    bottom = damageManager.shieldPowerBottom;
  }
}
