using UnityEngine;
using System.Collections;

public class Enemy_Move : MonoBehaviour {

	public GameObject EnemyBullet;
    public GameObject Corpse;
	private float bulletCooldown;
	private float spitCooldown;
    private float speed;
    private float topSpeed;
	// Use this for initialization
	void Start () {
		bulletCooldown = 5;
		spitCooldown = 5;
        topSpeed = 1.0f;
        speed = topSpeed;
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


		if ((ClosestTarget.transform.position - this.gameObject.transform.position).magnitude < 5 && spitCooldown < 1) {
			this.transform.LookAt(ClosestTarget.transform.position);
			if (bulletCooldown <= 0) {
				//print ("Firing");
				bulletCooldown = 5;
				spitCooldown = 5;
				Instantiate (EnemyBullet, this.transform.position, this.transform.rotation);
			} else {
				//print ("Aiming");
				bulletCooldown -= Time.deltaTime;
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


    void OnDestroy()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Score>().addScore(100);
        if (Random.Range(0, 5) <= 3)
        {
            Instantiate(Corpse, this.transform.position, Corpse.transform.rotation);
        }
    }
}
