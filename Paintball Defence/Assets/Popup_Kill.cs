using UnityEngine;
using System.Collections;

public class Popup_Kill : MonoBehaviour {

    public Animator popupTextAnimator;
    private float speed;

	// Use this for initialization
	void Start () {
        AnimatorClipInfo[] clipInfo = popupTextAnimator.GetCurrentAnimatorClipInfo(0);
        //print("Spawned Popup");
        Destroy(this.gameObject, clipInfo[0].clip.length);

	}
	
	// Update is called once per frame
	void Update () {
        //JURY RIGGED SOLUTION to popup text following the player's screen
        float speed = 0.2f;
        Vector3 move = new Vector3(0, 0, 0);
        //move Up
        if (Input.GetKey(KeyCode.UpArrow))
        {
            move.y += speed;
        }
        //move down
        if (Input.GetKey(KeyCode.DownArrow))
        {
            move.y -= speed;
        }
        //move left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move.x -= speed;
        }

        //move right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            move.x += speed;
        }
        this.transform.position -= move;
    }

    public void nudgePopup(float amountToNudgeX, float amountToNudgeY)
    {
        this.transform.position = new Vector3(this.transform.position.x + amountToNudgeX, this.transform.position.y + amountToNudgeY, this.transform.position.z);
    }

    void OnDestroy()
    {
        //print("Popup killed");
    }
}
