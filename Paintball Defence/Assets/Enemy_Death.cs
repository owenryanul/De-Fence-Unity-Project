using UnityEngine;
using System.Collections;

public class Enemy_Death : MonoBehaviour {

    public GameObject corpse;
    public GameObject enemyDeathEmiter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void killEnemy()
    {
        if (Random.Range(0, 5) <= 3)
        {
            Instantiate(corpse, this.transform.position, corpse.transform.rotation);
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Score>().addScore(100);
        Instantiate(enemyDeathEmiter, this.gameObject.transform.position, new Quaternion(0, 180, 180, 0));
        Destroy(this.gameObject);
    }
}
