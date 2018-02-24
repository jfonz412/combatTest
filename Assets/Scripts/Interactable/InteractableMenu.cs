using UnityEngine;
using UnityEngine.UI;

public class InteractableMenu : MonoBehaviour {

    [HideInInspector]
    public Text[] interactionOptions = new Text[4]; //actual text items

    Interactable currentInteractable;
    PlayerController player;

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

    void Start()
    {
        player = PlayerManager.instance.player.GetComponent<PlayerController>();
    }

    public void PopulateOptions(Interactable interactable)
    {
        currentInteractable = interactable;
        if (currentInteractable != null)
        { 
            for (int i = 0; i < interactionOptions.Length; i++)
            {
                interactionOptions[i] = transform.GetChild(i).GetComponent<Text>();
                interactionOptions[i].text = interactable.myInteractions[i];
            }
        }
    }

    public void PassChosenInteraction(Text interaction)
    {
        if (currentInteractable != null)
        {
            string chosenInteraction = interaction.text;
            player.InteractWithInteractable(chosenInteraction, currentInteractable);
        }
        CloseMenu();
    }

    public void CloseMenu()
    {
        Destroy(gameObject);
    }
}
