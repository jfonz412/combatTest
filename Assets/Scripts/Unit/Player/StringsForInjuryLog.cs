using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringsForInjuryLog : MonoBehaviour {

    public static string InjuryString(BodyParts.DamageInfo info)
    {

        Injuries.DamageType damageType = info.damageType;

        if(damageType == Injuries.DamageType.Penetration)
        {
            return PenetrationDamage(info);
        }
        else if (damageType == Injuries.DamageType.Impact)
        {
            return ImpactDamage(info);
        }
        else
        {
            Debug.LogError("Damage type not found!");
            return "";
        }
    }

    private static string PenetrationDamage(BodyParts.DamageInfo info)
    {
        int severityID = info.severityID;
        BodyParts.Parts part = info.bodyPart;
        string s;

        if (severityID == 0) //light scratch
        {
            s = "light scratch";
        }
        else if (severityID == 1) //cut
        {
            s = "small cut";
        }
        else if (severityID == 2) //deep cut
        {
            if (part == BodyParts.Parts.Head)
            {
                s = "chipped skull";
            }
            else if (part == BodyParts.Parts.Chest)
            {
                s = "chipped ribs";
            }
            else
            {
                s = "deep cut";
            }
        }
        else if (severityID == 3) //cut down to bone
        {
            if (part == BodyParts.Parts.Head)
            {
                s = "cracked skull";
            }
            else if (part == BodyParts.Parts.Neck)
            {
                s = "spine damage";
            }
            else if (part == BodyParts.Parts.Chest)
            {
                s = "cracked ribs";
            }
            else if (part == BodyParts.Parts.Abdomin)
            {
                s = "sliced open";
            }
            else
            {
                s = "horrible gash";
            }
        }
        else if (severityID == 4) //bone shattering cut
        {
            if (part == BodyParts.Parts.Head)
            {
                s = "skull shattered";
            }
            else if (part == BodyParts.Parts.Neck)
            {
                s = "nearly decapitated";
            }
            else if (part == BodyParts.Parts.Chest)
            {
                s = "punctured lung";
            }
            else if (part == BodyParts.Parts.Abdomin)
            {
                s = "innards scrambled";
            }
            else
            {
                s = "bones shattered";
            }
        }
        else if (severityID == 5) //severed
        {
            s = "severed";
        }
        else
        {
            s = "";
            Debug.LogError("SEVERITY ID NOT FOUND");
        }

        return s;
    }

    private static string ImpactDamage(BodyParts.DamageInfo info)
    {
        int severityID = info.severityID;
        BodyParts.Parts part = info.bodyPart;
        string s;

        if (severityID == 0)
        {
            s = "light bruise";
        }
        else if (severityID == 1)
        {
            s = "bruised";
        }
        else if (severityID == 2)
        {
            s = "badly bruised";
        }
        else if (severityID == 3) //pulverizing impact
        {
            if (part == BodyParts.Parts.Head)
            {
                s = "cracked skull";
            }
            else if (part == BodyParts.Parts.Chest)
            {
                s = "cracked ribs";
            }
            else if (part == BodyParts.Parts.Abdomin)
            {
                s = "internal bleeding";
            }
            else
            {
                s = "muscle/fat pulverized";
            }
        }
        else if (severityID == 4)
        {
            if (part == BodyParts.Parts.Head)
            {
                s = "caved in";
            }
            else if (part == BodyParts.Parts.Neck)
            {
                s = "windpipe crushed";
            }
            else if (part == BodyParts.Parts.Chest)
            {
                s = "punctured lung";
            }
            else if (part == BodyParts.Parts.Abdomin)
            {
                s = "serious internal bleeding";
            }
            else
            {
                s = "bones shattered";
            }
        }
        else if (severityID == 5)
        {
            s = "obliterated beyond recognition";
        }
        else
        {
            s = "";
            Debug.LogError("SEVERITY ID NOT FOUND");
        }

        return s;
    }
}



