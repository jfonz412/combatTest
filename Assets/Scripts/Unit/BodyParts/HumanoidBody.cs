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
        head.downedThreshold = 3;
        head.isVitalPart = true;

        head.stabInjuries[0] = "The point of the {1} pokes at the skin of {0}'s " + head.name + "!";
        head.stabInjuries[1] = "The point of the {1} leaves a decent gash flesh of {0}'s " + head.name + "!";
        head.stabInjuries[2] = "The point of the {1} jabs into {0}'s " + head.name + ", the neck is pulled with the force";
        head.stabInjuries[3] = "The point of the {1} slams into the " + head.name + " and cracks {0}'s skull!";
        head.stabInjuries[4] = "The blade of the {1} jams halfway into {0}'s skull!";
        head.stabInjuries[5] = "The blade of the {1} pierces completely through {0}'s " + head.name + "!";

        head.bluntInjuries[0] = "The force of the {1} leaves a light bruise on {0}'s " + head.name + "!";
        head.bluntInjuries[1] = "The force of the {1} bruises {0}'s " + head.name + "!";
        head.bluntInjuries[2] = "The force of the {1} heavily bruises {0}'s " + head.name + "!";
        head.bluntInjuries[3] = "The force of the {1} gives {0} a serious concussion";
        head.bluntInjuries[4] = "The force of the {1} cracks {0}'s skull!";
        head.bluntInjuries[5] = "The force of the {1} completely caves in {0}'s " + head.name + "!";

        return head;
    }

    private BodyPart Neck()
    {
        BodyPart neck = gameObject.AddComponent<BodyPart>();

        neck.name = "neck";
        neck.functioningLimit = 5;
        neck.downedThreshold = 3;
        neck.cantBreathThreshold = 3;
        neck.suffocationThreshold = 4;

        neck.bleedBonus = 2f;

        neck.stabInjuries[0] = "The point of the {1} pokes into the flesh of {0}'s " + neck.name + "!";
        neck.stabInjuries[1] = "The point of the {1} stabs into {0}'s " + neck.name + "!";
        neck.stabInjuries[2] = "The point of the {1} stabs deeply into {0}'s " + neck.name + "";
        neck.stabInjuries[3] = "The blade of the {1} pierces completely through {0}'s " + neck.name + "!";
        neck.stabInjuries[4] = "The blade of the {1} pierces completely through {0}'s " + neck.name + "!";
        neck.stabInjuries[5] = "The blade of the {1} pierces completely through {0}'s " + neck.name + "!";

        neck.bluntInjuries[0] = "The force of the {1} bruises {0}'s " + neck.name + "!";
        neck.bluntInjuries[1] = "The force of the {1} heavily bruises {0}'s " + neck.name + "!";
        neck.bluntInjuries[2] = "The force of the {1} seriously bruises {0}'s " + neck.name + "!";
        neck.bluntInjuries[3] = "The force of the {1} crushes the meat of {0}'s " + neck.name + "";
        neck.bluntInjuries[4] = "The force of the {1} crumples part of {0}'s " + neck.name + "!";
        neck.bluntInjuries[5] = "The force of the {1} completely crushes {0}'s " + neck.name + "!";

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

        chest.bluntInjuries[0] = "The force of the {1} leaves a light bruise on {0}'s " + chest.name + "!";
        chest.bluntInjuries[1] = "The force of the {1} bruises {0}'s " + chest.name + "!";
        chest.bluntInjuries[2] = "The force of the {1} heavily bruises {0}'s " + chest.name + "!";
        chest.bluntInjuries[3] = "The force of the {1} cracks the ribs of {0}'s " + chest.name + "";
        chest.bluntInjuries[4] = "The force of the {1} shatters the ribs of {0}'s " + chest.name + "!";
        chest.bluntInjuries[5] = "The force of the {1} completely caves in {0}'s " + chest.name + "!";
            
        chest.stabInjuries[0] = "The point of the {1} pokes at the skin of {0}'s " + chest.name + "!";
        chest.stabInjuries[1] = "The point of the {1} pokes into the flesh of {0}'s " + chest.name + "!";
        chest.stabInjuries[2] = "The point of the {1} tears through the muscle of {0}'s " + chest.name + "";
        chest.stabInjuries[3] = "The point of the {1} jams into the ribs {0}'s " + chest.name + "!";
        chest.stabInjuries[4] = "The blade of the {1} cuts through the ribs of {0}'s " + chest.name + "!";
        chest.stabInjuries[5] = "The blade of the {1} pierces completely through {0}'s " + chest.name + "!";

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

        abdomin.bluntInjuries[0] = "The force of the {1} leaves a light bruise on {0}'s " + abdomin.name + "!";
        abdomin.bluntInjuries[1] = "The force of the {1} bruises {0}'s " + abdomin.name + "!";
        abdomin.bluntInjuries[2] = "The force of the {1} heavily bruises {0}'s " + abdomin.name + "!";
        abdomin.bluntInjuries[3] = "The force of the {1} bruises {0}'s guts";
        abdomin.bluntInjuries[4] = "The force of the {1} against {0}'s " + abdomin.name + " causes serious internal bleeding!";
        abdomin.bluntInjuries[5] = "The force of the {1} obliterates {0}'s innards!";

        abdomin.stabInjuries[0] = "The point of the {1} pokes at the skin of {0}'s " + abdomin.name + "!";
        abdomin.stabInjuries[1] = "The point of the {1} pokes into the flesh of {0}'s " + abdomin.name + "!";
        abdomin.stabInjuries[2] = "The point of the {1} tears through the muscle of {0}'s " + abdomin.name + "!";
        abdomin.stabInjuries[3] = "The point of the {1} pokes {0} in the guts!";
        abdomin.stabInjuries[4] = "The blade of the {1} cuts deep into {0}'s " + abdomin.name + "!";
        abdomin.stabInjuries[5] = "The blade of the {1} pierces completely through {0}'s " + abdomin.name + "!";
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

        leftArm.bluntInjuries[0] = "The force of the {1} leaves a light bruise on {0}'s " + leftArm.name + "!";
        leftArm.bluntInjuries[1] = "The force of the {1} bruises {0}'s " + leftArm.name + "!";
        leftArm.bluntInjuries[2] = "The force of the {1} heavily bruises {0}'s " + leftArm.name + "!";
        leftArm.bluntInjuries[3] = "The force of the {1} cracking a bone in {0}'s " + leftArm.name + "!";
        leftArm.bluntInjuries[4] = "The force of the {1} crushes the bones of {0}'s " + leftArm.name + "!";
        leftArm.bluntInjuries[5] = "The force of the {1} completely pulverizes {0}'s " + leftArm.name + "!";

        leftArm.stabInjuries[0] = "The {1} pokes at the skin of {0}'s " + leftArm.name + "!";
        leftArm.stabInjuries[1] = "The {1} slices into flesh of {0}'s " + leftArm.name + "!";
        leftArm.stabInjuries[2] = "The {1} stabs into {0}'s " + leftArm.name + "!";
        leftArm.stabInjuries[3] = "The {1} stabs deep into {0}'s " + leftArm.name + "!";
        leftArm.stabInjuries[4] = "The {1} seriously gores {0}'s " + leftArm.name + "!";
        leftArm.stabInjuries[5] = "The blade of the {1} passes completely through {0}'s " + leftArm.name + "!";

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

        rightArm.bluntInjuries[0] = "The force of the {1} leaves a light bruise on {0}'s " + rightArm.name + "!";
        rightArm.bluntInjuries[1] = "The force of the {1} bruises {0}'s " + rightArm.name + "!";
        rightArm.bluntInjuries[2] = "The force of the {1} heavily bruises {0}'s " + rightArm.name + "!";
        rightArm.bluntInjuries[3] = "The force of the {1} cracking a bone in {0}'s " + rightArm.name + "!";
        rightArm.bluntInjuries[4] = "The force of the {1} crushes the bones of {0}'s " + rightArm.name + "!";
        rightArm.bluntInjuries[5] = "The force of the {1} completely pulverizes {0}'s " + rightArm.name + "!";

        rightArm.stabInjuries[0] = "The {1} pokes at the skin of {0}'s " + rightArm.name + "!";
        rightArm.stabInjuries[1] = "The {1} slices into flesh of {0}'s " + rightArm.name + "!";
        rightArm.stabInjuries[2] = "The {1} stabs into {0}'s " + rightArm.name + "!";
        rightArm.stabInjuries[3] = "The {1} stabs deep into {0}'s " + rightArm.name + "!";
        rightArm.stabInjuries[4] = "The {1} seriously gores {0}'s " + rightArm.name + "!";
        rightArm.stabInjuries[5] = "The blade of the {1} passes completely through {0}'s " + rightArm.name + "!";

        return rightArm;
    }

    private BodyPart LeftHand()
    {
        BodyPart leftHand = gameObject.AddComponent<BodyPart>();

        leftHand.armorSlot = Item.EquipmentSlot.Hands; //put this before callback is assigned in base class
        leftHand.weaponSlot = Item.EquipmentSlot.OffHand;
        leftHand.name = "left hand";
        leftHand.functioningLimit = 4;
        leftHand.vomitThreshold = 5;
        leftHand.downedThreshold = 4;
        leftHand.attack2 = true;

        leftHand.bluntInjuries[0] = "The force of the {1} leaves a light bruise on {0}'s " + leftHand.name + "!";
        leftHand.bluntInjuries[1] = "The force of the {1} bruises {0}'s " + leftHand.name + "!";
        leftHand.bluntInjuries[2] = "The force of the {1} heavily bruises {0}'s " + leftHand.name + "!";
        leftHand.bluntInjuries[3] = "The force of the {1} cracks the bones in {0}'s " + leftHand.name + "!";
        leftHand.bluntInjuries[4] = "The force of the {1} crushes the bones of {0}'s " + leftHand.name + "!";
        leftHand.bluntInjuries[5] = "The force of the {1} completely obliterates {0}'s " + leftHand.name + "!";

        leftHand.stabInjuries[0] = "The {1} pokes at the skin of {0}'s " + leftHand.name + "!";
        leftHand.stabInjuries[1] = "The {1} slices at the skin of {0}'s " + leftHand.name + "!";
        leftHand.stabInjuries[2] = "The {1} stabs into {0}'s " + leftHand.name + "!";
        leftHand.stabInjuries[3] = "The {1} stabs completely through {0}'s " + leftHand.name + "!";
        leftHand.stabInjuries[4] = "The {1} fractures the bones in {0}'s " + leftHand.name + "!";
        leftHand.stabInjuries[5] = "The {1} completely mangles {0}'s " + leftHand.name + "!";

        return leftHand;
    }

    private BodyPart RightHand()
    {
        BodyPart rightHand = gameObject.AddComponent<BodyPart>();

        rightHand.armorSlot = Item.EquipmentSlot.Hands; //put this before callback is assigned in base class
        rightHand.weaponSlot = Item.EquipmentSlot.MainHand;
        rightHand.name = "right hand";
        rightHand.functioningLimit = 4;
        rightHand.vomitThreshold = 5;
        rightHand.downedThreshold = 4;
        rightHand.attack1 = true;

        rightHand.bluntInjuries[0] = "The force of the {1} leaves a light bruise on {0}'s " + rightHand.name + "!";
        rightHand.bluntInjuries[1] = "The force of the {1} bruises {0}'s " + rightHand.name + "!";
        rightHand.bluntInjuries[2] = "The force of the {1} heavily bruises {0}'s " + rightHand.name + "!";
        rightHand.bluntInjuries[3] = "The force of the {1} cracks the bones in {0}'s " + rightHand.name + "!";
        rightHand.bluntInjuries[4] = "The force of the {1} crushes the bones of {0}'s " + rightHand.name + "!";
        rightHand.bluntInjuries[5] = "The force of the {1} completely obliterates {0}'s " + rightHand.name + "!";

        rightHand.stabInjuries[0] = "The {1} pokes at the skin of {0}'s " + rightHand.name + "!";
        rightHand.stabInjuries[1] = "The {1} slices at the skin of {0}'s " + rightHand.name + "!";
        rightHand.stabInjuries[2] = "The {1} stabs into {0}'s " + rightHand.name + "!";
        rightHand.stabInjuries[3] = "The {1} stabs completely through {0}'s " + rightHand.name + "!";
        rightHand.stabInjuries[4] = "The {1} fractures the bones in {0}'s " + rightHand.name + "!";
        rightHand.stabInjuries[5] = "The {1} completely mangles {0}'s " + rightHand.name + "!";

        return rightHand;
    }

    private BodyPart LeftLeg()
    {
        BodyPart leftLeg = gameObject.AddComponent<BodyPart>();

        leftLeg.armorSlot = Item.EquipmentSlot.Legs; //put this before callback is assigned in base class
        leftLeg.name = "left leg";
        leftLeg.functioningLimit = 4;
        leftLeg.vomitThreshold = 4;
        leftLeg.downedThreshold = 3;

        leftLeg.bluntInjuries[0] = "The force of the {1} leaves a light bruise on {0}'s " + leftLeg.name + "!";
        leftLeg.bluntInjuries[1] = "The force of the {1} bruises {0}'s " + leftLeg.name + "!";
        leftLeg.bluntInjuries[2] = "The force of the {1} heavily bruises {0}'s " + leftLeg.name + "!";
        leftLeg.bluntInjuries[3] = "The force of the {1} cracking a bone in {0}'s " + leftLeg.name + "!";
        leftLeg.bluntInjuries[4] = "The force of the {1} crushes the bones of {0}'s " + leftLeg.name + "!";
        leftLeg.bluntInjuries[5] = "The force of the {1} completely pulverizes {0}'s " + leftLeg.name + "!";

        leftLeg.stabInjuries[0] = "The {1} pokes at the skin of {0}'s " + leftLeg.name + "!";
        leftLeg.stabInjuries[1] = "The {1} slices into flesh of {0}'s " + leftLeg.name + "!";
        leftLeg.stabInjuries[2] = "The {1} stabs into {0}'s " + leftLeg.name + "!";
        leftLeg.stabInjuries[3] = "The {1} stabs deep into {0}'s " + leftLeg.name + "!";
        leftLeg.stabInjuries[4] = "The {1} seriously gores {0}'s " + leftLeg.name + "!";
        leftLeg.stabInjuries[5] = "The blade of the {1} passes completely through {0}'s " + leftLeg.name + "!";

        return leftLeg;
    }

    private BodyPart RightLeg()
    {
        BodyPart rightLeg = gameObject.AddComponent<BodyPart>();

        rightLeg.armorSlot = Item.EquipmentSlot.Legs; //put this before callback is assigned in base class
        rightLeg.name = "right leg";
        rightLeg.functioningLimit = 4;
        rightLeg.vomitThreshold = 4;
        rightLeg.downedThreshold = 3;

        rightLeg.bluntInjuries[0] = "The force of the {1} leaves a light bruise on {0}'s " + rightLeg.name + "!";
        rightLeg.bluntInjuries[1] = "The force of the {1} bruises {0}'s " + rightLeg.name + "!";
        rightLeg.bluntInjuries[2] = "The force of the {1} heavily bruises {0}'s " + rightLeg.name + "!";
        rightLeg.bluntInjuries[3] = "The force of the {1} cracking a bone in {0}'s " + rightLeg.name + "!";
        rightLeg.bluntInjuries[4] = "The force of the {1} crushes the bones of {0}'s " + rightLeg.name + "!";
        rightLeg.bluntInjuries[5] = "The force of the {1} completely pulverizes {0}'s " + rightLeg.name + "!";

        rightLeg.stabInjuries[0] = "The {1} pokes at the skin of {0}'s " + rightLeg.name + "!";
        rightLeg.stabInjuries[1] = "The {1} slices into flesh of {0}'s " + rightLeg.name + "!";
        rightLeg.stabInjuries[2] = "The {1} stabs into {0}'s " + rightLeg.name + "!";
        rightLeg.stabInjuries[3] = "The {1} stabs deep into {0}'s " + rightLeg.name + "!";
        rightLeg.stabInjuries[4] = "The {1} seriously gores {0}'s " + rightLeg.name + "!";
        rightLeg.stabInjuries[5] = "The blade of the {1} passes completely through {0}'s " + rightLeg.name + "!";

        return rightLeg;
    }

    private BodyPart LeftFoot()
    {
        BodyPart leftFoot = gameObject.AddComponent<BodyPart>();

        leftFoot.armorSlot = Item.EquipmentSlot.Feet; //put this before callback is assigned in base class
        leftFoot.name = "left foot";
        leftFoot.functioningLimit = 4;
        leftFoot.vomitThreshold = 5;
        leftFoot.downedThreshold = 3;

        leftFoot.bluntInjuries[0] = "The force of the {1} leaves a light bruise on {0}'s " + leftFoot.name + "!";
        leftFoot.bluntInjuries[1] = "The force of the {1} bruises {0}'s " + leftFoot.name + "!";
        leftFoot.bluntInjuries[2] = "The force of the {1} heavily bruises {0}'s " + leftFoot.name + "!";
        leftFoot.bluntInjuries[3] = "The force of the {1} cracks the bones in {0}'s " + leftFoot.name + "!";
        leftFoot.bluntInjuries[4] = "The force of the {1} crushes the bones of {0}'s " + leftFoot.name + "!";
        leftFoot.bluntInjuries[5] = "The force of the {1} completely obliterates {0}'s " + leftFoot.name + "!";

        leftFoot.stabInjuries[0] = "The {1} pokes at the skin of {0}'s " + leftFoot.name + "!";
        leftFoot.stabInjuries[1] = "The {1} slices at the skin of {0}'s " + leftFoot.name + "!";
        leftFoot.stabInjuries[2] = "The {1} stabs into {0}'s " + leftFoot.name + "!";
        leftFoot.stabInjuries[3] = "The {1} stabs completely through {0}'s " + leftFoot.name + "!";
        leftFoot.stabInjuries[4] = "The {1} fractures the bones in {0}'s " + leftFoot.name + " penetrates through";
        leftFoot.stabInjuries[5] = "The {1} completely mangles {0}'s " + leftFoot.name + "!";

        return leftFoot;
    }

    private BodyPart RightFoot()
    {        BodyPart rightFoot = gameObject.AddComponent<BodyPart>();

        rightFoot.armorSlot = Item.EquipmentSlot.Feet; //put this before callback is assigned in base class
        rightFoot.name = "right foot";
        rightFoot.functioningLimit = 4;
        rightFoot.vomitThreshold = 5;
        rightFoot.downedThreshold = 3;

        rightFoot.bluntInjuries[0] = "The force of the {1} leaves a light bruise on {0}'s " + rightFoot.name + "!";
        rightFoot.bluntInjuries[1] = "The force of the {1} bruises {0}'s " + rightFoot.name + "!";
        rightFoot.bluntInjuries[2] = "The force of the {1} heavily bruises {0}'s " + rightFoot.name + "!";
        rightFoot.bluntInjuries[3] = "The force of the {1} cracks the bones in {0}'s " + rightFoot.name + "!";
        rightFoot.bluntInjuries[4] = "The force of the {1} crushes the bones of {0}'s " + rightFoot.name + "!";
        rightFoot.bluntInjuries[5] = "The force of the {1} completely obliterates {0}'s " + rightFoot.name + "!";

        rightFoot.stabInjuries[0] = "The {1} pokes at the skin of {0}'s " + rightFoot.name + "!";
        rightFoot.stabInjuries[1] = "The {1} slices at the skin of {0}'s " + rightFoot.name + "!";
        rightFoot.stabInjuries[2] = "The {1} stabs into {0}'s " + rightFoot.name + "!";
        rightFoot.stabInjuries[3] = "The {1} stabs completely through {0}'s " + rightFoot.name + "!";
        rightFoot.stabInjuries[4] = "The {1} fractures the bones in {0}'s " + rightFoot.name + " penetrates through";
        rightFoot.stabInjuries[5] = "The {1} completely mangles {0}'s " + rightFoot.name + "!";

        return rightFoot;
    }
}
