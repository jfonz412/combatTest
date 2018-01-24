using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
    public float radius = 1f; //same as melee standard in weapon script
    public Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform; //might need to check if player exists
    }

    public virtual void DefaultInteraction()
    {
        Debug.Log("Default Interaction with " + this);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
