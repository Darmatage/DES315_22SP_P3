using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanielNunes_HalberdCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject particlePrefab;
    private Transform aoe;

    // Start is called before the first frame update
    void Start()
    {
        //aoe = transform.parent.root.Find("BotA09");
        //if (aoe == null)
        //{
        //    aoe = transform.parent.root.Find("BotA09(Clone)").Find("HalberdSlashAOE");
        //}
        //else
        //{
        //    aoe = aoe.Find("HalberdSlashAOE");
        //}
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
            Vector3 offset = FindObjectOfType<DanielNunes_Halberd>().transform.forward * 1.5f;
            //spawn particles
            GameObject particles = Instantiate(particlePrefab, other.ClosestPointOnBounds(transform.position) + offset, Quaternion.identity);
            Destroy(particles, 2.0f);

            //enable AOE collider for 0.75 seconds
            //aoe.GetComponent<SphereCollider>().enabled = true;
            //Invoke(nameof(DisableAOE), 0.75f);

            FindObjectOfType<DanielNunes_AnimationEvents>().endOfSlash = false;
        }
    }

    private void DisableAOE()
    {
        aoe.GetComponent<SphereCollider>().enabled = false;
    }
}
