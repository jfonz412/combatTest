using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour {
    public static GameObject popupText;
    public static GameObject canvas;

    //this is called in the PlayerController, just because there is only one of them.
    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        if(!popupText)
            popupText = Resources.Load("PopUpTextParent") as GameObject;
    }

    public static void CreateFloatingText(string text, Transform unitLocation)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(unitLocation.position);
        GameObject instance = Instantiate(popupText);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPos;
        instance.GetComponent<FloatingText>().SetText(text);
    }
}
