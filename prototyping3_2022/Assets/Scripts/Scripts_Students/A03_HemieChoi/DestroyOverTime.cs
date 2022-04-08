using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    private float lifeTime = 5.0f;
    private float timer = 0.0f;
    private WeaponController bulletData;
    public GameObject orca;

    // Start is called before the first frame update
    void Start()
    {
        bulletData = GameObject.FindWithTag("A03bot").GetComponent<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            --bulletData.duckOutCount;
            bulletData.showDuck();
            Destroy(gameObject);
        }
    }
}
