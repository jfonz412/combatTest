using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableMenu : MonoBehaviour {

    [HideInInspector]
    public Text option1, option2, option3, option4;

#region Singleton 
    [HideInInspector]
    public static InteractableMenu instance;
    void Awake()
    {
        if (instance != null) //if we find another instance
        {
            Destroy(instance.gameObject); //destroy the other menu
        }
        instance = this; //there can only be one!
    }
#endregion

    public void PopulateOptions(Interactable interactable)
    {
        //grab the interactable's interactions and populate myself with them
    }

    public void PassChosenInteraction()
    {
        //pass the interactable and the chosen interaction to the PlayerController
    }

    public void CloseMenu()
    {
        Destroy(gameObject);
    }
}
