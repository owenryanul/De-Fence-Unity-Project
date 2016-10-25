using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_InsufficentFunds : MonoBehaviour {

    private float timeTillHide;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        //Hide the warning again after 3 seconds
        if (this.gameObject.GetComponent<Text>().enabled)
        {
            timeTillHide -= Time.deltaTime;
            if (timeTillHide <= 0)
            {
                hideWarning();
            }
        }
	}

    public void hideWarning()
    {
        this.gameObject.GetComponent<Text>().enabled = false;
    }

    public void showWarning()
    {
        //disables the text componet of the ui to hide the warning.
        this.gameObject.GetComponent<Text>().enabled = true;
        timeTillHide = 3;
    }
}
