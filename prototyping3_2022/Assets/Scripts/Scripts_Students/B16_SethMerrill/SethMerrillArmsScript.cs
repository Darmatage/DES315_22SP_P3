using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SethMerrillArmsScript : MonoBehaviour
{
	public GameObject parent;
	public GameObject enemy;
	public GameHandler gh;
	public Transform emptySlot;
	public bool isHolding = false;
	private Transform enemyTransformParent;
    // Start is called before the first frame update
    void Start()
    {
		gh = GameObject.FindGameObjectsWithTag("GameHandler")[0].GetComponent<GameHandler>();
		if(parent.transform.parent.tag == "Player1")
		{
			enemy = GetPlayer("Player2");
		}
		else if(parent.transform.parent.tag == "Player2")
		{
			enemy = GetPlayer("Player1");
		}
		if(enemy != null) enemyTransformParent = enemy.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void Grab(GameObject go)
	{
		if(enemy == null) return;
		if(enemy != go) return;
		if(isHolding) Unhold();
		else Hold();
	}
	
	private void Hold()
	{
		if(enemy == null) return;
		enemy.GetComponent<BotBasic_Move>().isGrabbed = true;
		Reparent(enemy, emptySlot);
		isHolding = true;
	}
	
	public void Unhold()
	{
		if(enemy == null) return;
		enemy.GetComponent<BotBasic_Move>().isGrabbed = false;
		Reparent(enemy, enemyTransformParent);
		isHolding = false;
	}
	
	private void Unparent(GameObject go)
	{
		go.transform.parent = null;
	}
	
	private void Reparent(GameObject go, Transform t)
	{
		Unparent(enemy);
		go.transform.parent = t;
	}
	
	private GameObject GetPlayer(string tag)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag(tag);
		if(array.Length == 0) return null;
		return array[0].transform.GetChild(0).gameObject;
	}
}
