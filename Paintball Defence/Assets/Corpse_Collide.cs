using UnityEngine;
using System.Collections;

public class Corpse_Collide : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider col)
    {
        //print("wooo");
        if (col.tag == "Player Bullet")
        {
            //print("woop");
            if (Random.Range(0, 10) <= 1) //10% chance that the corpse will block a bullet.
            {
                Destroy(col.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
