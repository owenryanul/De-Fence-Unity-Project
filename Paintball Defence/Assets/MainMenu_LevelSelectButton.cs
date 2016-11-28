using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu_LevelSelectButton : MonoBehaviour {

    public GameObject[] MainMenuButtons;
    public GameObject[] LevelSelectMenuButtons;


    public void enterLevelSelectScreen()
    {
        GameObject thisButton = GameObject.Find("Level Select Button");
        foreach (GameObject abutton in LevelSelectMenuButtons)
        {
            print("Activating abutton");
            abutton.SetActive(true);
        }

        foreach(GameObject abutton in MainMenuButtons)
        {
            if (abutton.name != "Level Select Button")
            {
                print("Shutting Down abutton");
                abutton.SetActive(false);
            }
        }
        thisButton.SetActive(false);

    }

    public void backToMainMenu()
    {

        GameObject thisButton = GameObject.Find("Back Button");
        foreach (GameObject abutton in MainMenuButtons)
        {
            print("Activating abutton");
            abutton.SetActive(true);
        }

        foreach (GameObject abutton in LevelSelectMenuButtons)
        {
            if (abutton.name != "Back Button")
            {
                print("Shutting Down abutton");
                abutton.SetActive(false);
            }
        }
        thisButton.SetActive(false);

    }

    public void loadLevel(int level)
    {
        switch(level)
        {
            case 1: SceneManager.LoadScene("Test_Scene", LoadSceneMode.Single); break;
            case 2: SceneManager.LoadScene("Level_2", LoadSceneMode.Single); break;
        }
    }

    public void loadTutorial()
    {
        SceneManager.LoadScene("Level_Tutorial", LoadSceneMode.Single);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
