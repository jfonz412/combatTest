using UnityEngine;

public class NPCInteraction : Interactable
{
    public Dialogue dialog; //contains fields for name and text
    PlayerState playerState;
    public bool isDead = false;


    void Start()
    {
        myInteractions = new string[] { "Attack", "Talk", "Trade", "Inspect" };
        playerState = PlayerManager.instance.player.GetComponent<PlayerState>();
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
                TriggerTrade();
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

    //needs to put player in certain state? Similar to dialog state (can shop in combat but can be knocked out of it)
    void TriggerTrade()
    {
        ShopInventoryUI.instance.OpenShop(name);
        playerState.SetPlayerState(PlayerState.PlayerStates.Shopping);
        Debug.Log("Trading with " + name);
    }

    #endregion
}
