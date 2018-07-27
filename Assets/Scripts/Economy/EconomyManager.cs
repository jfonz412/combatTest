using UnityEngine;

public class EconomyManager : MonoBehaviour {


    private static EconomyManager instance;
    private MarketInventory marketInventory;

    #region Singleton

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of ScriptToolbox found");
            return;
        }
        instance = this;

        GatherComponents();
    }
    #endregion

    private void GatherComponents()
    {
        marketInventory = GetComponent<MarketInventory>();
    }

    public static EconomyManager GetInstance()
    {
        return instance;
    }

    public MarketInventory GetMarketInventory()
    {
        return marketInventory;
    }

}
