using UnityEngine;
using System.Collections;

public class Fence_Break : MonoBehaviour {

	private float FenceBreakCountdown;
	public GameObject brokenFence;
	// Use this for initialization
	void Start () {
		FenceBreakCountdown = 10;
	}
	
	// Update is called once per frame
	void Update () {

		if (FenceBreakCountdown <= 0) {
			GameObject[] fences = GameObject.FindGameObjectsWithTag ("Fence");
			GameObject fenceToBreak = fences [Random.Range (0, fences.Length)];
			Instantiate (brokenFence, fenceToBreak.transform.position, fenceToBreak.transform.rotation);
			Destroy (fenceToBreak);

			FenceBreakCountdown = Random.Range (10, 60);
		}
		else
		{
			FenceBreakCountdown -= Time.deltaTime;
		}
			

	}
}
