using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC_Bullet_Move_Spread : MonoBehaviour {

	private Vector3 target;
	private Vector3 heading;
	private float maxLifeTime;
	private float currentLifeTime;
	public float speed;
	private float currentspeed;
	public GameObject enemyDeathEmmiter;
	public float bulletspread;
	private List<GameObject> coverToIgnore;

	// Use this for initialization
	void Start () {

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		if (enemies.Length > 0)
		{
			GameObject ClosestEnemy = enemies [0];
			foreach (GameObject aEnemy in enemies) {
				if ((aEnemy.transform.position - this.gameObject.transform.position).magnitude < (ClosestEnemy.transform.position - this.gameObject.transform.position).magnitude) {
					ClosestEnemy = aEnemy;
				}
			}
			target = ClosestEnemy.transform.position;
			target.z = -1;

			maxLifeTime = 5;
			currentLifeTime = 0;

			heading = Vector3.Normalize((target - this.transform.position) + new Vector3(Random.Range(-bulletspread, bulletspread + 1), Random.Range(-bulletspread, bulletspread + 1), 0));

			//Ray aray = Camera.main.ScreenPointToRay (Input.mousePosition);
			//target = aray.origin - (aray.direction * );
			this.transform.LookAt (target);//takes target and worldup, but defaults to y-axis

			//speed = 40f;
			currentspeed = speed;

			coverToIgnore = new List<GameObject> ();
			GameObject[] coverAreas = GameObject.FindGameObjectsWithTag ("Cover_Area");
			foreach (GameObject aCoverArea in coverAreas) {
				if (aCoverArea.GetComponent<Collider> ().bounds.Contains (this.transform.position)) {
					//print ("adding cover area to ignore list");
					coverToIgnore.Add (aCoverArea.transform.parent.GetChild (0).gameObject);//the barrier part of the cover.
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {

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
		if (col.tag == "Enemy") 
		{
			Instantiate (enemyDeathEmmiter, col.gameObject.transform.position, new Quaternion(0, 180, 180 , 0));
			Destroy (col.gameObject);
			Destroy (this.gameObject);
		}

		if (col.tag == "Cover_Barrier" || col.tag == "EnemyTank_Barrier") 
		{
			bool ignorethisCollision = false;
			foreach (GameObject acover in coverToIgnore) 
			{
				if (acover.name == col.gameObject.name) 
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
