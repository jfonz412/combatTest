using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringsForInjuryLog : MonoBehaviour {

    public static string InjuryString(BodyPart.DamageInfo info)
    {
        Item.AttackType damageType = info.damageType;
        Debug.Log("Damage log not implemented!");
        return "";      
    }

    private static string PenetrationDamage(BodyPart.DamageInfo info)
    {
        /*
        int severityLevel = info.severityLevel;
        BodyPart part = info.bodyPart;
        string s;

        if (severityLevel == 0) //light scratch
        {
            s = "light scratch";
        }
        else if (severityLevel == 1) //cut
        {
            s = "small cut";
        }
        else if (severityLevel == 2) //deep cut
        {
            if (part == BodyPart.Head)
            {
                s = "chipped skull";
            }
            else if (part == BodyPart.Chest)
            {
                s = "chipped ribs";
            }
            else
            {
                s = "deep cut";
            }
        }
        else if (severityLevel == 3) //cut down to bone
        {
            if (part == BodyPart.Head)
            {
                s = "cracked skull";
            }
            else if (part == BodyPart.Neck)
            {
                s = "spine damage";
            }
            else if (part == BodyPart.Chest)
            {
                s = "cracked ribs";
            }
            else if (part == BodyPart.Abdomin)
            {
                s = "sliced open";
            }
            else
            {
                s = "horrible gash";
            }
        }
        else if (severityLevel == 4) //bone shattering cut
        {
            if (part == BodyPart.Head)
            {
                s = "skull shattered";
            }
            else if (part == BodyPart.Neck)
            {
                s = "nearly decapitated";
            }
            else if (part == BodyPart.Chest)
            {
                s = "punctured lung";
            }
            else if (part == BodyPart.Abdomin)
            {
                s = "innards scrambled";
            }
            else
            {
                s = "bones shattered";
            }
        }
        else if (severityLevel == 5) //severed
        {
            s = "severed";
        }
        else
        {
            s = "";
            Debug.LogError("SEVERITY ID NOT FOUND");
        }
        */

        return "NEED TO IMPLENT";
    }

    private static string ImpactDamage(BodyPart.DamageInfo info)
    {
        /*
        int severityLevel = info.severityLevel;
        BodyPart part = info.bodyPart;
        string s;

        if (severityLevel == 0)
        {
            s = "light bruise";
        }
        else if (severityLevel == 1)
        {
            s = "bruised";
        }
        else if (severityLevel == 2)
        {
            s = "badly bruised";
        }
        else if (severityLevel == 3) //pulverizing impact
        {
            if (part == BodyPart.Head)
            {
                s = "cracked skull";
            }
            else if (part == BodyPart.Chest)
            {
                s = "cracked ribs";
            }
            else if (part == BodyPart.Abdomin)
            {
                s = "internal bleeding";
            }
            else
            {
                s = "muscle/fat pulverized";
            }
        }
        else if (severityLevel == 4)
        {
            if (part == BodyPart.Head)
            {
                s = "caved in";
            }
            else if (part == BodyPart.Neck)
            {
                s = "windpipe crushed";
            }
            else if (part == BodyPart.Chest)
            {
                s = "punctured lung";
            }
            else if (part == BodyPart.Abdomin)
            {
                s = "serious internal bleeding";
            }
            else
            {
                s = "bones shattered";
            }
        }
        else if (severityLevel == 5)
        {
            s = "obliterated beyond recognition";
        }
        else
        {
            s = "";
            Debug.LogError("SEVERITY ID NOT FOUND");
        }
        */

        return "NEED TO IMPLENT";
    }
}



