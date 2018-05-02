using UnityEngine;
using UnityEngine.UI;

public class PlayerWallet : MonoBehaviour {
    public float balance = 0f;
    public Text displayedAmount;

    void Start()
    {
        UpdateDisplayedAmount();
    }

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

    void UpdateDisplayedAmount()
    {
        displayedAmount.text = " " + balance.ToString() + "g"; //will need to format this number eventually
    }
}
