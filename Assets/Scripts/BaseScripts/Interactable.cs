using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
    [HideInInspector]
    public Transform player;
    public float radius = 1f; //same as melee standard in weapon script
    public Dialogue dialog; //contains fields for name and text

    //virtual allows child classes to overwrite this function
    public virtual void DefaultInteraction()
    {
        player = PlayerManager.instance.player.transform;
        Debug.Log("Default Interaction with " + this);
    }

    //can be used for descriptions and observations as well
    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialog);
    }

    //debuggin' purposes
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
