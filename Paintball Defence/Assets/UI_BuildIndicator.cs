using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_BuildIndicator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setBuildIndicator (string inBuildName)
	{
		this.GetComponent<Text> ().text = "Build Selected: " + inBuildName;
	}
}
