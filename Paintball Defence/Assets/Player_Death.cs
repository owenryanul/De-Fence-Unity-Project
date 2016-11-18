using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player_Death : MonoBehaviour {

	// Use this for initialization
	void Start (){
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
        //print("Collision");
		if (col.tag == "Enemy" || col.tag == "EnemyBullet" || col.tag == "EnemyTank") 
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
	}
}
