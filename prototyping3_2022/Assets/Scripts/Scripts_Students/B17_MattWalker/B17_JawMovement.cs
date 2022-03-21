using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B17_JawMovement : MonoBehaviour
{
    public float JawCloseDirection; // negative value for upper jaw so it moves downward on bite
    public float BiteHeightToBodyHeight = 0.55f;
    public GameObject OtherJawObject;

    private bool IsReadyToBite;
    private bool IsBitingDown;
    private float BiteDirModifier; // Used to change the direction of the jaw movement for jaw open and closing
    private B17_JawMovement OtherJaw;

    // Start is called before the first frame update
    void Start()
    {
        IsBitingDown = true;
        IsReadyToBite = true;
        BiteDirModifier = 1.0f;

        OtherJaw = OtherJawObject.GetComponent<B17_JawMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if (IsReadyToBite)
		{
            if (CheckMouthFullyClosed())
			{
                IsBitingDown = false;
                BiteDirModifier = -1.0f;

                if (OtherJaw.GetIsBitingDown()) // let the other jaw finish closing too
                    return;
			}
            else if (CheckMouthFullyOpen())
			{
                IsBitingDown = true;
                BiteDirModifier = 1.0f;
			}

            // Move the jaw
            transform.Translate(new Vector3(0.0f, BiteDirModifier * JawCloseDirection * Time.deltaTime, 0.0f));
        }
    }

    bool CheckMouthFullyClosed()
	{
        if (JawCloseDirection < 0) // upper jaw
        {
            if (transform.localPosition.y <= 0.0f)
                return true;
            else
                return false;
        }
        else // lower jaw
        {
            if (transform.localPosition.y >= 0.0f)
                return true;
            else
                return false;
        }
    }

    bool CheckMouthFullyOpen()
    {
        if (JawCloseDirection < 0) // upper jaw
		{
            if (transform.localPosition.y >= BiteHeightToBodyHeight)
                return true;
            else
                return false;
        }
        else // lower jaw
		{
            if (transform.localPosition.y <= -1.0f * BiteHeightToBodyHeight)
                return true;
            else
                return false;
        }
    }

    public bool GetIsBitingDown()
	{
        return IsBitingDown;
	}
}
