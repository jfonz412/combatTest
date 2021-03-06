﻿using UnityEngine;
using UnityEngine.UI;

public class EquipmentStats : MonoBehaviour {

    Text[] equipmentInfo = new Text[5]; 

    #region Singleton 
    [HideInInspector]
    public static EquipmentStats instance;
    void Awake()
    {
        if (instance != null) //if we find another instance
        {
            Destroy(instance.gameObject); //destroy the other menu
        }
        instance = this; //there can only be one!
    }
    #endregion

    public void PopulateStats(string[] stats)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            //Debug.Log(equipmentInfo[i]); //IS OFTEN NULL
            equipmentInfo[i] = transform.GetChild(i).GetComponent<Text>();
            equipmentInfo[i].text = stats[i];
        }      
    }

    public void CloseMenu()
    {
        Destroy(gameObject);
    }
}
