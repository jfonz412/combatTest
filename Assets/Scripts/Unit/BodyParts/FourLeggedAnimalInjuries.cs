using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourLeggedAnimalInjuries : MonoBehaviour {

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
            {BodyParts.Parts.LeftArm, "The {1} ruptures {0}'s {2}, leaving a light scratch on the front left leg!" },
            {BodyParts.Parts.RightArm, "The {1} ruptures {0}'s {2}, leaving a light scratch on the front right leg!" },
            {BodyParts.Parts.LeftHand, "The {1} ruptures {0}'s {2}, leaving a light scratch on the front left paw" },
            {BodyParts.Parts.RightHand, "The {1} ruptures {0}'s {2}, leaving a light scratch on the front right paw!" },
            {BodyParts.Parts.Chest, "The {1} ruptures {0}'s {2}, scratching the chest!" },
            {BodyParts.Parts.Abdomin, "The {1} ruptures {0}'s {2}, scratching the belly!" },
            {BodyParts.Parts.LeftLeg, "The {1} ruptures {0}'s {2}, leaving a light scratch on the back left leg!" },
            {BodyParts.Parts.RightLeg, "The {1} ruptures {0}'s {2}, leaving a light scratch on the back right leg!" },
            {BodyParts.Parts.LeftFoot, "The {1} ruptures {0}'s {2}, leaving a light scratch on the back left paw!" },
            {BodyParts.Parts.RightFoot, "The {1} ruptures {0}'s {2}, leaving a light scratch on the back right paw!" },
        },
        new Dictionary<BodyParts.Parts, string>() //{1} slices
        {
            {BodyParts.Parts.Head, "The {1} pierces {0}'s {2}, slicing the flesh of the head!" },
            {BodyParts.Parts.Neck, "The {1} pierces {0}'s {2}, slicing the flesh of the neck!"},
            {BodyParts.Parts.LeftArm, "The {1} pierces {0}'s {2}, slicing the flesh of the front left leg!" },
            {BodyParts.Parts.RightArm, "The {1} pierces {0}'s {2}, slicing the flesh of the front right leg!" },
            {BodyParts.Parts.LeftHand, "The {1} pierces {0}'s {2}, slicing the flesh of the front left paw" },
            {BodyParts.Parts.RightHand, "The {1} pierces {0}'s {2}, slicing the flesh of the front right paw!" },
            {BodyParts.Parts.Chest, "The {1} pierces {0}'s {2}, slicing the flesh of the chest!" },
            {BodyParts.Parts.Abdomin, "The {1} pierces {0}'s {2}, slicing the flesh of the belly!" },
            {BodyParts.Parts.LeftLeg, "The {1} pierces {0}'s {2}, slicing the flesh of the back left leg!" },
            {BodyParts.Parts.RightLeg, "The {1} pierces {0}'s {2}, slicing the flesh of the back right leg!" },
            {BodyParts.Parts.LeftFoot, "The {1} pierces {0}'s {2}, slicing the flesh of the back left paw!" },
            {BodyParts.Parts.RightFoot, "The {1} pierces {0}'s {2}, slicing the flesh of the back right paw!" },
        },
        new Dictionary<BodyParts.Parts, string>() //deep cut
        {
            {BodyParts.Parts.Head, "The {1} cuts through {0}'s {2} and hacks deep into the head!" },
            {BodyParts.Parts.Neck, "The {1} cuts through {0}'s {2} and hacks deep into the neck!"},
            {BodyParts.Parts.LeftArm, "The {1} cuts through {0}'s {2} and hacks deep into the front left leg!" },
            {BodyParts.Parts.RightArm, "The {1} cuts through {0}'s {2} and hacks deep into the front right leg!" },
            {BodyParts.Parts.LeftHand, "The {1} cuts through {0}'s {2} and hacks deep into the front left paw" },
            {BodyParts.Parts.RightHand, "The {1} cuts through {0}'s {2} and hacks deep into the front right paw!" },
            {BodyParts.Parts.Chest, "The {1} cuts through {0}'s {2} and hacks deep into the chest!" },
            {BodyParts.Parts.Abdomin, "The {1} cuts through {0}'s {2} and hacks deep into the belly!" },
            {BodyParts.Parts.LeftLeg, "The {1} cuts through {0}'s {2} and hacks deep into the back left leg!" },
            {BodyParts.Parts.RightLeg, "The {1} cuts through {0}'s {2} and hacks deep into the back right leg!" },
            {BodyParts.Parts.LeftFoot, "The {1} cuts through {0}'s {2} and hacks deep into the back left paw!" },
            {BodyParts.Parts.RightFoot, "The {1} cuts through {0}'s {2} and hacks deep into the back right paw!" },
        },
        new Dictionary<BodyParts.Parts, string>() //cut to bone
        {
            {BodyParts.Parts.Head, "The {1} completely penetrates {0}'s {2}, cracking the skull!" },
            {BodyParts.Parts.Neck, "The {1} completely penetrates the {2}, almost decapitating {0}!"},
            {BodyParts.Parts.LeftArm, "The {1} completely penetrates {0}'s {2}, cutting down to the bone of the the front left leg!" },
            {BodyParts.Parts.RightArm, "The {1} completely penetrates {0}'s {2}, cutting down to the bone of the the front right leg!" },
            {BodyParts.Parts.LeftHand, "The {1} completely penetrates {0}'s {2}, cutting down to the bones of the the front left paw" },
            {BodyParts.Parts.RightHand, "The {1} completely penetrates {0}'s {2}, cutting down to the bones of the the front right paw!" },
            {BodyParts.Parts.Chest, "The {1} completely penetrates the {2} and cuts into {0}'s the ribs!" },
            {BodyParts.Parts.Abdomin, "The {1} completely penetrates {0}'s {2}, and slices the belly wide open!" },
            {BodyParts.Parts.LeftLeg, "The {1} completely penetrates {0}'s {2}, cutting down to the bone of the the back left leg!" },
            {BodyParts.Parts.RightLeg, "The {1} completely penetrates {0}'s {2}, cutting down to the bone of the the back right leg!" },
            {BodyParts.Parts.LeftFoot, "The {1} completely penetrates {0}'s {2}, cutting down to the bones of the the back left paw!" },
            {BodyParts.Parts.RightFoot, "The {1} completely penetrates {0}'s {2}, cutting down to the bones of the the back right paw!" },
        },
        new Dictionary<BodyParts.Parts, string>() //shatters bone
        {
            {BodyParts.Parts.Head, "The {1} easily carves into {0}'s {2} and shatters the skull!" },
            {BodyParts.Parts.Neck, "The {1} easily carves into the {0}'s {2}, hacking into the spine of the neck!"},
            {BodyParts.Parts.LeftArm, "The {1} easily carves into {0}'s {2}, completely shatttering the bone of the the front left leg!" },
            {BodyParts.Parts.RightArm, "The {1} easily carves into {0}'s {2}, completely shatttering the bone of the the front right leg!" },
            {BodyParts.Parts.LeftHand, "The {1} easily carves into {0}'s {2}, completely shatttering the bones of the the front left paw" },
            {BodyParts.Parts.RightHand, "The {1} easily carves into {0}'s {2}, completely shatttering the bones of the the front right paw!" },
            {BodyParts.Parts.Chest, "The {1} easily carves into {0}'s {2} completely shatters the ribs!" },
            {BodyParts.Parts.Abdomin, "The {1} easily carves into {0}'s {2}, scrambling his innards with the blade!" },
            {BodyParts.Parts.LeftLeg, "The {1} easily carves into {0}'s {2}, completely shatttering the bone of the the back left leg!" },
            {BodyParts.Parts.RightLeg, "The {1} easily carves into {0}'s {2}, completely shatttering the bone of the the back right leg!" },
            {BodyParts.Parts.LeftFoot, "The {1} easily carves into {0}'s {2}, completely shatttering the bones of the the back left paw!" },
            {BodyParts.Parts.RightFoot, "The {1} easily carves into {0}'s {2}, completely shatttering the bones of the the back right paw!" },
        },
        new Dictionary<BodyParts.Parts, string>() //sever
        {
            {BodyParts.Parts.Head, "The {1} effortlessly slices through {0}'s {2}, the blade embeds itself deep into the skull!" },
            {BodyParts.Parts.Neck, "The {1} effortlessly slices through the {2}, {0}'s head flies clean off!"},
            {BodyParts.Parts.LeftArm, "The {1} effortlessly slices through {0}'s {2}, severing the front left leg!" },
            {BodyParts.Parts.RightArm, "The {1} effortlessly slices through {0}'s {2}, severing the front right leg!" },
            {BodyParts.Parts.LeftHand, "The {1} effortlessly slices through {0}'s {2}, severing the front left paw" },
            {BodyParts.Parts.RightHand, "The {1} effortlessly slices through {0}'s {2}, severing the front right paw!" },
            {BodyParts.Parts.Chest, "The {1} effortlessly slices through the {2} and finds itself deeply ebedded in {0}'s chest!" },
            {BodyParts.Parts.Abdomin, "The {1} effortlessly slices through {0}'s {2} and deep into the belly, {0}'s innards spill to the floor!" },
            {BodyParts.Parts.LeftLeg, "The {1} effortlessly slices through {0}'s {2}, severing the back left leg!" },
            {BodyParts.Parts.RightLeg, "The {1} effortlessly slices through {0}'s {2}, severing the back right leg!" },
            {BodyParts.Parts.LeftFoot, "The {1} effortlessly slices through {0}'s {2}, severing the back left paw!" },
            {BodyParts.Parts.RightFoot, "The {1} effortlessly slices through {0}'s {2}, severing the back right paw!" },
        }
    };

    private static Dictionary<BodyParts.Parts, string>[] impactInjuries = new Dictionary<BodyParts.Parts, string>[] {
        //IMPACT
        new Dictionary<BodyParts.Parts, string>() //light bruise
        {
            {BodyParts.Parts.Head, "The force of the {1} dents {0}'s {2}, leaving small bump on the head!" },
            {BodyParts.Parts.Neck, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the neck!" },
            {BodyParts.Parts.LeftArm, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the front left leg!" },
            {BodyParts.Parts.RightArm, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the front right leg!" },
            {BodyParts.Parts.LeftHand, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the front left paw!" },
            {BodyParts.Parts.RightHand, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the front right paw!" },
            {BodyParts.Parts.Chest, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the chest!" },
            {BodyParts.Parts.Abdomin, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the belly!" },           
            {BodyParts.Parts.LeftLeg, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the back left leg!" },
            {BodyParts.Parts.RightLeg, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the back right leg!" },
            {BodyParts.Parts.LeftFoot, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the back left paw!" },
            {BodyParts.Parts.RightFoot, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the back right paw!" },
        },
        new Dictionary<BodyParts.Parts, string>() //bad bruise
        {
            {BodyParts.Parts.Head, "The force of the {1} dents {0}'s {2}, leaving large bump on the head!" },
            {BodyParts.Parts.Neck, "The force of the {1} dents {0}'s {2}, bruising the neck!" },
            {BodyParts.Parts.LeftArm, "The force of the {1} dents {0}'s {2}, bruising the front left leg!" },
            {BodyParts.Parts.RightArm, "The force of the {1} dents {0}'s {2}, bruising the front right leg!" },
            {BodyParts.Parts.LeftHand, "The force of the {1} dents {0}'s {2}, bruising the front left paw!" },
            {BodyParts.Parts.RightHand, "The force of the {1} dents {0}'s {2}, bruising the front right paw!" },
            {BodyParts.Parts.Chest, "The force of the {1} dents {0}'s {2}, bruising the chest!" },
            {BodyParts.Parts.Abdomin, "The force of the {1} dents {0}'s {2}, brusing the belly!" },
            {BodyParts.Parts.LeftLeg, "The force of the {1} dents {0}'s {2}, bruising the back left leg!" },
            {BodyParts.Parts.RightLeg, "The force of the {1} dents {0}'s {2}, bruising the back right leg!" },
            {BodyParts.Parts.LeftFoot, "The force of the {1} dents {0}'s {2}, bruising the back left paw!" },
            {BodyParts.Parts.RightFoot, "The force of the {1} dents {0}'s {2}, bruising the back right paw!" },
        },
        new Dictionary<BodyParts.Parts, string>() //heavy bruise
        {
            {BodyParts.Parts.Head, "The force of the {1} slams into {0}'s {2}, rattling the brain!" },
            {BodyParts.Parts.Neck, "The force of the {1} slams into {0}'s {2}, heavily bruising the neck!" },
            {BodyParts.Parts.LeftArm, "The force of the {1} slams into {0}'s {2}, heavily bruising the front left leg!" },
            {BodyParts.Parts.RightArm, "The force of the {1} slams into {0}'s {2}, heavily bruising the front right leg!" },
            {BodyParts.Parts.LeftHand, "The force of the {1} slams into {0}'s {2}, heavily bruising the front left paw!" },
            {BodyParts.Parts.RightHand, "The force of the {1} slams into {0}'s {2}, heavily bruising the front right paw!" },
            {BodyParts.Parts.Chest, "The force of the {1} slams into {0}'s {2}, heavily bruising the chest!" },
            {BodyParts.Parts.Abdomin, "The force of the {1} slams into {0}'s {2}, heavily bruising the belly!" },
            {BodyParts.Parts.LeftLeg, "The force of the {1} slams into {0}'s {2}, heavily bruising the back left leg!" },
            {BodyParts.Parts.RightLeg, "The force of the {1} slams into {0}'s {2}, heavily bruising the back right leg!" },
            {BodyParts.Parts.LeftFoot, "The force of the {1} slams into {0}'s {2}, heavily bruising the back left paw!" },
            {BodyParts.Parts.RightFoot, "The force of the {1} slams into {0}'s {2}, heavily bruising the back right paw!" },
        },
        new Dictionary<BodyParts.Parts, string>() //pulverize meat
        {
            {BodyParts.Parts.Head, "The force of the {1} crumples {0}'s {2}, cracking the skull!" },
            {BodyParts.Parts.Neck, "The force of the {1} crumples {0}'s {2} and pulverizes the meat of the neck!" },
            {BodyParts.Parts.LeftArm, "The force of the {1} crumples {0}'s {2}, pulverizing the meat of the front left leg!" },
            {BodyParts.Parts.RightArm, "The force of the {1} crumples {0}'s {2}, pulverizing the meat of the front right leg!" },
            {BodyParts.Parts.LeftHand, "The force of the {1} crumples {0}'s {2}, cracking the bones in the front left paw!" },
            {BodyParts.Parts.RightHand, "The force of the {1} crumples {0}'s {2}, cracking the bones in the front right paw!" },
            {BodyParts.Parts.Chest, "The force of the {1} crumples {0}'s {2}, cracking the ribs!" },
            {BodyParts.Parts.Abdomin, "The force of the {1} crumples {0}'s {2}, brusing the guts!" },
            {BodyParts.Parts.LeftLeg, "The force of the {1} crumples {0}'s {2}, pulverizing the meat of the back left leg!" },
            {BodyParts.Parts.RightLeg, "The force of the {1} crumples {0}'s {2}, pulverizing the meat of the back right leg!" },
            {BodyParts.Parts.LeftFoot, "The force of the {1} crumples {0}'s {2}, cracking the bones in the back left paw!" },
            {BodyParts.Parts.RightFoot, "The force of the {1} crumples {0}'s {2}, cracking the bones in the back right paw!" },
        },
        new Dictionary<BodyParts.Parts, string>() //bone shattering crush
        {
            {BodyParts.Parts.Head, "The force of the {1} crushes {0}'s {2}, caving in the head!" },
            {BodyParts.Parts.Neck, "The force of the {1} crushes {0}'s {2} a!ong with the windpipe!" },
            {BodyParts.Parts.LeftArm, "The force of the {1} crushes {0}'s {2}, the impact shatters the bones of the front left leg!" },
            {BodyParts.Parts.RightArm, "The force of the {1} crushes {0}'s {2}, the impact shatters the bones of the front right leg!" },
            {BodyParts.Parts.LeftHand, "The force of the {1} crushes {0}'s {2}, shattering the bones of the front left paw!" },
            {BodyParts.Parts.RightHand, "The force of the {1} crushes {0}'s {2}, shattering the bones of the front right paw!" },
            {BodyParts.Parts.Chest, "The force of the {1} crushes {0}'s {2}, shattering the ribs and puncturing the lungs!" },
            {BodyParts.Parts.Abdomin, "The force of the {1} crushes {0}'s {2}, damaging the innards!" },
            {BodyParts.Parts.LeftLeg, "The force of the {1} crushes {0}'s {2}, the impact shatters the bones of the back left leg!" },
            {BodyParts.Parts.RightLeg, "The force of the {1} crushes {0}'s {2}, the impact shatters the bones of the back right leg!" },
            {BodyParts.Parts.LeftFoot, "The force of the {1} crushes {0}'s {2}, shattering the bones of the back left paw!" },
            {BodyParts.Parts.RightFoot, "The force of the {1} crushes {0}'s {2}, shattering the bones of the back right paw!" },
        },
        new Dictionary<BodyParts.Parts, string>() //obliterates into mess
        {
            {BodyParts.Parts.Head, "The force of the {1} smashes completely through {0}'s {2}, obliterating the head!" },
            {BodyParts.Parts.Neck, "The force of the {1} smashes completely through {0}'s {2}, partially decapitating the head!" },
            {BodyParts.Parts.LeftArm, "The force of the {1} smashes completely through {0}'s {2}, the front left leg has been rendered totally useless!" },
            {BodyParts.Parts.RightArm, "The force of the {1} smashes completely through {0}'s {2}, the front right leg has been rendered totally useless!" },
            {BodyParts.Parts.LeftHand, "The force of the {1} smashes completely through {0}'s {2}, the front left paw has been mangled beyond recognition!" },
            {BodyParts.Parts.RightHand, "The force of the {1} smashes completely through {0}'s {2}, the front right paw has been mangled beyond recognition!" },
            {BodyParts.Parts.Chest, "The force of the {1} smashes completely through {0}'s {2}, obliterating the chest and exploding the heart!" },
            {BodyParts.Parts.Abdomin, "The force of the {1} smashes completely through {0}'s {2}, pulverizing the innards!" },
            {BodyParts.Parts.LeftLeg, "The force of the {1} smashes completely through {0}'s {2}, the back left leg has been rendered totally useless!" },
            {BodyParts.Parts.RightLeg, "The force of the {1} smashes completely through {0}'s {2}, the back right leg has been rendered totally useless!" },
            {BodyParts.Parts.LeftFoot, "The force of the {1} smashes completely through {0}'s {2}, the back left paw has been mangled beyond recognition!" },
            {BodyParts.Parts.RightFoot, "The force of the {1} smashes completely through {0}'s {2}, the back right paw has been mangled beyond recognition!" },
        },
    };
}
