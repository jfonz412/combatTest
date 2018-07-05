using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanInjuries : MonoBehaviour {

    /*
        {"Head", "Neck", "LeftArm", "RightArm",
        "LHand", "RHand", "Thorax", "Abdomin",
        "LeftLeg", "RightLeg", "LFoot", "RFoot" }
    */
    
    //should make this inheritable for all injury lists??? this is the same as BushBansheeInjuries
    public static Dictionary<BodyParts.Parts, string> GetInjuryList(Injuries.DamageType damageType, int severityID)
    {
        if(damageType == Injuries.DamageType.Impact)
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
            return new Dictionary<BodyParts.Parts, string>();
        }
    }

    private static Dictionary<BodyParts.Parts, string>[] penetrationInjuries = new Dictionary<BodyParts.Parts, string>[] {
        //PENETRATION
        new Dictionary<BodyParts.Parts, string>() //scratch
        {
            //I know these are almost all the same but I am leaving them as is now to keep my options open and to avoid confusion for how this is set up
            {BodyParts.Parts.Head, "The {1} ruptures {0}'s {2}, leaving a light scratch on the head!" },
            {BodyParts.Parts.Neck, "The {1} ruptures {0}'s {2}, leaving a light scratch on the neck!"},
            {BodyParts.Parts.LeftArm, "The {1} ruptures {0}'s {2}, leaving a light scratch on the left arm!" },
            {BodyParts.Parts.RightArm, "The {1} ruptures {0}'s {2}, leaving a light scratch on the right arm!" },
            {BodyParts.Parts.LeftHand, "The {1} ruptures {0}'s {2}, leaving a light scratch on the left hand" },
            {BodyParts.Parts.RightHand, "The {1} ruptures {0}'s {2}, leaving a light scratch on the right hand!" },
            {BodyParts.Parts.Chest, "The {1} ruptures {0}'s {2}, scratching the chest!" },
            {BodyParts.Parts.Abdomin, "The {1} ruptures {0}'s {2}, scratching the belly!" },
            {BodyParts.Parts.LeftLeg, "The {1} ruptures {0}'s {2}, leaving a light scratch on the left leg!" },
            {BodyParts.Parts.RightLeg, "The {1} ruptures {0}'s {2}, leaving a light scratch on the right leg!" },
            {BodyParts.Parts.LeftFoot, "The {1} ruptures {0}'s {2}, leaving a light scratch on the left foot!" },
            {BodyParts.Parts.RightFoot, "The {1} ruptures {0}'s {2}, leaving a light scratch on the right foot!" },
        },
        new Dictionary<BodyParts.Parts, string>() //{1} slices
        {
            {BodyParts.Parts.Head, "The {1} pierces {0}'s {2}, slicing the skin of the head!" },
            {BodyParts.Parts.Neck, "The {1} pierces {0}'s {2}, slicing the flesh of the neck!"},
            {BodyParts.Parts.LeftArm, "The {1} pierces {0}'s {2}, slicing the flesh of the left arm!" },
            {BodyParts.Parts.RightArm, "The {1} pierces {0}'s {2}, slicing the flesh of the right arm!" },
            {BodyParts.Parts.LeftHand, "The {1} pierces {0}'s {2}, slicing the flesh of the left hand" },
            {BodyParts.Parts.RightHand, "The {1} pierces {0}'s {2}, slicing the flesh of the right hand!" },
            {BodyParts.Parts.Chest, "The {1} pierces {0}'s {2}, slicing the flesh of the chest!" },
            {BodyParts.Parts.Abdomin, "The {1} pierces {0}'s {2}, slicing the flesh of the belly!" },
            {BodyParts.Parts.LeftLeg, "The {1} pierces {0}'s {2}, slicing the flesh of the left leg!" },
            {BodyParts.Parts.RightLeg, "The {1} pierces {0}'s {2}, slicing the flesh of the right leg!" },
            {BodyParts.Parts.LeftFoot, "The {1} pierces {0}'s {2}, slicing the flesh of the left foot!" },
            {BodyParts.Parts.RightFoot, "The {1} pierces {0}'s {2}, slicing the flesh of the right foot!" },
        },
        new Dictionary<BodyParts.Parts, string>() //deep cut
        {
            {BodyParts.Parts.Head, "The {1} cuts through {0}'s {2} and slams against the skull!" },
            {BodyParts.Parts.Neck, "The {1} cuts through {0}'s {2} and hacks deep into the neck!"},
            {BodyParts.Parts.LeftArm, "The {1} cuts through {0}'s {2} and hacks deep into the left arm!" },
            {BodyParts.Parts.RightArm, "The {1} cuts through {0}'s {2} and hacks deep into the right arm!" },
            {BodyParts.Parts.LeftHand, "The {1} cuts through {0}'s {2} and hacks deep into the left hand" },
            {BodyParts.Parts.RightHand, "The {1} cuts through {0}'s {2} and hacks deep into the right hand!" },
            {BodyParts.Parts.Chest, "The {1} cuts through {0}'s {2} and cuts into {0}'s ribs!" },
            {BodyParts.Parts.Abdomin, "The {1} cuts through {0}'s {2} and hacks deep into the flesh of the belly!" },
            {BodyParts.Parts.LeftLeg, "The {1} cuts through {0}'s {2} and hacks deep into the left leg!" },
            {BodyParts.Parts.RightLeg, "The {1} cuts through {0}'s {2} and hacks deep into the right leg!" },
            {BodyParts.Parts.LeftFoot, "The {1} cuts through {0}'s {2} and hacks deep into the left foot!" },
            {BodyParts.Parts.RightFoot, "The {1} cuts through {0}'s {2} and hacks deep into the right foot!" },
        },
        new Dictionary<BodyParts.Parts, string>() //cut to bone
        {
            {BodyParts.Parts.Head, "The {1} completely penetrates {0}'s {2}, cracking the skull!" },
            {BodyParts.Parts.Neck, "The {1} completely penetrates the {2}, hacking nearly to the spine of the neck!"},
            {BodyParts.Parts.LeftArm, "The {1} completely penetrates {0}'s {2}, cutting down to the bone of the the left arm!" },
            {BodyParts.Parts.RightArm, "The {1} completely penetrates {0}'s {2}, cutting down to the bone of the the right arm!" },
            {BodyParts.Parts.LeftHand, "The {1} completely penetrates {0}'s {2}, cutting down to the bones of the the left hand" },
            {BodyParts.Parts.RightHand, "The {1} completely penetrates {0}'s {2}, cutting down to the bones of the the right hand!" },
            {BodyParts.Parts.Chest, "The {1} completely penetrates {0}'s {2} and cracks the ribs!" },
            {BodyParts.Parts.Abdomin, "The {1} completely penetrates {0}'s {2}, and slices the belly wide open!" },
            {BodyParts.Parts.LeftLeg, "The {1} completely penetrates {0}'s {2}, cutting down to the bone of the the left leg!" },
            {BodyParts.Parts.RightLeg, "The {1} completely penetrates {0}'s {2}, cutting down to the bone of the the right leg!" },
            {BodyParts.Parts.LeftFoot, "The {1} completely penetrates {0}'s {2}, cutting down to the bones of the the left foot!" },
            {BodyParts.Parts.RightFoot, "The {1} completely penetrates {0}'s {2}, cutting down to the bones of the the right foot!" },
        },
        new Dictionary<BodyParts.Parts, string>() //shatters bone
        {
            {BodyParts.Parts.Head, "The {1} easily carves into {0}'s {2} and shatters the skull!" },
            {BodyParts.Parts.Neck, "The {1} easily carves into the {0}'s {2}, almost decapitating {0}!"},
            {BodyParts.Parts.LeftArm, "The {1} easily carves into {0}'s {2}, completely shatttering the bone of the the left arm!" },
            {BodyParts.Parts.RightArm, "The {1} easily carves into {0}'s {2}, completely shatttering the bone of the the right arm!" },
            {BodyParts.Parts.LeftHand, "The {1} easily carves into {0}'s {2}, completely shatttering the bones of the the left hand" },
            {BodyParts.Parts.RightHand, "The {1} easily carves into {0}'s {2}, completely shatttering the bones of the the right hand!" },
            {BodyParts.Parts.Chest, "The {1} easily carves into {0}'s {2} and completely shatters the ribs!" },
            {BodyParts.Parts.Abdomin, "The {1} easily carves into {0}'s {2}, scrambling his innards with the blade!" },
            {BodyParts.Parts.LeftLeg, "The {1} easily carves into {0}'s {2}, completely shatttering the bone of the the left leg!" },
            {BodyParts.Parts.RightLeg, "The {1} easily carves into {0}'s {2}, completely shatttering the bone of the the right leg!" },
            {BodyParts.Parts.LeftFoot, "The {1} easily carves into {0}'s {2}, completely shatttering the bones of the the left foot!" },
            {BodyParts.Parts.RightFoot, "The {1} easily carves into {0}'s {2}, completely shatttering the bones of the the right foot!" },
        },
        new Dictionary<BodyParts.Parts, string>() //sever
        {
            {BodyParts.Parts.Head, "The {1} effortlessly slices through {0}'s {2}, the blade embeds itself deep into the skull!" },
            {BodyParts.Parts.Neck, "The {1} effortlessly slices through the {2}, {0}'s head flies clean off!"},
            {BodyParts.Parts.LeftArm, "The {1} effortlessly slices through {0}'s {2}, severing the left arm!" },
            {BodyParts.Parts.RightArm, "The {1} effortlessly slices through {0}'s {2}, severing the right arm!" },
            {BodyParts.Parts.LeftHand, "The {1} effortlessly slices through {0}'s {2}, severing the left hand" },
            {BodyParts.Parts.RightHand, "The {1} effortlessly slices through {0}'s {2}, severing the right hand!" },
            {BodyParts.Parts.Chest, "The {1} effortlessly slices through the {2} and finds itself deeply ebedded in {0}'s chest!" },
            {BodyParts.Parts.Abdomin, "The {1} effortlessly slices through {0}'s {2} and deep into the belly, {0}'s innards spill to the floor!" },
            {BodyParts.Parts.LeftLeg, "The {1} effortlessly slices through {0}'s {2}, severing the left leg!" },
            {BodyParts.Parts.RightLeg, "The {1} effortlessly slices through {0}'s {2}, severing the right leg!" },
            {BodyParts.Parts.LeftFoot, "The {1} effortlessly slices through {0}'s {2}, severing the left foot!" },
            {BodyParts.Parts.RightFoot, "The {1} effortlessly slices through {0}'s {2}, severing the right foot!" },
        }
    };

    private static Dictionary<BodyParts.Parts, string>[] impactInjuries = new Dictionary<BodyParts.Parts, string>[] {
        //IMPACT
        new Dictionary<BodyParts.Parts, string>() //light bruise
        {
            {BodyParts.Parts.Head, "The force of the {1} dents {0}'s {2}, leaving small bump on the head!" },
            {BodyParts.Parts.Neck, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the neck!" },
            {BodyParts.Parts.LeftArm, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the left arm!" },
            {BodyParts.Parts.RightArm, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the right arm!" },
            {BodyParts.Parts.LeftHand, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the left hand!" },
            {BodyParts.Parts.RightHand, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the right hand!" },
            {BodyParts.Parts.Chest, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the chest!" },
            {BodyParts.Parts.Abdomin, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the belly!" },           
            {BodyParts.Parts.LeftLeg, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the left leg!" },
            {BodyParts.Parts.RightLeg, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the right leg!" },
            {BodyParts.Parts.LeftFoot, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the left foot!" },
            {BodyParts.Parts.RightFoot, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the right foot!" },
        },
        new Dictionary<BodyParts.Parts, string>() //bad bruise
        {
            {BodyParts.Parts.Head, "The force of the {1} dents {0}'s {2}, leaving large bump on the head!" },
            {BodyParts.Parts.Neck, "The force of the {1} dents {0}'s {2}, bruising the neck!" },
            {BodyParts.Parts.LeftArm, "The force of the {1} dents {0}'s {2}, bruising the left arm!" },
            {BodyParts.Parts.RightArm, "The force of the {1} dents {0}'s {2}, bruising the right arm!" },
            {BodyParts.Parts.LeftHand, "The force of the {1} dents {0}'s {2}, bruising the left hand!" },
            {BodyParts.Parts.RightHand, "The force of the {1} dents {0}'s {2}, bruising the right hand!" },
            {BodyParts.Parts.Chest, "The force of the {1} dents {0}'s {2}, bruising the chest!" },
            {BodyParts.Parts.Abdomin, "The force of the {1} dents {0}'s {2}, brusing the belly!" },
            {BodyParts.Parts.LeftLeg, "The force of the {1} dents {0}'s {2}, bruising the left leg!" },
            {BodyParts.Parts.RightLeg, "The force of the {1} dents {0}'s {2}, bruising the right leg!" },
            {BodyParts.Parts.LeftFoot, "The force of the {1} dents {0}'s {2}, bruising the left foot!" },
            {BodyParts.Parts.RightFoot, "The force of the {1} dents {0}'s {2}, bruising the right foot!" },
        },
        new Dictionary<BodyParts.Parts, string>() //heavy bruise
        {
            {BodyParts.Parts.Head, "The force of the {1} slams into {0}'s {2}, rattling the brain!" },
            {BodyParts.Parts.Neck, "The force of the {1} slams into {0}'s {2}, heavily bruising the neck!" },
            {BodyParts.Parts.LeftArm, "The force of the {1} slams into {0}'s {2}, heavily bruising the left arm!" },
            {BodyParts.Parts.RightArm, "The force of the {1} slams into {0}'s {2}, heavily bruising the right arm!" },
            {BodyParts.Parts.LeftHand, "The force of the {1} slams into {0}'s {2}, heavily bruising the left hand!" },
            {BodyParts.Parts.RightHand, "The force of the {1} slams into {0}'s {2}, heavily bruising the right hand!" },
            {BodyParts.Parts.Chest, "The force of the {1} slams into {0}'s {2}, heavily bruising the chest!" },
            {BodyParts.Parts.Abdomin, "The force of the {1} slams into {0}'s {2}, heavily bruising the belly!" },
            {BodyParts.Parts.LeftLeg, "The force of the {1} slams into {0}'s {2}, heavily bruising the left leg!" },
            {BodyParts.Parts.RightLeg, "The force of the {1} slams into {0}'s {2}, heavily bruising the right leg!" },
            {BodyParts.Parts.LeftFoot, "The force of the {1} slams into {0}'s {2}, heavily bruising the left foot!" },
            {BodyParts.Parts.RightFoot, "The force of the {1} slams into {0}'s {2}, heavily bruising the right foot!" },
        },
        new Dictionary<BodyParts.Parts, string>() //pulverize meat
        {
            {BodyParts.Parts.Head, "The force of the {1} crumples {0}'s {2}, cracking the skull!" },
            {BodyParts.Parts.Neck, "The force of the {1} crumples {0}'s {2} and pulverizes the meat of the neck!" },
            {BodyParts.Parts.LeftArm, "The force of the {1} crumples {0}'s {2}, pulverizing the meat of the left arm!" },
            {BodyParts.Parts.RightArm, "The force of the {1} crumples {0}'s {2}, pulverizing the meat of the right arm!" },
            {BodyParts.Parts.LeftHand, "The force of the {1} crumples {0}'s {2}, cracking the bones in the left hand!" },
            {BodyParts.Parts.RightHand, "The force of the {1} crumples {0}'s {2}, cracking the bones in the right hand!" },
            {BodyParts.Parts.Chest, "The force of the {1} crumples {0}'s {2}, cracking the ribs!" },
            {BodyParts.Parts.Abdomin, "The force of the {1} crumples {0}'s {2}, brusing the guts!" },
            {BodyParts.Parts.LeftLeg, "The force of the {1} crumples {0}'s {2}, pulverizing the meat of the left leg!" },
            {BodyParts.Parts.RightLeg, "The force of the {1} crumples {0}'s {2}, pulverizing the meat of the right leg!" },
            {BodyParts.Parts.LeftFoot, "The force of the {1} crumples {0}'s {2}, cracking the bones in the left foot!" },
            {BodyParts.Parts.RightFoot, "The force of the {1} crumples {0}'s {2}, cracking the bones in the right foot!" },
        },
        new Dictionary<BodyParts.Parts, string>() //bone shattering crush
        {
            {BodyParts.Parts.Head, "The force of the {1} crushes {0}'s {2}, caving in the head!" },
            {BodyParts.Parts.Neck, "The force of the {1} crushes {0}'s {2} along with the windpipe!" },
            {BodyParts.Parts.LeftArm, "The force of the {1} crushes {0}'s {2}, the impact shatters the bones of the left arm!" },
            {BodyParts.Parts.RightArm, "The force of the {1} crushes {0}'s {2}, the impact shatters the bones of the right arm!" },
            {BodyParts.Parts.LeftHand, "The force of the {1} crushes {0}'s {2}, shattering the bones of the left hand!" },
            {BodyParts.Parts.RightHand, "The force of the {1} crushes {0}'s {2}, shattering the bones of the right hand!" },
            {BodyParts.Parts.Chest, "The force of the {1} crushes {0}'s {2}, shattering the ribs and puncturing the lungs!" },
            {BodyParts.Parts.Abdomin, "The force of the {1} crushes {0}'s {2}, seriously damaging the innards!" },
            {BodyParts.Parts.LeftLeg, "The force of the {1} crushes {0}'s {2}, the impact shatters the bones of the left leg!" },
            {BodyParts.Parts.RightLeg, "The force of the {1} crushes {0}'s {2}, the impact shatters the bones of the right leg!" },
            {BodyParts.Parts.LeftFoot, "The force of the {1} crushes {0}'s {2}, shattering the bones of the left foot!" },
            {BodyParts.Parts.RightFoot, "The force of the {1} crushes {0}'s {2}, shattering the bones of the right foot!" },
        },
        new Dictionary<BodyParts.Parts, string>() //obliterates into mess
        {
            {BodyParts.Parts.Head, "The force of the {1} smashes completely through {0}'s {2}, obliterating the head!" },
            {BodyParts.Parts.Neck, "The force of the {1} smashes completely through {0}'s {2}, partially decapitating the head!" },
            {BodyParts.Parts.LeftArm, "The force of the {1} smashes completely through {0}'s {2}, the left arm has been rendered totally useless!" },
            {BodyParts.Parts.RightArm, "The force of the {1} smashes completely through {0}'s {2}, the right arm has been rendered totally useless!" },
            {BodyParts.Parts.LeftHand, "The force of the {1} smashes completely through {0}'s {2}, the left hand has been mangled beyond recognition!" },
            {BodyParts.Parts.RightHand, "The force of the {1} smashes completely through {0}'s {2}, the right hand has been mangled beyond recognition!" },
            {BodyParts.Parts.Chest, "The force of the {1} smashes completely through {0}'s {2}, obliterating the chest and exploding the heart!" },
            {BodyParts.Parts.Abdomin, "The force of the {1} smashes completely through {0}'s {2}, pulverizing the innards!" },
            {BodyParts.Parts.LeftLeg, "The force of the {1} smashes completely through {0}'s {2}, the left leg has been rendered totally useless!" },
            {BodyParts.Parts.RightLeg, "The force of the {1} smashes completely through {0}'s {2}, the right leg has been rendered totally useless!" },
            {BodyParts.Parts.LeftFoot, "The force of the {1} smashes completely through {0}'s {2}, the left foot has been mangled beyond recognition!" },
            {BodyParts.Parts.RightFoot, "The force of the {1} smashes completely through {0}'s {2}, the right foot has been mangled beyond recognition!" },
        },
    };
}
