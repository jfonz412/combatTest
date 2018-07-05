using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Injuries : MonoBehaviour {
    public enum DamageType { Penetration, Impact };

    public static void DamageMessage(BodyParts.DamageInfo damageInfo) //should include what is being damaged
    {
        string armor = damageInfo.armorName;
        string weapon = damageInfo.weaponName;
        string myName = damageInfo.victimName;
        BodyParts.Parts bodypart = damageInfo.bodyPart;

        Dictionary<BodyParts.Parts, string> injuryList = GetInjuryList(damageInfo);//new Dictionary<BodyParts.Parts, string>();

        string line = string.Format(injuryList[bodypart], myName, weapon, armor);
        BattleReport.AddToBattleReport(line);
    }

    private static Dictionary<BodyParts.Parts, string> GetInjuryList(BodyParts.DamageInfo damageInfo)
    {
        switch (damageInfo.bodyType)
        {
            case BodyParts.BodyType.Human:
                return HumanInjuries.GetInjuryList(damageInfo.damageType, damageInfo.severityLevel);
            case BodyParts.BodyType.BushBanshee:
                return BushBansheeInjuries.GetInjuryList(damageInfo.damageType, damageInfo.severityLevel);
            case BodyParts.BodyType.FourLeggedAnimal:
                return FourLeggedAnimalInjuries.GetInjuryList(damageInfo.damageType, damageInfo.severityLevel);
            default:
                Debug.LogError("Invalid bodytype");
                return new Dictionary<BodyParts.Parts, string>();
        }
    }
}
