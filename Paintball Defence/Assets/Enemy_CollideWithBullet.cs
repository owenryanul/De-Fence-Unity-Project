using UnityEngine;
using System.Collections;

public class Enemy_CollideWithBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		//on collision with a player bullet, destroy this enemy
		//if (col.gameObject.tag == "PlayerBullet")
		//{
			Destroy(gameObject);
		//}
	}
}
