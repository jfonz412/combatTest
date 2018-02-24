using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

    [HideInInspector]
    public Transform player;

    public float radius = 1f; //same as melee standard in weapon script

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
        Vector3 menuSpawnPoint = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);

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
