using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushBansheeInjuries : MonoBehaviour {

    /*
      {"LeftArm", "RightArm",
      "LHand", "RHand", "Thorax", "Abdomin",
      "LeftLeg", "RightLeg", "LeftFoot", "RightFoot" }
    */
    /*
    //should make this inheritable for all injury lists??? this is the same as HumanInjuries
    public static Dictionary<BodyPartController.Parts, string> GetInjuryList(Injuries.DamageType damageType, int severityID)
    {
        if (damageType == Injuries.DamageType.Impact)
        {
            return impactInjuries[severityID];
        }
        else if (damageType == Injuries.DamageType.Penetration)
        {
            return penetrationInjuries[severityID];
        }
        else
        {
            Debug.LogError("Invalid damage type");
            return new Dictionary<BodyPartController.Parts, string>();
        }
    }

    private static Dictionary<BodyPartController.Parts, string>[] penetrationInjuries = new Dictionary<BodyPartController.Parts, string>[] {
        //PENETRATION
        new Dictionary<BodyPartController.Parts, string>() //scratch
        {
            { BodyPartController.Parts.LeftArm, "The {1} hacks at {0}'s {2}, shaking loose some leaves from the left arm!" },
            { BodyPartController.Parts.RightArm, "The {1} hacks at {0}'s {2}, shaking loose some leaves from the right arm!" },
            { BodyPartController.Parts.LeftHand, "The {1} hacks at {0}'s {2}, shaking loose some leaves from the left hand" },
            { BodyPartController.Parts.RightHand, "The {1} hacks at {0}'s {2}, shaking loose some leaves from the right hand!" },
            { BodyPartController.Parts.Chest, "The {1} hacks at {0}'s {2}, shaking loose some leaves from the chest!" },
            { BodyPartController.Parts.Abdomin, "The {1} hacks at {0}'s {2}, shaking loose some leaves from the belly!" },
            { BodyPartController.Parts.LeftLeg, "The {1} hacks at {0}'s {2}, shaking loose some leaves from the left leg!" },
            { BodyPartController.Parts.RightLeg, "The {1} hacks at {0}'s {2}, shaking loose some leaves from the right leg!" },
            { BodyPartController.Parts.LeftFoot, "The {1} hacks at {0}'s {2}, shaking loose some leaves from the left foot!" },
            { BodyPartController.Parts.RightFoot, "The {1} hacks at {0}'s {2}, shaking loose some leaves from the right foot!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //{1} slices
        {
            { BodyPartController.Parts.LeftArm, "The {1} hacks into {0}'s {2}, leaves fall and twigs snap in the left arm!" },
            { BodyPartController.Parts.RightArm, "The {1} hacks into {0}'s {2}, leaves fall and twigs snap in the right arm!" },
            { BodyPartController.Parts.LeftHand, "The {1} hacks into {0}'s {2}, leaves fall and twigs snap in the left hand" },
            { BodyPartController.Parts.RightHand, "The {1} hacks into {0}'s {2}, leaves fall and twigs snap in the right hand!" },
            { BodyPartController.Parts.Chest, "The {1} hacks into {0}'s {2}, leaves fall and twigs snap in the chest!" },
            { BodyPartController.Parts.Abdomin, "The {1} hacks into {0}'s {2}, leaves fall and twigs snap in the belly!" },
            { BodyPartController.Parts.LeftLeg, "The {1} hacks into {0}'s {2}, leaves fall and twigs snap in the left leg!" },
            { BodyPartController.Parts.RightLeg, "The {1} hacks into {0}'s {2}, leaves fall and twigs snap in the right leg!" },
            { BodyPartController.Parts.LeftFoot, "The {1} hacks into {0}'s {2}, leaves fall and twigs snap in the left foot!" },
            { BodyPartController.Parts.RightFoot, "The {1} hacks into {0}'s {2}, leaves fall and twigs snap in the right foot!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //deep cut
        {
            { BodyPartController.Parts.LeftArm, "The {1} cuts through {0}'s {2} chipping main branch inside left arm!" },
            { BodyPartController.Parts.RightArm, "The {1} cuts through {0}'s {2} chipping main branch inside right arm!" },
            { BodyPartController.Parts.LeftHand, "The {1} cuts through {0}'s {2} chipping main branch inside left hand" },
            { BodyPartController.Parts.RightHand, "The {1} cuts through {0}'s {2} chipping main branch inside right hand!" },
            { BodyPartController.Parts.Chest, "The {1} cuts through {0}'s {2} chipping main branch inside chest!" },
            { BodyPartController.Parts.Abdomin, "The {1} cuts through {0}'s {2} chipping main branch inside belly!" },
            { BodyPartController.Parts.LeftLeg, "The {1} cuts through {0}'s {2} chipping main branch inside left leg!" },
            { BodyPartController.Parts.RightLeg, "The {1} cuts through {0}'s {2} chipping main branch inside right leg!" },
            { BodyPartController.Parts.LeftFoot, "The {1} cuts through {0}'s {2} chipping main branch inside left foot!" },
            { BodyPartController.Parts.RightFoot, "The {1} cuts through {0}'s {2} chipping main branch inside right foot!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //cut to bone
        {
            { BodyPartController.Parts.LeftArm, "The {1} completely hacks through {0}'s {2}, cutting into the main branch of the left arm!" },
            { BodyPartController.Parts.RightArm, "The {1} completely hacks through {0}'s {2}, cutting into the main branch of the right arm!" },
            { BodyPartController.Parts.LeftHand, "The {1} completely hacks through {0}'s {2}, slicing off some tendrils on the left hand" },
            { BodyPartController.Parts.RightHand, "The {1} completely hacks through {0}'s {2}, slicing off some tendrils on the right hand!" },
            { BodyPartController.Parts.Chest, "The {1} completely hacks through the {2} and cuts into {0}'s trunk!" },
            { BodyPartController.Parts.Abdomin, "The {1} completely hacks through the {2} and cuts into {0}'s trunk!" },
            { BodyPartController.Parts.LeftLeg, "The {1} completely hacks through {0}'s {2}, cutting into the main branch of the left leg!" },
            { BodyPartController.Parts.RightLeg, "The {1} completely hacks through {0}'s {2}, cutting into the main branch of the right leg!" },
            { BodyPartController.Parts.LeftFoot, "The {1} completely hacks through {0}'s {2}, slicing off some tendrils on the left foot!" },
            { BodyPartController.Parts.RightFoot, "The {1} completely hacks through {0}'s {2}, slicing off some tendrils on the right foot!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //shatters bone
        {
            { BodyPartController.Parts.LeftArm, "The {1} easily cuts through {0}'s {2}, hacking deep into the main branch of the left arm!" },
            { BodyPartController.Parts.RightArm, "The {1} easily cuts through {0}'s {2}, hacking deep into the main branch of the right arm!" },
            { BodyPartController.Parts.LeftHand, "The {1} easily cuts through {0}'s {2}, lobbing off most of the tendrils from the left hand" },
            { BodyPartController.Parts.RightHand, "The {1} easily cuts through {0}'s {2}, lobbing off most of the tendrils from the right hand!" },
            { BodyPartController.Parts.Chest, "The {1} easily cuts through {0}'s {2}, hacking deep into the main trunk!" },
            { BodyPartController.Parts.Abdomin, "The {1} easily cuts through {0}'s {2}, hacking deep into the main trunk!!" },
            { BodyPartController.Parts.LeftLeg, "The {1} easily cuts through {0}'s {2}, hacking deep into the main branch of the left leg!" },
            { BodyPartController.Parts.RightLeg, "The {1} easily cuts through {0}'s {2}, hacking deep into the main branch of the right leg!" },
            { BodyPartController.Parts.LeftFoot, "The {1} easily cuts through {0}'s {2}, lobbing off most of the tendrils from the left foot!" },
            { BodyPartController.Parts.RightFoot, "The {1} easily cuts through {0}'s {2}, lobbing off most of the tendrils fromthe right foot!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //sever
        {
            { BodyPartController.Parts.LeftArm, "The {1} effortlessly slices through {0}'s {2}, severing the left arm!" },
            { BodyPartController.Parts.RightArm, "The {1} effortlessly slices through {0}'s {2}, severing the right arm!" },
            { BodyPartController.Parts.LeftHand, "The {1} effortlessly slices through {0}'s {2}, severing the left hand" },
            { BodyPartController.Parts.RightHand, "The {1} effortlessly slices through {0}'s {2}, severing the right hand!" },
            { BodyPartController.Parts.Chest, "The {1} effortlessly slices through the {2} and finds itself deeply ebedded in {0}'s chest!" },
            { BodyPartController.Parts.Abdomin, "The {1} effortlessly slices through {0}'s {2} and deep into the belly of the beast!" },
            { BodyPartController.Parts.LeftLeg, "The {1} effortlessly slices through {0}'s {2}, severing the left leg!" },
            { BodyPartController.Parts.RightLeg, "The {1} effortlessly slices through {0}'s {2}, severing the right leg!" },
            { BodyPartController.Parts.LeftFoot, "The {1} effortlessly slices through {0}'s {2}, severing the left foot!" },
            { BodyPartController.Parts.RightFoot, "The {1} effortlessly slices through {0}'s {2}, severing the right foot!" },
        }
    };

    private static Dictionary<BodyPartController.Parts, string>[] impactInjuries = new Dictionary<BodyPartController.Parts, string>[] {
        //IMPACT
        new Dictionary<BodyPartController.Parts, string>() //light bruise
        {
            { BodyPartController.Parts.LeftArm, "The force of the {1} hits {0}'s {2}, damaging some of the leaves on the left arm!" },
            { BodyPartController.Parts.RightArm, "The force of the {1} hits {0}'s {2}, damaging some of the leaves on the right arm!" },
            { BodyPartController.Parts.LeftHand, "The force of the {1} hits {0}'s {2}, damaging some of the leaves on the left hand!" },
            { BodyPartController.Parts.RightHand, "The force of the {1} hits {0}'s {2}, damaging some of the leaves on the right hand!" },
            { BodyPartController.Parts.Chest, "The force of the {1} hits {0}'s {2}, damaging some of the leaves on the chest!" },
            { BodyPartController.Parts.Abdomin, "The force of the {1} hits {0}'s {2}, damaging some of the leaves on the belly!" },
            { BodyPartController.Parts.LeftLeg, "The force of the {1} hits {0}'s {2}, damaging some of the leaves on the left leg!" },
            { BodyPartController.Parts.RightLeg, "The force of the {1} hits {0}'s {2}, damaging some of the leaves on the right leg!" },
            { BodyPartController.Parts.LeftFoot, "The force of the {1} hits {0}'s {2}, damaging some of the leaves on the left foot!" },
            { BodyPartController.Parts.RightFoot, "The force of the {1} hits {0}'s {2}, damaging some of the leaves on the right foot!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //bad bruise
        {
            { BodyPartController.Parts.LeftArm, "The force of the {1} strikes {0}'s {2}, snapping some twigs in the left arm!" },
            { BodyPartController.Parts.RightArm, "The force of the {1} strikes {0}'s {2}, snapping some twigs in the right arm!" },
            { BodyPartController.Parts.LeftHand, "The force of the {1} strikes {0}'s {2}, snapping some twigs in the left hand!" },
            { BodyPartController.Parts.RightHand, "The force of the {1} strikes {0}'s {2}, snapping some twigs in the right hand!" },
            { BodyPartController.Parts.Chest, "The force of the {1} strikes {0}'s {2}, snapping some twigs in the chest!" },
            { BodyPartController.Parts.Abdomin, "The force of the {1} strikes {0}'s {2}, snapping some twigs in the belly!" },
            { BodyPartController.Parts.LeftLeg, "The force of the {1} strikes {0}'s {2}, snapping some twigs in the left leg!" },
            { BodyPartController.Parts.RightLeg, "The force of the {1} strikes {0}'s {2}, snapping some twigs in the right leg!" },
            { BodyPartController.Parts.LeftFoot, "The force of the {1} strikes {0}'s {2}, snapping some twigs in the left foot!" },
            { BodyPartController.Parts.RightFoot, "The force of the {1} strikes {0}'s {2}, snapping some twigs in the right foot!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //heavy bruise
        {
            { BodyPartController.Parts.LeftArm, "The force of the {1} slams into {0}'s {2}, smashing the leaves and twigs in the left arm!" },
            { BodyPartController.Parts.RightArm, "The force of the {1} slams into {0}'s {2}, smashing the leaves and twigs in the right arm!" },
            { BodyPartController.Parts.LeftHand, "The force of the {1} slams into {0}'s {2}, smashing the leaves and twigs in the left hand!" },
            { BodyPartController.Parts.RightHand, "The force of the {1} slams into {0}'s {2}, smashing the leaves and twigs in the right hand!" },
            { BodyPartController.Parts.Chest, "The force of the {1} slams into {0}'s {2}, smashing the leaves and twigs in the chest!" },
            { BodyPartController.Parts.Abdomin, "The force of the {1} slams into {0}'s {2}, smashing the leaves and twigs in the belly!" },
            { BodyPartController.Parts.LeftLeg, "The force of the {1} slams into {0}'s {2}, smashing the leaves and twigs in the left leg!" },
            { BodyPartController.Parts.RightLeg, "The force of the {1} slams into {0}'s {2}, smashing the leaves and twigs in the right leg!" },
            { BodyPartController.Parts.LeftFoot, "The force of the {1} slams into {0}'s {2}, smashing the leaves and twigs in the left foot!" },
            { BodyPartController.Parts.RightFoot, "The force of the {1} slams into {0}'s {2}, smashing the leaves and twigs in the right foot!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //pulverize meat
        {
            { BodyPartController.Parts.LeftArm, "The force of the {1} crumples {0}'s {2}, splintering the main branch of the left arm!" },
            { BodyPartController.Parts.RightArm, "The force of the {1} crumples {0}'s {2}, splintering the main branch of the right arm!" },
            { BodyPartController.Parts.LeftHand, "The force of the {1} crumples {0}'s {2}, splintering the main branch in the left hand!" },
            { BodyPartController.Parts.RightHand, "The force of the {1} crumples {0}'s {2}, splintering the main branch in the right hand!" },
            { BodyPartController.Parts.Chest, "The force of the {1} crumples {0}'s {2}, splintering the main branch in the chest!" },
            { BodyPartController.Parts.Abdomin, "The force of the {1} crumples {0}'s {2}, splintering the main branch in the belly!" },
            { BodyPartController.Parts.LeftLeg, "The force of the {1} crumples {0}'s {2}, splintering the main branch of the left leg!" },
            { BodyPartController.Parts.RightLeg, "The force of the {1} crumples {0}'s {2}, splintering the main branch of the right leg!" },
            { BodyPartController.Parts.LeftFoot, "The force of the {1} crumples {0}'s {2}, splintering the main branch in the left foot!" },
            { BodyPartController.Parts.RightFoot, "The force of the {1} crumples {0}'s {2}, splintering the main branch in the right foot!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //bone shattering crush
        {
            { BodyPartController.Parts.LeftArm, "The force of the {1} crushes {0}'s {2}, the impact shatters the main branch of the left arm!" },
            { BodyPartController.Parts.RightArm, "The force of the {1} crushes {0}'s {2}, the impact shatters the main branch of the right arm!" },
            { BodyPartController.Parts.LeftHand, "The force of the {1} crushes {0}'s {2}, shattering the main branch of the left hand!" },
            { BodyPartController.Parts.RightHand, "The force of the {1} crushes {0}'s {2}, shattering the main branch of the right hand!" },
            { BodyPartController.Parts.Chest, "The force of the {1} crushes {0}'s {2}, shattering main branch in the chest!" },
            { BodyPartController.Parts.Abdomin, "The force of the {1} crushes {0}'s {2}, shattering main branch in the belly!" },
            { BodyPartController.Parts.LeftLeg, "The force of the {1} crushes {0}'s {2}, the impact shatters the main branch of the left leg!" },
            { BodyPartController.Parts.RightLeg, "The force of the {1} crushes {0}'s {2}, the impact shatters the main branch of the right leg!" },
            { BodyPartController.Parts.LeftFoot, "The force of the {1} crushes {0}'s {2}, shattering the main branch in the left foot!" },
            { BodyPartController.Parts.RightFoot, "The force of the {1} crushes {0}'s {2}, shattering the main branch in the right foot!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //obliterates into mess
        {
            { BodyPartController.Parts.LeftArm, "The force of the {1} smashes completely through {0}'s {2}, the left arm has been rendered totally useless!" },
            { BodyPartController.Parts.RightArm, "The force of the {1} smashes completely through {0}'s {2}, the right arm has been rendered totally useless!" },
            { BodyPartController.Parts.LeftHand, "The force of the {1} smashes completely through {0}'s {2}, the left hand has been mangled beyond recognition!" },
            { BodyPartController.Parts.RightHand, "The force of the {1} smashes completely through {0}'s {2}, the right hand has been mangled beyond recognition!" },
            { BodyPartController.Parts.Chest, "The force of the {1} smashes completely through {0}'s {2}, obliterating the chest and destroying the trunk!" },
            { BodyPartController.Parts.Abdomin, "The force of the {1} smashes completely through {0}'s {2}, obliterating the belly and destroying the trunk!" },
            { BodyPartController.Parts.LeftLeg, "The force of the {1} smashes completely through {0}'s {2}, the left leg has been rendered totally useless!" },
            { BodyPartController.Parts.RightLeg, "The force of the {1} smashes completely through {0}'s {2}, the right leg has been rendered totally useless!" },
            { BodyPartController.Parts.LeftFoot, "The force of the {1} smashes completely through {0}'s {2}, the left foot has been mangled beyond recognition!" },
            { BodyPartController.Parts.RightFoot, "The force of the {1} smashes completely through {0}'s {2}, the right foot has been mangled beyond recognition!" },
        },
    };

    /*
    public static void DamageMessage(BodyPartController.DamageInfo damageInfo)
    {
        string armor = damageInfo.armorName;
        string weapon = damageInfo.weaponName;
        string damageType = damageInfo.attackType;
        string myName = damageInfo.victimName;
        BodyPartController.Parts bodypart = damageInfo.bodyPart;

        if (armor == null)
        {
            armor = "bare flesh";
        }
        if (weapon == null)
        {
            weapon = "bare hands";
        }

        Dictionary<BodyPartController.Parts, string> injuryList = new Dictionary<BodyPartController.Parts, string>();
        if (damageType == "Penetration")
        {
            injuryList = penetrationInjuries[damageInfo.severityID];
        }
        else
        {
            injuryList = impactInjuries[damageInfo.severityID];
        }

        Debug.Log("creating line for " + bodypart);
        string line = string.Format(injuryList[bodypart], myName, weapon, armor);
        BattleReport.AddToBattleReport(line);
    }
    */
}
