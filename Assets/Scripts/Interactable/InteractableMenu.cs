using UnityEngine;
using UnityEngine.UI;

public class InteractableMenu : MonoBehaviour {

    [HideInInspector]
    public Text[] interactionOptions = new Text[4]; //actual text items

    Interactable currentInteractable;
    PlayerMoveState player;

#region Singleton 

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
        player = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<PlayerMoveState>();
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
            string chosenInteraction = TranslateIfHarvestInteraction(interaction);
            player.InteractWithInteractable(chosenInteraction, currentInteractable);
        }
        CloseMenu();
    }

    public void CloseMenu()
    {
        Destroy(gameObject);
    }

    private string TranslateIfHarvestInteraction(Text interaction)
    {
        string s = interaction.text;
        if(s == "Mine" || s == "Chop")
        {
            return "Harvest";
        }
        else
        {
            return s;
        }
    }
}
