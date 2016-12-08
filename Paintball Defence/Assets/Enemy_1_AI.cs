﻿using UnityEngine;
using System.Collections;

public class Enemy_1_AI : MonoBehaviour {

    //public Transform EnemyBullet;
	//private float bulletCooldown;
	// Use this for initialization
    private float topSpeed;
    private float speed;
    private float soundCooldown;
    public AudioClip grrSound;
    private AudioSource soundSource;

	void Start () {
        //bulletCooldown = 5;
        topSpeed = 1.0f;
        speed = topSpeed;
	}

	// Update is called once per frame
	void Update () 
	{
		//float speed = topSpeed;

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
		if ((player.transform.position - this.gameObject.transform.position).magnitude < (ClosestTarget.transform.position - this.gameObject.transform.position).magnitude) {
			ClosestTarget = player;
		}

		this.transform.LookAt(ClosestTarget.transform.position);
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
			Destroy (this.gameObject);
		}
	}

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Corpse")
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
