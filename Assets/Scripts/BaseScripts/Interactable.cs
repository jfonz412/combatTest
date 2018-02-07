using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

    [HideInInspector]
    public Transform player;

    [HideInInspector]
    public string[] listOfInteractions; //will be used for the right-click dropdown menu

    public float radius = 1f; //same as melee standard in weapon script
    public Dialogue dialog; //contains fields for name and text

    //virtual allows child classes to overwrite this function
    public virtual void Interaction(string interaction)
    {
        player = PlayerManager.instance.player.transform;
        Debug.Log("Default Interaction with " + this);
    }

    //debuggin' purposes
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
