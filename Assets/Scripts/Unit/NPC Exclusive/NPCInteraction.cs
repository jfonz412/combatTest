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
        //myState.CheckNPCInteractionState(interaction);

        base.Interaction(interaction); //gets the reference to the player

        if (interaction == "Default")
        {
            interaction = defaultInteraction.ToString();
            myState.ValidateInteraction(interaction);
        }

        switch (interaction)
        {
            case "Attack":
                if (myState.ValidateInteraction(interaction))
                    TriggerAttack();
                break;
            case "Talk":
                if (myState.ValidateInteraction(interaction))
                    TriggerDialogue();
                break;
            case "Trade":
                if (myState.ValidateInteraction(interaction))
                    TriggerTrade();
                break;
            case "Inspect":
                if (myState.ValidateInteraction(interaction))
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
        //DialogueManager.instance.StartDialogue(dialog);
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
            PlayerState.SetPlayerState(PlayerState.PlayerStates.Shopping);
            ShopInventoryUI.instance.OpenShop(name);
            myShop.LoadShopInventory();
        }
        else
        {
            Debug.Log("This NPC cannot trade");
        }
       
    }
    #endregion
}
