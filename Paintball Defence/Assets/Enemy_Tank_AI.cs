using UnityEngine;
using System.Collections;

public class Enemy_Tank_AI : MonoBehaviour {

    //public Transform EnemyBullet;
    //private float bulletCooldown;
    // Use this for initialization

    //!! NOTE: Barrier destruction logic is contained within a script attached to the barrier part of the tank frame

    public GameObject deathEmitter;
    public GameObject coverDestroyedEmitter;
    private int PushersRemaining;

    void Start()
    {
        //bulletCooldown = 5;
        //this.transform.forward = new Vector3(0, -1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        PushersRemaining = 0;
        float speed = 0.5f;
        if(this.transform.FindChild("Tank Rider 1"))
        {
            PushersRemaining++;
        }
        if (this.transform.FindChild("Tank Rider 2"))
        {
            PushersRemaining++;
        }
        if (this.transform.FindChild("Tank Rider 3"))
        {
            PushersRemaining++;
        }
        if (this.transform.FindChild("Tank Rider 4"))
        {
            PushersRemaining++;
        }

        if(PushersRemaining < 1)
        {
            Instantiate(deathEmitter, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }

        GameObject[] targets = GameObject.FindGameObjectsWithTag("Friendly");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject ClosestTarget = player;
        if (targets.Length != 0)
        {
            foreach (GameObject aTarget in targets)
            {
                if ((aTarget.transform.position - this.gameObject.transform.position).magnitude < (ClosestTarget.transform.position - this.gameObject.transform.position).magnitude)
                {
                    ClosestTarget = aTarget;
                }
            }
        }
        if ((player.transform.position - this.gameObject.transform.position).magnitude < (ClosestTarget.transform.position - this.gameObject.transform.position).magnitude)
        {
            ClosestTarget = player;
        }

        Quaternion targetRotation = Quaternion.LookRotation(ClosestTarget.transform.position - this.transform.position, new Vector3(0, 0, -1));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * 1);
        //this.transform.LookAt(ClosestTarget.transform.position, new Vector3(0, 0, -1));
        //this.transform.rotation.eulerAngles.Set(this.transform.rotation.eulerAngles.x, 90, 0);
        this.transform.position = Vector3.MoveTowards(this.transform.position, ClosestTarget.transform.position, speed * Time.deltaTime);

        /*if (bulletCooldown <= 0) 
		{
			bulletCooldown = 5;
			Instantiate (EnemyBullet, this.transform.position, this.transform.rotation);
		}
		else 
		{
			bulletCooldown -= Time.deltaTime;
		}*/
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        else if(col.gameObject.tag == "Cover_Barrier")
        {
            Instantiate(coverDestroyedEmitter, col.transform.position, this.transform.rotation);
            Destroy(col.gameObject);
        }
    }
}
