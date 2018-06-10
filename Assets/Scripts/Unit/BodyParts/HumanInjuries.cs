using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanInjuries : MonoBehaviour {

    /*
    {"Head", "Neck", "LeftArm", "RightArm",
    "LHand", "RHand", "Thorax", "Abdomin",
    "LeftLeg", "RightLeg", "LFoot", "RFoot" }*/

    private static Dictionary<int, string>[] penetrationInjuries = new Dictionary<int, string>[] {
        //PENETRATION
        new Dictionary<int, string>() //scratch
        {
            //I know these are almost all the same but I am leaving them as is now to keep my options open and to avoid confusion for how this is set up
            { 0, "The {1} ruptures {0}'s {2}, leaving a light scratch on the head!" },
            { 1, "The {1} ruptures {0}'s {2}, leaving a light scratch on the neck!"},
            { 2, "The {1} ruptures {0}'s {2}, leaving a light scratch on the left arm!" },
            { 3, "The {1} ruptures {0}'s {2}, leaving a light scratch on the right arm!" },
            { 4, "The {1} ruptures {0}'s {2}, leaving a light scratch on the left hand" },
            { 5, "The {1} ruptures {0}'s {2}, leaving a light scratch on the right hand!" },
            { 6, "The {1} ruptures {0}'s {2}, scratching the chest!" },
            { 7, "The {1} ruptures {0}'s {2}, scratching the belly!" },
            { 8, "The {1} ruptures {0}'s {2}, leaving a light scratch on the left leg!" },
            { 9, "The {1} ruptures {0}'s {2}, leaving a light scratch on the right leg!" },
            { 10, "The {1} ruptures {0}'s {2}, leaving a light scratch on the left foot!" },
            { 11, "The {1} ruptures {0}'s {2}, leaving a light scratch on the right foot!" },
        },
        new Dictionary<int, string>() //{1} slices
        {
            { 0, "The {1} pierces {0}'s {2}, slicing the flesh of the head!" },
            { 1, "The {1} pierces {0}'s {2}, slicing the flesh of the neck!"},
            { 2, "The {1} pierces {0}'s {2}, slicing the flesh of the left arm!" },
            { 3, "The {1} pierces {0}'s {2}, slicing the flesh of the right arm!" },
            { 4, "The {1} pierces {0}'s {2}, slicing the flesh of the left hand" },
            { 5, "The {1} pierces {0}'s {2}, slicing the flesh of the right hand!" },
            { 6, "The {1} pierces {0}'s {2}, slicing the flesh of thechest!" },
            { 7, "The {1} pierces {0}'s {2}, slicing the flesh of the belly!" },
            { 8, "The {1} pierces {0}'s {2}, slicing the flesh of the left leg!" },
            { 9, "The {1} pierces {0}'s {2}, slicing the flesh of the right leg!" },
            { 10, "The {1} pierces {0}'s {2}, slicing the flesh of the left foot!" },
            { 11, "The {1} pierces {0}'s {2}, slicing the flesh of the right foot!" },
        },
        new Dictionary<int, string>() //deep cut
        {
            { 0, "The {1} cuts through {0}'s {2} and hacks deep into the flesh of the head!" },
            { 1, "The {1} cuts through {0}'s {2} and hacks deep into the flesh of the neck!"},
            { 2, "The {1} cuts through {0}'s {2} and hacks deep into the flesh of the left arm!" },
            { 3, "The {1} cuts through {0}'s {2} and hacks deep into the flesh of the right arm!" },
            { 4, "The {1} cuts through {0}'s {2} and hacks deep into the flesh of the left hand" },
            { 5, "The {1} cuts through {0}'s {2} and hacks deep into the flesh of the right hand!" },
            { 6, "The {1} cuts through {0}'s {2} and hacks deep into the flesh of the chest!" },
            { 7, "The {1} cuts through {0}'s {2} and hacks deep into the flesh of the belly!" },
            { 8, "The {1} cuts through {0}'s {2} and hacks deep into the flesh of the left leg!" },
            { 9, "The {1} cuts through {0}'s {2} and hacks deep into the flesh of the right leg!" },
            { 10, "The {1} cuts through {0}'s {2} and hacks deep into the flesh of the left foot!" },
            { 11, "The {1} cuts through {0}'s {2} and hacks deep into the flesh of the right foot!" },
        },
        new Dictionary<int, string>() //cut to bone
        {
            { 0, "The {1} completely penetrates {0}'s {2}, cracking the skull!" },
            { 1, "The {1} completely penetrates the {2}, almost decapitating {0}!"},
            { 2, "The {1} completely penetrates {0}'s {2}, cutting down to the bone of the the left arm!" },
            { 3, "The {1} completely penetrates {0}'s {2}, cutting down to the bone of the the right arm!" },
            { 4, "The {1} completely penetrates {0}'s {2}, cutting down to the bones of the the left hand" },
            { 5, "The {1} completely penetrates {0}'s {2}, cutting down to the bones of the the right hand!" },
            { 6, "The {1} completely penetrates the {2} and cuts into {0}'s the ribs!" },
            { 7, "The {1} completely penetrates {0}'s {2}, and slices the belly wide open!" },
            { 8, "The {1} completely penetrates {0}'s {2}, cutting down to the bone of the the left leg!" },
            { 9, "The {1} completely penetrates {0}'s {2}, cutting down to the bone of the the right leg!" },
            { 10, "The {1} completely penetrates {0}'s {2}, cutting down to the bones of the the left foot!" },
            { 11, "The {1} completely penetrates {0}'s {2}, cutting down to the bones of the the right foot!" },
        },
        new Dictionary<int, string>() //shatters bone
        {
            { 0, "The {1} easily carves into {0}'s {2} and shatters the skull!" },
            { 1, "The {1} easily carves into the {0}'s {2}, hacking into the spine of the neck!"},
            { 2, "The {1} easily carves into {0}'s {2}, completely shatttering the bone of the the left arm!" },
            { 3, "The {1} easily carves into {0}'s {2}, completely shatttering the bone of the the right arm!" },
            { 4, "The {1} easily carves into {0}'s {2}, completely shatttering the bones of the the left hand" },
            { 5, "The {1} easily carves into {0}'s {2}, completely shatttering the bones of the the right hand!" },
            { 6, "The {1} easily carves into {0}'s {2} completely shatters the ribs!" },
            { 7, "The {1} easily carves into {0}'s {2}, scrambling his innards with the blade!" },
            { 8, "The {1} easily carves into {0}'s {2}, completely shatttering the bone of the the left leg!" },
            { 9, "The {1} easily carves into {0}'s {2}, completely shatttering the bone of the the right leg!" },
            { 10, "The {1} easily carves into {0}'s {2}, completely shatttering the bones of the the left foot!" },
            { 11, "The {1} easily carves into {0}'s {2}, completely shatttering the bones of the the right foot!" },
        },
        new Dictionary<int, string>() //sever
        {
            { 0, "The {1} effortlessly slices through {0}'s {2}, the blade embeds itself deep into the skull!" },
            { 1, "The {1} effortlessly slices through the {2}, {0}'s head flies clean off!"},
            { 2, "The {1} effortlessly slices through {0}'s {2}, severing the left arm!" },
            { 3, "The {1} effortlessly slices through {0}'s {2}, severing the right arm!" },
            { 4, "The {1} effortlessly slices through {0}'s {2}, severing the left hand" },
            { 5, "The {1} effortlessly slices through {0}'s {2}, severing the right hand!" },
            { 6, "The {1} effortlessly slices through the {2} and finds itself deeply ebedded in {0}'s chest!" },
            { 7, "The {1} effortlessly slices through {0}'s {2} and deep into the belly, {0}'s innards spill to the floor!" },
            { 8, "The {1} effortlessly slices through {0}'s {2}, severing the left leg!" },
            { 9, "The {1} effortlessly slices through {0}'s {2}, severing the right leg!" },
            { 10, "The {1} effortlessly slices through {0}'s {2}, severing the left foot!" },
            { 11, "The {1} effortlessly slices through {0}'s {2}, severing the right foot!" },
        }
    };

    private static Dictionary<int, string>[] impactInjuries = new Dictionary<int, string>[] {
        //IMPACT
        new Dictionary<int, string>() //light bruise
        {
            { 0, "The force of the {1} dents {0}'s {2}, leaving small bump on the head!" },
            { 1, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the neck!" },
            { 2, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the left arm!" },
            { 3, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the right arm!" },
            { 4, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the left hand!" },
            { 5, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the right hand!" },
            { 6, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the chest!" },
            { 7, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the belly!" },           
            { 8, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the left leg!" },
            { 9, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the right leg!" },
            { 10, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the left foot!" },
            { 11, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the right foot!" },
        },
        new Dictionary<int, string>() //bad bruise
        {
            { 0, "The force of the {1} dents {0}'s {2}, leaving large bump on the head!" },
            { 1, "The force of the {1} dents {0}'s {2}, bruising the neck!" },
            { 2, "The force of the {1} dents {0}'s {2}, bruising the left arm!" },
            { 3, "The force of the {1} dents {0}'s {2}, bruising the right arm!" },
            { 4, "The force of the {1} dents {0}'s {2}, bruising the left hand!" },
            { 5, "The force of the {1} dents {0}'s {2}, bruising the right hand!" },
            { 6, "The force of the {1} dents {0}'s {2}, bruising the chest!" },
            { 7, "The force of the {1} dents {0}'s {2}, brusing the belly!" },
            { 8, "The force of the {1} dents {0}'s {2}, bruising the left leg!" },
            { 9, "The force of the {1} dents {0}'s {2}, bruising the right leg!" },
            { 10, "The force of the {1} dents {0}'s {2}, bruising the left foot!" },
            { 11, "The force of the {1} dents {0}'s {2}, bruising the right foot!" },
        },
        new Dictionary<int, string>() //heavy bruise
        {
            { 0, "The force of the {1} slams into {0}'s {2}, rattling the brain!" },
            { 1, "The force of the {1} slams into {0}'s {2}, heavily bruising the neck!" },
            { 2, "The force of the {1} slams into {0}'s {2}, heavily bruising the left arm!" },
            { 3, "The force of the {1} slams into {0}'s {2}, heavily bruising the right arm!" },
            { 4, "The force of the {1} slams into {0}'s {2}, heavily bruising the left hand!" },
            { 5, "The force of the {1} slams into {0}'s {2}, heavily bruising the right hand!" },
            { 6, "The force of the {1} slams into {0}'s {2}, heavily bruising the chest!" },
            { 7, "The force of the {1} slams into {0}'s {2}, heavily bruising the belly!" },
            { 8, "The force of the {1} slams into {0}'s {2}, heavily bruising the left leg!" },
            { 9, "The force of the {1} slams into {0}'s {2}, heavily bruising the right leg!" },
            { 10, "The force of the {1} slams into {0}'s {2}, heavily bruising the left foot!" },
            { 11, "The force of the {1} slams into {0}'s {2}, heavily bruising the right foot!" },
        },
        new Dictionary<int, string>() //pulverize meat
        {
            { 0, "The force of the {1} crumples {0}'s {2}, cracking the skull!" },
            { 1, "The force of the {1} crumples {0}'s {2} and pulverizes the meat of the neck!" },
            { 2, "The force of the {1} crumples {0}'s {2}, pulverizing the meat of the left arm!" },
            { 3, "The force of the {1} crumples {0}'s {2}, pulverizing the meat of the right arm!" },
            { 4, "The force of the {1} crumples {0}'s {2}, cracking the bones in the left hand!" },
            { 5, "The force of the {1} crumples {0}'s {2}, cracking the bones in the right hand!" },
            { 6, "The force of the {1} crumples {0}'s {2}, cracking the ribs!" },
            { 7, "The force of the {1} crumples {0}'s {2}, brusing the guts!" },
            { 8, "The force of the {1} crumples {0}'s {2}, pulverizing the meat of the left leg!" },
            { 9, "The force of the {1} crumples {0}'s {2}, pulverizing the meat of the right leg!" },
            { 10, "The force of the {1} crumples {0}'s {2}, cracking the bones in the left foot!" },
            { 11, "The force of the {1} crumples {0}'s {2}, cracking the bones in the right foot!" },
        },
        new Dictionary<int, string>() //bone shattering crush
        {
            { 0, "The force of the {1} crushes {0}'s {2}, caving in the head!" },
            { 1, "The force of the {1} crushes {0}'s {2} a!ong with the windpipe!" },
            { 2, "The force of the {1} crushes {0}'s {2}, the impact shatters the bones of the left arm!" },
            { 3, "The force of the {1} crushes {0}'s {2}, the impact shatters the bones of the right arm!" },
            { 4, "The force of the {1} crushes {0}'s {2}, shattering the bones of the left hand!" },
            { 5, "The force of the {1} crushes {0}'s {2}, shattering the bones of the right hand!" },
            { 6, "The force of the {1} crushes {0}'s {2}, shattering the ribs and puncturing the lungs!" },
            { 7, "The force of the {1} crushes {0}'s {2}, damaging the innards!" },
            { 8, "The force of the {1} crushes {0}'s {2}, the impact shatters the bones of the left leg!" },
            { 9, "The force of the {1} crushes {0}'s {2}, the impact shatters the bones of the right leg!" },
            { 10, "The force of the {1} crushes {0}'s {2}, shattering the bones of the left foot!" },
            { 11, "The force of the {1} crushes {0}'s {2}, shattering the bones of the right foot!" },
        },
        new Dictionary<int, string>() //obliterates into mess
        {
            { 0, "The force of the {1} smashes completely through {0}'s {2}, obliterating the head!" },
            { 1, "The force of the {1} smashes completely through {0}'s {2}, partially decapitating the head!" },
            { 2, "The force of the {1} smashes completely through {0}'s {2}, the left arm has been rendered totally useless!" },
            { 3, "The force of the {1} smashes completely through {0}'s {2}, the right arm has been rendered totally useless!" },
            { 4, "The force of the {1} smashes completely through {0}'s {2}, the left hand has been mangled beyond recognition!" },
            { 5, "The force of the {1} smashes completely through {0}'s {2}, the right hand has been mangled beyond recognition!" },
            { 6, "The force of the {1} smashes completely through {0}'s {2}, obliterating the chest and exploding the heart!" },
            { 7, "The force of the {1} smashes completely through {0}'s {2}, pulverizing the innards!" },
            { 8, "The force of the {1} smashes completely through {0}'s {2}, the left leg has been rendered totally useless!" },
            { 9, "The force of the {1} smashes completely through {0}'s {2}, the right leg has been rendered totally useless!" },
            { 10, "The force of the {1} smashes completely through {0}'s {2}, the left foot has been mangled beyond recognition!" },
            { 11, "The force of the {1} smashes completely through {0}'s {2}, the right foot has been mangled beyond recognition!" },
        },
    };


    public static void DamageMessage(HumanoidBody.DamageInfo damageInfo)
    {
        string armor = damageInfo.armorName;
        string weapon = damageInfo.weaponName;
        string damageType = damageInfo.attackType;
        string myName = damageInfo.victimName;
        int bodypart = damageInfo.bodyPartID;

        if (armor == null)
        {
            armor = "bare flesh";
        }
        if(weapon == null)
        {
            weapon = "bare hands";
        }

        Dictionary<int, string> injuryList = new Dictionary<int, string>();
        if(damageType == "Penetration")
        {
            injuryList = penetrationInjuries[damageInfo.severityID]; //SEVERITY HARDCODED AT LEVEL 0
        }
        else
        {
            injuryList = impactInjuries[damageInfo.severityID];
        }
        
        Debug.Log(string.Format(injuryList[bodypart], myName, weapon, armor));
    }
}
