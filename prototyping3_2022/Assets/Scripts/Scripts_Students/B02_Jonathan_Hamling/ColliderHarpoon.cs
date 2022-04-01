using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHarpoon : MonoBehaviour
{
    public GameObject player;
    public bool isHooked = false;
    public bool falseHook = false;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player)
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), other.collider);
        }
        else if (other.gameObject.tag.Equals("Player2") || other.gameObject.tag.Equals("Player1"))
        {
            isHooked = true;
        }
        else
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), other.collider);
            falseHook = true;
        }
    }

    IEnumerator destroyParticles(GameObject particles)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(particles);
    }
}
