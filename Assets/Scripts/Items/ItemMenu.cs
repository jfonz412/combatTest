using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour {

    [HideInInspector]
    public Text[] textBoxes = new Text[4]; 

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

    public void PopulateInfo(string[] itemInfo)
    {
        for (int i = 0; i < itemInfo.Length; i++)
        {
            textBoxes[i] = transform.GetChild(i).GetComponent<Text>();
            textBoxes[i].text = itemInfo[i];
        }
    }

    public void CloseMenu()
    {
        Destroy(gameObject);
    }

}
