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
            popupText = Resources.Load("PopUps/PopUpTextParent") as GameObject;
    }

    public static void CreateFloatingText(string text, Transform unitLocation)
    {
        Vector3 position = new Vector3(unitLocation.position.x, unitLocation.position.y + 0.5f, unitLocation.position.z);
        GameObject instance = Instantiate(popupText);

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = position;
        instance.GetComponent<FloatingText>().SetText(text);
    }
}
