using UnityEngine;
using UnityEngine.UI;

public class PlayerWallet : MonoBehaviour {
    public float balance = 0f;
    Text displayedAmount;

    #region Singleton
    public static PlayerWallet instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PlayerWallet found");
            return;
        }
        instance = this;
    }
    #endregion

    void Start()
    {
        displayedAmount = GetComponent<Text>();
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
