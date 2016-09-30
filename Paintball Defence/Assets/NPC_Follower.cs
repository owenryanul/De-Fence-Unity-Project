using UnityEngine;
using System.Collections;

public class NPC_Follower : MonoBehaviour {

		public Transform bullet;
		public GameObject FollowerBullet;
		private bool isFollower;
		private float shotCooldown;
		private Collider LevelBounds;
		private float leftOvershoot;
		private float rightOvershoot;
	private float upOvershoot;
	private float downOvershoot;

		// Use this for initialization
		void Start () {
			shotCooldown = 5;
			isFollower = true;
			LevelBounds = GameObject.FindGameObjectWithTag ("Level").GetComponent<BoxCollider>();
			upOvershoot = 0;
			downOvershoot = 0;
			leftOvershoot = 0;
			rightOvershoot = 0;
			print("npc created");
		}

		// Update is called once per frame
		void Update () {
			if (isFollower)
			{
				updateFollower ();
			} 
			else
			{
				updateGuard ();
			}
		}

		void updateGuard()
		{
			int range = 10;

			GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
			if (enemies.Length > 0) 
			{
				GameObject ClosestEnemy = enemies [0];
				foreach (GameObject aEnemy in enemies) {
					if ((aEnemy.transform.position - this.gameObject.transform.position).magnitude < (ClosestEnemy.transform.position - this.gameObject.transform.position).magnitude) {
						ClosestEnemy = aEnemy;
					}
				}

				if ((ClosestEnemy.transform.position - this.gameObject.transform.position).magnitude < range)
				{
					this.transform.LookAt (ClosestEnemy.transform);
					if (shotCooldown < 1)
					{
						shotCooldown = 5;
						Instantiate (bullet, this.gameObject.transform.position, this.gameObject.transform.rotation);
					} 
					else
					{
						shotCooldown -= Time.deltaTime;
					}
				}
			}
		}

	void updateFollower()
	{

		float speed = 0.2f;
		Vector3 move = new Vector3 (0, 0, 0);
		//move Up
		if (Input.GetKey(KeyCode.UpArrow)) 
		{
			if (downOvershoot == 0)
			{
				move.y += speed;
			} 
			else
			{
				downOvershoot -= speed;
				if (downOvershoot < 0) {downOvershoot = 0;}
			}


			if (!LevelBounds.bounds.Contains (new Vector3 (this.transform.position.x, this.transform.position.y + move.y, this.transform.position.z))) 
			{
				print ("Up error");
				move.y -= speed;
				upOvershoot += speed;
			}

		}
		//move down
		if (Input.GetKey(KeyCode.DownArrow)) 
		{
			if (upOvershoot == 0) {
				move.y -= speed;
			}
			else
			{
				upOvershoot -= speed;
				if (upOvershoot < 0) {upOvershoot = 0;}
			}

			if (!LevelBounds.bounds.Contains (new Vector3 (this.transform.position.x, this.transform.position.y + move.y, this.transform.position.z)))
			{
				print ("Down error");
				move.y += speed;
				downOvershoot += speed;
			}



		}
		//move left
		if (Input.GetKey(KeyCode.LeftArrow)) 
		{
			if (rightOvershoot == 0) 
			{
				move.x -= speed;
			}
			else
			{
				rightOvershoot -= speed;
				if (rightOvershoot < 0) {rightOvershoot = 0;}
			}

			if (!LevelBounds.bounds.Contains (new Vector3 (this.transform.position.x + move.x, this.transform.position.y, this.transform.position.z)))
			{
				print ("left error");
				move.x += speed;
				leftOvershoot += speed;
			}

		}

		//move right
		if (Input.GetKey(KeyCode.RightArrow)) 
		{
			if (leftOvershoot == 0) {
				move.x += speed;
			} 
			else 
			{
				leftOvershoot -= speed;
				if (leftOvershoot < 0) {leftOvershoot = 0;}
			}
				
			if (!LevelBounds.bounds.Contains (new Vector3 (this.transform.position.x + move.x, this.transform.position.y, this.transform.position.z)))
			{
				print ("right error");
				move.x -= speed;
				rightOvershoot += speed;
			}


		}
		this.transform.position += move;


		if (Input.GetMouseButton (0) && shotCooldown <= 0) {//0 for left mouse button, 1 for right, 2 for middle.
			//smokeEmit.Play ();
			Instantiate (FollowerBullet, this.transform.position, this.transform.rotation);
			shotCooldown = 5;
		}
		shotCooldown -= Time.deltaTime;

		GameObject playerObject = GameObject.FindGameObjectWithTag ("Player");
		Vector3 target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		target.z = -1;
		if (playerObject.GetComponent<Player_Action>().getFiringMode () == 0) 
		{
			this.transform.LookAt (target);
		}
		else if (playerObject.GetComponent<Player_Action>().getFiringMode () == 1) 
		{
			this.transform.LookAt (target + (this.transform.position - playerObject.transform.position));
		}
	}

		public void setIsFollower(bool inbool)
		{
			isFollower = inbool;
			print ("follower changed");
		}
}
