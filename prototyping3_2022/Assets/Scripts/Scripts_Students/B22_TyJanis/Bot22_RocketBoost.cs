using System.Collections;
using UnityEngine;

public class Bot22_RocketBoost : MonoBehaviour
{
    public GameObject leftBoost;
    public GameObject rightBoost;
	public GameObject body;
	public GameObject cooldownMeter;
	public GameObject spike;
	public float boostSpeed = 1000f;
	public float cooldownTime = 2.0f;
	private AudioSource _audio;
	public AudioClip chargeSound;
	private float _nextFireTime;
	private readonly float _thrustAmount = 1f;
	
	private bool _boosting;

	private Rigidbody _rb;
	private Renderer _cooldownColor;

	//grab axis from parent object
	[HideInInspector]
	public string button1;
	[HideInInspector]
	public string button2;
	[HideInInspector]
	public string button3;
	[HideInInspector]
	public string button4; // currently boost in player move script

	private static readonly int Color1 = Shader.PropertyToID("_Color");

	// Start is called before the first frame update
    void Start()
    {
	    var parent = gameObject.transform.parent;
	    button1 = parent.GetComponent<playerParent>().action1Input;
		button2 = parent.GetComponent<playerParent>().action2Input;
		button3 = parent.GetComponent<playerParent>().action3Input;
		button4 = parent.GetComponent<playerParent>().action4Input;

		if (body.GetComponent<Rigidbody>() != null)
		{
			_rb = body.GetComponent<Rigidbody>();
		}

		_cooldownColor = cooldownMeter.GetComponent<Renderer>();

		_audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
	    if(Time.time > _nextFireTime)
		{
			_cooldownColor.material.SetColor(Color1, Color.green);

			if ((Input.GetButton(button1)) && !_boosting)
			{
				_audio.PlayOneShot(chargeSound);
				_boosting = true;

				leftBoost.transform.Translate(0,_thrustAmount, 0);
				rightBoost.transform.Translate(0,_thrustAmount, 0);
			
				_rb.AddForce(transform.forward * boostSpeed, ForceMode.Impulse);
				StartCoroutine(TurnOffBoost()); 

				_nextFireTime = Time.time + cooldownTime;

			}

		}
		else
		{
			_cooldownColor.material.SetColor(Color1, Color.red);
		}

	    spike.GetComponent<HazardDamage>().damage = !_boosting ? 1.0f : 3.0f;

	    //Debug.Log(spike.GetComponent<HazardDamage>().damage);	
    }
    
    IEnumerator TurnOffBoost()
    {
		yield return new WaitForSeconds(0.6f);
		leftBoost.transform.Translate(0,-_thrustAmount, 0);
        rightBoost.transform.Translate(0,-_thrustAmount, 0);
		_boosting = false;
	}

}
