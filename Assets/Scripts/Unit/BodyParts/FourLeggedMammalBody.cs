﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourLeggedMammalBody : Body {
    List<BodyPart> bodyParts = new List<BodyPart>();
    public override List<BodyPart> GetBodyParts()
    {
        //make sure bodyparts haven't been loaded already before creating all new ones
        if (bodyParts.Count == 0)
        {
            bodyParts.Add(Head());
            bodyParts.Add(Neck());
            bodyParts.Add(Chest());
            bodyParts.Add(Abdomin());
            bodyParts.Add(FrontLeftLeg());
            bodyParts.Add(FrontRightLeg());
            bodyParts.Add(FrontLeftPaw());
            bodyParts.Add(FrontRightPaw());
            bodyParts.Add(BackLeftLeg());
            bodyParts.Add(BackRightLeg());
            bodyParts.Add(BackLeftPaw());
            bodyParts.Add(BackRightPaw());
        }
        return bodyParts;
    }

    private BodyPart Head()
    {
        BodyPart head = gameObject.AddComponent<BodyPart>();
        
        head.armorSlot = Item.EquipmentSlot.Head; //put this before callback is assigned in base class
        head.name = "head";
        head.functioningLimit = 5;
        head.knockoutThreshold = 3;
        head.vomitThreshold = 3;
        head.rockedThreshold = 2;
        head.downedThreshold = 2;
        head.fearLimit = 3;
        head.isVitalPart = true;

        head.stabInjuries[0] = "The point of {2}'s {1} pokes at the skin of {0}'s " + head.name + "!";
        head.stabInjuries[1] = "The point of {2}'s {1} leaves a decent gash flesh of {0}'s " + head.name + "!";
        head.stabInjuries[2] = "The point of {2}'s {1} jabs into {0}'s " + head.name + ", the neck is pulled with the force";
        head.stabInjuries[3] = "The point of {2}'s {1} slams into the " + head.name + " and cracks {0}'s skull!";
        head.stabInjuries[4] = "The blade of {2}'s {1} jams halfway into {0}'s skull!";
        head.stabInjuries[5] = "The blade of {2}'s {1} pierces completely through {0}'s " + head.name + "!";

        head.bluntInjuries[0] = "The force of {2}'s {1} leaves a light bruise on {0}'s " + head.name + "!";
        head.bluntInjuries[1] = "The force of {2}'s {1} bruises {0}'s " + head.name + "!";
        head.bluntInjuries[2] = "The force of {2}'s {1} heavily bruises {0}'s " + head.name + "!";
        head.bluntInjuries[3] = "The force of {2}'s {1} gives {0} a serious concussion";
        head.bluntInjuries[4] = "The force of {2}'s {1} cracks {0}'s skull!";
        head.bluntInjuries[5] = "The force of {2}'s {1} completely caves in {0}'s " + head.name + "!";

        head.clawInjuries[0] = "The {2}'s {1} scratches the skin of {0}'s " + head.name + "!";
        head.clawInjuries[1] = "The {2}'s {1} slashes the flesh of {0}'s " + head.name + "!";
        head.clawInjuries[2] = "The {2}'s {1} cuts into {0}'s " + head.name + ", the neck is pulled with the force!";
        head.clawInjuries[3] = "{2}'s {1} scrapes across {0}'s skull, leaving flesh dangling!";
        head.clawInjuries[4] = "The {2}'s {1} swipes half the flesh off {0}'s skull!";
        head.clawInjuries[5] = "The {2}'s {1} rips apart {0}'s " + head.name + "!";

        head.biteInjuries[0] = "The {2} nips at {0}'s " + head.name + "!";
        head.biteInjuries[1] = "The {2} bites down on {0}'s " + head.name + "!";
        head.biteInjuries[2] = "The {2} buries it's teeth into {0}'s " + head.name + "!";
        head.biteInjuries[3] = "The {2} bites down on the of flesh from {0}'s " + head.name + " and shakes!!";
        head.biteInjuries[4] = "The {2} rips off a chunk of flesh from {0's} " + head.name + "! with it's teeth!";
        head.biteInjuries[5] = "The {2} crushes {0}'s " + head.name + " with their jaws!";

        return head;
    }

    private BodyPart Neck()
    {
        BodyPart neck = gameObject.AddComponent<BodyPart>();

        neck.name = "neck";
        neck.functioningLimit = 5;
        neck.downedThreshold = 3;
        neck.cantBreathThreshold = 2;
        neck.suffocationThreshold = 3;
        neck.fearLimit = 3;

        neck.bleedBonus = 2f;

        neck.stabInjuries[0] = "The point of {2}'s {1} pokes into the flesh of {0}'s " + neck.name + "!";
        neck.stabInjuries[1] = "The point of {2}'s {1} stabs into {0}'s " + neck.name + "!";
        neck.stabInjuries[2] = "The point of {2}'s {1} stabs deeply into {0}'s " + neck.name + "";
        neck.stabInjuries[3] = "The blade of {2}'s {1} pierces completely through {0}'s " + neck.name + "!";
        neck.stabInjuries[4] = "The blade of {2}'s {1} pierces completely through {0}'s " + neck.name + "!";
        neck.stabInjuries[5] = "The blade of {2}'s {1} pierces completely through {0}'s " + neck.name + "!";

        neck.bluntInjuries[0] = "The force of {2}'s {1} bruises {0}'s " + neck.name + "!";
        neck.bluntInjuries[1] = "The force of {2}'s {1} heavily bruises {0}'s " + neck.name + "!";
        neck.bluntInjuries[2] = "The force of {2}'s {1} seriously bruises {0}'s " + neck.name + "!";
        neck.bluntInjuries[3] = "The force of {2}'s {1} crushes the meat of {0}'s " + neck.name + "";
        neck.bluntInjuries[4] = "The force of {2}'s {1} crumples part of {0}'s " + neck.name + "!";
        neck.bluntInjuries[5] = "The force of {2}'s {1} completely crushes {0}'s " + neck.name + "!";

        neck.clawInjuries[0] = "The {2}'s {1} scratches the skin of {0}'s " + neck.name + "!";
        neck.clawInjuries[1] = "The {2}'s {1} slashes the flesh of {0}'s " + neck.name + "!";
        neck.clawInjuries[2] = "The {2}'s {1} cuts into {0}'s " + neck.name + "!";
        neck.clawInjuries[3] = "{2}'s {1} slashes into {0}'s throat!";
        neck.clawInjuries[4] = "The {2}'s {1} rips a chunk off of {0}'s neck!";
        neck.clawInjuries[5] = "The {2}'s {1} rips apart {0}'s " + neck.name + "!";

        neck.biteInjuries[0] = "The {2} nips at {0}'s " + neck.name + "!";
        neck.biteInjuries[1] = "The {2} bites down into {0}'s " + neck.name + "!";
        neck.biteInjuries[2] = "The {2} buries it's teeth into {0}'s " + neck.name + "!";
        neck.biteInjuries[3] = "The {2} bites down on the of flesh from {0}'s " + neck.name + " and shakes!!";
        neck.biteInjuries[4] = "The {2} rips a chunk of flesh from {0's} " + neck.name + "! with it's teeth!";
        neck.biteInjuries[5] = "The {2} crushes {0}'s " + neck.name + " with their jaws!";

        return neck;
    }

    private BodyPart Chest()
    {
        BodyPart chest = gameObject.AddComponent<BodyPart>();

        chest.armorSlot = Item.EquipmentSlot.Chest; //put this before callback is assigned in base class
        chest.name = "chest";
        chest.functioningLimit = 4;
        chest.downedThreshold = 3;
        chest.cantBreathThreshold = 3;
        chest.suffocationThreshold = 4;
        chest.fearLimit = 3;

        chest.bluntInjuries[0] = "The force of {2}'s {1} leaves a light bruise on {0}'s " + chest.name + "!";
        chest.bluntInjuries[1] = "The force of {2}'s {1} bruises {0}'s " + chest.name + "!";
        chest.bluntInjuries[2] = "The force of {2}'s {1} heavily bruises {0}'s " + chest.name + "!";
        chest.bluntInjuries[3] = "The force of {2}'s {1} cracks the ribs of {0}'s " + chest.name + "";
        chest.bluntInjuries[4] = "The force of {2}'s {1} shatters the ribs of {0}'s " + chest.name + "!";
        chest.bluntInjuries[5] = "The force of {2}'s {1} completely caves in {0}'s " + chest.name + "!";

        chest.stabInjuries[0] = "The point of {2}'s {1} pokes at the skin of {0}'s " + chest.name + "!";
        chest.stabInjuries[1] = "The point of {2}'s {1} pokes into the flesh of {0}'s " + chest.name + "!";
        chest.stabInjuries[2] = "The point of {2}'s {1} tears through the muscle of {0}'s " + chest.name + "";
        chest.stabInjuries[3] = "The point of {2}'s {1} jams into the ribs {0}'s " + chest.name + "!";
        chest.stabInjuries[4] = "The blade of {2}'s {1} cuts through the ribs of {0}'s " + chest.name + "!";
        chest.stabInjuries[5] = "The blade of {2}'s {1} pierces completely through {0}'s " + chest.name + "!";

        chest.clawInjuries[0] = "The {2}'s {1} scratches the skin of {0}'s " + chest.name + "!";
        chest.clawInjuries[1] = "The {2}'s {1} slashes the flesh of {0}'s " + chest.name + "!";
        chest.clawInjuries[2] = "The {2}'s {1} cuts into {0}'s " + chest.name + " and scrapes across the ribs!";
        chest.clawInjuries[3] = "The {2}'s {1} hacks deep into {0}'s " + chest.name + ", cracking the ribs with the force!";
        chest.clawInjuries[4] = "The {2}'s {1} digs deep into {0}'s " + chest.name + " and tears off a large hunk of flesh!";
        chest.clawInjuries[5] = "The {2}'s {1} rips {0}'s " + chest.name + " wide open!";

        chest.biteInjuries[0] = "The {2} nips at {0}'s " + chest.name + "!";
        chest.biteInjuries[1] = "The {2} bites down into {0}'s " + chest.name + "!";
        chest.biteInjuries[2] = "The {2} bites down hard on the of flesh of{0}'s " + chest.name + "!";
        chest.biteInjuries[3] = "The {2} buries it's teeth into the muscle of {0}'s " + chest.name + " and shakes!!";
        chest.biteInjuries[4] = "The {2} tears a chunk of flesh from {0's} " + chest.name + "! with it's teeth!";
        chest.biteInjuries[5] = "The {2} rips open {0}'s " + chest.name + " with their jaws!";

        return chest;
    }

    private BodyPart Abdomin()
    {
        BodyPart abdomin = gameObject.AddComponent<BodyPart>();
        abdomin.armorSlot = Item.EquipmentSlot.Chest;
        abdomin.name = "abdomin";
        abdomin.functioningLimit = 4;
        abdomin.vomitThreshold = 3;
        abdomin.downedThreshold = 3;
        abdomin.cantBreathThreshold = 2;
        abdomin.fearLimit = 3;

        abdomin.bluntInjuries[0] = "The force of {2}'s {1} leaves a light bruise on {0}'s " + abdomin.name + "!";
        abdomin.bluntInjuries[1] = "The force of {2}'s {1} bruises {0}'s " + abdomin.name + "!";
        abdomin.bluntInjuries[2] = "The force of {2}'s {1} heavily bruises {0}'s " + abdomin.name + "!";
        abdomin.bluntInjuries[3] = "The force of {2}'s {1} bruises {0}'s guts";
        abdomin.bluntInjuries[4] = "The force of {2}'s {1} against {0}'s " + abdomin.name + " causes serious internal bleeding!";
        abdomin.bluntInjuries[5] = "The force of {2}'s {1} obliterates {0}'s innards!";

        abdomin.stabInjuries[0] = "The point of {2}'s {1} pokes at the skin of {0}'s " + abdomin.name + "!";
        abdomin.stabInjuries[1] = "The point of {2}'s {1} pokes into the flesh of {0}'s " + abdomin.name + "!";
        abdomin.stabInjuries[2] = "The point of {2}'s {1} tears through the muscle of {0}'s " + abdomin.name + "!";
        abdomin.stabInjuries[3] = "The point of {2}'s {1} pokes {0} in the guts!";
        abdomin.stabInjuries[4] = "The blade of {2}'s {1} cuts deep into {0}'s " + abdomin.name + "!";
        abdomin.stabInjuries[5] = "The blade of {2}'s {1} pierces completely through {0}'s " + abdomin.name + "!";

        abdomin.clawInjuries[0] = "The {2}'s {1} scratches the skin of {0}'s " + abdomin.name + "!";
        abdomin.clawInjuries[1] = "The {2}'s {1} slashes the flesh of {0}'s " + abdomin.name + "!";
        abdomin.clawInjuries[2] = "The {2}'s {1} cuts into {0}'s " + abdomin.name + " and scrapes across the ribs!";
        abdomin.clawInjuries[3] = "The {2}'s {1} hacks deep into {0}'s " + abdomin.name + ", slashing the innards!";
        abdomin.clawInjuries[4] = "The {2}'s {1} digs deep into {0}'s " + abdomin.name + " scooping at the guts!";
        abdomin.clawInjuries[5] = "The {2}'s {1} rips {0}'s " + abdomin.name + " wide open, spilling the innards onto the ground!";

        abdomin.biteInjuries[0] = "The {2} nips at {0}'s " + abdomin.name + "!";
        abdomin.biteInjuries[1] = "The {2} bites down into the skin of {0}'s " + abdomin.name + "!";
        abdomin.biteInjuries[2] = "The {2} bites down hard on the of flesh of{0}'s " + abdomin.name + "!";
        abdomin.biteInjuries[3] = "The {2} buries it's teeth into the muscle of {0}'s " + abdomin.name + " and shakes!";
        abdomin.biteInjuries[4] = "The {2} tears a chunk of meat from {0's} " + abdomin.name + "! with it's teeth!";
        abdomin.biteInjuries[5] = "The {2}'s teeth gnaw their way through the flesh and into the innards!";

        return abdomin;
    }

    private BodyPart FrontLeftLeg()
    {
        BodyPart FrontLeftLeg = gameObject.AddComponent<BodyPart>();

        FrontLeftLeg.armorSlot = Item.EquipmentSlot.Chest; //put this before callback is assigned in base class
        FrontLeftLeg.weaponSlot = Item.EquipmentSlot.OffHand;
        FrontLeftLeg.attack2 = true;
        FrontLeftLeg.name = "front left leg";
        FrontLeftLeg.functioningLimit = 4;
        FrontLeftLeg.vomitThreshold = 4;
        FrontLeftLeg.downedThreshold = 2;
        FrontLeftLeg.fearLimit = 4;

        FrontLeftLeg.bluntInjuries[0] = "The force of {2}'s {1} leaves a light bruise on {0}'s " + FrontLeftLeg.name + "!";
        FrontLeftLeg.bluntInjuries[1] = "The force of {2}'s {1} bruises {0}'s " + FrontLeftLeg.name + "!";
        FrontLeftLeg.bluntInjuries[2] = "The force of {2}'s {1} heavily bruises {0}'s " + FrontLeftLeg.name + "!";
        FrontLeftLeg.bluntInjuries[3] = "The force of {2}'s {1} cracking a bone in {0}'s " + FrontLeftLeg.name + "!";
        FrontLeftLeg.bluntInjuries[4] = "The force of {2}'s {1} crushes the bones of {0}'s " + FrontLeftLeg.name + "!";
        FrontLeftLeg.bluntInjuries[5] = "The force of {2}'s {1} completely pulverizes {0}'s " + FrontLeftLeg.name + "!";

        FrontLeftLeg.stabInjuries[0] = "{2}'s {1} pokes at the skin of {0}'s " + FrontLeftLeg.name + "!";
        FrontLeftLeg.stabInjuries[1] = "{2}'s {1} slices into flesh of {0}'s " + FrontLeftLeg.name + "!";
        FrontLeftLeg.stabInjuries[2] = "{2}'s {1} stabs into {0}'s " + FrontLeftLeg.name + "!";
        FrontLeftLeg.stabInjuries[3] = "{2}'s {1} stabs deep into {0}'s " + FrontLeftLeg.name + "!";
        FrontLeftLeg.stabInjuries[4] = "{2}'s {1} seriously gores {0}'s " + FrontLeftLeg.name + "!";
        FrontLeftLeg.stabInjuries[5] = "The blade of {2}'s {1} passes completely through {0}'s " + FrontLeftLeg.name + "!";

        FrontLeftLeg.clawInjuries[0] = "The {2}'s {1} scratches the skin of {0}'s " + FrontLeftLeg.name + "!";
        FrontLeftLeg.clawInjuries[1] = "The {2}'s {1} slashes the flesh of {0}'s " + FrontLeftLeg.name + "!";
        FrontLeftLeg.clawInjuries[2] = "The {2}'s {1} cuts into {0}'s " + FrontLeftLeg.name + " and scratches the bone!";
        FrontLeftLeg.clawInjuries[3] = "The {2}'s {1} hacks deep into {0}'s " + FrontLeftLeg.name + ", cutting away the muscle!";
        FrontLeftLeg.clawInjuries[4] = "The {2}'s {1} tears a chunk of flesh off of {0}'s " + FrontLeftLeg.name + "!";
        FrontLeftLeg.clawInjuries[5] = "The {2}'s {1} strips the meat from the bones of {0}'s " + FrontLeftLeg.name + "!";

        FrontLeftLeg.biteInjuries[0] = "The {2} nips at {0}'s " + FrontLeftLeg.name + "!";
        FrontLeftLeg.biteInjuries[1] = "The {2} bites down into the skin of {0}'s " + FrontLeftLeg.name + "!";
        FrontLeftLeg.biteInjuries[2] = "The {2} bites down hard on the of flesh of{0}'s " + FrontLeftLeg.name + "!";
        FrontLeftLeg.biteInjuries[3] = "The {2} tears a chunk of meat from {0's} " + FrontLeftLeg.name + "! with it's teeth!";
        FrontLeftLeg.biteInjuries[4] = "The {2}'s jaws grip the bones of {0}'s " + FrontLeftLeg.name + " and shakes out of it's socket!";
        FrontLeftLeg.biteInjuries[5] = "The {2}'s jaws tear {0}'s " + FrontLeftLeg.name + " completely off!";

        return FrontLeftLeg;
    }

    private BodyPart FrontRightLeg()
    {
        BodyPart FrontRightLeg = gameObject.AddComponent<BodyPart>();

        FrontRightLeg.armorSlot = Item.EquipmentSlot.Chest; //put this before callback is assigned in base class
        FrontRightLeg.weaponSlot = Item.EquipmentSlot.MainHand;
        FrontRightLeg.attack1 = true;
        FrontRightLeg.name = "front right leg";
        FrontRightLeg.functioningLimit = 4;
        FrontRightLeg.vomitThreshold = 4;
        FrontRightLeg.downedThreshold = 2;
        FrontRightLeg.fearLimit = 4;

        FrontRightLeg.bluntInjuries[0] = "The force of {2}'s {1} leaves a light bruise on {0}'s " + FrontRightLeg.name + "!";
        FrontRightLeg.bluntInjuries[1] = "The force of {2}'s {1} bruises {0}'s " + FrontRightLeg.name + "!";
        FrontRightLeg.bluntInjuries[2] = "The force of {2}'s {1} heavily bruises {0}'s " + FrontRightLeg.name + "!";
        FrontRightLeg.bluntInjuries[3] = "The force of {2}'s {1} cracking a bone in {0}'s " + FrontRightLeg.name + "!";
        FrontRightLeg.bluntInjuries[4] = "The force of {2}'s {1} crushes the bones of {0}'s " + FrontRightLeg.name + "!";
        FrontRightLeg.bluntInjuries[5] = "The force of {2}'s {1} completely pulverizes {0}'s " + FrontRightLeg.name + "!";

        FrontRightLeg.stabInjuries[0] = "{2}'s {1} pokes at the skin of {0}'s " + FrontRightLeg.name + "!";
        FrontRightLeg.stabInjuries[1] = "{2}'s {1} slices into flesh of {0}'s " + FrontRightLeg.name + "!";
        FrontRightLeg.stabInjuries[2] = "{2}'s {1} stabs into {0}'s " + FrontRightLeg.name + "!";
        FrontRightLeg.stabInjuries[3] = "{2}'s {1} stabs deep into {0}'s " + FrontRightLeg.name + "!";
        FrontRightLeg.stabInjuries[4] = "{2}'s {1} seriously gores {0}'s " + FrontRightLeg.name + "!";
        FrontRightLeg.stabInjuries[5] = "The blade of {2}'s {1} passes completely through {0}'s " + FrontRightLeg.name + "!";

        FrontRightLeg.clawInjuries[0] = "The {2}'s {1} scratches the skin of {0}'s " + FrontRightLeg.name + "!";
        FrontRightLeg.clawInjuries[1] = "The {2}'s {1} slashes the flesh of {0}'s " + FrontRightLeg.name + "!";
        FrontRightLeg.clawInjuries[2] = "The {2}'s {1} cuts into {0}'s " + FrontRightLeg.name + " and scratches the bone!";
        FrontRightLeg.clawInjuries[3] = "The {2}'s {1} hacks deep into {0}'s " + FrontRightLeg.name + ", cutting away the muscle!";
        FrontRightLeg.clawInjuries[4] = "The {2}'s {1} tears a chunk of flesh off of {0}'s " + FrontRightLeg.name + "!";
        FrontRightLeg.clawInjuries[5] = "The {2}'s {1} strips the meat from the bones of {0}'s " + FrontRightLeg.name + "!";

        FrontRightLeg.biteInjuries[0] = "The {2} nips at {0}'s " + FrontRightLeg.name + "!";
        FrontRightLeg.biteInjuries[1] = "The {2} bites down into the skin of {0}'s " + FrontRightLeg.name + "!";
        FrontRightLeg.biteInjuries[2] = "The {2} bites down hard on the of flesh of{0}'s " + FrontRightLeg.name + "!";
        FrontRightLeg.biteInjuries[3] = "The {2} tears a chunk of meat from {0's} " + FrontRightLeg.name + "! with it's teeth!";
        FrontRightLeg.biteInjuries[4] = "The {2}'s jaws grip the bones of {0}'s " + FrontRightLeg.name + " and shakes out of it's socket!";
        FrontRightLeg.biteInjuries[5] = "The {2}'s jaws tear {0}'s " + FrontRightLeg.name + " completely off!";

        return FrontRightLeg;
    }

    private BodyPart FrontLeftPaw()
    {
        BodyPart FrontLeftPaw = gameObject.AddComponent<BodyPart>();

        FrontLeftPaw.armorSlot = Item.EquipmentSlot.Hands; //put this before callback is assigned in base class
        FrontLeftPaw.weaponSlot = Item.EquipmentSlot.OffHand;
        FrontLeftPaw.name = "front left paw";
        FrontLeftPaw.functioningLimit = 4;
        FrontLeftPaw.vomitThreshold = 4;
        FrontLeftPaw.vomitThreshold = 3;
        FrontLeftPaw.fearLimit = 4;
        FrontLeftPaw.attack2 = true;

        FrontLeftPaw.bluntInjuries[0] = "The force of {2}'s {1} leaves a light bruise on {0}'s " + FrontLeftPaw.name + "!";
        FrontLeftPaw.bluntInjuries[1] = "The force of {2}'s {1} bruises {0}'s " + FrontLeftPaw.name + "!";
        FrontLeftPaw.bluntInjuries[2] = "The force of {2}'s {1} heavily bruises {0}'s " + FrontLeftPaw.name + "!";
        FrontLeftPaw.bluntInjuries[3] = "The force of {2}'s {1} cracks the bones in {0}'s " + FrontLeftPaw.name + "!";
        FrontLeftPaw.bluntInjuries[4] = "The force of {2}'s {1} crushes the bones of {0}'s " + FrontLeftPaw.name + "!";
        FrontLeftPaw.bluntInjuries[5] = "The force of {2}'s {1} completely obliterates {0}'s " + FrontLeftPaw.name + "!";

        FrontLeftPaw.stabInjuries[0] = "{2}'s {1} pokes at the skin of {0}'s " + FrontLeftPaw.name + "!";
        FrontLeftPaw.stabInjuries[1] = "{2}'s {1} slices at the skin of {0}'s " + FrontLeftPaw.name + "!";
        FrontLeftPaw.stabInjuries[2] = "{2}'s {1} stabs into {0}'s " + FrontLeftPaw.name + "!";
        FrontLeftPaw.stabInjuries[3] = "{2}'s {1} stabs completely through {0}'s " + FrontLeftPaw.name + "!";
        FrontLeftPaw.stabInjuries[4] = "{2}'s {1} fractures the bones in {0}'s " + FrontLeftPaw.name + "!";
        FrontLeftPaw.stabInjuries[5] = "{2}'s {1} completely mangles {0}'s " + FrontLeftPaw.name + "!";

        FrontLeftPaw.clawInjuries[0] = "The {2}'s {1} scratches the skin of {0}'s " + FrontLeftPaw.name + "!";
        FrontLeftPaw.clawInjuries[1] = "The {2}'s {1} slashes the flesh of {0}'s " + FrontLeftPaw.name + "!";
        FrontLeftPaw.clawInjuries[2] = "The {2}'s {1} cuts into {0}'s " + FrontLeftPaw.name + " and scrapes the bones!";
        FrontLeftPaw.clawInjuries[3] = "The {2}'s {1} hacks deep into {0}'s " + FrontLeftPaw.name + ", fracturing the bones!";
        FrontLeftPaw.clawInjuries[4] = "The {2}'s {1} tears a chunk of {0}'s " + FrontLeftPaw.name + " almost completely off!";
        FrontLeftPaw.clawInjuries[5] = "The {2}'s {1} completely mangles {0}'s " + FrontLeftPaw.name + "!";

        FrontLeftPaw.biteInjuries[0] = "The {2} nips at {0}'s " + FrontLeftPaw.name + "!";
        FrontLeftPaw.biteInjuries[1] = "The {2} bites down into the skin of {0}'s " + FrontLeftPaw.name + "!";
        FrontLeftPaw.biteInjuries[2] = "The {2} bites down hard into {0}'s " + FrontLeftPaw.name + "!";
        FrontLeftPaw.biteInjuries[3] = "The {2}'s bites deep into {0's} " + FrontLeftPaw.name + " and shakes!";
        FrontLeftPaw.biteInjuries[4] = "The {2}'s jaws rip off a part of " + FrontLeftPaw.name + "!";
        FrontLeftPaw.biteInjuries[5] = "The {2}'s jaws tear {0}'s " + FrontLeftPaw.name + " completely off!";
        return FrontLeftPaw;
    }

    private BodyPart FrontRightPaw()
    {
        BodyPart FrontRightPaw = gameObject.AddComponent<BodyPart>();

        FrontRightPaw.armorSlot = Item.EquipmentSlot.Hands; //put this before callback is assigned in base class
        FrontRightPaw.weaponSlot = Item.EquipmentSlot.MainHand;
        FrontRightPaw.name = "front right paw";
        FrontRightPaw.functioningLimit = 4;
        FrontRightPaw.vomitThreshold = 4;
        FrontRightPaw.vomitThreshold = 3;
        FrontRightPaw.fearLimit = 4;
        FrontRightPaw.attack1 = true;

        FrontRightPaw.bluntInjuries[0] = "The force of {2}'s {1} leaves a light bruise on {0}'s " + FrontRightPaw.name + "!";
        FrontRightPaw.bluntInjuries[1] = "The force of {2}'s {1} bruises {0}'s " + FrontRightPaw.name + "!";
        FrontRightPaw.bluntInjuries[2] = "The force of {2}'s {1} heavily bruises {0}'s " + FrontRightPaw.name + "!";
        FrontRightPaw.bluntInjuries[3] = "The force of {2}'s {1} cracks the bones in {0}'s " + FrontRightPaw.name + "!";
        FrontRightPaw.bluntInjuries[4] = "The force of {2}'s {1} crushes the bones of {0}'s " + FrontRightPaw.name + "!";
        FrontRightPaw.bluntInjuries[5] = "The force of {2}'s {1} completely obliterates {0}'s " + FrontRightPaw.name + "!";

        FrontRightPaw.stabInjuries[0] = "{2}'s {1} pokes at the skin of {0}'s " + FrontRightPaw.name + "!";
        FrontRightPaw.stabInjuries[1] = "{2}'s {1} slices at the skin of {0}'s " + FrontRightPaw.name + "!";
        FrontRightPaw.stabInjuries[2] = "{2}'s {1} stabs into {0}'s " + FrontRightPaw.name + "!";
        FrontRightPaw.stabInjuries[3] = "{2}'s {1} stabs completely through {0}'s " + FrontRightPaw.name + "!";
        FrontRightPaw.stabInjuries[4] = "{2}'s {1} fractures the bones in {0}'s " + FrontRightPaw.name + "!";
        FrontRightPaw.stabInjuries[5] = "{2}'s {1} completely mangles {0}'s " + FrontRightPaw.name + "!";

        FrontRightPaw.clawInjuries[0] = "The {2}'s {1} scratches the skin of {0}'s " + FrontRightPaw.name + "!";
        FrontRightPaw.clawInjuries[1] = "The {2}'s {1} slashes the flesh of {0}'s " + FrontRightPaw.name + "!";
        FrontRightPaw.clawInjuries[2] = "The {2}'s {1} cuts into {0}'s " + FrontRightPaw.name + " and scrapes the bones!";
        FrontRightPaw.clawInjuries[3] = "The {2}'s {1} hacks deep into {0}'s " + FrontRightPaw.name + ", fracturing the bones!";
        FrontRightPaw.clawInjuries[4] = "The {2}'s {1} tears a chunk of {0}'s " + FrontRightPaw.name + " almost completely off!";
        FrontRightPaw.clawInjuries[5] = "The {2}'s {1} completely mangles {0}'s " + FrontRightPaw.name + "!";

        FrontRightPaw.biteInjuries[0] = "The {2} nips at {0}'s " + FrontRightPaw.name + "!";
        FrontRightPaw.biteInjuries[1] = "The {2} bites down into the skin of {0}'s " + FrontRightPaw.name + "!";
        FrontRightPaw.biteInjuries[2] = "The {2} bites down hard into {0}'s " + FrontRightPaw.name + "!";
        FrontRightPaw.biteInjuries[3] = "The {2}'s bites deep into {0's} " + FrontRightPaw.name + " and shakes!";
        FrontRightPaw.biteInjuries[4] = "The {2}'s jaws rip off a part of " + FrontRightPaw.name + "!";
        FrontRightPaw.biteInjuries[5] = "The {2}'s jaws tear {0}'s " + FrontRightPaw.name + " completely off!";

        return FrontRightPaw;
    }

    private BodyPart BackLeftLeg()
    {
        BodyPart backLeftLeg = gameObject.AddComponent<BodyPart>();

        backLeftLeg.armorSlot = Item.EquipmentSlot.Legs; //put this before callback is assigned in base class
        backLeftLeg.name = "back left leg";
        backLeftLeg.functioningLimit = 4;
        backLeftLeg.vomitThreshold = 4;
        backLeftLeg.downedThreshold = 2;
        backLeftLeg.fearLimit = 4;

        backLeftLeg.bluntInjuries[0] = "The force of {2}'s {1} leaves a light bruise on {0}'s " + backLeftLeg.name + "!";
        backLeftLeg.bluntInjuries[1] = "The force of {2}'s {1} bruises {0}'s " + backLeftLeg.name + "!";
        backLeftLeg.bluntInjuries[2] = "The force of {2}'s {1} heavily bruises {0}'s " + backLeftLeg.name + "!";
        backLeftLeg.bluntInjuries[3] = "The force of {2}'s {1} cracking a bone in {0}'s " + backLeftLeg.name + "!";
        backLeftLeg.bluntInjuries[4] = "The force of {2}'s {1} crushes the bones of {0}'s " + backLeftLeg.name + "!";
        backLeftLeg.bluntInjuries[5] = "The force of {2}'s {1} completely pulverizes {0}'s " + backLeftLeg.name + "!";

        backLeftLeg.stabInjuries[0] = "{2}'s {1} pokes at the skin of {0}'s " + backLeftLeg.name + "!";
        backLeftLeg.stabInjuries[1] = "{2}'s {1} slices into flesh of {0}'s " + backLeftLeg.name + "!";
        backLeftLeg.stabInjuries[2] = "{2}'s {1} stabs into {0}'s " + backLeftLeg.name + "!";
        backLeftLeg.stabInjuries[3] = "{2}'s {1} stabs deep into {0}'s " + backLeftLeg.name + "!";
        backLeftLeg.stabInjuries[4] = "{2}'s {1} seriously gores {0}'s " + backLeftLeg.name + "!";
        backLeftLeg.stabInjuries[5] = "The blade of {2}'s {1} passes completely through {0}'s " + backLeftLeg.name + "!";

        backLeftLeg.clawInjuries[0] = "The {2}'s {1} scratches the skin of {0}'s " + backLeftLeg.name + "!";
        backLeftLeg.clawInjuries[1] = "The {2}'s {1} slashes the flesh of {0}'s " + backLeftLeg.name + "!";
        backLeftLeg.clawInjuries[2] = "The {2}'s {1} cuts into {0}'s " + backLeftLeg.name + " and scratches the bone!";
        backLeftLeg.clawInjuries[3] = "The {2}'s {1} hacks deep into {0}'s " + backLeftLeg.name + ", cutting away the muscle!";
        backLeftLeg.clawInjuries[4] = "The {2}'s {1} tears a chunk of flesh off of {0}'s " + backLeftLeg.name + "!";
        backLeftLeg.clawInjuries[5] = "The {2}'s {1} strips the meat from the bones of {0}'s " + backLeftLeg.name + "!";

        backLeftLeg.biteInjuries[0] = "The {2} nips at {0}'s " + backLeftLeg.name + "!";
        backLeftLeg.biteInjuries[1] = "The {2} bites down into the skin of {0}'s " + backLeftLeg.name + "!";
        backLeftLeg.biteInjuries[2] = "The {2} bites down hard on the of flesh of{0}'s " + backLeftLeg.name + "!";
        backLeftLeg.biteInjuries[3] = "The {2} tears a chunk of meat from {0's} " + backLeftLeg.name + "! with it's teeth!";
        backLeftLeg.biteInjuries[4] = "The {2}'s jaws grip the bones of {0}'s " + backLeftLeg.name + " and shakes out of it's socket!";
        backLeftLeg.biteInjuries[5] = "The {2}'s jaws tear {0}'s " + backLeftLeg.name + " completely off!";

        return backLeftLeg;
    }

    private BodyPart BackRightLeg()
    {
        BodyPart backRightLeg = gameObject.AddComponent<BodyPart>();

        backRightLeg.armorSlot = Item.EquipmentSlot.Legs; //put this before callback is assigned in base class
        backRightLeg.name = "back right leg";
        backRightLeg.functioningLimit = 4;
        backRightLeg.vomitThreshold = 4;
        backRightLeg.downedThreshold = 2;
        backRightLeg.fearLimit = 4;

        backRightLeg.bluntInjuries[0] = "The force of {2}'s {1} leaves a light bruise on {0}'s " + backRightLeg.name + "!";
        backRightLeg.bluntInjuries[1] = "The force of {2}'s {1} bruises {0}'s " + backRightLeg.name + "!";
        backRightLeg.bluntInjuries[2] = "The force of {2}'s {1} heavily bruises {0}'s " + backRightLeg.name + "!";
        backRightLeg.bluntInjuries[3] = "The force of {2}'s {1} cracking a bone in {0}'s " + backRightLeg.name + "!";
        backRightLeg.bluntInjuries[4] = "The force of {2}'s {1} crushes the bones of {0}'s " + backRightLeg.name + "!";
        backRightLeg.bluntInjuries[5] = "The force of {2}'s {1} completely pulverizes {0}'s " + backRightLeg.name + "!";

        backRightLeg.stabInjuries[0] = "{2}'s {1} pokes at the skin of {0}'s " + backRightLeg.name + "!";
        backRightLeg.stabInjuries[1] = "{2}'s {1} slices into flesh of {0}'s " + backRightLeg.name + "!";
        backRightLeg.stabInjuries[2] = "{2}'s {1} stabs into {0}'s " + backRightLeg.name + "!";
        backRightLeg.stabInjuries[3] = "{2}'s {1} stabs deep into {0}'s " + backRightLeg.name + "!";
        backRightLeg.stabInjuries[4] = "{2}'s {1} seriously gores {0}'s " + backRightLeg.name + "!";
        backRightLeg.stabInjuries[5] = "The blade of {2}'s {1} passes completely through {0}'s " + backRightLeg.name + "!";

        backRightLeg.clawInjuries[0] = "The {2}'s {1} scratches the skin of {0}'s " + backRightLeg.name + "!";
        backRightLeg.clawInjuries[1] = "The {2}'s {1} slashes the flesh of {0}'s " + backRightLeg.name + "!";
        backRightLeg.clawInjuries[2] = "The {2}'s {1} cuts into {0}'s " + backRightLeg.name + " and scratches the bone!";
        backRightLeg.clawInjuries[3] = "The {2}'s {1} hacks deep into {0}'s " + backRightLeg.name + ", cutting away the muscle!";
        backRightLeg.clawInjuries[4] = "The {2}'s {1} tears a chunk of flesh off of {0}'s " + backRightLeg.name + "!";
        backRightLeg.clawInjuries[5] = "The {2}'s {1} strips the meat from the bones of {0}'s " + backRightLeg.name + "!";

        backRightLeg.biteInjuries[0] = "The {2} nips at {0}'s " + backRightLeg.name + "!";
        backRightLeg.biteInjuries[1] = "The {2} bites down into the skin of {0}'s " + backRightLeg.name + "!";
        backRightLeg.biteInjuries[2] = "The {2} bites down hard on the of flesh of{0}'s " + backRightLeg.name + "!";
        backRightLeg.biteInjuries[3] = "The {2} tears a chunk of meat from {0's} " + backRightLeg.name + "! with it's teeth!";
        backRightLeg.biteInjuries[4] = "The {2}'s jaws grip the bones of {0}'s " + backRightLeg.name + " and shakes out of it's socket!";
        backRightLeg.biteInjuries[5] = "The {2}'s jaws tear {0}'s " + backRightLeg.name + " completely off!";

        return backRightLeg;
    }

    private BodyPart BackLeftPaw()
    {
        BodyPart BackLeftPaw = gameObject.AddComponent<BodyPart>();

        BackLeftPaw.armorSlot = Item.EquipmentSlot.Feet; //put this before callback is assigned in base class
        BackLeftPaw.name = "back left paw";
        BackLeftPaw.functioningLimit = 4;
        BackLeftPaw.vomitThreshold = 4;
        BackLeftPaw.downedThreshold = 3;
        BackLeftPaw.fearLimit = 4;

        BackLeftPaw.bluntInjuries[0] = "The force of {2}'s {1} leaves a light bruise on {0}'s " + BackLeftPaw.name + "!";
        BackLeftPaw.bluntInjuries[1] = "The force of {2}'s {1} bruises {0}'s " + BackLeftPaw.name + "!";
        BackLeftPaw.bluntInjuries[2] = "The force of {2}'s {1} heavily bruises {0}'s " + BackLeftPaw.name + "!";
        BackLeftPaw.bluntInjuries[3] = "The force of {2}'s {1} cracks the bones in {0}'s " + BackLeftPaw.name + "!";
        BackLeftPaw.bluntInjuries[4] = "The force of {2}'s {1} crushes the bones of {0}'s " + BackLeftPaw.name + "!";
        BackLeftPaw.bluntInjuries[5] = "The force of {2}'s {1} completely obliterates {0}'s " + BackLeftPaw.name + "!";

        BackLeftPaw.stabInjuries[0] = "{2}'s {1} pokes at the skin of {0}'s " + BackLeftPaw.name + "!";
        BackLeftPaw.stabInjuries[1] = "{2}'s {1} slices at the skin of {0}'s " + BackLeftPaw.name + "!";
        BackLeftPaw.stabInjuries[2] = "{2}'s {1} stabs into {0}'s " + BackLeftPaw.name + "!";
        BackLeftPaw.stabInjuries[3] = "{2}'s {1} stabs completely through {0}'s " + BackLeftPaw.name + "!";
        BackLeftPaw.stabInjuries[4] = "{2}'s {1} fractures the bones in {0}'s " + BackLeftPaw.name + " penetrates through";
        BackLeftPaw.stabInjuries[5] = "{2}'s {1} completely mangles {0}'s " + BackLeftPaw.name + "!";

        BackLeftPaw.clawInjuries[0] = "The {2}'s {1} scratches the skin of {0}'s " + BackLeftPaw.name + "!";
        BackLeftPaw.clawInjuries[1] = "The {2}'s {1} slashes the flesh of {0}'s " + BackLeftPaw.name + "!";
        BackLeftPaw.clawInjuries[2] = "The {2}'s {1} cuts into {0}'s " + BackLeftPaw.name + " and scrapes the bones!";
        BackLeftPaw.clawInjuries[3] = "The {2}'s {1} hacks deep into {0}'s " + BackLeftPaw.name + ", fracturing the bones!";
        BackLeftPaw.clawInjuries[4] = "The {2}'s {1} tears a chunk of {0}'s " + BackLeftPaw.name + " almost completely off!";
        BackLeftPaw.clawInjuries[5] = "The {2}'s {1} completely mangles {0}'s " + BackLeftPaw.name + "!";

        BackLeftPaw.biteInjuries[0] = "The {2} nips at {0}'s " + BackLeftPaw.name + "!";
        BackLeftPaw.biteInjuries[1] = "The {2} bites down into the skin of {0}'s " + BackLeftPaw.name + "!";
        BackLeftPaw.biteInjuries[2] = "The {2} bites down hard into {0}'s " + BackLeftPaw.name + "!";
        BackLeftPaw.biteInjuries[3] = "The {2}'s bites deep into {0's} " + BackLeftPaw.name + " and shakes!";
        BackLeftPaw.biteInjuries[4] = "The {2}'s jaws rip off a part of " + BackLeftPaw.name + "!";
        BackLeftPaw.biteInjuries[5] = "The {2}'s jaws tear {0}'s " + BackLeftPaw.name + " completely off!";

        return BackLeftPaw;
    }

    private BodyPart BackRightPaw()
    {
        BodyPart BackRightPaw = gameObject.AddComponent<BodyPart>();

        BackRightPaw.armorSlot = Item.EquipmentSlot.Feet; //put this before callback is assigned in base class
        BackRightPaw.name = "back right paw";
        BackRightPaw.functioningLimit = 4;
        BackRightPaw.vomitThreshold = 4;
        BackRightPaw.downedThreshold = 3;
        BackRightPaw.fearLimit = 4;

        BackRightPaw.bluntInjuries[0] = "The force of {2}'s {1} leaves a light bruise on {0}'s " + BackRightPaw.name + "!";
        BackRightPaw.bluntInjuries[1] = "The force of {2}'s {1} bruises {0}'s " + BackRightPaw.name + "!";
        BackRightPaw.bluntInjuries[2] = "The force of {2}'s {1} heavily bruises {0}'s " + BackRightPaw.name + "!";
        BackRightPaw.bluntInjuries[3] = "The force of {2}'s {1} cracks the bones in {0}'s " + BackRightPaw.name + "!";
        BackRightPaw.bluntInjuries[4] = "The force of {2}'s {1} crushes the bones of {0}'s " + BackRightPaw.name + "!";
        BackRightPaw.bluntInjuries[5] = "The force of {2}'s {1} completely obliterates {0}'s " + BackRightPaw.name + "!";

        BackRightPaw.stabInjuries[0] = "{2}'s {1} pokes at the skin of {0}'s " + BackRightPaw.name + "!";
        BackRightPaw.stabInjuries[1] = "{2}'s {1} slices at the skin of {0}'s " + BackRightPaw.name + "!";
        BackRightPaw.stabInjuries[2] = "{2}'s {1} stabs into {0}'s " + BackRightPaw.name + "!";
        BackRightPaw.stabInjuries[3] = "{2}'s {1} stabs completely through {0}'s " + BackRightPaw.name + "!";
        BackRightPaw.stabInjuries[4] = "{2}'s {1} fractures the bones in {0}'s " + BackRightPaw.name + " penetrates through";
        BackRightPaw.stabInjuries[5] = "{2}'s {1} completely mangles {0}'s " + BackRightPaw.name + "!";

        BackRightPaw.clawInjuries[0] = "The {2}'s {1} scratches the skin of {0}'s " + BackRightPaw.name + "!";
        BackRightPaw.clawInjuries[1] = "The {2}'s {1} slashes the flesh of {0}'s " + BackRightPaw.name + "!";
        BackRightPaw.clawInjuries[2] = "The {2}'s {1} cuts into {0}'s " + BackRightPaw.name + " and scrapes the bones!";
        BackRightPaw.clawInjuries[3] = "The {2}'s {1} hacks deep into {0}'s " + BackRightPaw.name + ", fracturing the bones!";
        BackRightPaw.clawInjuries[4] = "The {2}'s {1} tears a chunk of {0}'s " + BackRightPaw.name + " almost completely off!";
        BackRightPaw.clawInjuries[5] = "The {2}'s {1} completely mangles {0}'s " + BackRightPaw.name + "!";

        BackRightPaw.biteInjuries[0] = "The {2} nips at {0}'s " + BackRightPaw.name + "!";
        BackRightPaw.biteInjuries[1] = "The {2} bites down into the skin of {0}'s " + BackRightPaw.name + "!";
        BackRightPaw.biteInjuries[2] = "The {2} bites down hard into {0}'s " + BackRightPaw.name + "!";
        BackRightPaw.biteInjuries[3] = "The {2}'s bites deep into {0's} " + BackRightPaw.name + " and shakes!";
        BackRightPaw.biteInjuries[4] = "The {2}'s jaws rip off a part of " + BackRightPaw.name + "!";
        BackRightPaw.biteInjuries[5] = "The {2}'s jaws tear {0}'s " + BackRightPaw.name + " completely off!";

        return BackRightPaw;
    }
}
