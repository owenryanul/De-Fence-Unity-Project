using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_BuildIndicator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setBuildIndicator (string inBuildName)
	{
		this.GetComponent<Text> ().text = "Current Purchaseable: " + inBuildName;
	}

    public void setWeaponIndicator(string inWeaponName)
    {
        this.GetComponent<Text>().text = "Current Weapon: " + inWeaponName;
    }

    public void setAmmoIndicator(string inAmmoCount)
    {
        this.GetComponent<Text>().text = "Ammo: " + inAmmoCount;
    }

    public void setScoreIndicator(int inScore)
    {
        this.GetComponent<Text>().text = "Score: " + inScore;
    }
}
