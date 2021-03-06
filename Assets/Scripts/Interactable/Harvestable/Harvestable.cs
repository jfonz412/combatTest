﻿using UnityEngine;
using System.Collections;

public class Harvestable : Interactable {
    [SerializeField]
    private Sprite harvestedSprite;

    private enum HarvestType { Mine, Chop };
    [SerializeField]
    private HarvestType harvestType;
    [SerializeField]
    private Item.ToolType requiredTool;

    private bool harvested = false;
    [HideInInspector]
    public bool isHarvested {
                                get { return harvested; }
                                set { harvested = value;
                                        if (value == true)
                                        {
                                            ChangeSprite();
                                        };
                                    }
                            }

    void Start () {
        myInteractions = new string[] { harvestType.ToString(), "Inspect", "--", "--" };
    }

    public override void Interaction(string interaction)
    {
        base.Interaction(interaction); //gets the reference to the player

        if (interaction == "Default")
        {
            TriggerHarvest();
            return;
        }

        switch (interaction)
        {
            case "Harvest": //"Harvest" is recieved from the InteractionMenu if the player selects "Mine" or "Chop" or some other variation of Harvest
                    TriggerHarvest();
                break;
            case "Inspect":
                    InspectObject();
                break;
            default:
                break;
        }
    }

    private void TriggerHarvest()
    {
        if (UsingProperTool() && !harvested)
        {
            //not saving these in Start theoretically reduces time to load on start when a scene has many nodes
            ScriptToolbox.GetInstance().GetPlayerManager().playerStateMachine.RequestChangeState(StateMachine.States.Harvesting);
            GetComponent<NodeStateMachine>().RequestChangeState(StateMachine.States.Harvesting);
            Debug.Log("Starting harvest!");
        }
    }

    private bool UsingProperTool()
    {
        Item unitWeapon = player.GetComponent<BodyPartController>().attack1Parts[0].MyWeapon();
        bool usingProperTool;

        if (unitWeapon == null)
        {
            usingProperTool = false;
        }
        else if (unitWeapon.myToolType == requiredTool)
        {
            usingProperTool = true;
        }
        else
        {
            usingProperTool = false;
        }

        return usingProperTool;
    }

    private void ChangeSprite()
    {
        SpriteRenderer sp = GetComponentInChildren<SpriteRenderer>();
        sp.sprite = harvestedSprite;
    }


    //private IEnumerator HarvestNode()
    //{
    //    Animator playerAnim = player.GetComponent<Animator>();
    //    Collider2D nodeCol = GetComponent<Collider2D>();
    //    Collider2D playerCol = player.GetComponent<Collider2D>(); //eventually will allow other units to harvest
    //    string shake = "Shake";
    //    string harvest = "Harvest";

    //    float timePassed = 0;

    //    while (timePassed < timeToHarvest)
    //    {   
    //        if (!playerCol.IsTouching(nodeCol))
    //        {
    //            //Debug.Log("Player walked away from harvestable node");
    //            playerAnim.ResetTrigger(harvest);
    //            anim.ResetTrigger(shake);
    //            yield break;
    //        }

    //        //Debug.Log("triggering player shake");
    //        playerAnim.SetTrigger(harvest);

    //        yield return new WaitForSeconds(1f);

    //        timePassed++;
    //        //Debug.Log("Harvesting...(" + timePassed + ")");

    //        anim.SetTrigger(shake);

    //    }
    //    //Debug.Log("Done harvesting");

    //    playerAnim.ResetTrigger(harvest);
    //    anim.ResetTrigger(shake);
    //    DropAndDestroy();
    //    yield break;
    //}

    //private void DropAndDestroy()
    //{
    //    if (itemDrop != null)
    //    {
    //        Instantiate(itemDrop, transform.position, Quaternion.identity);
    //    }

    //    isHarvested = true;
    //}
}
