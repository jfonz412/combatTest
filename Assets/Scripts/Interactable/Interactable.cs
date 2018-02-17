﻿using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

    [HideInInspector]
    public Transform player;

    public float radius = 1f; //same as melee standard in weapon script
    public Dialogue dialog; //contains fields for name and text

    [HideInInspector]
    public string[] myInteractions = new string[4]; //use this to populate interact menu
    public enum DefaultInteractions { Attack, Talk, Pickup, Inspect };
    
    //virtual allows child classes to overwrite this function
    public virtual void Interaction(string interaction)
    {
        player = PlayerManager.instance.player.transform;
    }

    //maybe have this trigger dialogue window with italicized text
    public void InspectObject()
    {
        Debug.Log("Inspecting " + gameObject);
    }

    public virtual void OpenInteractionMenu()
    {
        Vector3 menuSpawnPoint = Camera.main.WorldToScreenPoint(transform.position);
        menuSpawnPoint = new Vector3(menuSpawnPoint.x + 30f, menuSpawnPoint.y + 30f, menuSpawnPoint.z);

        Instantiate(Resources.Load("InteractionMenu"), menuSpawnPoint, Quaternion.identity, FindObjectOfType<Canvas>().transform);

        InteractableMenu.instance.PopulateOptions(this);
    }

    //debuggin' purposes
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}