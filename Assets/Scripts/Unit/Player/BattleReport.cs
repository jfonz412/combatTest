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

    private PlayerState.PlayerStates[] activationImparingStates = new PlayerState.PlayerStates[]
    {
        PlayerState.PlayerStates.Shopping,
        PlayerState.PlayerStates.Speaking,
        PlayerState.PlayerStates.Paused,
        PlayerState.PlayerStates.Prompt
    };

    PlayerState.PlayerStates stateBeforePause;

    void Start ()
    {
        reportText = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
        scrollRect = transform.GetChild(0).GetChild(0).GetComponent<ScrollRect>();
        reportText.text = savedText;
    }

    void Update()
    {
        ToggleBattleReport();
    }

    public static void AddToBattleReport(string line)
    {
        reportText.text += line + newlines;
        savedText = reportText.text;
        lineCount++;

        if(lineCount >= 7 && scrollRect.verticalNormalizedPosition > 0)
        {
            scrollRect.verticalNormalizedPosition -= .05f;
            Debug.Log(scrollRect.verticalNormalizedPosition);
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
        if (PlayerState.CheckPlayerState(activationImparingStates))
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
            stateBeforePause = PlayerState.GetPlayerState();
            PlayerState.SetPlayerState(PlayerState.PlayerStates.BattleReport);
            Time.timeScale = 0;
        }
        else
        {
            PlayerState.SetPlayerState(stateBeforePause);
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
