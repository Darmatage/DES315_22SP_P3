using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B17_RollingEffects : MonoBehaviour
{

    public GameObject RollAbilityObject;
    private B17_RollAbility rollScript;
    public List<AudioClip> EnvironCollisionSounds;
    public List<AudioClip> PlayerCollisionSounds;
    private AudioSource audioSource;

    private bool offCooldown;
    bool isNonPlayerCollision;
    bool justHitPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rollScript = RollAbilityObject.GetComponent<B17_RollAbility>();
        audioSource = transform.GetComponent<AudioSource>();

        offCooldown = true;
        justHitPlayer = false;
    }

	private void Update()
	{
		if (justHitPlayer && offCooldown)
		{
            // disable damage on the frame after player hit is detected, so play nicely with BotBasic_Damage script
            //transform.tag = "Untagged";
            //offCooldown = false;
            //justHitPlayer = false;
            //Invoke("SetAsHazard", transform.parent.GetComponent<B17_RollAbility>().cooldown);

        }
	}

	void OnTriggerEnter(Collider collision)
    {
        // only play rolling collision sounds when the bot is rolling
        if (!rollScript.GetIsRolling())
            return;

        Transform otherRoot = collision.transform.root;
        string thisPlayer = gameObject.transform.root.tag;
        isNonPlayerCollision = false;

        // determine whether to play crash sound for player collision or environment collision
        if (thisPlayer == "Player1" && otherRoot.tag != "Player2")
        {
            isNonPlayerCollision = true;
        }
        else if (thisPlayer == "Player2" && otherRoot.tag != "Player1")
        {
            isNonPlayerCollision = true;
        }
        else
            isNonPlayerCollision = false;

        justHitPlayer = !isNonPlayerCollision;

        // play the sfx
        if (isNonPlayerCollision)
		{
            audioSource.PlayOneShot(EnvironCollisionSounds[UnityEngine.Random.Range(0, EnvironCollisionSounds.Count)]);
        }
		else
		{
            audioSource.PlayOneShot(PlayerCollisionSounds[UnityEngine.Random.Range(0, PlayerCollisionSounds.Count)]);

            // This disables the weapon too early... 
            transform.tag = "Untagged";
            offCooldown = false;
            justHitPlayer = false;
            Invoke("SetAsHazard", transform.parent.GetComponent<B17_RollAbility>().cooldown);
        }

    }

	private void SetAsHazard()
	{
        transform.tag = "Hazard";
        offCooldown = true;

    }
}
