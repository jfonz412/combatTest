using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushBansheeInjuries : MonoBehaviour {

    /*
      {"LeftArm", "RightArm",
      "LHand", "RHand", "Thorax", "Abdomin",
      "LeftLeg", "RightLeg", "LeftFoot", "RightFoot" }
      */

    private static Dictionary<BodyParts.Parts, string>[] penetrationInjuries = new Dictionary<BodyParts.Parts, string>[] {
        //PENETRATION
        new Dictionary<BodyParts.Parts, string>() //scratch
        {
            { BodyParts.Parts.LeftArm, "The {1} hacks at {0}'s {2}, shaking loose some leaves from the left arm!" },
            { BodyParts.Parts.RightArm, "The {1} hacks at {0}'s {2}, shaking loose some leaves from the right arm!" },
            { BodyParts.Parts.LeftHand, "The {1} hacks at {0}'s {2}, shaking loose some leaves from the left hand" },
            { BodyParts.Parts.RightHand, "The {1} hacks at {0}'s {2}, shaking loose some leaves from the right hand!" },
            { BodyParts.Parts.Chest, "The {1} hacks at {0}'s {2}, shaking loose some leaves from the chest!" },
            { BodyParts.Parts.Abdomin, "The {1} hacks at {0}'s {2}, shaking loose some leaves from the belly!" },
            { BodyParts.Parts.LeftLeg, "The {1} hacks at {0}'s {2}, shaking loose some leaves from the left leg!" },
            { BodyParts.Parts.RightLeg, "The {1} hacks at {0}'s {2}, shaking loose some leaves from the right leg!" },
            { BodyParts.Parts.LeftFoot, "The {1} hacks at {0}'s {2}, shaking loose some leaves from the left foot!" },
            { BodyParts.Parts.RightFoot, "The {1} hacks at {0}'s {2}, shaking loose some leaves from the right foot!" },
        },
        new Dictionary<BodyParts.Parts, string>() //{1} slices
        {
            { BodyParts.Parts.LeftArm, "The {1} hacks into {0}'s {2}, leaves fall and twigs snap in the left arm!" },
            { BodyParts.Parts.RightArm, "The {1} hacks into {0}'s {2}, leaves fall and twigs snap in the right arm!" },
            { BodyParts.Parts.LeftHand, "The {1} hacks into {0}'s {2}, leaves fall and twigs snap in the left hand" },
            { BodyParts.Parts.RightHand, "The {1} hacks into {0}'s {2}, leaves fall and twigs snap in the right hand!" },
            { BodyParts.Parts.Chest, "The {1} hacks into {0}'s {2}, leaves fall and twigs snap in the chest!" },
            { BodyParts.Parts.Abdomin, "The {1} hacks into {0}'s {2}, leaves fall and twigs snap in the belly!" },
            { BodyParts.Parts.LeftLeg, "The {1} hacks into {0}'s {2}, leaves fall and twigs snap in the left leg!" },
            { BodyParts.Parts.RightLeg, "The {1} hacks into {0}'s {2}, leaves fall and twigs snap in the right leg!" },
            { BodyParts.Parts.LeftFoot, "The {1} hacks into {0}'s {2}, leaves fall and twigs snap in the left foot!" },
            { BodyParts.Parts.RightFoot, "The {1} hacks into {0}'s {2}, leaves fall and twigs snap in the right foot!" },
        },
        new Dictionary<BodyParts.Parts, string>() //deep cut
        {
            { BodyParts.Parts.LeftArm, "The {1} cuts through {0}'s {2} chipping main branch inside left arm!" },
            { BodyParts.Parts.RightArm, "The {1} cuts through {0}'s {2} chipping main branch inside right arm!" },
            { BodyParts.Parts.LeftHand, "The {1} cuts through {0}'s {2} chipping main branch inside left hand" },
            { BodyParts.Parts.RightHand, "The {1} cuts through {0}'s {2} chipping main branch inside right hand!" },
            { BodyParts.Parts.Chest, "The {1} cuts through {0}'s {2} chipping main branch inside chest!" },
            { BodyParts.Parts.Abdomin, "The {1} cuts through {0}'s {2} chipping main branch inside belly!" },
            { BodyParts.Parts.LeftLeg, "The {1} cuts through {0}'s {2} chipping main branch inside left leg!" },
            { BodyParts.Parts.RightLeg, "The {1} cuts through {0}'s {2} chipping main branch inside right leg!" },
            { BodyParts.Parts.LeftFoot, "The {1} cuts through {0}'s {2} chipping main branch inside left foot!" },
            { BodyParts.Parts.RightFoot, "The {1} cuts through {0}'s {2} chipping main branch inside right foot!" },
        },
        new Dictionary<BodyParts.Parts, string>() //cut to bone
        {
            { BodyParts.Parts.LeftArm, "The {1} completely hacks through {0}'s {2}, cutting into the main branch of the left arm!" },
            { BodyParts.Parts.RightArm, "The {1} completely hacks through {0}'s {2}, cutting into the main branch of the right arm!" },
            { BodyParts.Parts.LeftHand, "The {1} completely hacks through {0}'s {2}, slicing off some tendrils on the left hand" },
            { BodyParts.Parts.RightHand, "The {1} completely hacks through {0}'s {2}, slicing off some tendrils on the right hand!" },
            { BodyParts.Parts.Chest, "The {1} completely hacks through the {2} and cuts into {0}'s trunk!" },
            { BodyParts.Parts.Abdomin, "The {1} completely hacks through the {2} and cuts into {0}'s trunk!" },
            { BodyParts.Parts.LeftLeg, "The {1} completely hacks through {0}'s {2}, cutting into the main branch of the left leg!" },
            { BodyParts.Parts.RightLeg, "The {1} completely hacks through {0}'s {2}, cutting into the main branch of the right leg!" },
            { BodyParts.Parts.LeftFoot, "The {1} completely hacks through {0}'s {2}, slicing off some tendrils on the left foot!" },
            { BodyParts.Parts.RightFoot, "The {1} completely hacks through {0}'s {2}, slicing off some tendrils on the right foot!" },
        },
        new Dictionary<BodyParts.Parts, string>() //shatters bone
        {
            { BodyParts.Parts.LeftArm, "The {1} easily cuts through {0}'s {2}, hacking deep into the main branch of the left arm!" },
            { BodyParts.Parts.RightArm, "The {1} easily cuts through {0}'s {2}, hacking deep into the main branch of the right arm!" },
            { BodyParts.Parts.LeftHand, "The {1} easily cuts through {0}'s {2}, lobbing off most of the tendrils from the left hand" },
            { BodyParts.Parts.RightHand, "The {1} easily cuts through {0}'s {2}, lobbing off most of the tendrils from the right hand!" },
            { BodyParts.Parts.Chest, "The {1} easily cuts through {0}'s {2}, hacking deep into the main trunk!" },
            { BodyParts.Parts.Abdomin, "The {1} easily cuts through {0}'s {2}, hacking deep into the main trunk!!" },
            { BodyParts.Parts.LeftLeg, "The {1} easily cuts through {0}'s {2}, hacking deep into the main branch of the left leg!" },
            { BodyParts.Parts.RightLeg, "The {1} easily cuts through {0}'s {2}, hacking deep into the main branch of the right leg!" },
            { BodyParts.Parts.LeftFoot, "The {1} easily cuts through {0}'s {2}, lobbing off most of the tendrils from the left foot!" },
            { BodyParts.Parts.RightFoot, "The {1} easily cuts through {0}'s {2}, lobbing off most of the tendrils fromthe right foot!" },
        },
        new Dictionary<BodyParts.Parts, string>() //sever
        {
            { BodyParts.Parts.LeftArm, "The {1} effortlessly slices through {0}'s {2}, severing the left arm!" },
            { BodyParts.Parts.RightArm, "The {1} effortlessly slices through {0}'s {2}, severing the right arm!" },
            { BodyParts.Parts.LeftHand, "The {1} effortlessly slices through {0}'s {2}, severing the left hand" },
            { BodyParts.Parts.RightHand, "The {1} effortlessly slices through {0}'s {2}, severing the right hand!" },
            { BodyParts.Parts.Chest, "The {1} effortlessly slices through the {2} and finds itself deeply ebedded in {0}'s chest!" },
            { BodyParts.Parts.Abdomin, "The {1} effortlessly slices through {0}'s {2} and deep into the belly of the beast!" },
            { BodyParts.Parts.LeftLeg, "The {1} effortlessly slices through {0}'s {2}, severing the left leg!" },
            { BodyParts.Parts.RightLeg, "The {1} effortlessly slices through {0}'s {2}, severing the right leg!" },
            { BodyParts.Parts.LeftFoot, "The {1} effortlessly slices through {0}'s {2}, severing the left foot!" },
            { BodyParts.Parts.RightFoot, "The {1} effortlessly slices through {0}'s {2}, severing the right foot!" },
        }
    };

    private static Dictionary<BodyParts.Parts, string>[] impactInjuries = new Dictionary<BodyParts.Parts, string>[] {
        //IMPACT
        new Dictionary<BodyParts.Parts, string>() //light bruise
        {
            { BodyParts.Parts.LeftArm, "The force of the {1} hits {0}'s {2}, damaging some of the leaves on the left arm!" },
            { BodyParts.Parts.RightArm, "The force of the {1} hits {0}'s {2}, damaging some of the leaves on the right arm!" },
            { BodyParts.Parts.LeftHand, "The force of the {1} hits {0}'s {2}, damaging some of the leaves on the left hand!" },
            { BodyParts.Parts.RightHand, "The force of the {1} hits {0}'s {2}, damaging some of the leaves on the right hand!" },
            { BodyParts.Parts.Chest, "The force of the {1} hits {0}'s {2}, damaging some of the leaves on the chest!" },
            { BodyParts.Parts.Abdomin, "The force of the {1} hits {0}'s {2}, damaging some of the leaves on the belly!" },
            { BodyParts.Parts.LeftLeg, "The force of the {1} hits {0}'s {2}, damaging some of the leaves on the left leg!" },
            { BodyParts.Parts.RightLeg, "The force of the {1} hits {0}'s {2}, damaging some of the leaves on the right leg!" },
            { BodyParts.Parts.LeftFoot, "The force of the {1} hits {0}'s {2}, damaging some of the leaves on the left foot!" },
            { BodyParts.Parts.RightFoot, "The force of the {1} hits {0}'s {2}, damaging some of the leaves on the right foot!" },
        },
        new Dictionary<BodyParts.Parts, string>() //bad bruise
        {
            { BodyParts.Parts.LeftArm, "The force of the {1} strikes {0}'s {2}, snapping some twigs in the left arm!" },
            { BodyParts.Parts.RightArm, "The force of the {1} strikes {0}'s {2}, snapping some twigs in the right arm!" },
            { BodyParts.Parts.LeftHand, "The force of the {1} strikes {0}'s {2}, snapping some twigs in the left hand!" },
            { BodyParts.Parts.RightHand, "The force of the {1} strikes {0}'s {2}, snapping some twigs in the right hand!" },
            { BodyParts.Parts.Chest, "The force of the {1} strikes {0}'s {2}, snapping some twigs in the chest!" },
            { BodyParts.Parts.Abdomin, "The force of the {1} strikes {0}'s {2}, snapping some twigs in the belly!" },
            { BodyParts.Parts.LeftLeg, "The force of the {1} strikes {0}'s {2}, snapping some twigs in the left leg!" },
            { BodyParts.Parts.RightLeg, "The force of the {1} strikes {0}'s {2}, snapping some twigs in the right leg!" },
            { BodyParts.Parts.LeftFoot, "The force of the {1} strikes {0}'s {2}, snapping some twigs in the left foot!" },
            { BodyParts.Parts.RightFoot, "The force of the {1} strikes {0}'s {2}, snapping some twigs in the right foot!" },
        },
        new Dictionary<BodyParts.Parts, string>() //heavy bruise
        {
            { BodyParts.Parts.LeftArm, "The force of the {1} slams into {0}'s {2}, smashing the leaves and twigs in the left arm!" },
            { BodyParts.Parts.RightArm, "The force of the {1} slams into {0}'s {2}, smashing the leaves and twigs in the right arm!" },
            { BodyParts.Parts.LeftHand, "The force of the {1} slams into {0}'s {2}, smashing the leaves and twigs in the left hand!" },
            { BodyParts.Parts.RightHand, "The force of the {1} slams into {0}'s {2}, smashing the leaves and twigs in the right hand!" },
            { BodyParts.Parts.Chest, "The force of the {1} slams into {0}'s {2}, smashing the leaves and twigs in the chest!" },
            { BodyParts.Parts.Abdomin, "The force of the {1} slams into {0}'s {2}, smashing the leaves and twigs in the belly!" },
            { BodyParts.Parts.LeftLeg, "The force of the {1} slams into {0}'s {2}, smashing the leaves and twigs in the left leg!" },
            { BodyParts.Parts.RightLeg, "The force of the {1} slams into {0}'s {2}, smashing the leaves and twigs in the right leg!" },
            { BodyParts.Parts.LeftFoot, "The force of the {1} slams into {0}'s {2}, smashing the leaves and twigs in the left foot!" },
            { BodyParts.Parts.RightFoot, "The force of the {1} slams into {0}'s {2}, smashing the leaves and twigs in the right foot!" },
        },
        new Dictionary<BodyParts.Parts, string>() //pulverize meat
        {
            { BodyParts.Parts.LeftArm, "The force of the {1} crumples {0}'s {2}, splintering the main branch of the left arm!" },
            { BodyParts.Parts.RightArm, "The force of the {1} crumples {0}'s {2}, splintering the main branch of the right arm!" },
            { BodyParts.Parts.LeftHand, "The force of the {1} crumples {0}'s {2}, splintering the main branch in the left hand!" },
            { BodyParts.Parts.RightHand, "The force of the {1} crumples {0}'s {2}, splintering the main branch in the right hand!" },
            { BodyParts.Parts.Chest, "The force of the {1} crumples {0}'s {2}, splintering the main branch in the chest!" },
            { BodyParts.Parts.Abdomin, "The force of the {1} crumples {0}'s {2}, splintering the main branch in the belly!" },
            { BodyParts.Parts.LeftLeg, "The force of the {1} crumples {0}'s {2}, splintering the main branch of the left leg!" },
            { BodyParts.Parts.RightLeg, "The force of the {1} crumples {0}'s {2}, splintering the main branch of the right leg!" },
            { BodyParts.Parts.LeftFoot, "The force of the {1} crumples {0}'s {2}, splintering the main branch in the left foot!" },
            { BodyParts.Parts.RightFoot, "The force of the {1} crumples {0}'s {2}, splintering the main branch in the right foot!" },
        },
        new Dictionary<BodyParts.Parts, string>() //bone shattering crush
        {
            { BodyParts.Parts.LeftArm, "The force of the {1} crushes {0}'s {2}, the impact shatters the main branch of the left arm!" },
            { BodyParts.Parts.RightArm, "The force of the {1} crushes {0}'s {2}, the impact shatters the main branch of the right arm!" },
            { BodyParts.Parts.LeftHand, "The force of the {1} crushes {0}'s {2}, shattering the main branch of the left hand!" },
            { BodyParts.Parts.RightHand, "The force of the {1} crushes {0}'s {2}, shattering the main branch of the right hand!" },
            { BodyParts.Parts.Chest, "The force of the {1} crushes {0}'s {2}, shattering main branch in the chest!" },
            { BodyParts.Parts.Abdomin, "The force of the {1} crushes {0}'s {2}, shattering main branch in the belly!" },
            { BodyParts.Parts.LeftLeg, "The force of the {1} crushes {0}'s {2}, the impact shatters the main branch of the left leg!" },
            { BodyParts.Parts.RightLeg, "The force of the {1} crushes {0}'s {2}, the impact shatters the main branch of the right leg!" },
            { BodyParts.Parts.LeftFoot, "The force of the {1} crushes {0}'s {2}, shattering the main branch in the left foot!" },
            { BodyParts.Parts.RightFoot, "The force of the {1} crushes {0}'s {2}, shattering the main branch in the right foot!" },
        },
        new Dictionary<BodyParts.Parts, string>() //obliterates into mess
        {
            { BodyParts.Parts.LeftArm, "The force of the {1} smashes completely through {0}'s {2}, the left arm has been rendered totally useless!" },
            { BodyParts.Parts.RightArm, "The force of the {1} smashes completely through {0}'s {2}, the right arm has been rendered totally useless!" },
            { BodyParts.Parts.LeftHand, "The force of the {1} smashes completely through {0}'s {2}, the left hand has been mangled beyond recognition!" },
            { BodyParts.Parts.RightHand, "The force of the {1} smashes completely through {0}'s {2}, the right hand has been mangled beyond recognition!" },
            { BodyParts.Parts.Chest, "The force of the {1} smashes completely through {0}'s {2}, obliterating the chest and destroying the trunk!" },
            { BodyParts.Parts.Abdomin, "The force of the {1} smashes completely through {0}'s {2}, obliterating the belly and destroying the trunk!" },
            { BodyParts.Parts.LeftLeg, "The force of the {1} smashes completely through {0}'s {2}, the left leg has been rendered totally useless!" },
            { BodyParts.Parts.RightLeg, "The force of the {1} smashes completely through {0}'s {2}, the right leg has been rendered totally useless!" },
            { BodyParts.Parts.LeftFoot, "The force of the {1} smashes completely through {0}'s {2}, the left foot has been mangled beyond recognition!" },
            { BodyParts.Parts.RightFoot, "The force of the {1} smashes completely through {0}'s {2}, the right foot has been mangled beyond recognition!" },
        },
    };


    public static void DamageMessage(BodyParts.DamageInfo damageInfo)
    {
        string armor = damageInfo.armorName;
        string weapon = damageInfo.weaponName;
        string damageType = damageInfo.attackType;
        string myName = damageInfo.victimName;
        BodyParts.Parts bodypart = damageInfo.bodyPart;

        if (armor == null)
        {
            armor = "bare flesh";
        }
        if (weapon == null)
        {
            weapon = "bare hands";
        }

        Dictionary<BodyParts.Parts, string> injuryList = new Dictionary<BodyParts.Parts, string>();
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
}
