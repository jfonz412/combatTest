using UnityEngine;

public class NPCInteractionStates : MonoBehaviour {

    private Brain myBrain;

    //if any of these are active we cannot do the associated interaction
    Brain.State[] invalidDialogueStates = new Brain.State[] 
    {
        Brain.State.Fighting,
        Brain.State.Fleeing,
        Brain.State.Shock,
        Brain.State.OvercomeByRage,
        Brain.State.OvercomeByFear,
        Brain.State.OvercomeByPain,
        Brain.State.Unconscious,
        Brain.State.Vomitting,
        Brain.State.Dead
    };

    Brain.State[] invalidTradeStates = new Brain.State[]    
    {
        Brain.State.Fighting,
        Brain.State.Fleeing,
        Brain.State.Shock,
        Brain.State.OvercomeByRage,
        Brain.State.OvercomeByFear,
        Brain.State.OvercomeByPain,
        Brain.State.Unconscious,
        Brain.State.Vomitting,
        Brain.State.Dead
    };

    Brain.State[] invalidInspectStates = new Brain.State[] 
    {

    };

    Brain.State[] invalidAttackStates = new Brain.State[]   
    {

    };

    private void Start()
    {
        myBrain = GetComponent<Brain>();
    }

    public bool AbleToInteract(string interaction)
    {
        if (interaction == "Attack")
        {
            //Debug.Log("Checking attack");
            //return !myBrain.ActiveStates(invalidAttackStates); //there are currently no invalid attack or inspect states
        }
        else if(interaction == "Talk")
        {
            //Debug.Log("Checking talk");
            return !myBrain.ActiveStates(invalidDialogueStates);
        }
        else if (interaction == "Trade")
        {
            //Debug.Log("Checking trade");
            return !myBrain.ActiveStates(invalidTradeStates);
        }
        else if (interaction == "Inspect")
        {
            //Debug.Log("Checking Inspect");
            //return !myBrain.ActiveStates(invalidInspectStates);
        }
        return true;
    }
}
