using UnityEngine;
using System.Collections;

public class HideShowCoverBox : MonoBehaviour {

	private bool showCoverBox;

	// Use this for initialization
	void Start () {
		showCoverBox = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.transform.GetChild (1).GetComponent<BoxCollider> ().bounds.Contains (GameObject.FindGameObjectWithTag ("Player").transform.position)) {
			showCoverBox = true;
		} else {
			showCoverBox = false;
		}

		if (showCoverBox && !this.gameObject.transform.GetChild (2).gameObject.GetComponent<Renderer> ().enabled) 
		{
				//disable render, not object, to hide the object
			this.gameObject.transform.GetChild (2).gameObject.GetComponent<Renderer> ().enabled = true;
			this.gameObject.transform.GetChild (3).gameObject.GetComponent<Renderer> ().enabled = true;
			this.gameObject.transform.GetChild (4).gameObject.GetComponent<Renderer> ().enabled = true;
			this.gameObject.transform.GetChild (5).gameObject.GetComponent<Renderer> ().enabled = true;
		} 
		else if (!showCoverBox && this.gameObject.transform.GetChild (2).gameObject.GetComponent<Renderer> ().enabled) 
		{
				//re-enable render, not object, to show the object again
			this.gameObject.transform.GetChild (2).gameObject.GetComponent<Renderer> ().enabled = false;
			this.gameObject.transform.GetChild (3).gameObject.GetComponent<Renderer> ().enabled = false;
			this.gameObject.transform.GetChild (4).gameObject.GetComponent<Renderer> ().enabled = false;
			this.gameObject.transform.GetChild (5).gameObject.GetComponent<Renderer> ().enabled = false;
		}
	}


}
