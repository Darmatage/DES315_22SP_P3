using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    
    private Transform transform;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.3f;
    private float dampingSpeed = 1.0f;
    Vector3 initialPosition;

    GameObject camera;

    void Awake()
    {
        camera = GameObject.Find("Main Camera");
        if (camera == null)
        {
            camera = GameObject.Find("StartCamera");
        }
    }

    void OnEnable()
    {
        initialPosition = camera.transform.localPosition;
    }

    void Update()
    {
        

        if (shakeDuration > 0)
        {
            camera.transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            camera.transform.localPosition = initialPosition;
        }
    }


    public void TriggerShake()
    {
        shakeDuration = 2.0f;
    }

    public void TriggerShake(float duration)
    {
        shakeDuration = duration;
    }
}
