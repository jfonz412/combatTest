using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class BattleReport : MonoBehaviour {
    [SerializeField]
    private GameObject reportObject;
    [SerializeField]
    private GameObject reportFilter;
    private static ScrollRect scrollRect;
    private static Text reportText;

    //lists seem to trim the newlines
    private static List<string> unfilteredReport = new List<string>();
    private static string newlines = System.Environment.NewLine + System.Environment.NewLine;

    void Start ()
    {
        reportText = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
        scrollRect = transform.GetChild(0).GetChild(0).GetComponent<ScrollRect>();
        LoadReport(unfilteredReport); //allows report to be loaded between scenes
    }

    public static void AddToBattleReport(string line, string[] filters = null)
    {
        reportText.text += line + newlines;
        unfilteredReport.Add(line);

        if (unfilteredReport.Count >= 7 && scrollRect.verticalNormalizedPosition > 0)
        {
            scrollRect.verticalNormalizedPosition -= .05f;
        }
        else if (scrollRect.verticalNormalizedPosition <= 0)
        {
            Debug.Log("Removing first line");
            RemoveFirstLine();
        }

        //add filters
        if(filters != null)
        {
            for (int i = 0; i < filters.Length; i++)
            {
                ReportFilter.AddToFilterList(filters[i]);
            }
        }
    }

    public static void FilterReport(string text)
    {
        Debug.Log("Filtering report on " + text);
        
        //clear the unfiltered report from the screen
        reportText.text = "";

        //search each line of unfilteredReport for this text
        //copy those elements into a filteredReport
        for (int i = 0; i < unfilteredReport.Count; i++)
        {
            if (unfilteredReport[i].Contains(text))
                reportText.text += unfilteredReport[i] + newlines;
        }
    }

    public void UnfilterReport()
    {
        Debug.Log("unfiltering report");
        //clear the filtered report from the screen
        reportText.text = "";

        //load unfiltered report
        for (int i = 0; i < unfilteredReport.Count; i++)
        {
            reportText.text += unfilteredReport[i] + newlines;
        }
    }

    private void LoadReport(List<string> report)
    {
        reportText.text = "";
        for (int i = 0; i < report.Count; i++)
        {
            reportText.text += report[i] + newlines;
        }
    }

    void Update()
    {
        ToggleBattleReport(); //make this a UI button to open report and we can get this out of Update(), which is causing an error when player (player's brain)
        //hasn't loaded yet
    }

    private static void RemoveFirstLine()
    {
        int index;
        index = reportText.text.IndexOf(System.Environment.NewLine);
        reportText.text = reportText.text.Substring(index + System.Environment.NewLine.Length);

        //removes second newline
        index = reportText.text.IndexOf(System.Environment.NewLine);
        reportText.text = reportText.text.Substring(index + System.Environment.NewLine.Length);

        unfilteredReport.RemoveAt(0);
    }

    private void ToggleBattleReport()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            reportObject.SetActive(!reportObject.activeSelf);
            reportFilter.SetActive(!reportFilter.activeSelf);
            Pause(reportObject.activeSelf);
        }
    }

    private void Pause(bool paused)
    {
        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ClearBattleReport()
    {
        unfilteredReport = new List<string>(); //leaving garbage behind?
        reportText.text = "";
        scrollRect.verticalNormalizedPosition = 1;

        ReportFilter.ClearFilters();
    }
}
