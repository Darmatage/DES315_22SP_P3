using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrej : MonoBehaviour
{
    WeaponController weaCon;
    LineRenderer lineRend;
    public int pointNum = 50;
    public float dtbwpt = 0.1f;
    public LayerMask collLayer;

    // Start is called before the first frame update
    void Start()
    {
        weaCon = GetComponent<WeaponController>();
        lineRend = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRend.positionCount = pointNum;
        List<Vector3> p = new List<Vector3>();
        Vector3 startPos = weaCon.shotPoint.position;
        Vector3 startVel = weaCon.shotPoint.up * weaCon.shootPower;

        for (float i = 0; i < pointNum; i+=dtbwpt)
        {
            Vector3 newPt = startPos + i * startVel;
            newPt.y = startPos.y + startVel.y * i + Physics.gravity.y / 2f * i * i;
            p.Add(newPt);

            if(Physics.OverlapSphere(newPt, 2, collLayer).Length > 0)
            {
                lineRend.positionCount = p.Count;
                break;
            }
        }

        lineRend.SetPositions(p.ToArray());
    }
}
