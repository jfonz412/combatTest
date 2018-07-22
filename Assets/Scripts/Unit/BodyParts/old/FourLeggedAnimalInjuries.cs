using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourLeggedAnimalInjuries : MonoBehaviour {

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
            {BodyPartController.Parts.LeftArm, "The {1} ruptures {0}'s {2}, leaving a light scratch on the front left leg!" },
            {BodyPartController.Parts.RightArm, "The {1} ruptures {0}'s {2}, leaving a light scratch on the front right leg!" },
            {BodyPartController.Parts.LeftHand, "The {1} ruptures {0}'s {2}, leaving a light scratch on the front left paw" },
            {BodyPartController.Parts.RightHand, "The {1} ruptures {0}'s {2}, leaving a light scratch on the front right paw!" },
            {BodyPartController.Parts.Chest, "The {1} ruptures {0}'s {2}, scratching the chest!" },
            {BodyPartController.Parts.Abdomin, "The {1} ruptures {0}'s {2}, scratching the belly!" },
            {BodyPartController.Parts.LeftLeg, "The {1} ruptures {0}'s {2}, leaving a light scratch on the back left leg!" },
            {BodyPartController.Parts.RightLeg, "The {1} ruptures {0}'s {2}, leaving a light scratch on the back right leg!" },
            {BodyPartController.Parts.LeftFoot, "The {1} ruptures {0}'s {2}, leaving a light scratch on the back left paw!" },
            {BodyPartController.Parts.RightFoot, "The {1} ruptures {0}'s {2}, leaving a light scratch on the back right paw!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //{1} slices
        {
            {BodyPartController.Parts.Head, "The {1} pierces {0}'s {2}, slicing the flesh of the head!" },
            {BodyPartController.Parts.Neck, "The {1} pierces {0}'s {2}, slicing the flesh of the neck!"},
            {BodyPartController.Parts.LeftArm, "The {1} pierces {0}'s {2}, slicing the flesh of the front left leg!" },
            {BodyPartController.Parts.RightArm, "The {1} pierces {0}'s {2}, slicing the flesh of the front right leg!" },
            {BodyPartController.Parts.LeftHand, "The {1} pierces {0}'s {2}, slicing the flesh of the front left paw" },
            {BodyPartController.Parts.RightHand, "The {1} pierces {0}'s {2}, slicing the flesh of the front right paw!" },
            {BodyPartController.Parts.Chest, "The {1} pierces {0}'s {2}, slicing the flesh of the chest!" },
            {BodyPartController.Parts.Abdomin, "The {1} pierces {0}'s {2}, slicing the flesh of the belly!" },
            {BodyPartController.Parts.LeftLeg, "The {1} pierces {0}'s {2}, slicing the flesh of the back left leg!" },
            {BodyPartController.Parts.RightLeg, "The {1} pierces {0}'s {2}, slicing the flesh of the back right leg!" },
            {BodyPartController.Parts.LeftFoot, "The {1} pierces {0}'s {2}, slicing the flesh of the back left paw!" },
            {BodyPartController.Parts.RightFoot, "The {1} pierces {0}'s {2}, slicing the flesh of the back right paw!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //deep cut
        {
            {BodyPartController.Parts.Head, "The {1} cuts through {0}'s {2} and hacks deep into the head!" },
            {BodyPartController.Parts.Neck, "The {1} cuts through {0}'s {2} and hacks deep into the neck!"},
            {BodyPartController.Parts.LeftArm, "The {1} cuts through {0}'s {2} and hacks deep into the front left leg!" },
            {BodyPartController.Parts.RightArm, "The {1} cuts through {0}'s {2} and hacks deep into the front right leg!" },
            {BodyPartController.Parts.LeftHand, "The {1} cuts through {0}'s {2} and hacks deep into the front left paw" },
            {BodyPartController.Parts.RightHand, "The {1} cuts through {0}'s {2} and hacks deep into the front right paw!" },
            {BodyPartController.Parts.Chest, "The {1} cuts through {0}'s {2} and hacks deep into the chest!" },
            {BodyPartController.Parts.Abdomin, "The {1} cuts through {0}'s {2} and hacks deep into the belly!" },
            {BodyPartController.Parts.LeftLeg, "The {1} cuts through {0}'s {2} and hacks deep into the back left leg!" },
            {BodyPartController.Parts.RightLeg, "The {1} cuts through {0}'s {2} and hacks deep into the back right leg!" },
            {BodyPartController.Parts.LeftFoot, "The {1} cuts through {0}'s {2} and hacks deep into the back left paw!" },
            {BodyPartController.Parts.RightFoot, "The {1} cuts through {0}'s {2} and hacks deep into the back right paw!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //cut to bone
        {
            {BodyPartController.Parts.Head, "The {1} completely penetrates {0}'s {2}, cracking the skull!" },
            {BodyPartController.Parts.Neck, "The {1} completely penetrates the {2}, almost decapitating {0}!"},
            {BodyPartController.Parts.LeftArm, "The {1} completely penetrates {0}'s {2}, cutting down to the bone of the the front left leg!" },
            {BodyPartController.Parts.RightArm, "The {1} completely penetrates {0}'s {2}, cutting down to the bone of the the front right leg!" },
            {BodyPartController.Parts.LeftHand, "The {1} completely penetrates {0}'s {2}, cutting down to the bones of the the front left paw" },
            {BodyPartController.Parts.RightHand, "The {1} completely penetrates {0}'s {2}, cutting down to the bones of the the front right paw!" },
            {BodyPartController.Parts.Chest, "The {1} completely penetrates the {2} and cuts into {0}'s the ribs!" },
            {BodyPartController.Parts.Abdomin, "The {1} completely penetrates {0}'s {2}, and slices the belly wide open!" },
            {BodyPartController.Parts.LeftLeg, "The {1} completely penetrates {0}'s {2}, cutting down to the bone of the the back left leg!" },
            {BodyPartController.Parts.RightLeg, "The {1} completely penetrates {0}'s {2}, cutting down to the bone of the the back right leg!" },
            {BodyPartController.Parts.LeftFoot, "The {1} completely penetrates {0}'s {2}, cutting down to the bones of the the back left paw!" },
            {BodyPartController.Parts.RightFoot, "The {1} completely penetrates {0}'s {2}, cutting down to the bones of the the back right paw!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //shatters bone
        {
            {BodyPartController.Parts.Head, "The {1} easily carves into {0}'s {2} and shatters the skull!" },
            {BodyPartController.Parts.Neck, "The {1} easily carves into the {0}'s {2}, hacking into the spine of the neck!"},
            {BodyPartController.Parts.LeftArm, "The {1} easily carves into {0}'s {2}, completely shatttering the bone of the the front left leg!" },
            {BodyPartController.Parts.RightArm, "The {1} easily carves into {0}'s {2}, completely shatttering the bone of the the front right leg!" },
            {BodyPartController.Parts.LeftHand, "The {1} easily carves into {0}'s {2}, completely shatttering the bones of the the front left paw" },
            {BodyPartController.Parts.RightHand, "The {1} easily carves into {0}'s {2}, completely shatttering the bones of the the front right paw!" },
            {BodyPartController.Parts.Chest, "The {1} easily carves into {0}'s {2} completely shatters the ribs!" },
            {BodyPartController.Parts.Abdomin, "The {1} easily carves into {0}'s {2}, scrambling his innards with the blade!" },
            {BodyPartController.Parts.LeftLeg, "The {1} easily carves into {0}'s {2}, completely shatttering the bone of the the back left leg!" },
            {BodyPartController.Parts.RightLeg, "The {1} easily carves into {0}'s {2}, completely shatttering the bone of the the back right leg!" },
            {BodyPartController.Parts.LeftFoot, "The {1} easily carves into {0}'s {2}, completely shatttering the bones of the the back left paw!" },
            {BodyPartController.Parts.RightFoot, "The {1} easily carves into {0}'s {2}, completely shatttering the bones of the the back right paw!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //sever
        {
            {BodyPartController.Parts.Head, "The {1} effortlessly slices through {0}'s {2}, the blade embeds itself deep into the skull!" },
            {BodyPartController.Parts.Neck, "The {1} effortlessly slices through the {2}, {0}'s head flies clean off!"},
            {BodyPartController.Parts.LeftArm, "The {1} effortlessly slices through {0}'s {2}, severing the front left leg!" },
            {BodyPartController.Parts.RightArm, "The {1} effortlessly slices through {0}'s {2}, severing the front right leg!" },
            {BodyPartController.Parts.LeftHand, "The {1} effortlessly slices through {0}'s {2}, severing the front left paw" },
            {BodyPartController.Parts.RightHand, "The {1} effortlessly slices through {0}'s {2}, severing the front right paw!" },
            {BodyPartController.Parts.Chest, "The {1} effortlessly slices through the {2} and finds itself deeply ebedded in {0}'s chest!" },
            {BodyPartController.Parts.Abdomin, "The {1} effortlessly slices through {0}'s {2} and deep into the belly, {0}'s innards spill to the floor!" },
            {BodyPartController.Parts.LeftLeg, "The {1} effortlessly slices through {0}'s {2}, severing the back left leg!" },
            {BodyPartController.Parts.RightLeg, "The {1} effortlessly slices through {0}'s {2}, severing the back right leg!" },
            {BodyPartController.Parts.LeftFoot, "The {1} effortlessly slices through {0}'s {2}, severing the back left paw!" },
            {BodyPartController.Parts.RightFoot, "The {1} effortlessly slices through {0}'s {2}, severing the back right paw!" },
        }
    };

    private static Dictionary<BodyPartController.Parts, string>[] impactInjuries = new Dictionary<BodyPartController.Parts, string>[] {
        //IMPACT
        new Dictionary<BodyPartController.Parts, string>() //light bruise
        {
            {BodyPartController.Parts.Head, "The force of the {1} dents {0}'s {2}, leaving small bump on the head!" },
            {BodyPartController.Parts.Neck, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the neck!" },
            {BodyPartController.Parts.LeftArm, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the front left leg!" },
            {BodyPartController.Parts.RightArm, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the front right leg!" },
            {BodyPartController.Parts.LeftHand, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the front left paw!" },
            {BodyPartController.Parts.RightHand, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the front right paw!" },
            {BodyPartController.Parts.Chest, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the chest!" },
            {BodyPartController.Parts.Abdomin, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the belly!" },           
            {BodyPartController.Parts.LeftLeg, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the back left leg!" },
            {BodyPartController.Parts.RightLeg, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the back right leg!" },
            {BodyPartController.Parts.LeftFoot, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the back left paw!" },
            {BodyPartController.Parts.RightFoot, "The force of the {1} dents {0}'s {2}, leaving a light bruise on the back right paw!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //bad bruise
        {
            {BodyPartController.Parts.Head, "The force of the {1} dents {0}'s {2}, leaving large bump on the head!" },
            {BodyPartController.Parts.Neck, "The force of the {1} dents {0}'s {2}, bruising the neck!" },
            {BodyPartController.Parts.LeftArm, "The force of the {1} dents {0}'s {2}, bruising the front left leg!" },
            {BodyPartController.Parts.RightArm, "The force of the {1} dents {0}'s {2}, bruising the front right leg!" },
            {BodyPartController.Parts.LeftHand, "The force of the {1} dents {0}'s {2}, bruising the front left paw!" },
            {BodyPartController.Parts.RightHand, "The force of the {1} dents {0}'s {2}, bruising the front right paw!" },
            {BodyPartController.Parts.Chest, "The force of the {1} dents {0}'s {2}, bruising the chest!" },
            {BodyPartController.Parts.Abdomin, "The force of the {1} dents {0}'s {2}, brusing the belly!" },
            {BodyPartController.Parts.LeftLeg, "The force of the {1} dents {0}'s {2}, bruising the back left leg!" },
            {BodyPartController.Parts.RightLeg, "The force of the {1} dents {0}'s {2}, bruising the back right leg!" },
            {BodyPartController.Parts.LeftFoot, "The force of the {1} dents {0}'s {2}, bruising the back left paw!" },
            {BodyPartController.Parts.RightFoot, "The force of the {1} dents {0}'s {2}, bruising the back right paw!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //heavy bruise
        {
            {BodyPartController.Parts.Head, "The force of the {1} slams into {0}'s {2}, rattling the brain!" },
            {BodyPartController.Parts.Neck, "The force of the {1} slams into {0}'s {2}, heavily bruising the neck!" },
            {BodyPartController.Parts.LeftArm, "The force of the {1} slams into {0}'s {2}, heavily bruising the front left leg!" },
            {BodyPartController.Parts.RightArm, "The force of the {1} slams into {0}'s {2}, heavily bruising the front right leg!" },
            {BodyPartController.Parts.LeftHand, "The force of the {1} slams into {0}'s {2}, heavily bruising the front left paw!" },
            {BodyPartController.Parts.RightHand, "The force of the {1} slams into {0}'s {2}, heavily bruising the front right paw!" },
            {BodyPartController.Parts.Chest, "The force of the {1} slams into {0}'s {2}, heavily bruising the chest!" },
            {BodyPartController.Parts.Abdomin, "The force of the {1} slams into {0}'s {2}, heavily bruising the belly!" },
            {BodyPartController.Parts.LeftLeg, "The force of the {1} slams into {0}'s {2}, heavily bruising the back left leg!" },
            {BodyPartController.Parts.RightLeg, "The force of the {1} slams into {0}'s {2}, heavily bruising the back right leg!" },
            {BodyPartController.Parts.LeftFoot, "The force of the {1} slams into {0}'s {2}, heavily bruising the back left paw!" },
            {BodyPartController.Parts.RightFoot, "The force of the {1} slams into {0}'s {2}, heavily bruising the back right paw!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //pulverize meat
        {
            {BodyPartController.Parts.Head, "The force of the {1} crumples {0}'s {2}, cracking the skull!" },
            {BodyPartController.Parts.Neck, "The force of the {1} crumples {0}'s {2} and pulverizes the meat of the neck!" },
            {BodyPartController.Parts.LeftArm, "The force of the {1} crumples {0}'s {2}, pulverizing the meat of the front left leg!" },
            {BodyPartController.Parts.RightArm, "The force of the {1} crumples {0}'s {2}, pulverizing the meat of the front right leg!" },
            {BodyPartController.Parts.LeftHand, "The force of the {1} crumples {0}'s {2}, cracking the bones in the front left paw!" },
            {BodyPartController.Parts.RightHand, "The force of the {1} crumples {0}'s {2}, cracking the bones in the front right paw!" },
            {BodyPartController.Parts.Chest, "The force of the {1} crumples {0}'s {2}, cracking the ribs!" },
            {BodyPartController.Parts.Abdomin, "The force of the {1} crumples {0}'s {2}, brusing the guts!" },
            {BodyPartController.Parts.LeftLeg, "The force of the {1} crumples {0}'s {2}, pulverizing the meat of the back left leg!" },
            {BodyPartController.Parts.RightLeg, "The force of the {1} crumples {0}'s {2}, pulverizing the meat of the back right leg!" },
            {BodyPartController.Parts.LeftFoot, "The force of the {1} crumples {0}'s {2}, cracking the bones in the back left paw!" },
            {BodyPartController.Parts.RightFoot, "The force of the {1} crumples {0}'s {2}, cracking the bones in the back right paw!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //bone shattering crush
        {
            {BodyPartController.Parts.Head, "The force of the {1} crushes {0}'s {2}, caving in the head!" },
            {BodyPartController.Parts.Neck, "The force of the {1} crushes {0}'s {2} a!ong with the windpipe!" },
            {BodyPartController.Parts.LeftArm, "The force of the {1} crushes {0}'s {2}, the impact shatters the bones of the front left leg!" },
            {BodyPartController.Parts.RightArm, "The force of the {1} crushes {0}'s {2}, the impact shatters the bones of the front right leg!" },
            {BodyPartController.Parts.LeftHand, "The force of the {1} crushes {0}'s {2}, shattering the bones of the front left paw!" },
            {BodyPartController.Parts.RightHand, "The force of the {1} crushes {0}'s {2}, shattering the bones of the front right paw!" },
            {BodyPartController.Parts.Chest, "The force of the {1} crushes {0}'s {2}, shattering the ribs and puncturing the lungs!" },
            {BodyPartController.Parts.Abdomin, "The force of the {1} crushes {0}'s {2}, damaging the innards!" },
            {BodyPartController.Parts.LeftLeg, "The force of the {1} crushes {0}'s {2}, the impact shatters the bones of the back left leg!" },
            {BodyPartController.Parts.RightLeg, "The force of the {1} crushes {0}'s {2}, the impact shatters the bones of the back right leg!" },
            {BodyPartController.Parts.LeftFoot, "The force of the {1} crushes {0}'s {2}, shattering the bones of the back left paw!" },
            {BodyPartController.Parts.RightFoot, "The force of the {1} crushes {0}'s {2}, shattering the bones of the back right paw!" },
        },
        new Dictionary<BodyPartController.Parts, string>() //obliterates into mess
        {
            {BodyPartController.Parts.Head, "The force of the {1} smashes completely through {0}'s {2}, obliterating the head!" },
            {BodyPartController.Parts.Neck, "The force of the {1} smashes completely through {0}'s {2}, partially decapitating the head!" },
            {BodyPartController.Parts.LeftArm, "The force of the {1} smashes completely through {0}'s {2}, the front left leg has been rendered totally useless!" },
            {BodyPartController.Parts.RightArm, "The force of the {1} smashes completely through {0}'s {2}, the front right leg has been rendered totally useless!" },
            {BodyPartController.Parts.LeftHand, "The force of the {1} smashes completely through {0}'s {2}, the front left paw has been mangled beyond recognition!" },
            {BodyPartController.Parts.RightHand, "The force of the {1} smashes completely through {0}'s {2}, the front right paw has been mangled beyond recognition!" },
            {BodyPartController.Parts.Chest, "The force of the {1} smashes completely through {0}'s {2}, obliterating the chest and exploding the heart!" },
            {BodyPartController.Parts.Abdomin, "The force of the {1} smashes completely through {0}'s {2}, pulverizing the innards!" },
            {BodyPartController.Parts.LeftLeg, "The force of the {1} smashes completely through {0}'s {2}, the back left leg has been rendered totally useless!" },
            {BodyPartController.Parts.RightLeg, "The force of the {1} smashes completely through {0}'s {2}, the back right leg has been rendered totally useless!" },
            {BodyPartController.Parts.LeftFoot, "The force of the {1} smashes completely through {0}'s {2}, the back left paw has been mangled beyond recognition!" },
            {BodyPartController.Parts.RightFoot, "The force of the {1} smashes completely through {0}'s {2}, the back right paw has been mangled beyond recognition!" },
        },
    };
    */
}
