using UnityEngine;

public class NPCInteraction : Interactable
{
    public Dialogue dialog; //contains fields for name and text

    LoadShop myShop;
    NPCInteractionStates myState;

    void Start()
    {
        myInteractions = new string[] { "Attack", "Talk", "Trade", "Inspect" };
        myShop = GetComponent<LoadShop>();
        myState = GetComponent<NPCInteractionStates>();
    }

    public override void Interaction(string interaction)
    {
        myState.CheckNPCInteractionState(interaction);
    }


    #region Possible Interactions for NPCs

    //can be used for descriptions and observations as well
    void TriggerDialogue()
    {
        GetComponent<UnitAnimController>().FaceDirection(transform.position, player.position);
        DialogueManager.instance.StartDialogue(dialog);
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
            ShopInventoryUI.instance.OpenShop(name);
            myShop.LoadShopInventory();
            PlayerState.SetPlayerState(PlayerState.PlayerStates.Shopping);
        }
        else
        {
            Debug.Log("This NPC cannot trade");
        }
       
    }
    #endregion

    #region Interaction Types
    public void NeutralInteractions(string interaction)
    {

        base.Interaction(interaction); //gets the reference to the player

        if (interaction == "Default")
        {
            interaction = defaultInteraction.ToString();
        }

        switch (interaction)
        {
            case "Attack":
                TriggerAttack();
                break;
            case "Talk":
                TriggerDialogue();
                break;
            case "Trade":
                TriggerTrade();
                break;
            case "Inspect":
                InspectObject();
                break;
            default:
                break;
        }

    }

    public void FightingPlayerInteractions(string interaction)
    {
        base.Interaction(interaction); //gets the reference to the player
        defaultInteraction = DefaultInteractions.Attack;

        if (interaction == "Default")
        {
            interaction = defaultInteraction.ToString();
        }

        switch (interaction)
        {
            case "Attack":
                TriggerAttack();
                break;
            case "Talk":
                Debug.Log("This NPC doesn't want to talk to you!");
                break;
            case "Trade":
                Debug.Log("This NPC doesn't want to trade to you!");
                break;
            case "Inspect":
                Debug.Log("You can't inspect them right now...");
                break;
            default:
                break;
        }
    }

    public void FleeingPlayerInteractions(string interaction)
    {
        base.Interaction(interaction); //gets the reference to the player
        defaultInteraction = DefaultInteractions.Attack;

        if (interaction == "Default")
        {
            interaction = defaultInteraction.ToString();
        }

        switch (interaction)
        {
            case "Attack":
                TriggerAttack();
                break;
            case "Talk":
                Debug.Log("This NPC doesn't want to talk to you!");
                break;
            case "Trade":
                Debug.Log("This NPC doesn't want to trade to you!");
                break;
            case "Inspect":
                Debug.Log("You can't inspect them right now...");
                break;
            default:
                break;
        }
    }

    public void FightingNPCInteractions(string interaction)
    {
        base.Interaction(interaction); //gets the reference to the player

        if (interaction == "Default")
        {
            interaction = defaultInteraction.ToString();
        }

        switch (interaction)
        {
            case "Attack":
                TriggerAttack();
                break;
            case "Talk":
                Debug.Log("This NPC can't talk to you!");
                break;
            case "Trade":
                Debug.Log("This NPC can't trade to you!");
                break;
            case "Inspect":
                InspectObject();
                break;
            default:
                break;
        }
    }

    public void FleeingNPCInteractions(string interaction)
    {
        base.Interaction(interaction); //gets the reference to the player

        if (interaction == "Default")
        {
            interaction = defaultInteraction.ToString();
        }

        switch (interaction)
        {
            case "Attack":
                TriggerAttack();
                break;
            case "Talk":
                Debug.Log("This NPC can't talk to you!");
                break;
            case "Trade":
                Debug.Log("This NPC can't trade to you!");
                break;
            case "Inspect":
                InspectObject();
                break;
            default:
                break;
        }
    }

    #endregion
}
