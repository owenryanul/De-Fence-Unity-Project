using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	public GameObject enemy;
    public GameObject enemy_exploder;
    public GameObject enemy_shooter;
	private float spawncooldown;
	private float mobCooldown;

	// Use this for initialization
	void Start () {
		spawncooldown = 5;
		mobCooldown = 5;
	}
	
	// Update is called once per frame
	void Update () {
		if (spawncooldown < 1) 
		{
            GameObject[] spawnpoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
            if (spawnpoints.Length > 0)
            {
                Vector3 spawnpoint = spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position;
                while (GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>().bounds.Contains(spawnpoint))
                {
                    //if spawnpoint in player, pick new spawnpoint
                    spawnpoint = new Vector3(Random.Range(-14, 14), Random.Range(-14, 14), -1);
                }

                if (mobCooldown < 1)
                {
                    print("Enemy Mob Spawned");
                    Instantiate(enemy, spawnpoint, enemy.transform.rotation);
                    Instantiate(enemy, spawnpoint + new Vector3(1.5f, 1.5f, 0), enemy.transform.rotation);
                    Instantiate(enemy, spawnpoint + new Vector3(-1.5f, 1.5f, 0), enemy.transform.rotation);
                    Instantiate(enemy, spawnpoint + new Vector3(1.5f, -1.5f, 0), enemy.transform.rotation);
                    Instantiate(enemy, spawnpoint + new Vector3(-1.5f, -1.5f, 0), enemy.transform.rotation);
                    mobCooldown = 5;
                }
                else
                {
                    int thing = Random.Range(0, 3);//Documentation says max inclusive, but is acting exclusive
                    print(thing);
                    switch (thing)
                    {
                        case 1:
                            Instantiate(enemy_exploder, spawnpoint, enemy.transform.rotation);
                            print("Enemy Exploder Spawned");
                            mobCooldown--;
                            break;

                        case 2:
                            Instantiate(enemy_shooter, spawnpoint, enemy.transform.rotation);
                            print("Enemy Shooter Spawned");
                            mobCooldown--;
                            break;

                        default: 
                            Instantiate(enemy, spawnpoint, enemy.transform.rotation);
                            print("Enemy Spawned");
                            mobCooldown--;
                            break;
                    }

                }
                spawncooldown = 5;
            }
		}
		else 
		{
			spawncooldown -= Time.deltaTime;
		}
	}
}
