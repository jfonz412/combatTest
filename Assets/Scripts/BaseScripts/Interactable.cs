using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
    [HideInInspector]
    public Transform player;
    public float radius = 1f; //same as melee standard in weapon script

    public virtual void DefaultInteraction()
    {
        player = PlayerManager.instance.player.transform;
        Debug.Log("Default Interaction with " + this);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
