using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuantityPrompt : MonoBehaviour {

    public GameObject promptWindow;
    [HideInInspector]
    public bool waitingForInput = false;
    [HideInInspector]
    public int enteredAmount = 0;

    private Brain playerBrain;

    IEnumerator waitForInput;

    private void Start()
    {
        playerBrain = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<Brain>();
    }

    public void TriggerPrompt()
    {
        waitForInput = WaitForInput();
        StartCoroutine(waitForInput);
    }

    private IEnumerator WaitForInput()
    {
        PromptState(true);
        while (waitingForInput && playerBrain.ActiveState(Brain.State.Prompted))
        {
            yield return null;
        }
        PromptState(false);
    }

    private void PromptState(bool prompting)
    {
        if (prompting)
        {
            playerBrain.ToggleState(Brain.State.Prompted, true);
            promptWindow.SetActive(true);
            waitingForInput = true;
        }
        else
        {
            playerBrain.ToggleState(Brain.State.Prompted, false); 
            promptWindow.SetActive(false);
        }
    }

    public int GetQuantity()
    {
        int quantity = enteredAmount;
        enteredAmount = 0;
        return quantity;
    }

    public void OKButtonPressed(Text input) //Text input?
    {
        waitingForInput = false;

        //if int not passed in we just return 0
        if(!int.TryParse(input.text, out enteredAmount)) 
        {
            enteredAmount = 0;
        }
    }

    public void CancelButtonPressed()
    {
        waitingForInput = false;
        enteredAmount = 0;
    }
}
