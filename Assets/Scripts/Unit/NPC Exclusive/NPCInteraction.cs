using UnityEngine;

public class NPCInteraction : Interactable
{
    Transform myParent;
    public Dialogue dialog; //contains fields for name and text
    public bool isDead = false;


    void Start()
    {
        myParent = transform.parent;
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

    public void RemovePeacefulInteractions()
    {
        for (int i = 0; i < myInteractions.Length; i++)
        {
            if (myInteractions[i] != "Attack")
            {
                myInteractions[i] = "--";
            }
        }
        defaultInteraction = DefaultInteractions.Attack;
    }

    #region Possible Interactions for NPCs

    //can be used for descriptions and observations as well
    void TriggerDialogue()
    {
        myParent.GetComponent<UnitAnimator>().FaceDirection(transform.position, player.position);
        DialogueManager.instance.StartDialogue(dialog);
    }

    void TriggerAttack()
    {
        AttackController playerAttackController = player.GetComponent<AttackController>();
        if (playerAttackController.lastKnownTarget != transform)
        {
            playerAttackController.EngageTarget(true, transform.parent);
        }
    }

    #endregion
}
