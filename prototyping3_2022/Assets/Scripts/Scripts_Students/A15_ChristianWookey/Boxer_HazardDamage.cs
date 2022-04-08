using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxer_HazardDamage : HazardDamage{
	private GameHandler gameHandler;

	public GameObject particleLocation;

	public Material[] materials;

	private float delay = 0f;

    public void Update()
    {
		delay = Mathf.Max((delay - Time.deltaTime), 0f);
	}

    void OnCollisionEnter(Collision other) {
		if (delay <= 0f)
		{
			GameObject damageParticles = Instantiate(particlesPrefab, particleLocation.transform.position, other.transform.rotation);
			damageParticles.GetComponent<ParticleSystemRenderer>().material = materials[Random.Range(0, materials.Length)];
			damageParticles.GetComponent<ParticleSystem>().Emit(1);
			delay = 0.1f;
			Destroy(damageParticles, 1f);
		}
	}

}

	//NOTE: this script is just damage
	//hazard object movement is managed by their button
	//reporting damage is done by the damage script on the bots 