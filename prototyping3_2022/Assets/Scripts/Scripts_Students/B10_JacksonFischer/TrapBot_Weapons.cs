using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBot_Weapons : MonoBehaviour
{
	public GameObject mine_prefab;
	public GameObject tracking_prefab;

	//grab axis from parent object
	public string button1;
	public string button2;
	public string button3;
	public string button4; // currently boost in player move script

	void Start()
	{
		button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
		button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
		button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
		button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;
	}

	void Update()
	{
		if (Input.GetButtonDown(button1))
		{
			GameObject mine = Instantiate(mine_prefab);


			/*if (mine.transform.parent.CompareTag("Player1"))
            {
				MineAttack mine_stats = (MineAttack) mine.GetComponent(typeof(MineAttack));
				mine_stats.player_number = 1;

            }

			else if (mine.transform.parent.CompareTag("Player2"))
            {
				MineAttack mine_stats = (MineAttack)mine.GetComponent(typeof(MineAttack));
				mine_stats.player_number = 2;
            }*/

			mine.transform.parent = this.transform;
			Vector3 new_mine_local_position = mine.transform.localPosition;
			new_mine_local_position.y = -0.75f;
			new_mine_local_position.z = -5.0f;
			mine.transform.localPosition = new_mine_local_position;

			mine.transform.parent = null;

		}

		if (Input.GetButtonDown(button2))
        {
			GameObject tracking = Instantiate(tracking_prefab);

			/*GameObject[] targets = new GameObject[1];

			if (this.transform.parent.tag == "Player1")
				targets = GameObject.FindGameObjectsWithTag("Player2");
			

			else if(this.transform.parent.tag == "Player2")
				targets = GameObject.FindGameObjectsWithTag("Player1");
			
			int player_index = 0;

			for(int i = 0; i < targets.Length; i++)
            {
				if (targets[i] != null)
                {
					player_index = i;
					break;
				}
            }

			GameObject other_player = targets[player_index].transform.GetChild(0).gameObject;

			TrackingAttack attack = (TrackingAttack)tracking.GetComponent("TrackingAttack");
			attack.target = other_player;*/



			tracking.transform.SetParent(this.transform);
			Vector3 new_tracking_local_position = tracking.transform.localPosition;
			new_tracking_local_position.x = 1.0f;
			new_tracking_local_position.z = 5.0f;
			tracking.transform.localPosition = new_tracking_local_position;

			//tracking.transform.parent = null;
        }
	}

	
}
