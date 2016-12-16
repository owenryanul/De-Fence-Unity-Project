using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupText_Controller : MonoBehaviour {

    private static GameObject pistollerScorePopup;
    private static GameObject costPopup;
    private static GameObject canvas;

    public static void setupPopupText_Controller()
    {
        pistollerScorePopup = Resources.Load<GameObject>("Score Popup Parent");
        costPopup = Resources.Load<GameObject>("Cost Popup Parent");
        //canvas = GameObject.FindGameObjectWithTag("UI_Canvas");
        canvas = GameObject.Find("UI_Canvas");
        //print("Setupready");
    }

    public static void createPistollerScorePopup(Canvas aCanvas)
    {
        GameObject instance = (GameObject)Instantiate(pistollerScorePopup);
        //Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        //print("Instance" + instance.name);
        //print("Canvas:" + canvas.name);
        instance.transform.SetParent(aCanvas.transform, false);
        //instance.transform.position = screenPosition;
    }

    public static void createCostPopup(Transform location, int cost)
    {
        GameObject instance = (GameObject)Instantiate(costPopup);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        string atext = "-" + cost;
        Animator aAnimator = instance.GetComponentInChildren<Animator>();
        aAnimator.GetComponent<Text>().text = atext;
        //note: popup text will not display AT ALL if the rect transform is too small to fit the text.
    }

    public static void createCostPopup(Vector3 location, int cost)
    {
        GameObject instance = (GameObject)Instantiate(costPopup);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        string atext = "-" + cost;
        Animator aAnimator = instance.GetComponentInChildren<Animator>();
        aAnimator.GetComponent<Text>().text = atext;
        //note: popup text will not display AT ALL if the rect transform is too small to fit the text.
    }

    /*public static void createPistollerScorePopup(Transform location, Canvas canvasIn)
    {
        GameObject instance = (GameObject)Instantiate(pistollerScorePopup);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(canvasIn.transform, false);
        instance.transform.position = screenPosition;
    }*/
}
