using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InjuryLog : MonoBehaviour {
    [SerializeField]
    private Text log;

    public void DisplayInjuryList(List<string> injuryList)
    {
        string injuryLine;

        if (injuryList == null)
            Debug.LogError("Injury list is null!");

        for (int i = 0; i < injuryList.Count; i++)
        {
            injuryLine = injuryList[i] + System.Environment.NewLine;
            log.text += injuryLine;
            Debug.Log("Displaying injury: " + injuryLine);
        }
    }

    public void ClearInjuryList()
    {
        log.text = "";
    }
}
