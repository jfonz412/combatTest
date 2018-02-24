﻿using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour {

    [HideInInspector]
    public Text[] equipmentInfo = new Text[4]; 

    Item currentItem;

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
        currentItem = item;
        string[] stats = GetItemStats(item);

        for (int i = 0; i < stats.Length; i++)
        {
            equipmentInfo[i] = transform.GetChild(i).GetComponent<Text>();
            equipmentInfo[i].text = stats[i];
        }
    }

    public void UseItem()
    {
        if (currentItem != null)
        {
            currentItem.Use();
        }
        CloseMenu();
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
            myStats[1] = "Use";
            myStats[2] = "Value: " + item.value.ToString();
            myStats[3] = "Quantity: " + item.quantity.ToString();           
        }
        return myStats;
    }
}