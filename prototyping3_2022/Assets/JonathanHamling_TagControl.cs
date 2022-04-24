using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JonathanHamling_TagControl : MonoBehaviour
{
    [SerializeField]
    private TextMesh text;
    [SerializeField]
    private HarpoonGun gun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gun.isRecharge)
            text.text = "Harpoon: Not Ready";
        else
            text.text = "Harpoon: Ready";
    }
}
