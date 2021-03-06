﻿using UnityEngine;

public class NPCInteraction : Interactable
{
    private LoadShop myShop;

    void Start()
    {
        myInteractions = new string[] { "Attack", "Talk", "Trade", "Inspect" };
        myShop = GetComponent<LoadShop>();
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


    #region Possible Interactions for NPCs

    //can be used for descriptions and observations as well
    void TriggerDialogue()
    {
        UnitStateMachine u = GetComponent<UnitStateMachine>();
        u.RequestChangeState(StateMachine.States.Talking);
    }

    void TriggerAttack()
    {
        Debug.Log("Player is attacking " + gameObject.name);
        UnitStateMachine u = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<UnitStateMachine>();
        u.currentThreat = transform;
        u.RequestChangeState(StateMachine.States.Fight);
    }

    void TriggerTrade()
    {
        if(myShop != null)
        {
            UnitStateMachine u = GetComponent<UnitStateMachine>();
            u.RequestChangeState(StateMachine.States.Shopping);
        }
        else
        {
            Debug.Log("This NPC cannot trade");
        }    
    }
    #endregion
}
