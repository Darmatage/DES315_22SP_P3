using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B17_LeechStats : MonoBehaviour
{
    private string thisPlayer;
    public float ScaleIncreaseAmount = 1.1f;
    public float ScaleIncreaseSpeed = 100f;
    public bool IsLeeching;
    //public float cooldown = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        thisPlayer = gameObject.transform.root.tag;
        IsLeeching = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        Transform otherRoot = other.transform.root;
        
        if (otherRoot.tag != thisPlayer)
            if (otherRoot.tag == "Player1" || otherRoot.tag == "Player2")
			{
                IsLeeching = true;
                StartCoroutine(IncreaseSize(transform.parent.localScale * ScaleIncreaseAmount));
			}
    }

    IEnumerator IncreaseSize(Vector3 targetScale)
	{
        while (transform.parent.localScale.magnitude < targetScale.magnitude)
		{
            transform.parent.localScale += transform.parent.localScale * ScaleIncreaseSpeed * Time.deltaTime;
            yield return null;
        }

        IsLeeching = false;
	}
}
