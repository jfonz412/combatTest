using UnityEngine;
using System.Collections;

public class UnitReactions : MonoBehaviour
{
    AttackController attackController;
    NPCInteraction interactions;
    // Use this for initialization
    void Start()
    {
        interactions = GetComponent<NPCInteraction>();
        attackController = GetComponent<AttackController>();
    }

    /***-----------------------------------------NPC FUNCTIONS----------------------------------------------- ***/

    // Might eventually be able to call the appropriate respnse based on variables assigned in the inspector
    // might forget about player reactions and instead have one parent UnitReaction script, and then a FactionReaction variable that will determine how
    // the unit will react to certain things
    public void ReactToDisturbance(string disturbanceType, Transform target = null)
    {
        //if the player is being attacked
        if (gameObject.name == "Player")
        {
            if (disturbanceType == "Damage Taken" && attackController.lastKnownTarget == null)
            {
                //do nothing for now
            }
        }
        else //if anything else is attacked
        {
            //target the last unit that attacked it
            if (disturbanceType == "Damage Taken" && attackController.lastKnownTarget != target)
            {
                attackController.EngageTarget(true, target);
                for(int i = 0; i < interactions.myInteractions.Length; i++)
                {
                    if(interactions.myInteractions[i] != "Attack")
                    {
                        interactions.myInteractions[i] = "--";
                    }
                }
            }
        }
    }
}
