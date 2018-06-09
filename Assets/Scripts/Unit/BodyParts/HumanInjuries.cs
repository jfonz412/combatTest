using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanInjuries : MonoBehaviour {
    //will be swapped out for 
    private static string armor;
    private new static string name;
    private string weapon;

    /*
    {"Head", "Neck", "LeftArm", "RightArm",
    "LHand", "RHand", "Thorax", "Abdomin",
    "LeftLeg", "RightLeg", "LFoot", "RFoot" }*/

    private static Dictionary<int, string>[] penetrationInjuries = new Dictionary<int, string>[] {
        //PENETRATION
        new Dictionary<int, string>() //scratch
        {
            { 0, "The blade grazes " + name + "'s head, leaving a light scratch!" },
            { 1, "The blade grazes " + name + "'s neck, leaving a light scratch!"},
            { 2, "The blade grazes " + name + "'s left arm, leaving a light scratch!" },
            { 3, "The blade grazes " + name + "'s right arm, leaving a light scratch!" },
            { 4, "The blade grazes " + name + "'s left hand, leaving a light scratch!" },
            { 5, "The blade grazes " + name + "'s right hand, leaving a light scratch!" },
            { 6, "The blade grazes " + name + "'s thorax, leaving a light scratch!" },
            { 7, "The blade grazes " + name + "'s abdomin, leaving a light scratch!" },
            { 8, "The blade grazes " + name + "'s left leg, leaving a light scratch!" },
            { 9, "The blade grazes " + name + "'s right leg, leaving a light scratch!" },
            { 10, "The blade grazes " + name + "'s left foot, leaving a light scratch!" },
            { 11, "The blade grazes " + name + "'s right foot, leaving a light scratch!" },
        },
        new Dictionary<int, string>() //blade slices
        {
            { 0, "" },
            { 1, "" },
            { 2, "" },
            { 3, "" },
            { 4, "" },
            { 5, "" },
            { 6, "" },
            { 7, "" },
            { 8, "" },
            { 9, "" },
            { 10, "" },
            { 11, "" },
        },
        new Dictionary<int, string>() //deep cut
        {
            { 0, "" },
            { 1, "" },
            { 2, "" },
            { 3, "" },
            { 4, "" },
            { 5, "" },
            { 6, "" },
            { 7, "" },
            { 8, "" },
            { 9, "" },
            { 10, "" },
            { 11, "" },
        },
        new Dictionary<int, string>() //cut to bone
        {
            { 0, "" },
            { 1, "" },
            { 2, "" },
            { 3, "" },
            { 4, "" },
            { 5, "" },
            { 6, "" },
            { 7, "" },
            { 8, "" },
            { 9, "" },
            { 10, "" },
            { 11, "" },
        },
        new Dictionary<int, string>() //shatters bone
        {
            { 0, "" },
            { 1, "" },
            { 2, "" },
            { 3, "" },
            { 4, "" },
            { 5, "" },
            { 6, "" },
            { 7, "" },
            { 8, "" },
            { 9, "" },
            { 10, "" },
            { 11, "" },
        },
        new Dictionary<int, string>() //sever
        {
            { 0, "" },
            { 1, "" },
            { 2, "" },
            { 3, "" },
            { 4, "" },
            { 5, "" },
            { 6, "" },
            { 7, "" },
            { 8, "" },
            { 9, "" },
            { 10, "" },
            { 11, "" },
        }
    };

    private static Dictionary<int, string>[] impactInjuries = new Dictionary<int, string>[] {
        //IMPACT
        new Dictionary<int, string>() //light bruise
        {
            { 0, "The force leaves a light bruise on " + name + "'s head!" },
            { 1, "The force leaves a light bruise on " + name + "'s neck!" },
            { 2, "The force leaves a light bruise on " + name + "'s left arm!" },
            { 3, "The force leaves a light bruise on " + name + "'s right arm!" },
            { 4, "The force leaves a light bruise on " + name + "'s left hand!" },
            { 5, "The force leaves a light bruise on " + name + "'s right hand!" },
            { 6, "The force leaves a light bruise on " + name + "'s thorax!" },
            { 7, "The force leaves a light bruise on " + name + "'s abdomin!" },
            { 8, "The force leaves a light bruise on " + name + "'s left leg!" },
            { 9, "The force leaves a light bruise on " + name + "'s right leg!" },
            { 10, "The force leaves a light bruise on " + name + "'s left foot!" },
            { 11, "The force leaves a light bruise on " + name + "'s right foot!" },
        },
        new Dictionary<int, string>() //bad bruise
        {
            { 0, "" },
            { 1, "" },
            { 2, "" },
            { 3, "" },
            { 4, "" },
            { 5, "" },
            { 6, "" },
            { 7, "" },
            { 8, "" },
            { 9, "" },
            { 10, "" },
            { 11, "" },
        },
        new Dictionary<int, string>() //heavy bruise
        {
            { 0, "" },
            { 1, "" },
            { 2, "" },
            { 3, "" },
            { 4, "" },
            { 5, "" },
            { 6, "" },
            { 7, "" },
            { 8, "" },
            { 9, "" },
            { 10, "" },
            { 11, "" },
        },
        new Dictionary<int, string>() //pulverize meat
        {
            { 0, "" },
            { 1, "" },
            { 2, "" },
            { 3, "" },
            { 4, "" },
            { 5, "" },
            { 6, "" },
            { 7, "" },
            { 8, "" },
            { 9, "" },
            { 10, "" },
            { 11, "" },
        },
        new Dictionary<int, string>() //bone shattering crush
        {
            { 0, "" },
            { 1, "" },
            { 2, "" },
            { 3, "" },
            { 4, "" },
            { 5, "" },
            { 6, "" },
            { 7, "" },
            { 8, "" },
            { 9, "" },
            { 10, "" },
            { 11, "" },
        },
        new Dictionary<int, string>() //obliterates into mess
        {
            { 0, "" },
            { 1, "" },
            { 2, "" },
            { 3, "" },
            { 4, "" },
            { 5, "" },
            { 6, "" },
            { 7, "" },
            { 8, "" },
            { 9, "" },
            { 10, "" },
            { 11, "" },
        },
    };

    public static void PenetrationDamage(int severityID, int bodyPartID, string _name)
    {
        name = _name; //not working
        Dictionary<int, string> injuryList = penetrationInjuries[severityID];
        Debug.Log(injuryList[bodyPartID]);
    }

    public static void ImpactDamage(int severityID, int bodyPartID, string _name)
    {
        name = _name; //not working
        Debug.Log(severityID);
        Dictionary<int, string> injuryList = impactInjuries[0];
        Debug.Log(injuryList[bodyPartID]);
    }
}
