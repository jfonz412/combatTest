using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuantityPrompt : MonoBehaviour {

    public GameObject promptWindow;
    public Text quantityInput;
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

    public int GetQuantity()
    {
        int quantity = enteredAmount;
        enteredAmount = 0;
        return quantity;
    }

    IEnumerator WaitForInput()
    {
        PlayerState.SetPlayerState(PlayerState.PlayerStates.Prompt);
        promptWindow.SetActive(true);
        waitingForInput = true;

        //what happens if player is hit while inputting? Must cancel this Coroutine if knocked out of shoppping/prompt states
        while (!Input.GetKeyDown(KeyCode.Space)) // && PlayerState == Prompt
        {
            yield return null;
        }

        PlayerState.SetPlayerState(PlayerState.PlayerStates.Shopping); //should just be previous state to catch edge cases?
        promptWindow.SetActive(false);
        waitingForInput = false;
        //int.TryParse(quantityInput.text, out enteredAmount); //should put this into entered amount automatically
        enteredAmount = 10;
    }
}
