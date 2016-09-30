﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private Collider LevelBounds;

	// Use this for initialization
	void Start () {
		LevelBounds = GameObject.FindGameObjectWithTag ("Level").GetComponent<BoxCollider>();
	}
		

	// Update is called once per frame
	void Update () 
	{
		float speed = 0.2f;
		Vector3 move = new Vector3 (0, 0, 0);
		//move Up
		if (Input.GetKey(KeyCode.UpArrow)) 
		{
			move.y += speed;
			if (!LevelBounds.bounds.Contains (new Vector3 (this.transform.position.x, this.transform.position.y + move.y, this.transform.position.z)))
			{
				print ("Up error");
				move.y -= speed;
			}
		}
		//move down
		if (Input.GetKey(KeyCode.DownArrow)) 
		{
			move.y -= speed;
			if (!LevelBounds.bounds.Contains (new Vector3 (this.transform.position.x, this.transform.position.y + move.y, this.transform.position.z)))
			{
				print ("Down error");
				move.y += speed;
			}
		}
		//move left
		if (Input.GetKey(KeyCode.LeftArrow)) 
		{
			move.x -= speed;
			if (!LevelBounds.bounds.Contains (new Vector3 (this.transform.position.x + move.x, this.transform.position.y, this.transform.position.z)))
			{
				print ("left error");
				move.x += speed;
			}
		}

		//move right
		if (Input.GetKey(KeyCode.RightArrow)) 
		{
			move.x += speed;
			if (!LevelBounds.bounds.Contains (new Vector3 (this.transform.position.x + move.x, this.transform.position.y, this.transform.position.z)))
			{
				print ("right error");
				move.x -= speed;
			}
		}
		this.transform.position += move;

	}


}