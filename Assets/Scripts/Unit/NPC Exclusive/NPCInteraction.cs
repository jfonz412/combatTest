using UnityEngine;

public class NPCInteraction : Interactable
{
    public Dialogue dialog; //contains fields for name and text
    //public bool isDead = false;
    public enum InteractionState { FleeingPlayer, FleeingNPC, FightingPlayer, FightingNPC, Neutral, Trading, Talking, Dead };
    InteractionState currentState = InteractionState.Neutral; //default

    LoadShop myShop;

    void Start()
    {
        myInteractions = new string[] { "Attack", "Talk", "Trade", "Inspect" };
        myShop = GetComponent<LoadShop>();
    }

    public override void Interaction(string interaction)
    {
        CheckNPCInteractionState(interaction);
    }

    //change to ChangeNPCInteractionState(Enum state) or something
    void RemovePeacefulInteractions()
    {
        for (int i = 0; i < myInteractions.Length; i++)
        {
            if (myInteractions[i] != "Attack")
            {
                myInteractions[i] = "--";
            }
        }

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

    #region States
    public void SetInteractionState(InteractionState newState) 
    {

        switch (newState)
        {
            case InteractionState.Neutral:
                currentState = newState;
                break;
            case InteractionState.Talking:
                currentState = newState;
                break;
            case InteractionState.FightingPlayer:
                currentState = newState;
                break;
            case InteractionState.FightingNPC:
                currentState = newState;
                break;
            case InteractionState.FleeingPlayer:
                currentState = newState;
                break;
            case InteractionState.FleeingNPC:
                currentState = newState;
                break;
            case InteractionState.Trading:
                currentState = newState;
                break;
            case InteractionState.Dead:
                currentState = newState;
                break;
            default:
                Debug.LogError("Unknown player state");
                break;
        }
        //Debug.Log(name + " is: " + currentState);
    }
    #endregion

    #region StateReactions

    void CheckNPCInteractionState(string interaction)
    {
        if (currentState == InteractionState.Neutral)
        {
            NeutralInteractions(interaction);
        }
        else if (currentState == InteractionState.FightingPlayer)
        {
            FightingPlayerInteractions(interaction);
        }
        else if (currentState == InteractionState.FleeingPlayer)
        {
            FleeingPlayerInteractions(interaction);
        }
        else if (currentState == InteractionState.FightingNPC)
        {
            FightingNPCInteractions(interaction);
        }
        else if (currentState == InteractionState.FleeingNPC)
        {
            FleeingNPCInteractions(interaction);
        }
        else if (currentState == InteractionState.Talking)
        {
            Debug.Log("Talking NPCInteraction state not implemented");
        }
        else if (currentState == InteractionState.Trading)
        {
            Debug.Log("Trading NPCInteraction state not implemented");
        }
        else if (currentState == InteractionState.Dead)
        {
            return;
        }
    }

    void NeutralInteractions(string interaction)
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

    void FightingPlayerInteractions(string interaction)
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

    void FleeingPlayerInteractions(string interaction)
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

    void FightingNPCInteractions(string interaction)
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

    void FleeingNPCInteractions(string interaction)
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
