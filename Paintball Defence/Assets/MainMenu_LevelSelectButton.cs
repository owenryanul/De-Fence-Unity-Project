using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu_LevelSelectButton : MonoBehaviour {

    public GameObject[] MainMenuButtons;
    public GameObject[] LevelSelectMenuButtons;
    public GameObject[] controlsMenu;
    public GameObject[] aboutMenu;


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



    public void enterControlsScreen()
    {
        GameObject thisButton = GameObject.Find("Controls Button");
        foreach (GameObject abutton in controlsMenu)
        {
            print("Activating abutton");
            abutton.SetActive(true);
        }

        foreach (GameObject abutton in MainMenuButtons)
        {
            if (abutton.name != "Controls Button")
            {
                print("Shutting Down abutton");
                abutton.SetActive(false);
            }
        }
        thisButton.SetActive(false);

    }

    public void enterAboutScreen()
    {
        GameObject thisButton = GameObject.Find("About Button");
        foreach (GameObject abutton in aboutMenu)
        {
            print("Activating abutton");
            abutton.SetActive(true);
        }

        foreach (GameObject abutton in MainMenuButtons)
        {
            if (abutton.name != "About Button")
            {
                print("Shutting Down abutton");
                abutton.SetActive(false);
            }
        }
        thisButton.SetActive(false);

    }

    public void backToMainMenu()
    {
        GameObject thisButton = new GameObject();//placeholder contents
        //find the currently active back button
        foreach (GameObject abackButton in GameObject.FindGameObjectsWithTag("UI_BackButton"))
        {
            if(abackButton.activeSelf)
            {
                thisButton = abackButton;
                break;
            }
        }

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

        foreach (GameObject aUIElement in controlsMenu)
        {
            if (aUIElement.name != "Back Button 2")
            {
                print("Shutting Down aUIElement");
                aUIElement.SetActive(false);
            }

        }

        foreach (GameObject aUIElement in aboutMenu)
        {
            if (aUIElement.name != "Back Button 3")
            {
                print("Shutting Down aUIElement");
                aUIElement.SetActive(false);
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
            case 3: SceneManager.LoadScene("Level_3", LoadSceneMode.Single); break;
            case 4: SceneManager.LoadScene("Level_4", LoadSceneMode.Single); break;
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
