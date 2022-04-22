using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B08_Weapons : MonoBehaviour
{
    //grab axis from parent object
	public string button1;
	public string button2;
	public string button3;
	public string button4; // currently boost in player move script

	public GameObject Pivot;

	private float StartingAngle;

	private bool SwingReady;
	private bool SwingInProgress;
	private float currentY = 0.0f;
	private float buildUp = 0.1f;


	void Start()
	{
		button1 = gameObject.transform.parent.GetComponent<playerParent>().action1Input;
		button2 = gameObject.transform.parent.GetComponent<playerParent>().action2Input;
		button3 = gameObject.transform.parent.GetComponent<playerParent>().action3Input;
		button4 = gameObject.transform.parent.GetComponent<playerParent>().action4Input;

		SwingReady = true;
		SwingInProgress = false;
		
		

	}
    void Update()
    {
		if ((Input.GetButtonDown(button1)) && (SwingReady == true))
		{
			SwingInProgress = true;
			SwingReady = false;
			StartCoroutine(WithdrawWeapon());
		}

		if ((SwingInProgress == true))
        {
			
			Pivot.transform.Rotate(0, Time.deltaTime*2160, 0);
			currentY += Time.deltaTime * 2160;
			if (currentY >= 360)
            {
				currentY = 0.0f;
				SwingInProgress=false;
            }

		}
	}

	IEnumerator WithdrawWeapon()
	{
		yield return new WaitForSeconds(3.0f);
		SwingReady = true;
	}
}
