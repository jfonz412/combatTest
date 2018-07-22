using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartDamageLimits : MonoBehaviour {
    /*
    // { Normal, Shock, CantBreathe, OvercomeByPain, Unconscious, Vomitting, Downed, Rocked, OvercomeByFear, OvercomeByRage }
    // A 6 MEANS THIS PART WILL NEVER TRIGGER THAT STATE 

    //BLEEDING (WILL ALWAYS BE SEVERITY > 2
    //OvercomeByPain, fear and rage will be determined by overall body health
    //Unconscious/rocked will be just a head check (severity > 2)

    //UnitStatusController will check the part's severity level against the severity limit for each possible status, which sounds expensive but it's one way
    //to do it

    //VITALITY
    public static Dictionary<BodyPartController.Parts, int> partDamageLimits = new Dictionary<BodyPartController.Parts, int>()
    {
        { BodyPartController.Parts.Abdomin, 4 },
        { BodyPartController.Parts.Chest, 5 },
        { BodyPartController.Parts.Head, 4 },
        { BodyPartController.Parts.LeftArm, 4 },
        { BodyPartController.Parts.LeftHand, 3 },
        { BodyPartController.Parts.RightArm, 4 },
        { BodyPartController.Parts.RightHand, 3 },
        { BodyPartController.Parts.LeftLeg, 4 },
        { BodyPartController.Parts.LeftFoot, 3 },
        { BodyPartController.Parts.RightLeg, 4 },
        { BodyPartController.Parts.RightFoot, 3 },
        { BodyPartController.Parts.Neck, 5 },
    };

    //SHOCK
    public static Dictionary<BodyPartController.Parts, int> partBleedLimits = new Dictionary<BodyPartController.Parts, int>()
    {
        { BodyPartController.Parts.Abdomin, 2 },
        { BodyPartController.Parts.Chest, 2 },
        { BodyPartController.Parts.Head, 3 },
        { BodyPartController.Parts.LeftArm, 2 },
        { BodyPartController.Parts.LeftHand, 2 },
        { BodyPartController.Parts.RightArm, 2 },
        { BodyPartController.Parts.RightHand, 2 },
        { BodyPartController.Parts.LeftLeg, 2 },
        { BodyPartController.Parts.LeftFoot, 2 },
        { BodyPartController.Parts.RightLeg, 2 },
        { BodyPartController.Parts.RightFoot, 2 },
        { BodyPartController.Parts.Neck, 1 },
    };

    public static Dictionary<BodyPartController.Parts, int> partCantBreatheLimits = new Dictionary<BodyPartController.Parts, int>()
    {
        { BodyPartController.Parts.Abdomin, 2 },
        { BodyPartController.Parts.Chest, 4 },
        { BodyPartController.Parts.Head, 6 },
        { BodyPartController.Parts.LeftArm, 6 },
        { BodyPartController.Parts.LeftHand, 6 },
        { BodyPartController.Parts.RightArm, 6 },
        { BodyPartController.Parts.RightHand, 6 },
        { BodyPartController.Parts.LeftLeg, 6 },
        { BodyPartController.Parts.LeftFoot, 6 },
        { BodyPartController.Parts.RightLeg, 6 },
        { BodyPartController.Parts.RightFoot, 6 },
        { BodyPartController.Parts.Neck, 4 },
    };

    //vomit
    public static Dictionary<BodyPartController.Parts, int> partVomitLimits = new Dictionary<BodyPartController.Parts, int>()
    {
        { BodyPartController.Parts.Abdomin, 2 },
        { BodyPartController.Parts.Chest, 6 },
        { BodyPartController.Parts.Head, 2 },
        { BodyPartController.Parts.LeftArm, 5 },
        { BodyPartController.Parts.LeftHand, 5 },
        { BodyPartController.Parts.RightArm, 5 },
        { BodyPartController.Parts.RightHand, 5 },
        { BodyPartController.Parts.LeftLeg, 5 },
        { BodyPartController.Parts.LeftFoot, 5 },
        { BodyPartController.Parts.RightLeg, 5 },
        { BodyPartController.Parts.RightFoot, 5 },
        { BodyPartController.Parts.Neck, 6 },
    };

    //knocked down
    public static Dictionary<BodyPartController.Parts, int> partDownedLimits = new Dictionary<BodyPartController.Parts, int>()
    {
        { BodyPartController.Parts.Abdomin, 3 },
        { BodyPartController.Parts.Chest, 3 },
        { BodyPartController.Parts.Head, 2 },
        { BodyPartController.Parts.LeftArm, 3 },
        { BodyPartController.Parts.LeftHand, 5 },
        { BodyPartController.Parts.RightArm, 3 },
        { BodyPartController.Parts.RightHand, 5 },
        { BodyPartController.Parts.LeftLeg, 2 },
        { BodyPartController.Parts.LeftFoot, 2 },
        { BodyPartController.Parts.RightLeg, 2 },
        { BodyPartController.Parts.RightFoot, 2 },
        { BodyPartController.Parts.Neck, 2 },
    };
    */
}
