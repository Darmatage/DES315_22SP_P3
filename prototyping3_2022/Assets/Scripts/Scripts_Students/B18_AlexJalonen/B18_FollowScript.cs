using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B18_FollowScript : MonoBehaviour
{
    public Transform follow;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = follow.position + offset;
    }
}
