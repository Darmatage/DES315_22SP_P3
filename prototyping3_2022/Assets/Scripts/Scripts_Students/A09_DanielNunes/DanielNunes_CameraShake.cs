using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanielNunes_CameraShake : MonoBehaviour
{
	//how far the camera can be sent
	[SerializeField]
	public float radius;
	//max duration of camera shake
	[SerializeField]
	public float maxDuration;

	private float duration = 0.0f;
	private Vector3 originalPosition;

	// Start is called before the first frame update
	void Start()
	{
		//store the starting position as the original position 
		originalPosition = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		//while the duration is not at 0
		if (duration > 0.0f)
		{
			//offset the position from the original position with a vector randomly generated using randomRange (between the given amounts)
			float x = originalPosition.x + Random.Range(-radius, radius);
			float y = originalPosition.y + Random.Range(-radius, radius);

			transform.position = new Vector3(x, y, transform.position.z);

			//decrease the duration timer
			duration -= Time.deltaTime;
		}
		//once the duration has ended
		else
		{
			//keep the duration at 0
			duration = 0.0f;
			//set the position back to the original position
			transform.position = originalPosition;
		}
	}

	public void Shake()
	{
		//set the duration to the max value
		duration = maxDuration;
	}

	public void ResetCamera()
	{
		//keep the duration at 0
		duration = 0.0f;
		//set the position back to the original position
		transform.position = originalPosition;
	}
}
