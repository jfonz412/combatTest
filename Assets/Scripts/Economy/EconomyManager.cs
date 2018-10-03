using UnityEngine;
using UnityEngine.SceneManagement;

public class EconomyManager : MonoBehaviour {
    private static EconomyManager instance;

    #region Singleton

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of EconomyManager found");
            return;
        }
        instance = this;
    }
    #endregion

    public static EconomyManager GetInstance()
    {
        return instance;
    }

    public MarketInventory GetMarketInventory()
    {
        Debug.Log("Current shops: " + GetComponent<MarketInventory>().ToString());
        return GetComponent<MarketInventory>();
    }

}
