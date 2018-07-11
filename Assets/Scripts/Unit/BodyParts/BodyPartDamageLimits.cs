using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartDamageLimits : MonoBehaviour {

    // { Normal, Shock, CantBreathe, OvercomeByPain, Unconscious, Vomitting, Downed, Rocked, OvercomeByFear, OvercomeByRage }
    // A 6 MEANS THIS PART WILL NEVER TRIGGER THAT STATE 

    //BLEEDING (WILL ALWAYS BE SEVERITY > 2
    //OvercomeByPain, fear and rage will be determined by overall body health
    //Unconscious/rocked will be just a head check (severity > 2)

    //UnitStatusController will check the part's severity level against the severity limit for each possible status, which sounds expensive but it's one way
    //to do it

    //VITALITY
    public static Dictionary<BodyParts.Parts, int> partDamageLimits = new Dictionary<BodyParts.Parts, int>()
    {
        { BodyParts.Parts.Abdomin, 4 },
        { BodyParts.Parts.Chest, 5 },
        { BodyParts.Parts.Head, 4 },
        { BodyParts.Parts.LeftArm, 4 },
        { BodyParts.Parts.LeftHand, 3 },
        { BodyParts.Parts.RightArm, 4 },
        { BodyParts.Parts.RightHand, 3 },
        { BodyParts.Parts.LeftLeg, 4 },
        { BodyParts.Parts.LeftFoot, 3 },
        { BodyParts.Parts.RightLeg, 4 },
        { BodyParts.Parts.RightFoot, 3 },
        { BodyParts.Parts.Neck, 5 },
    };

    //SHOCK
    public static Dictionary<BodyParts.Parts, int> partBleedLimits = new Dictionary<BodyParts.Parts, int>()
    {
        { BodyParts.Parts.Abdomin, 2 },
        { BodyParts.Parts.Chest, 2 },
        { BodyParts.Parts.Head, 3 },
        { BodyParts.Parts.LeftArm, 2 },
        { BodyParts.Parts.LeftHand, 2 },
        { BodyParts.Parts.RightArm, 2 },
        { BodyParts.Parts.RightHand, 2 },
        { BodyParts.Parts.LeftLeg, 2 },
        { BodyParts.Parts.LeftFoot, 2 },
        { BodyParts.Parts.RightLeg, 2 },
        { BodyParts.Parts.RightFoot, 2 },
        { BodyParts.Parts.Neck, 1 },
    };

    public static Dictionary<BodyParts.Parts, int> partCantBreatheLimits = new Dictionary<BodyParts.Parts, int>()
    {
        { BodyParts.Parts.Abdomin, 2 },
        { BodyParts.Parts.Chest, 4 },
        { BodyParts.Parts.Head, 6 },
        { BodyParts.Parts.LeftArm, 6 },
        { BodyParts.Parts.LeftHand, 6 },
        { BodyParts.Parts.RightArm, 6 },
        { BodyParts.Parts.RightHand, 6 },
        { BodyParts.Parts.LeftLeg, 6 },
        { BodyParts.Parts.LeftFoot, 6 },
        { BodyParts.Parts.RightLeg, 6 },
        { BodyParts.Parts.RightFoot, 6 },
        { BodyParts.Parts.Neck, 4 },
    };

    //vomit
    public static Dictionary<BodyParts.Parts, int> partVomitLimits = new Dictionary<BodyParts.Parts, int>()
    {
        { BodyParts.Parts.Abdomin, 2 },
        { BodyParts.Parts.Chest, 6 },
        { BodyParts.Parts.Head, 2 },
        { BodyParts.Parts.LeftArm, 5 },
        { BodyParts.Parts.LeftHand, 5 },
        { BodyParts.Parts.RightArm, 5 },
        { BodyParts.Parts.RightHand, 5 },
        { BodyParts.Parts.LeftLeg, 5 },
        { BodyParts.Parts.LeftFoot, 5 },
        { BodyParts.Parts.RightLeg, 5 },
        { BodyParts.Parts.RightFoot, 5 },
        { BodyParts.Parts.Neck, 6 },
    };

    //knocked down
    public static Dictionary<BodyParts.Parts, int> partDownedLimits = new Dictionary<BodyParts.Parts, int>()
    {
        { BodyParts.Parts.Abdomin, 3 },
        { BodyParts.Parts.Chest, 3 },
        { BodyParts.Parts.Head, 2 },
        { BodyParts.Parts.LeftArm, 3 },
        { BodyParts.Parts.LeftHand, 4 },
        { BodyParts.Parts.RightArm, 3 },
        { BodyParts.Parts.RightHand, 4 },
        { BodyParts.Parts.LeftLeg, 2 },
        { BodyParts.Parts.LeftFoot, 2 },
        { BodyParts.Parts.RightLeg, 2 },
        { BodyParts.Parts.RightFoot, 2 },
        { BodyParts.Parts.Neck, 2 },
    };
}
