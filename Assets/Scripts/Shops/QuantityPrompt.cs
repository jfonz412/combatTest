using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuantityPrompt : MonoBehaviour {

    public GameObject promptWindow;
    [HideInInspector]
    public bool waitingForInput = false;
    [HideInInspector]
    public int enteredAmount = 0;

    private PlayerStateMachine psm;

    IEnumerator waitForInput;

    private void Start()
    {
        psm = ScriptToolbox.GetInstance().GetPlayerManager().playerStateMachine;
    }

    public void TriggerPrompt()
    {
        psm.prompted = true; //prevents shop screen from closing when we exit shopping state
        psm.RequestChangeState(UnitStateMachine.UnitState.Prompted);
        promptWindow.SetActive(true);
    }

    public int GetQuantity()
    {
        int quantity = enteredAmount;
        enteredAmount = 0;
        return quantity;
    }

    public void OKButtonPressed(Text input) //Text input?
    {
        //if int not passed in we just return 0
        if (!int.TryParse(input.text, out enteredAmount)) 
        {
            enteredAmount = 0;
        }

        psm.prompted = false;
        psm.RequestChangeState(UnitStateMachine.UnitState.Shopping);
        promptWindow.SetActive(false);
    }

    public void CancelButtonPressed()
    {
        psm.prompted = false;
        psm.RequestChangeState(UnitStateMachine.UnitState.Shopping);
        promptWindow.SetActive(false);
        enteredAmount = 0;
    }
}
