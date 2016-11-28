using UnityEngine;
using System.Collections;

public class Enemy_Shooter_AI: MonoBehaviour {

	public GameObject EnemyBullet;
    public bool isTargetDummy;
	private float AimingTimeRemaining;
	private float spitCooldown;
    private float speed;
    private float topSpeed;
    private float maxRange;
	// Use this for initialization
	void Start () {
		AimingTimeRemaining = 3;
		spitCooldown = 5;
        if (isTargetDummy)
        {
            topSpeed = 0;//immoblises the shooter if its a target dummy.
        }
        else
        {
            topSpeed = 1.0f;
        }
        speed = topSpeed;
        maxRange = 12;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//float speed = 1.0f;

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


		if ((ClosestTarget.transform.position - this.gameObject.transform.position).magnitude < maxRange && spitCooldown < 1) {
			this.transform.LookAt(ClosestTarget.transform.position);
			if (AimingTimeRemaining <= 0) {
				//print ("Firing");
				AimingTimeRemaining = 3;
				spitCooldown = 5;
				Instantiate (EnemyBullet, this.transform.position, this.transform.rotation);
			} else {
				//print ("Aiming");
				AimingTimeRemaining -= Time.deltaTime;
			}
		}
		else
		{
			//print ("Running");
			spitCooldown -= Time.deltaTime;
			Vector3 heading = ClosestTarget.transform.position;
			heading.z = -1;
			this.transform.LookAt(heading);
			this.transform.position = Vector3.MoveTowards(this.transform.position, heading, speed * Time.deltaTime);
		} 
		
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			Destroy (this.gameObject);
		}
	}

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Corpse")
        {
            speed = topSpeed / 2;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Corpse")
        {
            speed = topSpeed;
        }
    }
}
