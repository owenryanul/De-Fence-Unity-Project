using UnityEngine;
using System.Collections;

public class Player_Action : MonoBehaviour {

    public GameObject PlayerBullet;
    public GameObject PlayerFlamethrowerBullet;
    public GameObject PlayerShotgunBullet;

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
    private int shotgunAmmo;

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
        shotgunAmmo = 0;
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
                case 1: //shooting rifle
                    Instantiate(PlayerBullet, this.transform.position, PlayerBullet.transform.rotation);
                    shotCooldown = 0.5f;
                    GameObject.FindGameObjectWithTag("UI_AmmoIndicator").GetComponent<UI_BuildIndicator>().setAmmoIndicator("Infinate");
                    break;
                case 2: //shooting flamethrower
                    if (flamerthrowerAmmo > 0)
                    {
                        Instantiate(PlayerFlamethrowerBullet, this.transform.position, PlayerBullet.transform.rotation);
                        shotCooldown = 0.01f;
                        flamerthrowerAmmo--;
                        GameObject.FindGameObjectWithTag("UI_AmmoIndicator").GetComponent<UI_BuildIndicator>().setAmmoIndicator("" + flamerthrowerAmmo);
                    }
                    break;
                case 3: //shooting shotgun
                    if (shotgunAmmo > 0)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            Instantiate(PlayerShotgunBullet, this.transform.position, PlayerBullet.transform.rotation);
                        }
                        shotCooldown = 1f;
                        shotgunAmmo--;
                        GameObject.FindGameObjectWithTag("UI_AmmoIndicator").GetComponent<UI_BuildIndicator>().setAmmoIndicator("" + shotgunAmmo);
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
                            if (this.gameObject.GetComponent<Player_Score>().getScore() >= 300)
                            {
                                Instantiate(NPC_F_Pistoller, targetVector, NPC_F_Pistoller.transform.rotation);
                                this.gameObject.GetComponent<Player_Score>().addScore(-300);
                                PopupText_Controller.createCostPopup(targetVector, 300);
                            }
                            else
                            {
                                GameObject.FindGameObjectWithTag("UI_Warning").GetComponent<UI_InsufficentFunds>().showWarning();
                            }
                        }
                        else
                        {
                            if (this.gameObject.GetComponent<Player_Score>().getScore() >= 300)
                            {
                                Instantiate(NPC_Pistoller, targetVector, NPC_Pistoller.transform.rotation);
                                this.gameObject.GetComponent<Player_Score>().addScore(-300);
                                PopupText_Controller.createCostPopup(targetVector, 300);
                            }
                            else
                            {
                                GameObject.FindGameObjectWithTag("UI_Warning").GetComponent<UI_InsufficentFunds>().showWarning();
                            }
                        }
                        break;

                    case 3:
                        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                        {
                            
                            if (this.gameObject.GetComponent<Player_Score>().getScore() >= 800)
                            {
                                Instantiate(NPC_F_MachineGunner, targetVector, NPC_F_MachineGunner.transform.rotation);
                                this.gameObject.GetComponent<Player_Score>().addScore(-800);
                                PopupText_Controller.createCostPopup(targetVector, 800);
                            }
                            else
                            {
                                GameObject.FindGameObjectWithTag("UI_Warning").GetComponent<UI_InsufficentFunds>().showWarning();
                            }
                        }
                        else
                        {
                            if (this.gameObject.GetComponent<Player_Score>().getScore() >= 800)
                            {
                                Instantiate(NPC_MachineGunner, targetVector, NPC_MachineGunner.transform.rotation);
                                this.gameObject.GetComponent<Player_Score>().addScore(-800);
                                PopupText_Controller.createCostPopup(targetVector, 800);
                            }
                            else
                            {
                                GameObject.FindGameObjectWithTag("UI_Warning").GetComponent<UI_InsufficentFunds>().showWarning();
                            }
                        }
                        break;

                    case 4:
                        
                        if (this.gameObject.GetComponent<Player_Score>().getScore() >= 300)
                        {
                            currentGhost = (GameObject)Instantiate(GhostBarricade, targetVector, GhostBarricade.transform.rotation);
                            currentlyBuilding = true;
                            this.gameObject.GetComponent<Player_Score>().addScore(-300);
                            PopupText_Controller.createCostPopup(targetVector, 300);
                        }
                        else
                        {
                            GameObject.FindGameObjectWithTag("UI_Warning").GetComponent<UI_InsufficentFunds>().showWarning();
                        }
                        break;

                    case 5:
                        if (this.gameObject.GetComponent<Player_Score>().getScore() >= 500)
                        {
                            currentGhost = (GameObject)Instantiate(GhostWall, targetVector, GhostWall.transform.rotation);
                            currentlyBuilding = true;
                            this.gameObject.GetComponent<Player_Score>().addScore(-500);
                            PopupText_Controller.createCostPopup(targetVector, 500);
                        }
                        else
                        {
                            GameObject.FindGameObjectWithTag("UI_Warning").GetComponent<UI_InsufficentFunds>().showWarning();
                        }
                        break;
                }
            }
            else if (hotbar == 1)//weapon hotbar
            {
                switch(currentWeapon)
                {
                    case 1:
                            //rifle, basic starting weapon
                        break;
                    case 2:
                        //flamethrower ammo purchasing
                            if (this.gameObject.GetComponent<Player_Score>().getScore() >= 300)
                            {
                                flamerthrowerAmmo += 100;
                                GameObject.FindGameObjectWithTag("UI_AmmoIndicator").GetComponent<UI_BuildIndicator>().setAmmoIndicator("" + flamerthrowerAmmo);
                                this.gameObject.GetComponent<Player_Score>().addScore(-300);
                                PopupText_Controller.createCostPopup(this.transform, 300);
                                print("Flamer ammo purchased, new ammo: " + flamerthrowerAmmo + ", new score: " + this.gameObject.GetComponent<Player_Score>().getScore());
                            }
                            else
                            {
                                GameObject.FindGameObjectWithTag("UI_Warning").GetComponent<UI_InsufficentFunds>().showWarning();
                            }
                        break;
                    case 3:
                        //shotgun ammo purchasing
                        if (this.gameObject.GetComponent<Player_Score>().getScore() >= 200)
                        {
                            shotgunAmmo += 10;
                            GameObject.FindGameObjectWithTag("UI_AmmoIndicator").GetComponent<UI_BuildIndicator>().setAmmoIndicator("" + shotgunAmmo);
                            this.gameObject.GetComponent<Player_Score>().addScore(-200);
                            PopupText_Controller.createCostPopup(this.transform, 200);
                            print("Shotgun ammo purchased, new ammo: " + shotgunAmmo + ", new score: " + this.gameObject.GetComponent<Player_Score>().getScore());
                        }
                        else
                        {
                            GameObject.FindGameObjectWithTag("UI_Warning").GetComponent<UI_InsufficentFunds>().showWarning();
                        }
                        break;
                }
            }
        }

        if (Input.GetMouseButtonUp(1) && currentlyBuilding)
        {
            //print("Building " + buildMode);
            switch(buildMode)
            {
                case 4://placing barricade, showing ghost
                    //print("starting to Building");
                    Instantiate(Barricade, currentGhost.transform.position, currentGhost.transform.rotation);
                    //print("Finished Building");
                        break;

                case 5: //placing wall, showing ghost
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



        updateUIHotbar();

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

    private void updateUIHotbar()
    {
        if (hotbar == 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                buildMode = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                buildMode = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
               buildMode = 3;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                buildMode = 4;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                buildMode = 5;
            }
            else if(Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if(buildMode < 5)//!!!!!!!Update this as more things are added
                {
                    buildMode++;
                }
            }
            else if(Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if(buildMode > 1)
                {
                    buildMode--;
                }
            }
        }
        else if (hotbar == 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentWeapon = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
               currentWeapon = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                currentWeapon = 3;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (currentWeapon < 3)//!!!!!!!Update this as more things are added
                {
                    currentWeapon++;
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (currentWeapon > 1)
                {
                    currentWeapon--;
                }
            }
        }

        


        if (hotbar == 0)
        {
            switch (buildMode)
            {
                case 1: GameObject.FindGameObjectWithTag("UI_SelectBuildIndicator").GetComponent<UI_BuildIndicator>().setBuildIndicator("None"); break;
                case 2: GameObject.FindGameObjectWithTag("UI_SelectBuildIndicator").GetComponent<UI_BuildIndicator>().setBuildIndicator("Pistoller (300)"); break;
                case 3: GameObject.FindGameObjectWithTag("UI_SelectBuildIndicator").GetComponent<UI_BuildIndicator>().setBuildIndicator("Machine Gunner (800)"); break;
                case 4: GameObject.FindGameObjectWithTag("UI_SelectBuildIndicator").GetComponent<UI_BuildIndicator>().setBuildIndicator("Barricade (300)"); break;
                case 5: GameObject.FindGameObjectWithTag("UI_SelectBuildIndicator").GetComponent<UI_BuildIndicator>().setBuildIndicator("Wall (500)"); break;
            }
        }
        else if (hotbar == 1)
        {
            switch(currentWeapon)
            {
                case 1: GameObject.FindGameObjectWithTag("UI_SelectBuildIndicator").GetComponent<UI_BuildIndicator>().setWeaponIndicator("Rifle"); break;
                case 2:
                    GameObject.FindGameObjectWithTag("UI_SelectBuildIndicator").GetComponent<UI_BuildIndicator>().setWeaponIndicator("Flamethrower (300)");
                    GameObject.FindGameObjectWithTag("UI_AmmoIndicator").GetComponent<UI_BuildIndicator>().setAmmoIndicator("" + flamerthrowerAmmo);
                    break;
                case 3:
                    GameObject.FindGameObjectWithTag("UI_SelectBuildIndicator").GetComponent<UI_BuildIndicator>().setWeaponIndicator("Shotgun (200)");
                    GameObject.FindGameObjectWithTag("UI_AmmoIndicator").GetComponent<UI_BuildIndicator>().setAmmoIndicator("" + shotgunAmmo);
                    break;
            }
        }
    }

}
