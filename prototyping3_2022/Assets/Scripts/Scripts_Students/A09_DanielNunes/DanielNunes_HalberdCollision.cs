using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanielNunes_HalberdCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject particlePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the halberd hit the ground and was at the end of the swing animation
        if (other.gameObject.layer == LayerMask.NameToLayer("ground") && FindObjectOfType<DanielNunes_AnimationEvents>().endOfSlash)
        {
            //spawn particles
            GameObject particles = Instantiate(particlePrefab, other.ClosestPointOnBounds(transform.position), Quaternion.identity);
            Destroy(particles, 2.0f);

            FindObjectOfType<DanielNunes_AnimationEvents>().endOfSlash = false;
        }
    }
}
