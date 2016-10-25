using UnityEngine;
using System.Collections;

public class Fence_Break : MonoBehaviour {

	private float FenceBreakCountdown;
    public GameObject tank;
	public GameObject brokenFence;
	// Use this for initialization
	void Start () {
		FenceBreakCountdown = 10;
	}
	
	// Update is called once per frame
	void Update () {

		if (FenceBreakCountdown <= 0) {
            //get unbroken fence at random
			GameObject[] fences = GameObject.FindGameObjectsWithTag ("Fence");
            if (fences.Length > 0)
            {
                GameObject fenceToBreak = fences[Random.Range(0, fences.Length)];

                //create the broken version of the brokenfence
                Instantiate(brokenFence, fenceToBreak.transform.position, fenceToBreak.transform.rotation);

                //Create tank that broke the fence and have it face the centre
                Quaternion rotationOfTank = tank.transform.rotation;
                rotationOfTank.SetLookRotation((new Vector3(0, 0, -1)) - fenceToBreak.transform.position, new Vector3(0, 0, -1));
                Instantiate(tank, fenceToBreak.transform.position, rotationOfTank);

                //Destory the fence we're removing
                Destroy(fenceToBreak);
            }

			FenceBreakCountdown = Random.Range (10, 60);
		}
		else
		{
			FenceBreakCountdown -= Time.deltaTime;
		}
			

	}
}
