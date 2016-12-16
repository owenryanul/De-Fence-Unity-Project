using UnityEngine;
using System.Collections;

public class FixChildRotation : MonoBehaviour {

    public Transform rotationToBeFixedTo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    this.gameObject.transform.rotation = rotationToBeFixedTo.rotation;
	}
}
