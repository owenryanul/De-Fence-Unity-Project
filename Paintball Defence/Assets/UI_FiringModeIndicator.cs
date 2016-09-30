using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_FiringModeIndicator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setFireModeIndicator (string inFireModeName)
	{
		this.GetComponent<Text> ().text = "Firing Mode: " + inFireModeName;
	}
}
