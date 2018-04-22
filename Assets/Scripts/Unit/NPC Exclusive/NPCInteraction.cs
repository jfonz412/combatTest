using UnityEngine;

public class NPCInteraction : Interactable
{
    public Dialogue dialog; //contains fields for name and text
    public bool isDead = false;

    LoadShop myShop;

    void Start()
    {
        myInteractions = new string[] { "Attack", "Talk", "Trade", "Inspect" };
        myShop = GetComponent<LoadShop>();
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
        //GetComponent<UnitAnimator>().FaceDirection(transform.position, player.position);
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

    //needs to put player in certain state? Similar to dialog state (can shop in combat but can be knocked out of it)
    void TriggerTrade()
    {
        //might make everyone have a LoadShop script so you can trade with everyone. Shouldn't result
        //in too much technical overhead but possibly more work for myself
        if(myShop != null)
        {
            ShopInventoryUI.instance.OpenShop(name);
            myShop.LoadShopInventory();
            PlayerState.SetPlayerState(PlayerState.PlayerStates.Shopping);
            //Debug.Log("Trading with " + name);
        }
        else
        {
            Debug.Log("This NPC cannot trade");
        }
        
    }

    #endregion
}
