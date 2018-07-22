using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Injuries : MonoBehaviour {
    public static void DamageMessage(BodyPart.DamageInfo damageInfo) //should include what is being damaged
    {
        /*
        string armor = damageInfo.armorName;
        string weapon = damageInfo.weaponName;
        string myName = damageInfo.victimName;
        BodyPartController.Parts bodypart = damageInfo.bodyPart;

        Dictionary<BodyPartController.Parts, string> injuryList = GetInjuryList(damageInfo);//new Dictionary<BodyPartController.Parts, string>();

        string line = string.Format(injuryList[bodypart], myName, weapon, armor);
        BattleReport.AddToBattleReport(line);
    }

    private static Dictionary<BodyPartController.Parts, string> GetInjuryList(BodyPartController.DamageInfo damageInfo)
    {
        Debug.LogWarning("inuries is an obsolete script, don't touch it");
        return new Dictionary<BodyPartController.Parts, string>();

        /*
        switch (damageInfo.bodyType)
        {
            case BodyPartController.BodyType.Human:
                return HumanInjuries.GetInjuryList(damageInfo.damageType, damageInfo.severityLevel);
            case BodyPartController.BodyType.BushBanshee:
                return BushBansheeInjuries.GetInjuryList(damageInfo.damageType, damageInfo.severityLevel);
            case BodyPartController.BodyType.FourLeggedAnimal:
                return FourLeggedAnimalInjuries.GetInjuryList(damageInfo.damageType, damageInfo.severityLevel);
            default:
                Debug.LogError("Invalid bodytype");
                return new Dictionary<BodyPartController.Parts, string>();
        }
        
    }*/
    }
}
