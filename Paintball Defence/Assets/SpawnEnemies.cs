using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	public GameObject enemy;
    public GameObject enemy_exploder;
    public GameObject enemy_shooter;
	private float spawncooldown;
	private float mobCooldown;
    private bool spawningMob;
    private int mobMembersYetToSpawn;
    private float mobMemberCooldown;
    private Vector3 mobSpawnpoint;

	// Use this for initialization
	void Start () {
		spawncooldown = (10);
		mobCooldown = 5;
        mobMembersYetToSpawn = 5;
        mobMemberCooldown = 2;
        spawningMob = false;
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

                if (mobCooldown <= 0)//spawn a mob of basic enemies
                {
                    if (spawningMob)
                    {
                        if (mobMemberCooldown <= 0)
                        {
                            if (mobMembersYetToSpawn > 0)//spawn individual members of the mob with a 1 second delay
                            {
                                print("Enemy Mob Spawned");
                                Instantiate(enemy, mobSpawnpoint, enemy.transform.rotation);
                                mobMemberCooldown = 2;
                                mobMembersYetToSpawn--;
                            }
                            else//finish spawning the mob and return to normal spawning routine
                            {
                                mobCooldown = 5;
                                mobMembersYetToSpawn = 5;
                                spawningMob = false;
                                spawncooldown = 5;
                            }
                        }
                        else
                        {
                            mobMemberCooldown -= Time.deltaTime;
                        }
                    }
                    else//start the mob spawning routine
                    {
                        spawningMob = true;
                        mobSpawnpoint = spawnpoint;
                    }
                }
                else//spawn random type of enemy
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
                    spawncooldown = ((10 / GameObject.FindGameObjectsWithTag("SpawnPoint").Length) + 1);

                }
                
            }
		}
		else 
		{
			spawncooldown -= Time.deltaTime;
		}
	}
}
