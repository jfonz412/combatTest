using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : Interactable
{
    public DefaultInteractions defaultInteraction;
    public Dialogue dialog; //contains fields for name and text
    public bool isDead = false;


    void Start()
    {
        myInteractions = new string[] { "Attack", "Talk", "Trade", "Inspect" };
    }

    public override void Interaction(string interaction)
    {
        if (isDead)
        {
            return;
        }

        base.Interaction(interaction); //gets the reference to the player

        if (interaction == "Default")
        {
            interaction = defaultInteraction.ToString();
        }

        //chose which interaction to trigger
        switch (interaction)
        {
            case "Attack":
                TriggerAttack();
                break;
            case "Talk":
                TriggerDialogue();
                break;
            case "Trade":
                //TriggerTrade();
                Debug.Log("Trading with " + name);
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
        GetComponent<UnitAnimator>().FaceDirection(transform.position, player.position);
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

    #endregion
}
