using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private Collider[] LevelBounds;

	// Use this for initialization
	void Start () {
        LevelBounds = GameObject.FindGameObjectWithTag("Level").GetComponentsInChildren<BoxCollider>();
		//LevelBounds = GameObject.FindGameObjectWithTag ("Level").GetComponent<BoxCollider>();
	}
		

	// Update is called once per frame
	void Update () 
	{
		float speed = 0.2f;
		Vector3 move = new Vector3 (0, 0, 0);
		//move Up
		if (Input.GetKey(KeyCode.UpArrow)) 
		{
			move.y += speed;
            bool isInLevelBounds = false;
            foreach (BoxCollider aCollider in LevelBounds)
            {
                if (aCollider.bounds.Contains(new Vector3(this.transform.position.x, this.transform.position.y + move.y, this.transform.position.z)))
                {
                    isInLevelBounds = true;
                    break;   
                }
            }
            if(!isInLevelBounds)
            {
                print("Unable to move player Up, out of level bounds");
                move.y -= speed;
            }
		}
		//move down
		if (Input.GetKey(KeyCode.DownArrow)) 
		{
			move.y -= speed;
            bool isInLevelBounds = false;
            foreach (BoxCollider aCollider in LevelBounds)
            {
                if (aCollider.bounds.Contains(new Vector3(this.transform.position.x, this.transform.position.y + move.y, this.transform.position.z)))
                {
                    isInLevelBounds = true;
                    break;
                }
            }
            if (!isInLevelBounds)
            {
                print("Unable to move player Down, out of level bounds");
                move.y += speed;
            }
        }
		//move left
		if (Input.GetKey(KeyCode.LeftArrow)) 
		{
			move.x -= speed;
            bool isInLevelBounds = false;
            foreach (BoxCollider aCollider in LevelBounds)
            {
                if (aCollider.bounds.Contains(new Vector3(this.transform.position.x + move.x, this.transform.position.y, this.transform.position.z)))
                {
                    isInLevelBounds = true;
                    break;
                }
            }
            if (!isInLevelBounds)
            {
                print("Can't move Left, out of level bounds");
                move.x += speed;
            }
        }

		//move right
		if (Input.GetKey(KeyCode.RightArrow)) 
		{
            move.x += speed;
            bool isInLevelBounds = false;
            foreach (BoxCollider aCollider in LevelBounds)
            {
                if (aCollider.bounds.Contains(new Vector3(this.transform.position.x + move.x, this.transform.position.y, this.transform.position.z)))
                {
                    isInLevelBounds = true;
                    break;
                }
            }
            if (!isInLevelBounds)
            {
                print("Unable to move player right, out of level bounds");
                move.x -= speed;
            }
        }
		this.transform.position += move;

        foreach(GameObject aPopup in GameObject.FindGameObjectsWithTag("Score_Popup"))
        {
            aPopup.GetComponent<Popup_Kill>().nudgePopup(move.x, move.y);
        }

	}


}
