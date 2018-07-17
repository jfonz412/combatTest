using System.Collections.Generic;
using UnityEngine;

public class HealthDoll : MonoBehaviour {
    private DollPart[] dollParts;
    private BodyParts playerBody;
    Dictionary<BodyParts.Parts, int> playerHealth;

    // Use this for initialization
    void Start () {
        playerBody = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<BodyParts>();
        CollectDollParts();
        playerBody.onDamageTaken += DamageBodyPart;
        playerBody.onHealthLoaded += LoadSavedBodyPartHealth;
	}

	private void DamageBodyPart(BodyParts.DamageInfo info)
    {
        Debug.Log(playerHealth[BodyParts.Parts.Chest] + "  " + playerHealth[BodyParts.Parts.RightHand] + " " + playerHealth[BodyParts.Parts.LeftArm] + " " + playerHealth[BodyParts.Parts.Neck] + "  " + playerHealth[BodyParts.Parts.RightArm] + " " + playerHealth[BodyParts.Parts.LeftLeg] + "  " + playerHealth[BodyParts.Parts.RightLeg] + " " + playerHealth[BodyParts.Parts.Head] + "  " + playerHealth[BodyParts.Parts.LeftHand] + " " + playerHealth[BodyParts.Parts.Abdomin]);
        for (int i = 0; i < dollParts.Length; i++)
        {
            if (dollParts[i].dollPart == info.bodyPart)
            {
                BodyParts.Parts p = info.bodyPart;

                Debug.Log(" playerHealth[p] is " + playerHealth[p]);

                playerHealth[p] = info.severityLevel;
                Color color = DeterminePartColor(playerHealth[p]);
                dollParts[i].ChangeColor(color);
        
                LogDamage(dollParts[i], info); 

                break;
            }
        }
        Debug.Log("Exited for loop");
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
        playerHealth = new Dictionary<BodyParts.Parts, int>(playerBody.GetPartDamage());
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
        else if (severityLevel == 5)
        {
            Color transparent = Color.red;
            transparent.a = 0.5f;
            return transparent;
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
