using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanInjuries : MonoBehaviour {

    /*
        {"Head", "Neck", "LeftArm", "RightArm",
        "LHand", "RHand", "Thorax", "Abdomin",
        "LeftLeg", "RightLeg", "LFoot", "RFoot" }
    
    
    //should make this inheritable for all injury lists??? this is the same as BushBansheeInjuries
    public static Dictionary<BodyPartController.Parts, string> GetInjuryList(Injuries.DamageType damageType, int severityID)
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
            return new Dictionary<BodyPartController.Parts, string>();
        }
    }

    private static Dictionary<BodyPartController.Parts, string>[] penetrationInjuries = new Dictionary<BodyPartController.Parts, string>[] {
        //PENETRATION
        new Dictionary<BodyPartController.Parts, string>() //scratch
        {
            //I know these are almost all the same but I am leaving them as is now to keep my options open and to avoid confusion for how this is set up
            {BodyPartController.Parts.Head, "The {1} ruptures {0}'s {2}, leaving a light scratch on the head!" },
            {BodyPartController.Parts.Neck, "The {1} ruptures {0}'s {2}, leaving a light scratch on the neck!"},
            {BodyPartController.Parts.LeftArm, "The {1} ruptures {0}'s {2}, leaving a light scratch on the left arm!" },
            {BodyPartController.Parts.RightArm, "The {1} ruptures {0}'s {2}, leaving a light scratch on the right arm!" },
            {BodyPartController.Parts.LeftHand, "The {1} ruptures {0}'s {2}, leaving a light scratch on the left hand" },
            {BodyPartController.Parts.RightHand, "The {1} ruptures {0}'s {2}, leaving a light scratch on the right hand!" },
            {BodyPartController.Parts.Chest, "The {1} ruptures {0}'s {2}, scratching the chest!" },
            {BodyPartController.Parts.Abdomin, "The {1} ruptures {0}'s {2}, scratching the belly!" },
            {BodyPartController.Parts.LeftLeg, "The {1} ruptures {0}'s {2}, leaving a light scratch on the left leg!" },
            {BodyPartController.Parts.RightLeg, "The {1} ruptures {0}'s {2}, leaving a light scratch on the right leg!" },
            {BodyPartController.Parts.LeftFoot, "The {1} ruptures {0}'s {2}, leaving a light scratch on the left foot!" },
            {BodyPartController.Parts.RightFoot, "The {1} ruptures {0}'s {2}, leaving a light scratch on the right foot!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //{1} slices
        {
            {BodyPartController.Parts.Head, "The {1} pierces {0}'s {2}, slicing the skin of the head!" },
            {BodyPartController.Parts.Neck, "The {1} pierces {0}'s {2}, slicing the flesh of the neck!"},
            {BodyPartController.Parts.LeftArm, "The {1} pierces {0}'s {2}, slicing the flesh of the left arm!" },
            {BodyPartController.Parts.RightArm, "The {1} pierces {0}'s {2}, slicing the flesh of the right arm!" },
            {BodyPartController.Parts.LeftHand, "The {1} pierces {0}'s {2}, slicing the flesh of the left hand" },
            {BodyPartController.Parts.RightHand, "The {1} pierces {0}'s {2}, slicing the flesh of the right hand!" },
            {BodyPartController.Parts.Chest, "The {1} pierces {0}'s {2}, slicing the flesh of the chest!" },
            {BodyPartController.Parts.Abdomin, "The {1} pierces {0}'s {2}, slicing the flesh of the belly!" },
            {BodyPartController.Parts.LeftLeg, "The {1} pierces {0}'s {2}, slicing the flesh of the left leg!" },
            {BodyPartController.Parts.RightLeg, "The {1} pierces {0}'s {2}, slicing the flesh of the right leg!" },
            {BodyPartController.Parts.LeftFoot, "The {1} pierces {0}'s {2}, slicing the flesh of the left foot!" },
            {BodyPartController.Parts.RightFoot, "The {1} pierces {0}'s {2}, slicing the flesh of the right foot!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //deep cut
        {
            {BodyPartController.Parts.Head, "The {1} cuts through {0}'s {2} and slams against the skull!" },
            {BodyPartController.Parts.Neck, "The {1} cuts through {0}'s {2} and hacks into the neck!"},
            {BodyPartController.Parts.LeftArm, "The {1} cuts through {0}'s {2} and hacks deep into the left arm!" },
            {BodyPartController.Parts.RightArm, "The {1} cuts through {0}'s {2} and hacks deep into the right arm!" },
            {BodyPartController.Parts.LeftHand, "The {1} cuts through {0}'s {2} and hacks deep into the left hand" },
            {BodyPartController.Parts.RightHand, "The {1} cuts through {0}'s {2} and hacks deep into the right hand!" },
            {BodyPartController.Parts.Chest, "The {1} cuts through {0}'s {2} and cuts into {0}'s ribs!" },
            {BodyPartController.Parts.Abdomin, "The {1} cuts through {0}'s {2} and hacks deep into the flesh of the belly!" },
            {BodyPartController.Parts.LeftLeg, "The {1} cuts through {0}'s {2} and hacks deep into the left leg!" },
            {BodyPartController.Parts.RightLeg, "The {1} cuts through {0}'s {2} and hacks deep into the right leg!" },
            {BodyPartController.Parts.LeftFoot, "The {1} cuts through {0}'s {2} and hacks deep into the left foot!" },
            {BodyPartController.Parts.RightFoot, "The {1} cuts through {0}'s {2} and hacks deep into the right foot!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //cut to bone
        {
            {BodyPartController.Parts.Head, "The {1} completely penetrates {0}'s {2}, cracking the skull!" },
            {BodyPartController.Parts.Neck, "The {1} completely penetrates the {2}, hacking deep into the neck!"},
            {BodyPartController.Parts.LeftArm, "The {1} completely penetrates {0}'s {2}, cutting down to the bone of the the left arm!" },
            {BodyPartController.Parts.RightArm, "The {1} completely penetrates {0}'s {2}, cutting down to the bone of the the right arm!" },
            {BodyPartController.Parts.LeftHand, "The {1} completely penetrates {0}'s {2}, cutting down to the bones of the the left hand" },
            {BodyPartController.Parts.RightHand, "The {1} completely penetrates {0}'s {2}, cutting down to the bones of the the right hand!" },
            {BodyPartController.Parts.Chest, "The {1} completely penetrates {0}'s {2} and cracks the ribs!" },
            {BodyPartController.Parts.Abdomin, "The {1} completely penetrates {0}'s {2}, and slices the belly wide open!" },
            {BodyPartController.Parts.LeftLeg, "The {1} completely penetrates {0}'s {2}, cutting down to the bone of the the left leg!" },
            {BodyPartController.Parts.RightLeg, "The {1} completely penetrates {0}'s {2}, cutting down to the bone of the the right leg!" },
            {BodyPartController.Parts.LeftFoot, "The {1} completely penetrates {0}'s {2}, cutting down to the bones of the the left foot!" },
            {BodyPartController.Parts.RightFoot, "The {1} completely penetrates {0}'s {2}, cutting down to the bones of the the right foot!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //shatters bone
        {
            {BodyPartController.Parts.Head, "The {1} easily carves into {0}'s {2} and shatters the skull!" },
            {BodyPartController.Parts.Neck, "The {1} easily carves into the {0}'s {2}, almost decapitating {0}!"},
            {BodyPartController.Parts.LeftArm, "The {1} easily carves into {0}'s {2}, completely shatttering the bone of the the left arm!" },
            {BodyPartController.Parts.RightArm, "The {1} easily carves into {0}'s {2}, completely shatttering the bone of the the right arm!" },
            {BodyPartController.Parts.LeftHand, "The {1} easily carves into {0}'s {2}, completely shatttering the bones of the the left hand" },
            {BodyPartController.Parts.RightHand, "The {1} easily carves into {0}'s {2}, completely shatttering the bones of the the right hand!" },
            {BodyPartController.Parts.Chest, "The {1} easily carves into {0}'s {2} and completely shatters the ribs!" },
            {BodyPartController.Parts.Abdomin, "The {1} easily carves into {0}'s {2}, scrambling his innards with the blade!" },
            {BodyPartController.Parts.LeftLeg, "The {1} easily carves into {0}'s {2}, completely shatttering the bone of the the left leg!" },
            {BodyPartController.Parts.RightLeg, "The {1} easily carves into {0}'s {2}, completely shatttering the bone of the the right leg!" },
            {BodyPartController.Parts.LeftFoot, "The {1} easily carves into {0}'s {2}, completely shatttering the bones of the the left foot!" },
            {BodyPartController.Parts.RightFoot, "The {1} easily carves into {0}'s {2}, completely shatttering the bones of the the right foot!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //sever
        {
            {BodyPartController.Parts.Head, "The {1} effortlessly slices through {0}'s {2}, the blade embeds itself deep into the skull!" },
            {BodyPartController.Parts.Neck, "The {1} effortlessly slices through the {2}, {0}'s head flies clean off!"},
            {BodyPartController.Parts.LeftArm, "The {1} effortlessly slices through {0}'s {2}, severing the left arm!" },
            {BodyPartController.Parts.RightArm, "The {1} effortlessly slices through {0}'s {2}, severing the right arm!" },
            {BodyPartController.Parts.LeftHand, "The {1} effortlessly slices through {0}'s {2}, severing the left hand" },
            {BodyPartController.Parts.RightHand, "The {1} effortlessly slices through {0}'s {2}, severing the right hand!" },
            {BodyPartController.Parts.Chest, "The {1} effortlessly slices through the {2} and finds itself deeply ebedded in {0}'s chest!" },
            {BodyPartController.Parts.Abdomin, "The {1} effortlessly slices through {0}'s {2} and deep into the belly, {0}'s innards spill to the floor!" },
            {BodyPartController.Parts.LeftLeg, "The {1} effortlessly slices through {0}'s {2}, severing the left leg!" },
            {BodyPartController.Parts.RightLeg, "The {1} effortlessly slices through {0}'s {2}, severing the right leg!" },
            {BodyPartController.Parts.LeftFoot, "The {1} effortlessly slices through {0}'s {2}, severing the left foot!" },
            {BodyPartController.Parts.RightFoot, "The {1} effortlessly slices through {0}'s {2}, severing the right foot!" },
        }
    };

    private static Dictionary<BodyPartController.Parts, string>[] impactInjuries = new Dictionary<BodyPartController.Parts, string>[] {
        //IMPACT
        new Dictionary<BodyPartController.Parts, string>() //light bruise
        {
            {BodyPartController.Parts.Head, "The force of the {1} smacks against {0}'s {2}, leaving small bump on the head!" },
            {BodyPartController.Parts.Neck, "The force of the {1} smacks against {0}'s {2}, leaving a light bruise on the neck!" },
            {BodyPartController.Parts.LeftArm, "The force of the {1} smacks against {0}'s {2}, leaving a light bruise on the left arm!" },
            {BodyPartController.Parts.RightArm, "The force of the {1} smacks against {0}'s {2}, leaving a light bruise on the right arm!" },
            {BodyPartController.Parts.LeftHand, "The force of the {1} smacks against {0}'s {2}, leaving a light bruise on the left hand!" },
            {BodyPartController.Parts.RightHand, "The force of the {1} smacks against {0}'s {2}, leaving a light bruise on the right hand!" },
            {BodyPartController.Parts.Chest, "The force of the {1} smacks against {0}'s {2}, leaving a light bruise on the chest!" },
            {BodyPartController.Parts.Abdomin, "The force of the {1} smacks against {0}'s {2}, leaving a light bruise on the belly!" },           
            {BodyPartController.Parts.LeftLeg, "The force of the {1} smacks against {0}'s {2}, leaving a light bruise on the left leg!" },
            {BodyPartController.Parts.RightLeg, "The force of the {1} smacks against {0}'s {2}, leaving a light bruise on the right leg!" },
            {BodyPartController.Parts.LeftFoot, "The force of the {1} smacks against {0}'s {2}, leaving a light bruise on the left foot!" },
            {BodyPartController.Parts.RightFoot, "The force of the {1} smacks against {0}'s {2}, leaving a light bruise on the right foot!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //bad bruise
        {
            {BodyPartController.Parts.Head, "The force of the {1} smacks against {0}'s {2}, leaving large bump on the head!" },
            {BodyPartController.Parts.Neck, "The force of the {1} smacks against {0}'s {2}, bruising the neck!" },
            {BodyPartController.Parts.LeftArm, "The force of the {1} smacks against {0}'s {2}, bruising the left arm!" },
            {BodyPartController.Parts.RightArm, "The force of the {1} smacks against {0}'s {2}, bruising the right arm!" },
            {BodyPartController.Parts.LeftHand, "The force of the {1} smacks against {0}'s {2}, bruising the left hand!" },
            {BodyPartController.Parts.RightHand, "The force of the {1} smacks against {0}'s {2}, bruising the right hand!" },
            {BodyPartController.Parts.Chest, "The force of the {1} smacks against {0}'s {2}, bruising the chest!" },
            {BodyPartController.Parts.Abdomin, "The force of the {1} smacks against {0}'s {2}, brusing the belly!" },
            {BodyPartController.Parts.LeftLeg, "The force of the {1} smacks against {0}'s {2}, bruising the left leg!" },
            {BodyPartController.Parts.RightLeg, "The force of the {1} smacks against {0}'s {2}, bruising the right leg!" },
            {BodyPartController.Parts.LeftFoot, "The force of the {1} smacks against {0}'s {2}, bruising the left foot!" },
            {BodyPartController.Parts.RightFoot, "The force of the {1} smacks against {0}'s {2}, bruising the right foot!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //heavy bruise
        {
            {BodyPartController.Parts.Head, "The force of the {1} slams into {0}'s {2}, rattling the brain!" },
            {BodyPartController.Parts.Neck, "The force of the {1} slams into {0}'s {2}, heavily bruising the neck!" },
            {BodyPartController.Parts.LeftArm, "The force of the {1} slams into {0}'s {2}, heavily bruising the left arm!" },
            {BodyPartController.Parts.RightArm, "The force of the {1} slams into {0}'s {2}, heavily bruising the right arm!" },
            {BodyPartController.Parts.LeftHand, "The force of the {1} slams into {0}'s {2}, heavily bruising the left hand!" },
            {BodyPartController.Parts.RightHand, "The force of the {1} slams into {0}'s {2}, heavily bruising the right hand!" },
            {BodyPartController.Parts.Chest, "The force of the {1} slams into {0}'s {2}, heavily bruising the chest!" },
            {BodyPartController.Parts.Abdomin, "The force of the {1} slams into {0}'s {2}, heavily bruising the belly!" },
            {BodyPartController.Parts.LeftLeg, "The force of the {1} slams into {0}'s {2}, heavily bruising the left leg!" },
            {BodyPartController.Parts.RightLeg, "The force of the {1} slams into {0}'s {2}, heavily bruising the right leg!" },
            {BodyPartController.Parts.LeftFoot, "The force of the {1} slams into {0}'s {2}, heavily bruising the left foot!" },
            {BodyPartController.Parts.RightFoot, "The force of the {1} slams into {0}'s {2}, heavily bruising the right foot!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //pulverize meat
        {
            {BodyPartController.Parts.Head, "The force of the {1} crumples {0}'s {2}, cracking the skull!" },
            {BodyPartController.Parts.Neck, "The force of the {1} crumples {0}'s {2} and pulverizes the meat of the neck!" },
            {BodyPartController.Parts.LeftArm, "The force of the {1} crumples {0}'s {2}, pulverizing the meat of the left arm!" },
            {BodyPartController.Parts.RightArm, "The force of the {1} crumples {0}'s {2}, pulverizing the meat of the right arm!" },
            {BodyPartController.Parts.LeftHand, "The force of the {1} crumples {0}'s {2}, cracking the bones in the left hand!" },
            {BodyPartController.Parts.RightHand, "The force of the {1} crumples {0}'s {2}, cracking the bones in the right hand!" },
            {BodyPartController.Parts.Chest, "The force of the {1} crumples {0}'s {2}, cracking the ribs!" },
            {BodyPartController.Parts.Abdomin, "The force of the {1} crumples {0}'s {2}, brusing the guts!" },
            {BodyPartController.Parts.LeftLeg, "The force of the {1} crumples {0}'s {2}, pulverizing the meat of the left leg!" },
            {BodyPartController.Parts.RightLeg, "The force of the {1} crumples {0}'s {2}, pulverizing the meat of the right leg!" },
            {BodyPartController.Parts.LeftFoot, "The force of the {1} crumples {0}'s {2}, cracking the bones in the left foot!" },
            {BodyPartController.Parts.RightFoot, "The force of the {1} crumples {0}'s {2}, cracking the bones in the right foot!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //bone shattering crush
        {
            {BodyPartController.Parts.Head, "The force of the {1} crushes {0}'s {2}, caving in the head!" },
            {BodyPartController.Parts.Neck, "The force of the {1} crushes {0}'s {2} along with the windpipe!" },
            {BodyPartController.Parts.LeftArm, "The force of the {1} crushes {0}'s {2}, the impact shatters the bones of the left arm!" },
            {BodyPartController.Parts.RightArm, "The force of the {1} crushes {0}'s {2}, the impact shatters the bones of the right arm!" },
            {BodyPartController.Parts.LeftHand, "The force of the {1} crushes {0}'s {2}, shattering the bones of the left hand!" },
            {BodyPartController.Parts.RightHand, "The force of the {1} crushes {0}'s {2}, shattering the bones of the right hand!" },
            {BodyPartController.Parts.Chest, "The force of the {1} crushes {0}'s {2}, shattering the ribs and puncturing the lungs!" },
            {BodyPartController.Parts.Abdomin, "The force of the {1} crushes {0}'s {2}, seriously damaging the innards!" },
            {BodyPartController.Parts.LeftLeg, "The force of the {1} crushes {0}'s {2}, the impact shatters the bones of the left leg!" },
            {BodyPartController.Parts.RightLeg, "The force of the {1} crushes {0}'s {2}, the impact shatters the bones of the right leg!" },
            {BodyPartController.Parts.LeftFoot, "The force of the {1} crushes {0}'s {2}, shattering the bones of the left foot!" },
            {BodyPartController.Parts.RightFoot, "The force of the {1} crushes {0}'s {2}, shattering the bones of the right foot!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //obliterates into mess
        {
            {BodyPartController.Parts.Head, "The force of the {1} smashes completely through {0}'s {2}, obliterating the head!" },
            {BodyPartController.Parts.Neck, "The force of the {1} smashes completely through {0}'s {2}, partially decapitating the head!" },
            {BodyPartController.Parts.LeftArm, "The force of the {1} smashes completely through {0}'s {2}, the left arm has been rendered totally useless!" },
            {BodyPartController.Parts.RightArm, "The force of the {1} smashes completely through {0}'s {2}, the right arm has been rendered totally useless!" },
            {BodyPartController.Parts.LeftHand, "The force of the {1} smashes completely through {0}'s {2}, the left hand has been mangled beyond recognition!" },
            {BodyPartController.Parts.RightHand, "The force of the {1} smashes completely through {0}'s {2}, the right hand has been mangled beyond recognition!" },
            {BodyPartController.Parts.Chest, "The force of the {1} smashes completely through {0}'s {2}, obliterating the chest and exploding the heart!" },
            {BodyPartController.Parts.Abdomin, "The force of the {1} smashes completely through {0}'s {2}, pulverizing the innards!" },
            {BodyPartController.Parts.LeftLeg, "The force of the {1} smashes completely through {0}'s {2}, the left leg has been rendered totally useless!" },
            {BodyPartController.Parts.RightLeg, "The force of the {1} smashes completely through {0}'s {2}, the right leg has been rendered totally useless!" },
            {BodyPartController.Parts.LeftFoot, "The force of the {1} smashes completely through {0}'s {2}, the left foot has been mangled beyond recognition!" },
            {BodyPartController.Parts.RightFoot, "The force of the {1} smashes completely through {0}'s {2}, the right foot has been mangled beyond recognition!" },
        },
    };
    */
}
