using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanielNunes_AddCameraShake : MonoBehaviour
{
    public Camera[] cams;
    // Start is called before the first frame update
    void Start()
    {
        //get all cameras in the scene
        cams = FindObjectsOfType<Camera>();

        foreach (Camera cam in cams)
        {
            DanielNunes_CameraShake cs = cam.gameObject.AddComponent<DanielNunes_CameraShake>();
            cs.maxDuration = 0.25f;
            cs.radius = 0.4f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
