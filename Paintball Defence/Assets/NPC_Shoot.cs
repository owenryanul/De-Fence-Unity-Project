using UnityEngine;
using System.Collections;

public class NPC_Shoot : MonoBehaviour {

	public Transform bullet;
	private bool isFollower;
	private float shotCooldown;
	private Collider LevelBounds;
    private float scoreCooldown;

	// Use this for initialization
	void Start () {
		shotCooldown = 5;
        scoreCooldown = 5;
		isFollower = false;
		LevelBounds = GameObject.FindGameObjectWithTag ("Level").GetComponent<BoxCollider>();
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

        //generate points
        if(scoreCooldown <= 0)
        {
            scoreCooldown = 1;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Score>().addScore(10);
            PopupText_Controller.createPistollerScorePopup(this.transform);
        }
        else
        {
            scoreCooldown -= Time.deltaTime;
        }
	}

	void updateFollower()
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

	public void setIsFollower(bool inbool)
	{
		isFollower = inbool;
		print ("follower changed");
	}
}