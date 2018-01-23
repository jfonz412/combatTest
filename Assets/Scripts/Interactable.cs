using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
    public float radius = 1f; //same as melee standard in weapon script

    public virtual void DefaultInteraction(Transform playerTransform)
    {
        Debug.Log("Default Interaction with " + playerTransform);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
