using System.Collections.Generic;
using UnityEngine;

public class HealthDoll : MonoBehaviour {
    private DollPart[] dollParts;
    private HumanoidBody playerBody;
    Dictionary<BodyParts.Parts, float> playerHealth;

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

                playerHealth[p] -= info.damageDealt; //callback is prob empty which is giving us an error here
                Color color = DeterminePartColor(playerHealth[p]);
                dollParts[i].ChangeColor(color);
                LogDamage(dollParts[i], info.severityID); //info.damageType

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
        playerHealth = playerBody.GetBodyPartHealth();

        //for each bodypart in the dictionary, we check their health the determine their color
        for (int i = 0; i < dollParts.Length; i++)
        {
            Color color = DeterminePartColor(playerHealth[dollParts[i].dollPart]);
            dollParts[i].ChangeColor(color);
        }
    }

    private Color DeterminePartColor(float healthOfPart)
    {
        if (healthOfPart >= 75)
        {
            return Color.green;
        }
        else if (healthOfPart >= 50)
        {
            return Color.yellow;
        }
        else if (healthOfPart >= 25)
        {
            return new Color32(255, 140, 0, 255); //orange
        }
        else if(healthOfPart < 25)
        {
            return Color.red;
        }
        else
        {
            Debug.LogError("Shouldn't reach this!");
            return Color.white;
        }
    }

    private void LogDamage(DollPart part, int severityID) // Injuries.DamageType damageType
    {
        string injury = InjuryString(severityID);
        part.LogInjury(injury);
    }

    private string InjuryString(int severityID)
    {
        string s;

        if(severityID == 0)
        {
            s = "light scratch";
        }
        else if (severityID == 1)
        {
            s = "cut open";
        }
        else if(severityID == 2)
        {
            s = "deep cut";
        }
        else if (severityID == 3)
        {
            s = "very deep gash";
        }
        else if (severityID == 4)
        {
            s = "horribly wounded";
        }
        else if (severityID == 5)
        {
            s = "obliterated";
        }
        else
        {
            s = "";
            Debug.LogError("SEVERITY ID NOT FOUND");
        }

        return s;
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
