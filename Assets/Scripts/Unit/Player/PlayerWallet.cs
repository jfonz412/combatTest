using UnityEngine;
using UnityEngine.UI;

public class PlayerWallet : MonoBehaviour {

    private float balance;
    public Text displayedAmount;

    public void Withdraw(float withdrawl)
    {
        balance -= withdrawl;
        UpdateDisplayedAmount();
    }

    public void Deposit(float deposit)
    {
        balance = balance + deposit;
        UpdateDisplayedAmount();
    }

    private void UpdateDisplayedAmount()
    {
        displayedAmount.text = " " + balance.ToString() + "g"; //will need to format this number eventually
    }

    public void LoadSavedBalance(float savedBalance = -1f)
    {
        if(savedBalance == -1)
        {
            balance = 500f;
        }
        else
        {
            balance = savedBalance;
        }
        UpdateDisplayedAmount();
    }

    public float GetCurrentBalance()
    {
        return balance;
    }
}
