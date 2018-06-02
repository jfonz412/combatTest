﻿using UnityEngine;
using System.Collections;

public class Harvestable : Interactable {
    [HideInInspector]
    public bool isHarvested = false;

    [SerializeField]
    private string harvestType;
    [SerializeField]
    private float timeToHarvest = 5f;
    [SerializeField]
    private GameObject itemDrop;
    //[SerializeField]
    //private float dropChance;

    //private static int nodeNumber = 0;

    void Start () {
        myInteractions = new string[] { harvestType, "Inspect", "--", "--" };      
        //timeToHarvest = timeToHarvest * toolBonus;
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
        Debug.Log("Harvesting " + gameObject);
        IEnumerator harvest = HarvestNode();
        StartCoroutine(harvest);
    }
    
    private IEnumerator HarvestNode()
    {
        Collider2D nodeCol = GetComponent<Collider2D>();
        Collider2D playerCol = player.GetComponent<Collider2D>(); //eventually will allow other units to harvest
        
        float timePassed = 0;

        while (timePassed < timeToHarvest)
        {
            if (!playerCol.IsTouching(nodeCol))
            {
                Debug.Log("Player walked away from harvestable node");
                yield break;
            }

            yield return new WaitForSeconds(1f);
            timePassed++;
            Debug.Log("Harvesting...(" + timePassed + ")");
        }
        Debug.Log("Done harvesting");

        DropAndDestroy();
        yield break;
    }

    private void DropAndDestroy()
    {
        if (itemDrop != null)
        {
            Instantiate(itemDrop, transform.position, Quaternion.identity);
        }

        isHarvested = true;
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}