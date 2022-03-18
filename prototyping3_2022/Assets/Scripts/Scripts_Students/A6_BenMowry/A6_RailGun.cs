using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A6_RailGun : MonoBehaviour {
    private float charge;
    private int chargeLevel;
    private bool isCharging;
    private float moveSpeedTemp;
    private float damageTemp;
    private AudioSource source;

    public KeyCode chargeKey;
    public KeyCode fireKey;

    public float chargeThresh;
    public float chargeMax;
    public int chargeLevelMax;
    public float chargeMoveSpeed;

    public GameObject chargeText;
    public GameObject levelText;

    public AudioClip chargeSound;
    public AudioClip fireSound;

    // Start is called before the first frame update
    void Start() {
        charge = 0;
        chargeLevel = 1;
        isCharging = false;
        moveSpeedTemp = GetComponentInParent<BotBasic_Move>().moveSpeed;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        source = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(chargeKey)) {
            isCharging = true;

            source.clip = chargeSound;
            source.volume = 1.0f;
            source.Play();
            source.loop = true;
        }

        if (Input.GetKeyUp(chargeKey)) {
            GetComponentInParent<BotBasic_Move>().moveSpeed = moveSpeedTemp;
            isCharging = false;
            source.Stop();
            source.loop = false;
        }

        if (isCharging) {
            charge = Mathf.Clamp(charge + Time.deltaTime, 0.0f, chargeMax);
            GetComponentInParent<BotBasic_Move>().moveSpeed = chargeMoveSpeed;
            
            //source.pitch = Mathf.Lerp(0.0f, 1.25f, charge);
        }

        if (charge >= chargeThresh * chargeLevel) {
            chargeLevel = Mathf.Clamp(chargeLevel + 1, 0, chargeLevelMax);
        }

        if (Input.GetMouseButton(0)) {
            if (chargeLevel > 1) {
                Fire(chargeLevel);
                ResetCharge();
            }
        }

        chargeText.GetComponent<TextMesh>().text = "Charge: " + charge.ToString();
        levelText.GetComponent<TextMesh>().text = "Level: " + chargeLevel.ToString();
    }

    private void Fire(int level) {
        damageTemp = GetComponent<HazardDamage>().damage;
        GetComponent<HazardDamage>().damage *= level;

        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;

        source.volume = .25f;
        source.pitch = 1;
        source.loop = false;
        source.clip = fireSound;
        source.Play();
        
        StartCoroutine(TurnOffRailgun());
    }

    private void ResetCharge() {
        charge = 0.0f;
        chargeLevel = 1;
        source.pitch = 1;
    }

    IEnumerator TurnOffRailgun() {
        yield return new WaitForSeconds(0.3f);
        GetComponent<HazardDamage>().damage = damageTemp;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }
}
