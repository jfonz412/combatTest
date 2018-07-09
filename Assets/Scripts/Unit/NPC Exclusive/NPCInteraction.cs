using UnityEngine;

public class NPCInteraction : Interactable
{
    public Dialogue dialog; //contains fields for name and text

    private LoadShop myShop;
    private NPCInteractionStates myState;

    void Start()
    {
        myInteractions = new string[] { "Attack", "Talk", "Trade", "Inspect" };
        myShop = GetComponent<LoadShop>();
        myState = GetComponent<NPCInteractionStates>();
    }

    public override void Interaction(string interaction)
    {
        base.Interaction(interaction); //gets the reference to the player

        //need to set default interaction state depending on the state of this unit and the disposition of the unit towards the player

        if (interaction == "Default") //Default is passed here from PlayerController if this unit is LeftClicked
        {
            interaction = defaultInteraction.ToString();
        }

        switch (interaction)
        {
            case "Attack":
                if (myState.AbleToInteract(interaction))
                    TriggerAttack();
                break;
            case "Talk":
                if (myState.AbleToInteract(interaction))
                    TriggerDialogue();
                break;
            case "Trade":
                if (myState.AbleToInteract(interaction))
                    TriggerTrade();
                break;
            case "Inspect":
                if (myState.AbleToInteract(interaction))
                    InspectObject();
                break;
            default:
                break;
        }
    }


    #region Possible Interactions for NPCs

    //can be used for descriptions and observations as well
    void TriggerDialogue()
    {
        GetComponent<UnitAnimController>().FaceDirection(transform.position, player.position);
        ScriptToolbox.GetInstance().GetDialogueManager().StartDialogue(dialog);
    }

    void TriggerAttack()
    {
        AttackController playerAttackController = player.GetComponent<AttackController>();
        if (playerAttackController.lastKnownTarget != transform)
        {
            playerAttackController.EngageTarget(true, transform);
        }
    }

    void TriggerTrade()
    {
        if(myShop != null)
        {
            ShopInventoryUI.instance.ShopUIToggle(true, name);
            myShop.LoadShopInventory();
        }
        else
        {
            Debug.Log("This NPC cannot trade");
        }
       
    }
    #endregion
}
