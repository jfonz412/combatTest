using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuantityPrompt : MonoBehaviour {

    public GameObject promptWindow;
    //public Text quantityInput;
    public bool waitingForInput = false;
    public int enteredAmount = 0;

    IEnumerator waitForInput;

    #region Singleton
    //insures that we can easily access the inventory, and that there is only one 
    //inventory at all times
    public static QuantityPrompt instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of QuantityPrompt found");
            return;
        }
        instance = this;
    }
    #endregion

    public void TriggerPrompt()
    {
        waitForInput = WaitForInput();
        StartCoroutine(waitForInput);
    }


    IEnumerator WaitForInput()
    {
        PromptState(true);
        while (waitingForInput && PlayerState.currentState == PlayerState.PlayerStates.Prompt)
        {
            yield return null;
        }
        PromptState(false);
    }

    void PromptState(bool prompting)
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

        //if int not passed in just return 0
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
