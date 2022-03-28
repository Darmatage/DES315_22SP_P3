using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class B18_Firebar : MonoBehaviour
{
    public Image frame;
    public Image windupBar;
    public Image heatBar;

    private float frameWidth;

    // Start is called before the first frame update
    void Awake()
    {
        frameWidth = frame.rectTransform.sizeDelta.x;
    }

    public void SetWindup(float percent)
    {
        windupBar.rectTransform.position += new Vector3(percent * frameWidth, 0, 0);
    }

    public void SetHeatBar(float percent)
    {
        heatBar.rectTransform.sizeDelta = new Vector2(frameWidth * percent * 2, .3f);
    }

    public void SetColor(Color color)
    {
        heatBar.color = color;
    }
}
