using UnityEngine;
using System.Collections;

public class PopupText_Controller : MonoBehaviour {

    private static GameObject pistollerScorePopup;
    private static GameObject canvas;

    public static void setupPopupText_Controller()
    {
        pistollerScorePopup = Resources.Load<GameObject>("Score Popup Parent");
        canvas = GameObject.Find("Canvas");
    }

    public static void createPistollerScorePopup(Transform location)
    {
        GameObject instance = (GameObject)Instantiate(pistollerScorePopup);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
    }

    /*public static void createPistollerScorePopup(Transform location, Canvas canvasIn)
    {
        GameObject instance = (GameObject)Instantiate(pistollerScorePopup);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(canvasIn.transform, false);
        instance.transform.position = screenPosition;
    }*/
}
