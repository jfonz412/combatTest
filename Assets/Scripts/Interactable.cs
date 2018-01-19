using UnityEngine;

public class Interactable : MonoBehaviour {
    public float radius = 1f; //same as melee as of 1/18

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
