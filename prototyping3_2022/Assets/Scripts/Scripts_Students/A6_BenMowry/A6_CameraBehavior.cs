using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A6_CameraBehavior : MonoBehaviour
{
    public Camera cameraToLookAt;
    private bool isPlayer1;
    private Transform cam;

    // Use this for initialization 
    void Start() {
        if(cameraToLookAt != null) {
            cam = cameraToLookAt.transform;
        }

        isPlayer1 = gameObject.transform.root.GetComponent<playerParent>().isPlayer1;

        if ((isPlayer1 == true) && (GameObject.FindWithTag("camP1") != null)) {
            cam = GameObject.FindWithTag("camP1").transform;
        }
        else if (GameObject.FindWithTag("camP2") != null) {
            cam = GameObject.FindWithTag("camP2").transform;
        }
        else {
            cam = Camera.main.transform;
        }
    }

    // Update is called once per frame 
    void LateUpdate() {
        transform.LookAt(cam);
        transform.rotation = Quaternion.LookRotation(cam.forward);
    }
}
