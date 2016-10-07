using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Weapon_Indicator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setWeaponIndicator(string inBuildName)
    {
        this.GetComponent<Text>().text = "Weapon Selected: " + inBuildName;
    }
}
