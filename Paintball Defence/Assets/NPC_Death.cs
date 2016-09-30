using UnityEngine;
using System.Collections;

public class NPC_Death : MonoBehaviour {

	public Transform deathParticleEmmiter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Enemy" || col.tag == "EnemyBullet") 
		{
			Destroy (this.gameObject);
			Instantiate (deathParticleEmmiter, this.transform.position, this.transform.rotation);
		}
	}
}
