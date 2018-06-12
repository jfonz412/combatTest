using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BodyParts : MonoBehaviour {
    public enum Parts { Head, Neck, LeftArm, RightArm, LeftHand, RightHand, Chest, Abdomin, LeftLeg, RightLeg, LeftFoot, RightFoot }
    public Dictionary<BodyParts.Parts, float> bodyPartHealth;

    protected CombatSkills mySkills;

    protected float totalBlood; //100 * number of bodyparts
    protected float baseHealth = 100f;

    protected virtual void Start()
    {
        mySkills = GetComponent<CombatSkills>();
    }

    public virtual void RecieveAttack(AttackInfo recievedAttack)
    {
        //Debug.Log("Attack Recieved");
    }

    public virtual float OverallHealth()
    {
        Debug.LogError("Base method should not be called here!");
        return 0;
    }
}
