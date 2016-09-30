using UnityEngine;
using System.Collections;

public class Cover_Placed : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 targetVector = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		targetVector.z = -1;
		//For unknown reasons, lookat() was rotating the entire prefab 90 aroud the y-axis, no matter what I did. 
		this.transform.LookAt (targetVector);
		if (this.transform.eulerAngles.y != 270) 
		{
			this.transform.Rotate (new Vector3 (0, 0, 180));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
