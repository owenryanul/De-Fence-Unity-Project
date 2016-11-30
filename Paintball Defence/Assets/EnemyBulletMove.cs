using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBulletMove : MonoBehaviour {

	private Vector3 target;
	private Vector3 heading;
	private float maxLifeTime;
	private float currentLifeTime;
	private List<GameObject> coverToIgnore;

	// Use this for initialization
	void Start () {
		//startlocation set by enemy shooting script
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("Friendly");
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		GameObject ClosestTarget = player;
		if (targets.Length != 0) {
			foreach (GameObject aTarget in targets) {
				if ((aTarget.transform.position - this.gameObject.transform.position).magnitude < (ClosestTarget.transform.position - this.gameObject.transform.position).magnitude) {
					ClosestTarget = aTarget;
				}
			}
		}
		target = ClosestTarget.transform.position;
		target.z = -1;

		maxLifeTime = 5;
		currentLifeTime = 0;

		heading = Vector3.Normalize(target - this.transform.position);


		this.transform.LookAt(target);//takes target and worldup, but defaults to y-axis

		coverToIgnore = new List<GameObject> ();
		GameObject[] coverAreas = GameObject.FindGameObjectsWithTag ("Cover_Area");
		foreach (GameObject aCoverArea in coverAreas) {
			if (aCoverArea.GetComponent<Collider> ().bounds.Contains (this.transform.position)) {
				//print ("adding cover area to ignore list");
				coverToIgnore.Add (aCoverArea.transform.parent.GetChild (0).gameObject);//the barrier part of the cover.
			}
		}
	}

	// Update is called once per frame
	void Update () {
		float speed = 20f;

		this.transform.position += ((heading * speed) * Time.deltaTime);
		if (currentLifeTime >= maxLifeTime) {
			Destroy (gameObject);
		}
		else
		{
			currentLifeTime += Time.deltaTime;
		}
	}
		
	void OnTriggerEnter(Collider col)
	{
		//print("collision in");
		if (col.tag == "Player") 
		{
			Destroy (this.gameObject);
		}

		if (col.tag == "Cover_Barrier" || col.tag == "EnemyTank_Barrier") 
		{
			bool ignorethisCollision = false;
			foreach (GameObject acover in coverToIgnore) 
			{
				if (acover.GetInstanceID() == col.gameObject.GetInstanceID()) 
				{
					//print("ignoring this collision");
					ignorethisCollision = true;
					break;
				}
			}

			if (!ignorethisCollision) 
			{
				Destroy (this.gameObject);
			}
		}
	}
}
