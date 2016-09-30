using UnityEngine;
using System.Collections;

public class Enemy2_Behaviour : MonoBehaviour {

	public GameObject enemyBlastEmmitter;
	private float blastCountdown;
	private bool blasting;
	// Use this for initialization
	void Start () {
		blastCountdown = 5;
		blasting = false;
	}

	// Update is called once per frame
	void Update () 
	{
		float speed = 1.0f;
		Vector3 playerPos = GameObject.FindGameObjectWithTag ("Player").transform.position;

		if (!blasting) 
		{
			//print ("Moving not blasting");
			this.transform.LookAt (playerPos);
			this.transform.position = Vector3.MoveTowards (this.transform.position, playerPos, speed * Time.deltaTime);
		}
		else if (blastCountdown <= 0)
		{
			GameObject[] allcover = GameObject.FindGameObjectsWithTag ("Cover");
			foreach (GameObject acover in allcover) 
			{
				if ((acover.transform.position - this.transform.position).magnitude <= 4)
				{
					Destroy (acover);
				}
			}
			Instantiate (enemyBlastEmmitter, this.transform.position, new Quaternion(0, 0, 180 , 0));
			Destroy (this.gameObject);
		} 
		else 
		{
			blastCountdown -= Time.deltaTime;
		}

	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			Destroy (this.gameObject);
		}

		if (col.gameObject.tag == "Cover_Barrier") 
		{
			print ("Swithcing to blasting");
			blasting = true;
		}
	}
}
