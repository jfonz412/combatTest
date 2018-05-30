using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuantityPrompt : MonoBehaviour {

    public GameObject promptWindow;
    [HideInInspector]
    public bool waitingForInput = false;
    [HideInInspector]
    public int enteredAmount = 0;

    IEnumerator waitForInput;

    public void TriggerPrompt()
    {
        waitForInput = WaitForInput();
        StartCoroutine(waitForInput);
    }

    private IEnumerator WaitForInput()
    {
        PromptState(true);
        while (waitingForInput && PlayerState.GetPlayerState() == PlayerState.PlayerStates.Prompt)
        {
            yield return null;
        }
        PromptState(false);
    }

    private void PromptState(bool prompting)
    {
        if (prompting)
        {
            PlayerState.SetPlayerState(PlayerState.PlayerStates.Prompt);
            promptWindow.SetActive(true);
            waitingForInput = true;
        }
        else
        {
            PlayerState.SetPlayerState(PlayerState.PlayerStates.Shopping); //should just be previous state to catch edge cases?
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
