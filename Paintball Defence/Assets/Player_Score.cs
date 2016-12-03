using UnityEngine;
using System.Collections;

public class Player_Score : MonoBehaviour {

    private int score;

	// Use this for initialization
	void Start () {
        score = 0;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public int getScore()
    {
        return score;
    }

    public void setScore(int inScore)
    {
        score = inScore;
        GameObject.FindGameObjectWithTag("UI_Score").GetComponent<UI_BuildIndicator>().setScoreIndicator(score);
        //print("Player Score set, now:" + score);
    }

    public void addScore(int inScore)
    {
        score += inScore;
        GameObject.FindGameObjectWithTag("UI_Score").GetComponent<UI_BuildIndicator>().setScoreIndicator(score);
        //print("Player Score changed, now:" + score);
    }
}
