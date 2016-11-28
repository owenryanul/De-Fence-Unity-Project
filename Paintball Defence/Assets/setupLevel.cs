using UnityEngine;
using System.Collections;

public class setupLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PopupText_Controller.setupPopupText_Controller();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Score>().setScore(500);	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
