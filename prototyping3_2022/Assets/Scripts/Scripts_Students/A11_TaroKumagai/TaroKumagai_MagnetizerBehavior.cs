using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaroKumagai_MagnetizerBehavior : MonoBehaviour
{

    public Material InactiveMaterial;
    public Material ActiveMaterial;

    private MeshRenderer meshRenderer;
    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = InactiveMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive) 
            meshRenderer.material = ActiveMaterial;
        else 
            meshRenderer.material = InactiveMaterial;
    }
}
