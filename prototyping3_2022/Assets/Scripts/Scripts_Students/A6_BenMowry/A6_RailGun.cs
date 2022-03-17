using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A6_RailGun : MonoBehaviour
{
    private float charge;
    private int chargeLevel;
    private bool isCharging;
    private float moveSpeedTemp;

    public KeyCode chargeKey;
    public KeyCode fireKey;

    public float chargeThresh;
    public float chargeMax;
    public int chargeLevelMax;
    public float chargeMoveSpeed;

    public GameObject chargeText;
    public GameObject levelText;

    // Start is called before the first frame update
    void Start()
    {
        charge = 0;
        chargeLevel = 1;
        isCharging = false;
        moveSpeedTemp = GetComponentInParent<BotBasic_Move>().moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(chargeKey)) {
            isCharging = true;
        }

        if(Input.GetKeyUp(chargeKey)) {
            GetComponentInParent<BotBasic_Move>().moveSpeed = moveSpeedTemp;
            isCharging = false;
        }

        if(isCharging) {
            charge = Mathf.Clamp(charge + Time.deltaTime, 0.0f, chargeMax);
            GetComponentInParent<BotBasic_Move>().moveSpeed = chargeMoveSpeed;
        }

        if(charge >= chargeThresh * chargeLevel) {
            chargeLevel = Mathf.Clamp(chargeLevel + 1, 0, chargeLevelMax);
        }

        if (Input.GetKey(fireKey)) {
            if(chargeLevel > 1) {
                Fire(chargeLevel);
                ResetCharge();
            }
        }

        chargeText.GetComponent<TextMesh>().text = "Charge: " + charge.ToString();
        levelText.GetComponent<TextMesh>().text = "Level: " + chargeLevel.ToString();

    }

    private void Fire(int level) {

    }

    private void ResetCharge() {
        charge = 0.0f;
        chargeLevel = 1;
    }
}
