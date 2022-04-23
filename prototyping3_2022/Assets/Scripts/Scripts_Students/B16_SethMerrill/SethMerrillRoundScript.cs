using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SethMerrillRoundScript : MonoBehaviour
{
	public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
		string tag = parent.transform.parent.parent.tag;
		if(tag == "Player1")
		{
			GetComponent<HazardDamage>().isPlayer1Weapon = true;
		}
		else if(tag == "Player2")
		{
			GetComponent<HazardDamage>().isPlayer2Weapon = true;
		} 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void OnCollisionEnter(Collision col)
	{
		Destroy(gameObject, .2f);
	}
}
