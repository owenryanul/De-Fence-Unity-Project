using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCBullet_Move : MonoBehaviour {

	private Vector3 target;
	private Vector3 heading;
	private float maxLifeTime;
	private float currentLifeTime;
	private float speed;
	private float currentspeed;
	public GameObject enemyDeathEmmiter;
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

			heading = Vector3.Normalize (target - this.transform.position);

			//Ray aray = Camera.main.ScreenPointToRay (Input.mousePosition);
			//target = aray.origin - (aray.direction * );
			this.transform.LookAt (target);//takes target and worldup, but defaults to y-axis

			speed = 40f;
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
            col.gameObject.GetComponent<Enemy_Death>().killEnemy();
			Destroy (this.gameObject);
		}

		if (col.tag == "Cover_Barrier" || col.tag == "EnemyTank_Barrier") 
		{
			bool ignorethisCollision = false;
            //!!!Note to self, if no cover exisits ignore.
            if (coverToIgnore.Count > 0)
            {
                foreach (GameObject acover in coverToIgnore)
                {
                    if (acover.GetInstanceID() == col.gameObject.GetInstanceID())
                    {
                        //print("ignoring this collision");
                        ignorethisCollision = true;
                        break;
                    }
                }
            }

			if (!ignorethisCollision) 
			{
                //if the barriers are not to be ignored due to the bullet source being close to them, then the bullet collides with the barrier and is destroyed.
                //if the barrier is the barrier of a tank enemy, inflict damage on the barrier before destroying the bullet.
                if (col.tag == "EnemyTank_Barrier")
                {
                    print("Detected Enemy Tank Barrier");
                    col.gameObject.GetComponent<Enemy_Tank_BarrierDamage>().damageBarrier();
                }
                Destroy (this.gameObject);
			}
		}
	}
}
