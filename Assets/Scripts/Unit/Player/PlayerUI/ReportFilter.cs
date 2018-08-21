using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportFilter : MonoBehaviour {
    private static List<string> reportFilters = new List<string>();
    private static Transform buttonContainer;
    private static Vector2 buttonSpawnPoint;
    private static int numberOfButtons = 0;

	// Use this for initialization
	void Start ()
    {
        buttonContainer = transform.GetChild(1).GetChild(0).GetChild(0);
        buttonSpawnPoint = new Vector2(buttonContainer.position.x, buttonContainer.position.y + 495f);
    }

    public static void AddToFilterList(string option)
    {
        for (int i = 0; i < reportFilters.Count; i++)
        {
            if (option == reportFilters[i])
                return;
        }
        reportFilters.Add(option);
        Debug.Log("adding " + option);
        AddButtonForOption(option);
    }

    private static void AddButtonForOption(string option)
    {
        GameObject newButton = Instantiate(Resources.Load("FilterOption"), Vector2.zero, Quaternion.identity, buttonContainer) as GameObject;
        newButton.transform.localPosition = new Vector2(0f, 495f + (numberOfButtons * -20f));
        newButton.transform.GetChild(0).GetComponent<Text>().text = option;
        newButton.GetComponent<Button>().onClick.AddListener(delegate { BattleReport.FilterReport(option); });
        numberOfButtons++;
        
    }
}
