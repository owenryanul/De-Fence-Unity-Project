using UnityEngine;
using System.Collections;

public class Camera_FollowPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 playerPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
		this.transform.position = new Vector3(playerPos.x, playerPos.y, this.transform.position.z);
	}
}
