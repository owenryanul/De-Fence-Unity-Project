using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet_Move : MonoBehaviour {

	private Vector3 target;
	private Vector3 heading;
	private float maxLifeTime;
	private float currentLifeTime;
	private float speed;
	private float currentspeed;
	public GameObject enemyDeathEmmiter;
	private List<GameObject> coverToIgnore;

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player_Action>().getTargetVector();


		maxLifeTime = 5;
		currentLifeTime = 0;

		heading = Vector3.Normalize(target - this.transform.position);

		this.transform.position = GameObject.FindGameObjectWithTag ("Player").transform.position;
		//Ray aray = Camera.main.ScreenPointToRay (Input.mousePosition);
		//target = aray.origin - (aray.direction * );
		this.transform.LookAt(target);//takes target and worldup, but defaults to y-axis

		speed = 40f;
		currentspeed = speed;

		coverToIgnore = new List<GameObject> ();
		GameObject[] coverAreas = GameObject.FindGameObjectsWithTag ("Cover_Area");
		foreach (GameObject aCoverArea in coverAreas) 
		{
			if (aCoverArea.GetComponent<Collider> ().bounds.Contains (this.transform.position))
			{
				//print ("adding cover area to ignore list");
				coverToIgnore.Add(aCoverArea.transform.parent.GetChild(0).gameObject);//the barrier part of the cover.
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		this.transform.position += ((heading * speed) * Time.deltaTime);
		if (currentLifeTime >= maxLifeTime) {
			Destroy (gameObject);
		}
		else
		{
			currentLifeTime += Time.deltaTime;
		}
	}




    void OnTriggerEnter(Collider col)
    {
        //print("collision in");
        if (col.tag == "Enemy")
        {
            Instantiate(enemyDeathEmmiter, col.gameObject.transform.position, new Quaternion(0, 180, 180, 0));
            Destroy(col.gameObject);
            Destroy(this.gameObject);
        }

        if (col.tag == "Cover_Barrier")
        {
            if (coverToIgnore.Count != 0)
            {
                bool ignorethisCollision = false;
                foreach (GameObject acover in coverToIgnore)
                {
                    if (acover.name == col.gameObject.name)
                    {
                        //print("ignoring this collision");
                        ignorethisCollision = true;
                        break;
                    }
                }

                if (!ignorethisCollision)
                {
                    Destroy(this.gameObject);
                }
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
