using UnityEngine.UI;
using UnityEngine;

public class BattleReport : MonoBehaviour {
    [SerializeField]
    private GameObject reportObject;
    private static ScrollRect scrollRect;
    private static Text reportText;
    private static string savedText = "";
    private static int lineCount;
    private static string newlines = System.Environment.NewLine + System.Environment.NewLine;
    private Brain playerBrain;

    private Brain.State[] activationImparingStates = new Brain.State[]
    {
        Brain.State.Shopping,
        Brain.State.Talking,
        Brain.State.Paused,
        Brain.State.Prompted
    };

    void Start ()
    {
        playerBrain = ScriptToolbox.GetInstance().GetPlayerManager().playerBrain;
        reportText = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
        scrollRect = transform.GetChild(0).GetChild(0).GetComponent<ScrollRect>();
        reportText.text = savedText;
    }

    void Update()
    {
        ToggleBattleReport(); //make this a UI button to open report and we can get this out of Update(), which is causing an error when player (player's brain)
        //hasn't loaded yet
    }

    public static void AddToBattleReport(string line)
    {
        reportText.text += line + newlines;
        savedText = reportText.text;
        lineCount++;

        if(lineCount >= 7 && scrollRect.verticalNormalizedPosition > 0)
        {
            scrollRect.verticalNormalizedPosition -= .05f;
        }
        else if (scrollRect.verticalNormalizedPosition <= 0)
        {
            Debug.Log("Removing first line");
            RemoveFirstLine();
        }
    }

    private static void RemoveFirstLine()
    {
        int index;
        index = reportText.text.IndexOf(System.Environment.NewLine);
        reportText.text = reportText.text.Substring(index + System.Environment.NewLine.Length);

        //removes second newline
        index = reportText.text.IndexOf(System.Environment.NewLine);
        reportText.text = reportText.text.Substring(index + System.Environment.NewLine.Length);

        lineCount--;
    }

    private void ToggleBattleReport()
    {
        if (playerBrain.ActiveStates(activationImparingStates))
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            reportObject.SetActive(!reportObject.activeSelf);
            Pause(reportObject.activeSelf);
        }
    }

    private void Pause(bool paused)
    {
        if (paused)
        {
            playerBrain.ToggleState(Brain.State.BattleReportOpen, true);
            Time.timeScale = 0;
        }
        else
        {
            playerBrain.ToggleState(Brain.State.BattleReportOpen, false);
            Time.timeScale = 1;
        }
    }

    public void ClearBattleReport()
    {
        savedText = "";
        reportText.text = "";
        scrollRect.verticalNormalizedPosition = 1;
        lineCount = 0;
    }
}
