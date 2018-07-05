using System.Collections.Generic;
using UnityEngine;

public class HealthDoll : MonoBehaviour {
    private DollPart[] dollParts;
    private HumanoidBody playerBody;
    Dictionary<BodyParts.Parts, int> playerHealth;

    // Use this for initialization
    void Start () {
        playerBody = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<HumanoidBody>();
        CollectDollParts();
        //LoadSavedBodyPartHealth(); //activates everytime this is activated
        playerBody.onDamageTaken += DamageBodyPart;
        playerBody.onHealthLoaded += LoadSavedBodyPartHealth;
	}
	
	private void DamageBodyPart(BodyParts.DamageInfo info)
    {
        for (int i = 0; i < dollParts.Length; i++)
        {
            if(dollParts[i].dollPart == info.bodyPart)
            {
                BodyParts.Parts p = info.bodyPart;

                playerHealth[p] = info.severityLevel;
                Color color = DeterminePartColor(playerHealth[p]);
                dollParts[i].ChangeColor(color);
                LogDamage(dollParts[i], info); 

                return;
            }
        }
        Debug.LogError("BodyPart not found!!! Should not exit the for loop!");
    }

    private void CollectDollParts()
    {
        Transform dollParent = transform.GetChild(0).GetChild(0);
        int n  = dollParent.childCount;
        dollParts = new DollPart[n];

        for(int i = 0; i < n; i++)
        {
            dollParts[i] = dollParent.GetChild(i).GetChild(0).GetComponent<DollPart>();
        }

        if(dollParts == null)
        {
            Debug.LogError("Dollparts is null!");
        }
        //dollParts = transform.GetChild(0).GetChild(0).GetComponentsInChildren<DollPart>();
    }

    private void LoadSavedBodyPartHealth()
    {
        //set the dictionary to the bodyPart's dictionary
        playerHealth = playerBody.GetPartDamage();

        //for each bodypart in the dictionary, we check their health the determine their color
        for (int i = 0; i < dollParts.Length; i++)
        {
            Color color = DeterminePartColor(playerHealth[dollParts[i].dollPart]);
            dollParts[i].ChangeColor(color);
        }
    }

    private Color DeterminePartColor(int severityLevel)
    {
        if (severityLevel == 0 || severityLevel == 1)
        {
            return Color.green;
        }
        else if (severityLevel == 2)
        {
            return Color.yellow;
        }
        else if (severityLevel == 3)
        {
            return new Color32(255, 140, 0, 255); //orange
        }
        else if(severityLevel == 4)
        {
            return Color.red;
        }
        else
        {
            Debug.LogError("Shouldn't reach this!");
            return Color.white;
        }
    }

    private void LogDamage(DollPart part, BodyParts.DamageInfo info) // Injuries.DamageType damageType
    {
        string injury = StringsForInjuryLog.InjuryString(info);
        part.LogInjury(injury);
    }

    public Dictionary<BodyParts.Parts, List<string>> SaveInjuryLog()
    {
        Dictionary<BodyParts.Parts, List<string>> injuryLog = new Dictionary<BodyParts.Parts, List<string>>();

        for (int i = 0; i < dollParts.Length; i++)
        {
            injuryLog[dollParts[i].dollPart] = dollParts[i].GetInjuryLog();
        }

        return injuryLog;
    }

    public void LoadInjuryLog(Dictionary<BodyParts.Parts, List<string>> savedInjuryLog)
    {
        for (int i = 0; i < dollParts.Length; i++) 
        {
            dollParts[i].LoadInjuryLog(savedInjuryLog[dollParts[i].dollPart]);
        }
    }
}
