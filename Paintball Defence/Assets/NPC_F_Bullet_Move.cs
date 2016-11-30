using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC_F_Bullet_Move : MonoBehaviour {
	public float bulletspread;
	public float speed;
	private Vector3 target;
	private Vector3 heading;
	private float maxLifeTime;
	private float currentLifeTime;
	private float currentspeed;
	public GameObject enemyDeathEmmiter;
	private List<GameObject> coverToIgnore;

	// Use this for initialization
	void Start () {
		GameObject playerObject = GameObject.FindGameObjectWithTag ("Player");
		target = playerObject.GetComponent<Player_Action> ().getTargetVector ();
		Vector3 preheading = Vector3.zero;
		if (playerObject.GetComponent<Player_Action> ().getFiringMode () == 0) //focused 
		{
			preheading = (target - this.transform.position) + new Vector3 (Random.Range (-bulletspread, bulletspread + 1), Random.Range (-bulletspread, bulletspread + 1), 0);
			heading = Vector3.Normalize(preheading);
            this.transform.rotation = Quaternion.LookRotation(heading);//takes target and worldup, but defaults to y-axis
        } 
		else if (playerObject.GetComponent<Player_Action> ().getFiringMode () == 1) //parralell
		{
			preheading = (target - playerObject.transform.position) + new Vector3 (Random.Range (-bulletspread, bulletspread + 1), Random.Range (-bulletspread, bulletspread + 1), 0);
			heading = Vector3.Normalize(preheading);
            this.transform.rotation = Quaternion.LookRotation(heading);//takes target and worldup, but defaults to y-axis
        }
			maxLifeTime = 5;
			currentLifeTime = 0;

			

			//Ray aray = Camera.main.ScreenPointToRay (Input.mousePosition);
			//target = aray.origin - (aray.direction * );
		    /*if (playerObject.GetComponent<Player_Action> ().getFiringMode () == 0) //focused 
			{
				this.transform.LookAt (preheading);//takes target and worldup, but defaults to y-axis
			} 
			else if (playerObject.GetComponent<Player_Action> ().getFiringMode () == 1) //parralell
			{
				this.transform.LookAt (preheading);//takes target and worldup, but defaults to y-axis
			}*/
			

			//speed = 40f;
			currentspeed = speed;

			coverToIgnore = new List<GameObject> ();
			GameObject[] coverAreas = GameObject.FindGameObjectsWithTag ("Cover_Area");
			foreach (GameObject aCoverArea in coverAreas) {
				if (aCoverArea.GetComponent<Collider> ().bounds.Contains (this.transform.position)) {
					print ("adding cover area to ignore list");
					coverToIgnore.Add (aCoverArea.transform.parent.GetChild (0).gameObject);//the barrier part of the cover.
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
		print("collision in");
		if (col.tag == "Enemy") 
		{
            col.gameObject.GetComponent<Enemy_Death>().killEnemy();
			Destroy (this.gameObject);
		}

		if (col.tag == "Cover_Barrier" || col.tag == "EnemyTank_Barrier") 
		{
			bool ignorethisCollision = false;
			foreach (GameObject acover in coverToIgnore) 
			{
				if (acover.GetInstanceID() == col.gameObject.GetInstanceID()) 
				{
					print("ignoring this collision");
					ignorethisCollision = true;
					break;
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
