﻿using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour {

    [HideInInspector]
    public Text[] equipmentInfo = new Text[4]; 

    #region Singleton 
    [HideInInspector]
    public static ItemMenu instance;
    void Awake()
    {
        if (instance != null) //if we find another instance
        {
            Destroy(instance.gameObject); //destroy the other menu
        }
        instance = this; //there can only be one!
    }
    #endregion

    public void PopulateStats(Item item)
    {
        string[] stats = GetItemStats(item);

        for (int i = 0; i < stats.Length; i++)
        {
            equipmentInfo[i] = transform.GetChild(i).GetComponent<Text>();
            equipmentInfo[i].text = stats[i];
        }
    }

    public void CloseMenu()
    {
        Destroy(gameObject);
    }

    string[] GetItemStats(Item item)
    {
        string[] myStats = new string[4];
        if (item != null)
        {           
            myStats[0] = item.name;
            myStats[1] = "Value: " + item.currentValue.ToString();
            if (item.stackable)
            {
                myStats[2] = "Quantity: " + item.quantity.ToString();
            }
            myStats[3] = "This is a hardcoded description for the item. It will be replaced with a custom string, but for now I want something as long as a typical item description might be.";
        }
        return myStats;
    }
}
