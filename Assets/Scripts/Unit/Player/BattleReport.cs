using UnityEngine.UI;
using UnityEngine;

public class BattleReport : MonoBehaviour {
    [SerializeField]
    private GameObject reportObject;
    private static Text report;
    private static int lineCount;
    private static string newlines = System.Environment.NewLine + System.Environment.NewLine;

    private PlayerState.PlayerStates[] activationImparingStates = new PlayerState.PlayerStates[]
    {
        PlayerState.PlayerStates.Shopping,
        PlayerState.PlayerStates.Speaking,
        PlayerState.PlayerStates.Paused,
        //PlayerState.PlayerStates.Dead,
        PlayerState.PlayerStates.Prompt
    };


    void Start ()
    {
        report = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
    }

    void Update()
    {
        ToggleBattleReport();
    }

    public static void AddToBattleReport(string line)
    {
        report.text += line + newlines;
        lineCount++;


        if (lineCount >= 20)
        {
            RemoveFirstLine();
        }
    }

    public static void RemoveFirstLine()
    {
        int index;
        index = report.text.IndexOf(System.Environment.NewLine);
        report.text = report.text.Substring(index + System.Environment.NewLine.Length);

        //removes second newline
        index = report.text.IndexOf(System.Environment.NewLine);
        report.text = report.text.Substring(index + System.Environment.NewLine.Length);

        lineCount--;
    }

    private void ToggleBattleReport()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            reportObject.SetActive(!reportObject.activeSelf);
        }

        if (reportObject.activeSelf && PlayerState.CheckPlayerState(activationImparingStates))
        {
            reportObject.SetActive(false);
        }
    }
}
