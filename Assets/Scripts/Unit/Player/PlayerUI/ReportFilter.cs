using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportFilter : MonoBehaviour {
    private static List<string> reportFilters = new List<string>();
    private static List<GameObject> buttons = new List<GameObject>(); //so we can easily clear the buttons
    private static Transform buttonContainer;
    private static Vector2 buttonSpawnPoint;

	// Use this for initialization
	void Start ()
    {
        buttonContainer = transform.GetChild(1).GetChild(0).GetChild(0);
        buttonSpawnPoint = new Vector2(buttonContainer.position.x, buttonContainer.position.y + 495f);
        LoadFilters();
    }

    public static void AddToFilterList(string option)
    {
        //check if filter already exists
        for (int i = 0; i < reportFilters.Count; i++)
        {
            if (option == reportFilters[i])
                return;
        }

        reportFilters.Add(option);
        AddButtonForOption(option);
    }

    public static void ClearFilters()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            //buttons[i].SetActive(false);
            Destroy(buttons[i]);
        }
        reportFilters = new List<string>(); //may leave garbage behind
        buttons = new List<GameObject>();
    }

    private static void AddButtonForOption(string option)
    {
        GameObject newButton = Instantiate(Resources.Load("FilterOption"), Vector2.zero, Quaternion.identity, buttonContainer) as GameObject;
        newButton.transform.localPosition = new Vector2(0f, 495f + (buttons.Count * -20f));
        newButton.transform.GetChild(0).GetComponent<Text>().text = option;
        newButton.GetComponent<Button>().onClick.AddListener(delegate { BattleReport.FilterReport(option); });
        buttons.Add(newButton);
    }

    private void LoadFilters()
    {
        //clear buttons
        buttons = new List<GameObject>();

        for (int i = 0; i < reportFilters.Count; i++)
        {
            AddButtonForOption(reportFilters[i]);
        }        
    }
}
