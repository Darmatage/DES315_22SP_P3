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
			string player_number = this.transform.parent.tag;
			GameObject[] mines = null;

			if (player_number == "Player1")
				mines = GameObject.FindGameObjectsWithTag("Player1_Mine");

			else if (player_number == "Player2")
				mines = GameObject.FindGameObjectsWithTag("Player2_Mine");

			if (mines == null || mines.Length < 5)
            {
				GameObject mine = Instantiate(mine_prefab);

				mine.transform.parent = this.transform;


				if (player_number == "Player1")
					mine.tag = "Player1_Mine";

				else if (player_number == "Player2")
					mine.tag = "Player2_Mine";



				Vector3 new_mine_local_position = mine.transform.localPosition;
				new_mine_local_position.y = -0.75f;
				new_mine_local_position.z = -4.0f;
				mine.transform.localPosition = new_mine_local_position;

				mine.transform.parent = null;
			}
		}

		if (Input.GetButtonDown(button2))
        {
			string player_number = this.transform.parent.tag;
			GameObject monkey = null;

			if (player_number == "Player1")
				monkey = GameObject.FindGameObjectWithTag("P1_Tracker");

			else if (player_number == "Player2")
				monkey = GameObject.FindGameObjectWithTag("P2_Tracker");

			if (monkey == null)
            {
				GameObject tracking = Instantiate(tracking_prefab);

				tracking.transform.SetParent(this.transform);

				if (player_number == "Player1")
					tracking.tag = "P1_Tracker";

				else if (player_number == "Player2")
					tracking.tag = "P2_Tracker";


				
				Vector3 new_tracking_local_position = tracking.transform.localPosition;
				new_tracking_local_position.x = 1.0f;
				new_tracking_local_position.z = 5.0f;
				tracking.transform.localPosition = new_tracking_local_position;
			}
        }
	}

	
}
