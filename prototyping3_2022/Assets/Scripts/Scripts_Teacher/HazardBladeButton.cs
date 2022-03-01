using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBladeButton : MonoBehaviour
{

	public GameObject theBlade;
	public GameObject theButton;
	private float theButtonUpPos;
	public bool isSpinning = true;
	public bool isMoving = false;
	public float bladeRotateSpeed = 8f; 
	public float bladeMoveTime = 1.0f; 
	public Transform bladePathStart; 
	public Transform bladePathEnd; 
	public bool atStart = true;

    void Start()
    {
		if (theButton != null){
			theButtonUpPos = theButton.transform.position.y;
		}
    }

    void FixedUpdate()
    {
		if (isSpinning == true){
			theBlade.transform.Rotate(0,bladeRotateSpeed, 0);
		}
     	
		//move the blade
		if (isMoving == true){
			if (atStart == true){
				bladeRotateSpeed = bladeRotateSpeed * -1;
				Vector3 targetPosition = bladePathEnd.position;
				StartCoroutine(LerpPosition(targetPosition, bladeMoveTime));
			}
			else if (atStart == false){
				bladeRotateSpeed = bladeRotateSpeed * -1;
				Vector3 targetPosition = bladePathStart.position;
				StartCoroutine(LerpPosition(targetPosition, bladeMoveTime));
			}
		}

		//stop the blade
		if (theBlade.transform.position.x == bladePathEnd.transform.position.x){
			ButtonUp();
			atStart = false;
		}
		else if (theBlade.transform.position.x == bladePathStart.transform.position.x){
			ButtonUp();
			atStart = true;
		}
    }

	public void OnTriggerEnter(Collider other){
		if ((other.transform.root.gameObject.tag=="Player1")||(other.transform.root.gameObject.tag=="Player2")){
			theButton.transform.position = new Vector3(theButton.transform.position.x, theButtonUpPos - 0.4f, theButton.transform.position.z);
			isMoving = true;
			Renderer buttonRend = theButton.GetComponent<Renderer>();
			buttonRend.material.color = new Color(2.0f, 0.5f, 0.5f, 2.5f); 
		}
	}

	public void ButtonUp(){
		isMoving = false;
		theButton.transform.position = new Vector3(theButton.transform.position.x, theButtonUpPos, theButton.transform.position.z);
		Renderer buttonRend = theButton.GetComponent<Renderer>();
		buttonRend.material.color = Color.white;
	}


	IEnumerator LerpPosition(Vector3 targetPosition, float duration){
		float time = 0;
		Vector3 startPosition = theBlade.transform.position;

		while (time < duration)
		{
			theBlade.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
			time += Time.deltaTime;
			yield return null;
		}
		theBlade.transform.position = targetPosition;
	}

		
}
