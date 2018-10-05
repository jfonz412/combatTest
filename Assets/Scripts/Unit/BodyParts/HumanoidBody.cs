using System.Collections.Generic;
using UnityEngine;

public class HumanoidBody : Body
{
    List<BodyPart> bodyParts = new List<BodyPart>();

    public override List<BodyPart> GetBodyParts()
    {
        //make sure bodyparts haven't been loaded already before creating all new ones
        if(bodyParts.Count == 0)
        {
            bodyParts.Add(Head());
            bodyParts.Add(Neck());
            bodyParts.Add(Chest());
            bodyParts.Add(Abdomin());
            bodyParts.Add(LeftArm());
            bodyParts.Add(RightArm());
            bodyParts.Add(LeftHand());
            bodyParts.Add(RightHand());
            bodyParts.Add(LeftLeg());
            bodyParts.Add(RightLeg());
            bodyParts.Add(LeftFoot());
            bodyParts.Add(RightFoot());
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
        neck.cantBreathThreshold = 1;
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

    private BodyPart LeftArm()
    {
        BodyPart leftArm = gameObject.AddComponent<BodyPart>();

        leftArm.armorSlot = Item.EquipmentSlot.Chest; //put this before callback is assigned in base class
        leftArm.weaponSlot = Item.EquipmentSlot.OffHand;
        leftArm.attack2 = true;
        leftArm.name = "left arm";
        leftArm.functioningLimit = 4;
        leftArm.vomitThreshold = 5;
        leftArm.downedThreshold = 3;
        leftArm.fearLimit = 4;

        leftArm.bluntInjuries[0] = "The force of {2}'s {1} leaves a light bruise on {0}'s " + leftArm.name + "!";
        leftArm.bluntInjuries[1] = "The force of {2}'s {1} bruises {0}'s " + leftArm.name + "!";
        leftArm.bluntInjuries[2] = "The force of {2}'s {1} heavily bruises {0}'s " + leftArm.name + "!";
        leftArm.bluntInjuries[3] = "The force of {2}'s {1} cracking a bone in {0}'s " + leftArm.name + "!";
        leftArm.bluntInjuries[4] = "The force of {2}'s {1} crushes the bones of {0}'s " + leftArm.name + "!";
        leftArm.bluntInjuries[5] = "The force of {2}'s {1} completely pulverizes {0}'s " + leftArm.name + "!";

        leftArm.stabInjuries[0] = "{2}'s {1} pokes at the skin of {0}'s " + leftArm.name + "!";
        leftArm.stabInjuries[1] = "{2}'s {1} slices into flesh of {0}'s " + leftArm.name + "!";
        leftArm.stabInjuries[2] = "{2}'s {1} stabs into {0}'s " + leftArm.name + "!";
        leftArm.stabInjuries[3] = "{2}'s {1} stabs deep into {0}'s " + leftArm.name + "!";
        leftArm.stabInjuries[4] = "{2}'s {1} seriously gores {0}'s " + leftArm.name + "!";
        leftArm.stabInjuries[5] = "The blade of {2}'s {1} passes completely through {0}'s " + leftArm.name + "!";

        leftArm.clawInjuries[0] = "The {2}'s {1} scratches the skin of {0}'s " + leftArm.name + "!";
        leftArm.clawInjuries[1] = "The {2}'s {1} slashes the flesh of {0}'s " + leftArm.name + "!";
        leftArm.clawInjuries[2] = "The {2}'s {1} cuts into {0}'s " + leftArm.name + " and scratches the bone!";
        leftArm.clawInjuries[3] = "The {2}'s {1} hacks deep into {0}'s " + leftArm.name + ", cutting away the muscle!";
        leftArm.clawInjuries[4] = "The {2}'s {1} tears a chunk of flesh off of {0}'s " + leftArm.name + "!";
        leftArm.clawInjuries[5] = "The {2}'s {1} strips the meat from the bones of {0}'s " + leftArm.name + "!";

        leftArm.biteInjuries[0] = "The {2} nips at {0}'s " + leftArm.name + "!";
        leftArm.biteInjuries[1] = "The {2} bites down into the skin of {0}'s " + leftArm.name + "!";
        leftArm.biteInjuries[2] = "The {2} bites down hard on the of flesh of{0}'s " + leftArm.name + "!";
        leftArm.biteInjuries[3] = "The {2} tears a chunk of meat from {0's} " + leftArm.name + "! with it's teeth!";
        leftArm.biteInjuries[4] = "The {2}'s jaws grip the bones of {0}'s " + leftArm.name + " and shakes out of it's socket!";
        leftArm.biteInjuries[5] = "The {2}'s jaws tear {0}'s " + leftArm.name + " completely off!";

        return leftArm;
    }

    private BodyPart RightArm()
    {
        BodyPart rightArm = gameObject.AddComponent<BodyPart>();

        rightArm.armorSlot = Item.EquipmentSlot.Chest; //put this before callback is assigned in base class
        rightArm.weaponSlot = Item.EquipmentSlot.MainHand;
        rightArm.attack1 = true;
        rightArm.name = "right arm";
        rightArm.functioningLimit = 4;
        rightArm.vomitThreshold = 5;
        rightArm.downedThreshold = 3;
        rightArm.fearLimit = 4;

        rightArm.bluntInjuries[0] = "The force of {2}'s {1} leaves a light bruise on {0}'s " + rightArm.name + "!";
        rightArm.bluntInjuries[1] = "The force of {2}'s {1} bruises {0}'s " + rightArm.name + "!";
        rightArm.bluntInjuries[2] = "The force of {2}'s {1} heavily bruises {0}'s " + rightArm.name + "!";
        rightArm.bluntInjuries[3] = "The force of {2}'s {1} cracking a bone in {0}'s " + rightArm.name + "!";
        rightArm.bluntInjuries[4] = "The force of {2}'s {1} crushes the bones of {0}'s " + rightArm.name + "!";
        rightArm.bluntInjuries[5] = "The force of {2}'s {1} completely pulverizes {0}'s " + rightArm.name + "!";

        rightArm.stabInjuries[0] = "{2}'s {1} pokes at the skin of {0}'s " + rightArm.name + "!";
        rightArm.stabInjuries[1] = "{2}'s {1} slices into flesh of {0}'s " + rightArm.name + "!";
        rightArm.stabInjuries[2] = "{2}'s {1} stabs into {0}'s " + rightArm.name + "!";
        rightArm.stabInjuries[3] = "{2}'s {1} stabs deep into {0}'s " + rightArm.name + "!";
        rightArm.stabInjuries[4] = "{2}'s {1} seriously gores {0}'s " + rightArm.name + "!";
        rightArm.stabInjuries[5] = "The blade of {2}'s {1} passes completely through {0}'s " + rightArm.name + "!";

        rightArm.clawInjuries[0] = "The {2}'s {1} scratches the skin of {0}'s " + rightArm.name + "!";
        rightArm.clawInjuries[1] = "The {2}'s {1} slashes the flesh of {0}'s " + rightArm.name + "!";
        rightArm.clawInjuries[2] = "The {2}'s {1} cuts into {0}'s " + rightArm.name + " and scratches the bone!";
        rightArm.clawInjuries[3] = "The {2}'s {1} hacks deep into {0}'s " + rightArm.name + ", cutting away the muscle!";
        rightArm.clawInjuries[4] = "The {2}'s {1} tears a chunk of flesh off of {0}'s " + rightArm.name + "!";
        rightArm.clawInjuries[5] = "The {2}'s {1} strips the meat from the bones of {0}'s " + rightArm.name + "!";

        rightArm.biteInjuries[0] = "The {2} nips at {0}'s " + rightArm.name + "!";
        rightArm.biteInjuries[1] = "The {2} bites down into the skin of {0}'s " + rightArm.name + "!";
        rightArm.biteInjuries[2] = "The {2} bites down hard on the of flesh of{0}'s " + rightArm.name + "!";
        rightArm.biteInjuries[3] = "The {2} tears a chunk of meat from {0's} " + rightArm.name + "! with it's teeth!";
        rightArm.biteInjuries[4] = "The {2}'s jaws grip the bones of {0}'s " + rightArm.name + " and shakes out of it's socket!";
        rightArm.biteInjuries[5] = "The {2}'s jaws tear {0}'s " + rightArm.name + " completely off!";

        return rightArm;
    }

    private BodyPart LeftHand()
    {
        BodyPart leftHand = gameObject.AddComponent<BodyPart>();

        leftHand.armorSlot = Item.EquipmentSlot.Hands; //put this before callback is assigned in base class
        leftHand.weaponSlot = Item.EquipmentSlot.OffHand;
        leftHand.name = "left hand";
        leftHand.functioningLimit = 4;
        leftHand.vomitThreshold = 4;
        leftHand.downedThreshold = 4;
        leftHand.attack2 = true;
        leftHand.fearLimit = 4;

        leftHand.bluntInjuries[0] = "The force of {2}'s {1} leaves a light bruise on {0}'s " + leftHand.name + "!";
        leftHand.bluntInjuries[1] = "The force of {2}'s {1} bruises {0}'s " + leftHand.name + "!";
        leftHand.bluntInjuries[2] = "The force of {2}'s {1} heavily bruises {0}'s " + leftHand.name + "!";
        leftHand.bluntInjuries[3] = "The force of {2}'s {1} cracks the bones in {0}'s " + leftHand.name + "!";
        leftHand.bluntInjuries[4] = "The force of {2}'s {1} crushes the bones of {0}'s " + leftHand.name + "!";
        leftHand.bluntInjuries[5] = "The force of {2}'s {1} completely obliterates {0}'s " + leftHand.name + "!";

        leftHand.stabInjuries[0] = "{2}'s {1} pokes at the skin of {0}'s " + leftHand.name + "!";
        leftHand.stabInjuries[1] = "{2}'s {1} slices at the skin of {0}'s " + leftHand.name + "!";
        leftHand.stabInjuries[2] = "{2}'s {1} stabs into {0}'s " + leftHand.name + "!";
        leftHand.stabInjuries[3] = "{2}'s {1} stabs completely through {0}'s " + leftHand.name + "!";
        leftHand.stabInjuries[4] = "{2}'s {1} fractures the bones in {0}'s " + leftHand.name + "!";
        leftHand.stabInjuries[5] = "{2}'s {1} completely mangles {0}'s " + leftHand.name + "!";

        leftHand.clawInjuries[0] = "The {2}'s {1} scratches the skin of {0}'s " + leftHand.name + "!";
        leftHand.clawInjuries[1] = "The {2}'s {1} slashes the flesh of {0}'s " + leftHand.name + "!";
        leftHand.clawInjuries[2] = "The {2}'s {1} cuts into {0}'s " + leftHand.name + " and scrapes the bones!";
        leftHand.clawInjuries[3] = "The {2}'s {1} hacks deep into {0}'s " + leftHand.name + ", fracturing the bones!";
        leftHand.clawInjuries[4] = "The {2}'s {1} tears a chunk of {0}'s " + leftHand.name + " almost completely off!";
        leftHand.clawInjuries[5] = "The {2}'s {1} completely mangles {0}'s " + leftHand.name + "!";

        leftHand.biteInjuries[0] = "The {2} nips at {0}'s " + leftHand.name + "!";
        leftHand.biteInjuries[1] = "The {2} bites down into the skin of {0}'s " + leftHand.name + "!";
        leftHand.biteInjuries[2] = "The {2} bites down hard into {0}'s " + leftHand.name + "!";
        leftHand.biteInjuries[3] = "The {2}'s bites deep into {0's} " + leftHand.name + " and shakes!";
        leftHand.biteInjuries[4] = "The {2}'s jaws rip off a part of " + leftHand.name + "!";
        leftHand.biteInjuries[5] = "The {2}'s jaws tear {0}'s " + leftHand.name + " completely off!";

        return leftHand;
    }

    private BodyPart RightHand()
    {
        BodyPart rightHand = gameObject.AddComponent<BodyPart>();

        rightHand.armorSlot = Item.EquipmentSlot.Hands; //put this before callback is assigned in base class
        rightHand.weaponSlot = Item.EquipmentSlot.MainHand;
        rightHand.name = "right hand";
        rightHand.functioningLimit = 4;
        rightHand.vomitThreshold = 4;
        rightHand.downedThreshold = 4;
        rightHand.fearLimit = 4;
        rightHand.attack1 = true;

        rightHand.bluntInjuries[0] = "The force of {2}'s {1} leaves a light bruise on {0}'s " + rightHand.name + "!";
        rightHand.bluntInjuries[1] = "The force of {2}'s {1} bruises {0}'s " + rightHand.name + "!";
        rightHand.bluntInjuries[2] = "The force of {2}'s {1} heavily bruises {0}'s " + rightHand.name + "!";
        rightHand.bluntInjuries[3] = "The force of {2}'s {1} cracks the bones in {0}'s " + rightHand.name + "!";
        rightHand.bluntInjuries[4] = "The force of {2}'s {1} crushes the bones of {0}'s " + rightHand.name + "!";
        rightHand.bluntInjuries[5] = "The force of {2}'s {1} completely obliterates {0}'s " + rightHand.name + "!";

        rightHand.stabInjuries[0] = "{2}'s {1} pokes at the skin of {0}'s " + rightHand.name + "!";
        rightHand.stabInjuries[1] = "{2}'s {1} slices at the skin of {0}'s " + rightHand.name + "!";
        rightHand.stabInjuries[2] = "{2}'s {1} stabs into {0}'s " + rightHand.name + "!";
        rightHand.stabInjuries[3] = "{2}'s {1} stabs completely through {0}'s " + rightHand.name + "!";
        rightHand.stabInjuries[4] = "{2}'s {1} fractures the bones in {0}'s " + rightHand.name + "!";
        rightHand.stabInjuries[5] = "{2}'s {1} completely mangles {0}'s " + rightHand.name + "!";

        rightHand.clawInjuries[0] = "The {2}'s {1} scratches the skin of {0}'s " + rightHand.name + "!";
        rightHand.clawInjuries[1] = "The {2}'s {1} slashes the flesh of {0}'s " + rightHand.name + "!";
        rightHand.clawInjuries[2] = "The {2}'s {1} cuts into {0}'s " + rightHand.name + " and scrapes the bones!";
        rightHand.clawInjuries[3] = "The {2}'s {1} hacks deep into {0}'s " + rightHand.name + ", fracturing the bones!";
        rightHand.clawInjuries[4] = "The {2}'s {1} tears a chunk of {0}'s " + rightHand.name + " almost completely off!";
        rightHand.clawInjuries[5] = "The {2}'s {1} completely mangles {0}'s " + rightHand.name + "!";

        rightHand.biteInjuries[0] = "The {2} nips at {0}'s " + rightHand.name + "!";
        rightHand.biteInjuries[1] = "The {2} bites down into the skin of {0}'s " + rightHand.name + "!";
        rightHand.biteInjuries[2] = "The {2} bites down hard into {0}'s " + rightHand.name + "!";
        rightHand.biteInjuries[3] = "The {2}'s bites deep into {0's} " + rightHand.name + " and shakes!";
        rightHand.biteInjuries[4] = "The {2}'s jaws rip off a part of " + rightHand.name + "!";
        rightHand.biteInjuries[5] = "The {2}'s jaws tear {0}'s " + rightHand.name + " completely off!";

        return rightHand;
    }

    private BodyPart LeftLeg()
    {
        BodyPart leftLeg = gameObject.AddComponent<BodyPart>();

        leftLeg.armorSlot = Item.EquipmentSlot.Legs; //put this before callback is assigned in base class
        leftLeg.name = "left leg";
        leftLeg.functioningLimit = 4;
        leftLeg.vomitThreshold = 4;
        leftLeg.downedThreshold = 2;
        leftLeg.fearLimit = 4;

        leftLeg.bluntInjuries[0] = "The force of {2}'s {1} leaves a light bruise on {0}'s " + leftLeg.name + "!";
        leftLeg.bluntInjuries[1] = "The force of {2}'s {1} bruises {0}'s " + leftLeg.name + "!";
        leftLeg.bluntInjuries[2] = "The force of {2}'s {1} heavily bruises {0}'s " + leftLeg.name + "!";
        leftLeg.bluntInjuries[3] = "The force of {2}'s {1} cracking a bone in {0}'s " + leftLeg.name + "!";
        leftLeg.bluntInjuries[4] = "The force of {2}'s {1} crushes the bones of {0}'s " + leftLeg.name + "!";
        leftLeg.bluntInjuries[5] = "The force of {2}'s {1} completely pulverizes {0}'s " + leftLeg.name + "!";

        leftLeg.stabInjuries[0] = "{2}'s {1} pokes at the skin of {0}'s " + leftLeg.name + "!";
        leftLeg.stabInjuries[1] = "{2}'s {1} slices into flesh of {0}'s " + leftLeg.name + "!";
        leftLeg.stabInjuries[2] = "{2}'s {1} stabs into {0}'s " + leftLeg.name + "!";
        leftLeg.stabInjuries[3] = "{2}'s {1} stabs deep into {0}'s " + leftLeg.name + "!";
        leftLeg.stabInjuries[4] = "{2}'s {1} seriously gores {0}'s " + leftLeg.name + "!";
        leftLeg.stabInjuries[5] = "The blade of {2}'s {1} passes completely through {0}'s " + leftLeg.name + "!";

        leftLeg.clawInjuries[0] = "The {2}'s {1} scratches the skin of {0}'s " + leftLeg.name + "!";
        leftLeg.clawInjuries[1] = "The {2}'s {1} slashes the flesh of {0}'s " + leftLeg.name + "!";
        leftLeg.clawInjuries[2] = "The {2}'s {1} cuts into {0}'s " + leftLeg.name + " and scratches the bone!";
        leftLeg.clawInjuries[3] = "The {2}'s {1} hacks deep into {0}'s " + leftLeg.name + ", cutting away the muscle!";
        leftLeg.clawInjuries[4] = "The {2}'s {1} tears a chunk of flesh off of {0}'s " + leftLeg.name + "!";
        leftLeg.clawInjuries[5] = "The {2}'s {1} strips the meat from the bones of {0}'s " + leftLeg.name + "!";

        leftLeg.biteInjuries[0] = "The {2} nips at {0}'s " + leftLeg.name + "!";
        leftLeg.biteInjuries[1] = "The {2} bites down into the skin of {0}'s " + leftLeg.name + "!";
        leftLeg.biteInjuries[2] = "The {2} bites down hard on the of flesh of {0}'s " + leftLeg.name + "!";
        leftLeg.biteInjuries[3] = "The {2} tears a chunk of meat from {0's} " + leftLeg.name + "! with it's teeth!";
        leftLeg.biteInjuries[4] = "The {2}'s jaws grip the bones of {0}'s " + leftLeg.name + " and shake out of it's socket!";
        leftLeg.biteInjuries[5] = "The {2}'s jaws tear {0}'s " + leftLeg.name + " completely off!";

        return leftLeg;
    }

    private BodyPart RightLeg()
    {
        BodyPart rightLeg = gameObject.AddComponent<BodyPart>();

        rightLeg.armorSlot = Item.EquipmentSlot.Legs; //put this before callback is assigned in base class
        rightLeg.name = "right leg";
        rightLeg.functioningLimit = 4;
        rightLeg.vomitThreshold = 4;
        rightLeg.downedThreshold = 2;
        rightLeg.fearLimit = 4;

        rightLeg.bluntInjuries[0] = "The force of {2}'s {1} leaves a light bruise on {0}'s " + rightLeg.name + "!";
        rightLeg.bluntInjuries[1] = "The force of {2}'s {1} bruises {0}'s " + rightLeg.name + "!";
        rightLeg.bluntInjuries[2] = "The force of {2}'s {1} heavily bruises {0}'s " + rightLeg.name + "!";
        rightLeg.bluntInjuries[3] = "The force of {2}'s {1} cracking a bone in {0}'s " + rightLeg.name + "!";
        rightLeg.bluntInjuries[4] = "The force of {2}'s {1} crushes the bones of {0}'s " + rightLeg.name + "!";
        rightLeg.bluntInjuries[5] = "The force of {2}'s {1} completely pulverizes {0}'s " + rightLeg.name + "!";

        rightLeg.stabInjuries[0] = "{2}'s {1} pokes at the skin of {0}'s " + rightLeg.name + "!";
        rightLeg.stabInjuries[1] = "{2}'s {1} slices into flesh of {0}'s " + rightLeg.name + "!";
        rightLeg.stabInjuries[2] = "{2}'s {1} stabs into {0}'s " + rightLeg.name + "!";
        rightLeg.stabInjuries[3] = "{2}'s {1} stabs deep into {0}'s " + rightLeg.name + "!";
        rightLeg.stabInjuries[4] = "{2}'s {1} seriously gores {0}'s " + rightLeg.name + "!";
        rightLeg.stabInjuries[5] = "The blade of {2}'s {1} passes completely through {0}'s " + rightLeg.name + "!";

        rightLeg.clawInjuries[0] = "The {2}'s {1} scratches the skin of {0}'s " + rightLeg.name + "!";
        rightLeg.clawInjuries[1] = "The {2}'s {1} slashes the flesh of {0}'s " + rightLeg.name + "!";
        rightLeg.clawInjuries[2] = "The {2}'s {1} cuts into {0}'s " + rightLeg.name + " and scratches the bone!";
        rightLeg.clawInjuries[3] = "The {2}'s {1} hacks deep into {0}'s " + rightLeg.name + ", cutting away the muscle!";
        rightLeg.clawInjuries[4] = "The {2}'s {1} tears a chunk of flesh off of {0}'s " + rightLeg.name + "!";
        rightLeg.clawInjuries[5] = "The {2}'s {1} strips the meat from the bones of {0}'s " + rightLeg.name + "!";

        rightLeg.biteInjuries[0] = "The {2} nips at {0}'s " + rightLeg.name + "!";
        rightLeg.biteInjuries[1] = "The {2} bites down into the skin of {0}'s " + rightLeg.name + "!";
        rightLeg.biteInjuries[2] = "The {2} bites down hard on the of flesh of {0}'s " + rightLeg.name + "!";
        rightLeg.biteInjuries[3] = "The {2} tears a chunk of meat from {0's} " + rightLeg.name + "! with it's teeth!";
        rightLeg.biteInjuries[4] = "The {2}'s jaws grip the bones of {0}'s " + rightLeg.name + " and shake out of it's socket!";
        rightLeg.biteInjuries[5] = "The {2}'s jaws tear {0}'s " + rightLeg.name + " completely off!";

        return rightLeg;
    }

    private BodyPart LeftFoot()
    {
        BodyPart leftFoot = gameObject.AddComponent<BodyPart>();

        leftFoot.armorSlot = Item.EquipmentSlot.Feet; //put this before callback is assigned in base class
        leftFoot.name = "left foot";
        leftFoot.functioningLimit = 4;
        leftFoot.vomitThreshold = 4;
        leftFoot.downedThreshold = 2;
        leftFoot.fearLimit = 4;

        leftFoot.bluntInjuries[0] = "The force of {2}'s {1} leaves a light bruise on {0}'s " + leftFoot.name + "!";
        leftFoot.bluntInjuries[1] = "The force of {2}'s {1} bruises {0}'s " + leftFoot.name + "!";
        leftFoot.bluntInjuries[2] = "The force of {2}'s {1} heavily bruises {0}'s " + leftFoot.name + "!";
        leftFoot.bluntInjuries[3] = "The force of {2}'s {1} cracks the bones in {0}'s " + leftFoot.name + "!";
        leftFoot.bluntInjuries[4] = "The force of {2}'s {1} crushes the bones of {0}'s " + leftFoot.name + "!";
        leftFoot.bluntInjuries[5] = "The force of {2}'s {1} completely obliterates {0}'s " + leftFoot.name + "!";

        leftFoot.stabInjuries[0] = "{2}'s {1} pokes at the skin of {0}'s " + leftFoot.name + "!";
        leftFoot.stabInjuries[1] = "{2}'s {1} slices at the skin of {0}'s " + leftFoot.name + "!";
        leftFoot.stabInjuries[2] = "{2}'s {1} stabs into {0}'s " + leftFoot.name + "!";
        leftFoot.stabInjuries[3] = "{2}'s {1} stabs completely through {0}'s " + leftFoot.name + "!";
        leftFoot.stabInjuries[4] = "{2}'s {1} fractures the bones in {0}'s " + leftFoot.name + " penetrates through";
        leftFoot.stabInjuries[5] = "{2}'s {1} completely mangles {0}'s " + leftFoot.name + "!";

        leftFoot.clawInjuries[0] = "The {2}'s {1} scratches the skin of {0}'s " + leftFoot.name + "!";
        leftFoot.clawInjuries[1] = "The {2}'s {1} slashes the flesh of {0}'s " + leftFoot.name + "!";
        leftFoot.clawInjuries[2] = "The {2}'s {1} cuts into {0}'s " + leftFoot.name + " and scrapes the bones!";
        leftFoot.clawInjuries[3] = "The {2}'s {1} hacks deep into {0}'s " + leftFoot.name + ", fracturing the bones!";
        leftFoot.clawInjuries[4] = "The {2}'s {1} tears a chunk of {0}'s " + leftFoot.name + " almost completely off!";
        leftFoot.clawInjuries[5] = "The {2}'s {1} completely mangles {0}'s " + leftFoot.name + "!";

        leftFoot.biteInjuries[0] = "The {2} nips at {0}'s " + leftFoot.name + "!";
        leftFoot.biteInjuries[1] = "The {2} bites down into the skin of {0}'s " + leftFoot.name + "!";
        leftFoot.biteInjuries[2] = "The {2} bites down hard into {0}'s " + leftFoot.name + "!";
        leftFoot.biteInjuries[3] = "The {2}'s bites deep into {0's} " + leftFoot.name + " and shakes!";
        leftFoot.biteInjuries[4] = "The {2}'s jaws rip off a part of " + leftFoot.name + "!";
        leftFoot.biteInjuries[5] = "The {2}'s jaws tear {0}'s " + leftFoot.name + " completely off!";

        return leftFoot;
    }

    private BodyPart RightFoot()
    {        BodyPart rightFoot = gameObject.AddComponent<BodyPart>();

        rightFoot.armorSlot = Item.EquipmentSlot.Feet; //put this before callback is assigned in base class
        rightFoot.name = "right foot";
        rightFoot.functioningLimit = 4;
        rightFoot.vomitThreshold = 4;
        rightFoot.downedThreshold = 2;
        rightFoot.fearLimit = 4;

        rightFoot.bluntInjuries[0] = "The force of {2}'s {1} leaves a light bruise on {0}'s " + rightFoot.name + "!";
        rightFoot.bluntInjuries[1] = "The force of {2}'s {1} bruises {0}'s " + rightFoot.name + "!";
        rightFoot.bluntInjuries[2] = "The force of {2}'s {1} heavily bruises {0}'s " + rightFoot.name + "!";
        rightFoot.bluntInjuries[3] = "The force of {2}'s {1} cracks the bones in {0}'s " + rightFoot.name + "!";
        rightFoot.bluntInjuries[4] = "The force of {2}'s {1} crushes the bones of {0}'s " + rightFoot.name + "!";
        rightFoot.bluntInjuries[5] = "The force of {2}'s {1} completely obliterates {0}'s " + rightFoot.name + "!";

        rightFoot.stabInjuries[0] = "{2}'s {1} pokes at the skin of {0}'s " + rightFoot.name + "!";
        rightFoot.stabInjuries[1] = "{2}'s {1} slices at the skin of {0}'s " + rightFoot.name + "!";
        rightFoot.stabInjuries[2] = "{2}'s {1} stabs into {0}'s " + rightFoot.name + "!";
        rightFoot.stabInjuries[3] = "{2}'s {1} stabs completely through {0}'s " + rightFoot.name + "!";
        rightFoot.stabInjuries[4] = "{2}'s {1} fractures the bones in {0}'s " + rightFoot.name + " penetrates through";
        rightFoot.stabInjuries[5] = "{2}'s {1} completely mangles {0}'s " + rightFoot.name + "!";

        rightFoot.clawInjuries[0] = "The {2}'s {1} scratches the skin of {0}'s " + rightFoot.name + "!";
        rightFoot.clawInjuries[1] = "The {2}'s {1} slashes the flesh of {0}'s " + rightFoot.name + "!";
        rightFoot.clawInjuries[2] = "The {2}'s {1} cuts into {0}'s " + rightFoot.name + " and scrapes the bones!";
        rightFoot.clawInjuries[3] = "The {2}'s {1} hacks deep into {0}'s " + rightFoot.name + ", fracturing the bones!";
        rightFoot.clawInjuries[4] = "The {2}'s {1} tears a chunk of {0}'s " + rightFoot.name + " almost completely off!";
        rightFoot.clawInjuries[5] = "The {2}'s {1} completely mangles {0}'s " + rightFoot.name + "!";

        rightFoot.biteInjuries[0] = "The {2} nips at {0}'s " + rightFoot.name + "!";
        rightFoot.biteInjuries[1] = "The {2} bites down into the skin of {0}'s " + rightFoot.name + "!";
        rightFoot.biteInjuries[2] = "The {2} bites down hard into {0}'s " + rightFoot.name + "!";
        rightFoot.biteInjuries[3] = "The {2}'s bites deep into {0's} " + rightFoot.name + " and shakes!";
        rightFoot.biteInjuries[4] = "The {2}'s jaws rip off a part of " + rightFoot.name + "!";
        rightFoot.biteInjuries[5] = "The {2}'s jaws tear {0}'s " + rightFoot.name + " completely off!";

        return rightFoot;
    }
}
