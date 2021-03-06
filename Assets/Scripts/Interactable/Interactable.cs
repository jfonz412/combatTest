﻿using UnityEngine;

public class Interactable : MonoBehaviour {

    [HideInInspector]
    public Transform player;

    [HideInInspector]
    public string[] myInteractions = new string[4]; //use this to populate interact menu

    public enum DefaultInteractions { Attack, Talk, Trade, Pickup, Inspect, Harvest };
    public DefaultInteractions defaultInteraction; //allows us to select default interaction for this object

    public float radius = 1f; //same as melee standard in weapon script

    //can eventually take in more info about the unit interacting if we want to open this up to NPCs
    public virtual void Interaction(string interaction) 
    {
        //cache the player, or is it better to just get the singleton's reference when I need it?
        player = ScriptToolbox.GetInstance().GetPlayerManager().player.transform;
    }

    //maybe have this trigger dialogue window with italicized text
    public void InspectObject()
    {
        Debug.Log("Inspecting " + gameObject);
    }

    public virtual void OpenInteractionMenu()
    {
        Vector3 menuSpawnPoint = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);

        Instantiate(Resources.Load("PopUps/Interactable/InteractionMenu"), menuSpawnPoint, Quaternion.identity, CanvasUI.instance.transform); //FindObjectOfType is iffy, might not always grab proper canvas

        InteractableMenu.instance.PopulateOptions(this);
    }


    //debuggin' purposes
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
