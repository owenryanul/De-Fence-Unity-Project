using UnityEngine;
using System.Collections;

public class GhostCoverFaceMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

		Vector3 targetVector = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		targetVector.z = -1;
		//For unknown reasons, lookat() was rotating the entire prefab 90 aroud the y-axis, no matter what I did. 

		this.transform.LookAt (targetVector);
		//print ("This rotation = " + this.transform.rotation);
		//this.transform.eulerAngles = new  Vector3(0, 0, this.transform.eulerAngles.z);
			//this.transform.Rotate (new Vector3 (0, -90, 0));
		/* else {
			this.transform.Rotate (new Vector3 (0, -90, 0));
		}*/
		//print ("Ghost Rotation: " + this.transform.rotation.eulerAngles);
	}

	public void destroyGhost()
	{
		Destroy (this.gameObject);
	}
		
}
