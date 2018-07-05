using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartDamageLimits : MonoBehaviour {

    //limit before part becomes unusable or would cause death
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
        { BodyParts.Parts.Neck, 4 },
    };

    //another dictionary for part status trigger limits at which we will trigger the appropriate status for an injured part
}
