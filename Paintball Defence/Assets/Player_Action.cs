using UnityEngine;
using System.Collections;

public class Player_Action : MonoBehaviour {

    public GameObject PlayerBullet;
    public GameObject PlayerFlamethrowerBullet;

    public GameObject Barricade;
    public GameObject Wall;
    public GameObject GhostBarricade;
    public GameObject GhostWall;
    public GameObject NPC_Pistoller;
    public GameObject NPC_F_Pistoller;
    public GameObject NPC_MachineGunner;
    public GameObject NPC_F_MachineGunner;

    private GameObject currentGhost;
    private Vector3 targetVector;
    private int buildMode;
    private int currentWeapon;
    private bool currentlyBuilding;

    private int hotbar;
    private int firingMode;


    private float shotCooldown;
    private int flamerthrowerAmmo;

	// Use this for initialization
	void Start () {
        targetVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetVector.z = -1;
        buildMode = 0;
        currentWeapon = 1;
        currentlyBuilding = false;
        firingMode = 0; //0 for focused, 1 for parrallel
        shotCooldown = 1;
        flamerthrowerAmmo = 0;
	}
	
	// Update is called once per frame
	void Update () {

        targetVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetVector.z = -1;

        shotCooldown -= Time.deltaTime;

        //shooting
        if (Input.GetMouseButton(0) && shotCooldown <= 0) //0 for left, 1 for right, 2 for middle.
        {
            switch (currentWeapon)
            {
                case 1: 
                    Instantiate(PlayerBullet, this.transform.position, PlayerBullet.transform.rotation);
                    shotCooldown = 1;
                    break;
                case 2:
                    if (flamerthrowerAmmo > 0)
                    {
                        Instantiate(PlayerFlamethrowerBullet, this.transform.position, PlayerBullet.transform.rotation);
                        shotCooldown = 0.01f;
                        flamerthrowerAmmo--;
                        GameObject.FindGameObjectWithTag("UI_AmmoIndicator").GetComponent<UI_BuildIndicator>().setAmmoIndicator("" + flamerthrowerAmmo);
                    }
                    break;
            }
        }
        //building/reloading
        else if (Input.GetMouseButtonDown(1))
        {
            if (hotbar == 0)
            {
                switch (buildMode)
                {
                    case 2:
                        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                        {
                            Instantiate(NPC_F_Pistoller, targetVector, NPC_F_Pistoller.transform.rotation);
                        }
                        else
                        {
                            Instantiate(NPC_Pistoller, targetVector, NPC_Pistoller.transform.rotation);
                        }
                        break;

                    case 3:
                        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                        {
                            Instantiate(NPC_F_MachineGunner, targetVector, NPC_F_MachineGunner.transform.rotation);
                        }
                        else
                        {
                            Instantiate(NPC_MachineGunner, targetVector, NPC_MachineGunner.transform.rotation);
                        }
                        break;

                    case 4:
                        currentGhost = (GameObject)Instantiate(GhostBarricade, targetVector, GhostBarricade.transform.rotation);
                        currentlyBuilding = true;
                        break;

                    case 5:
                        currentGhost = (GameObject)Instantiate(GhostWall, targetVector, GhostWall.transform.rotation);
                        currentlyBuilding = true;
                        break;
                }
            }
            else if (hotbar == 1)
            {
                switch(currentWeapon)
                {
                    case 1:
                            
                        break;
                    case 2:
                        flamerthrowerAmmo += 100;
                        GameObject.FindGameObjectWithTag("UI_AmmoIndicator").GetComponent<UI_BuildIndicator>().setAmmoIndicator("" + flamerthrowerAmmo);
                        break;
                }
            }
        }

        if (Input.GetMouseButtonUp(1) && currentlyBuilding)
        {
            //print("Building " + buildMode);
            switch(buildMode)
            {
                case 4:
                    //print("starting to Building");
                    Instantiate(Barricade, currentGhost.transform.position, currentGhost.transform.rotation);
                    //print("Finished Building");
                        break;

                case 5:
                    Instantiate(Wall, currentGhost.transform.position, currentGhost.transform.rotation);
                    break;
            }
            currentlyBuilding = false;
            Destroy(currentGhost);
        }


        if(Input.GetKeyDown(KeyCode.X))
        {
            if (firingMode == 0)
            {
                firingMode = 1;
                GameObject.FindGameObjectWithTag("UI_FiringModeIndicator").GetComponent<UI_FiringModeIndicator>().setFireModeIndicator("Parrallel");
            }
            else
            {
                firingMode = 0;
                GameObject.FindGameObjectWithTag("UI_FiringModeIndicator").GetComponent<UI_FiringModeIndicator>().setFireModeIndicator("Focused");
            }
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(hotbar == 0)
            {
                hotbar = 1;
                buildMode = 1;
                currentWeapon = 1;
                GameObject.FindGameObjectWithTag("UI_SelectBuildIndicator").GetComponent<UI_BuildIndicator>().setWeaponIndicator("Rifle");
            }
            else
            {
                hotbar = 0;
                buildMode = 1;
                currentWeapon = 1;
                GameObject.FindGameObjectWithTag("UI_SelectBuildIndicator").GetComponent<UI_BuildIndicator>().setBuildIndicator("None");
            }
        }



        if (hotbar == 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                GameObject.FindGameObjectWithTag("UI_SelectBuildIndicator").GetComponent<UI_BuildIndicator>().setBuildIndicator("None");
                buildMode = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                GameObject.FindGameObjectWithTag("UI_SelectBuildIndicator").GetComponent<UI_BuildIndicator>().setBuildIndicator("Pistoller");
                buildMode = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                GameObject.FindGameObjectWithTag("UI_SelectBuildIndicator").GetComponent<UI_BuildIndicator>().setBuildIndicator("Machine Gunner");
                buildMode = 3;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                GameObject.FindGameObjectWithTag("UI_SelectBuildIndicator").GetComponent<UI_BuildIndicator>().setBuildIndicator("Barricade");
                buildMode = 4;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                GameObject.FindGameObjectWithTag("UI_SelectBuildIndicator").GetComponent<UI_BuildIndicator>().setBuildIndicator("Wall");
                buildMode = 5;
            }
        }
        else if(hotbar == 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                GameObject.FindGameObjectWithTag("UI_SelectBuildIndicator").GetComponent<UI_BuildIndicator>().setWeaponIndicator("Rifle");
                currentWeapon = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                GameObject.FindGameObjectWithTag("UI_SelectBuildIndicator").GetComponent<UI_BuildIndicator>().setWeaponIndicator("Flamethrower");
                GameObject.FindGameObjectWithTag("UI_AmmoIndicator").GetComponent<UI_BuildIndicator>().setAmmoIndicator("" + flamerthrowerAmmo);
                currentWeapon = 2;
            }
        }

    }

    public Vector3 getTargetVector()
    {
        Vector3 thisTargetVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        thisTargetVector.z = -1;
        return thisTargetVector;
    }

    public int getFiringMode()
    {
        return firingMode;
    }
}
