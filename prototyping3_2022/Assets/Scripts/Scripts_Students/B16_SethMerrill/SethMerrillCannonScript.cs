using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SethMerrillCannonScript : MonoBehaviour
{	
	public GameObject round;
	public float speed;
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		
    }
	
	public void Fire()
	{
		SpawnRound();
	}
	
	void SpawnRound()
	{
		GameObject roundInstance = Instantiate(round, transform.position + transform.up, transform.rotation);
		roundInstance.GetComponent<Rigidbody>().AddForce(transform.up * speed * 10.0f);
	}
}
