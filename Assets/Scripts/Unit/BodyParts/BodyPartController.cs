﻿using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class BodyPartController : MonoBehaviour {
    private EquipmentManager equipment;
    private CombatSkills mySkills;
    private UnitReactions unitReactions;
    private UnitStatusController statusController;
    private UnitAnimController anim;
    private AttackReactionSkills attackReaction;
    private Brain myBrain;

    public float totalBlood = 1200; //hardcoded, come up with a better formula for this?

    //public delegate void OnDamageTaken(DamageInfo info);
    //public OnDamageTaken onDamageTaken;
    public delegate void OnHealthLoaded();
    public OnHealthLoaded onHealthLoaded;


    private List<BodyPart> bodyParts = new List<BodyPart>();
    [SerializeField]
    private List<BodyPart> vitalParts;

    //private Dictionary<Item.EquipmentSlot, ArmorInfo> myArmor = new Dictionary<Item.EquipmentSlot, ArmorInfo>();
    private Brain.State[] vulnerableStates = new Brain.State[]
    {
        Brain.State.CantBreathe,
        Brain.State.Dead,
        Brain.State.Rocked,
        Brain.State.Shock,
        Brain.State.Unconscious,
        Brain.State.Vomitting
        //Brain.State.Downed, //can still block, dodge, parry while on the ground 
    };

    private void Start() //make private?
    {
        mySkills = GetComponent<CombatSkills>();
        unitReactions = GetComponent<UnitReactions>();
        anim = GetComponent<UnitAnimController>();
        statusController = GetComponent<UnitStatusController>();
        mySkills.onSkillGained += UpdateSkills;
        myBrain = GetComponent<Brain>();

        UpdateSkills();

        //catch
        if(vitalParts == null)
        {
            Debug.LogError("Please add vital parts");
            vitalParts = new List<BodyPart> { bodyParts[0] };
        }
    }

    //gives us a precentage to multiply skills by to get their current effectiveness
    public float OverallHealth()
    {
        float currentDamage = 0;

        for(int i = 0; i < bodyParts.Count; i++)
        {
            currentDamage += bodyParts[i].SeverityLevel();
        }

        float fullHealth = ((5 * bodyParts.Count) * bodyParts.Count);
        float health = (fullHealth - currentDamage) / fullHealth;
        return health;
    }

    public bool PartTooInjured(List<BodyPart> partsToCheck)
    {
        for (int i = 0; i < partsToCheck.Count; i++)
        {
            BodyPart p = partsToCheck[i];
            if (p.IsTooInjured())
            {
                return true;
            }
        }
        return false;
    }

    //for each BodyPart to load itself to the controller
    public void AddBodyPart(BodyPart bodyPart)
    {
        bodyParts.Add(bodyPart); //adds a refrence to this bodypart, not a copy
    }


    private void UpdateSkills()
    {
        Debug.Log("should this stilll be here");
        attackReaction = mySkills.GetAttackReactionSkills();
    }

    #region Recieve Attack

    public virtual void RecieveAttack(AttackInfo recievedAttack, Transform myAttacker)
    {
        unitReactions.ReactToAttackAgainstSelf(myAttacker);
        if (Hit())
        {
            BodyPart bodyPart = GetRandomPart();
            bodyPart.TakeDamage(recievedAttack);
        }

        if (PartTooInjured(vitalParts))
        {
            myBrain.Die();
            string line = "<color=red>" + gameObject.name + " has been mortally wounded!</color>";
            BattleReport.AddToBattleReport(line);
        }
    }

    private bool Hit()
    {
        if (myBrain.ActiveStates(vulnerableStates))
            return true;

        if (Random.Range(0, 100) <= attackReaction.dodge)
        {
            string line = "<color=green>" + gameObject.name + " parried the attack!</color>";
            FloatingTextController.CreateFloatingText("Parry", transform, Color.green);
            anim.Dodge();
            mySkills.ExperienceGain(CombatSkills.CombatSkill.Dodge, 50f);
            BattleReport.AddToBattleReport(line);
            return false;
        }
        else if (Random.Range(0, 100) <= attackReaction.block)
        {
            string line = "<color=blue>" + gameObject.name + " blocked the attack!</color>";
            FloatingTextController.CreateFloatingText("Block", transform, Color.blue);
            anim.Block();
            mySkills.ExperienceGain(CombatSkills.CombatSkill.Block, 50f);
            BattleReport.AddToBattleReport(line);
            return false;
        }
        else if (Random.Range(0, 100) <= attackReaction.parry)
        {
            string line = "<color=yellow>" + gameObject.name + " parried the attack!</color>";
            FloatingTextController.CreateFloatingText("Parry", transform, Color.yellow);
            anim.Parry();
            mySkills.ExperienceGain(CombatSkills.CombatSkill.Parry, 50f);
            GetComponent<AttackTimer>().ResetAttackTimer(0f);
            BattleReport.AddToBattleReport(line);
            return false;
        }

        return true;
    }

    //picks a random part out of bodyParts
    private BodyPart GetRandomPart()
    {
        while (true)
        {
            int infiniteLoopStopper = 0;
            int n = Random.Range(0, bodyParts.Count);

            if (n > 0)
                n--;

            //prevents severed/obliterated part from being attacked
            if (bodyParts[n].SeverityLevel() < 5)
            {
                return bodyParts[n];
            }
            else if (infiniteLoopStopper >= 100)
            {
                Debug.LogError("Can't find bodypart after 100 tries");
                break;
            }
            infiniteLoopStopper++;
        }
        Debug.LogError("shouldn't be touching this");
        return bodyParts[0];
    }
    #endregion

    #region Equipping BodyParts
    public void EquipArmor(Item item)
    {
        for (int i = 0; i < bodyParts.Count; i++)
        {
            bodyParts[i].EquipAsArmor(item);
        }
    }

    public void EquipWeapon(Item item)
    {
        for (int i = 0; i < bodyParts.Count; i++)
        {
            bodyParts[i].EquipAsWeapon(item);
        }
    }

    //returns a list of parts with attack1
    public List<BodyPart> Attack1Parts()
    {
        List<BodyPart> bp = new List<BodyPart>();

        for (int i = 0; i < bodyParts.Count; i++)
        {
            if (bodyParts[i].attack1)
            {
                bp.Add(bodyParts[i]);
            }
        }

        return bp;
    }

    //returns a list of pasts with attack2
    public List<BodyPart> Attack2Parts()
    {
        List<BodyPart> bp = new List<BodyPart>();

        for (int i = 0; i < bodyParts.Count; i++)
        {
            if (bodyParts[i].attack2)
            {
                bp.Add(bodyParts[i]);
            }
        }

        return bp;
    }

    #endregion

    #region Bleeding and Suffocation

        public void Bleed(float amount)
        {
            StartCoroutine(Bleeding(amount));
        }

        public void Suffocate(BodyPart part)
        {
            //currently prevents two different parts suffocating a unit (neck and chest for example)
            if (!myBrain.ActiveState(Brain.State.Suffocating))
            {
                myBrain.ToggleState(Brain.State.Suffocating, true);
                string line = "<color=red>" + gameObject.name + " is suffocating to death!" + "</color>";
                BattleReport.AddToBattleReport(line);
                anim.Suffocation();
                StartCoroutine(Suffocating(part));
            }
        }

        private IEnumerator Bleeding(float blood)
        {
            //using 20 here because 12 parts * 5 severity = 1200 (roughly, this gets inaacurate with creatures with more/less parts)
            float bloodNeeded = 600; //half of 1200 which I've set as a hardcoded default blood volume for all units

            while (blood > 0 && totalBlood > 0)
            {
                totalBlood -= blood;
                blood = (blood / 2) - 1f;
                //Debug.Log(gameObject.name + " is bleeding for " + damage + " damage");
                yield return new WaitForSeconds(1f);
            }

            if (totalBlood <= bloodNeeded)
            {
                SlipIntoShock();
            }

            yield break;
        }

        private void SlipIntoShock()
        {
            if (!myBrain.ActiveState(Brain.State.Shock)) // if not already in sheck
            {
                string line = "<color=red>" + gameObject.name + " is experiencing shock from loss of blood!</color>";
                BattleReport.AddToBattleReport(line);
                myBrain.ToggleState(Brain.State.Shock, true);
                anim.Shock();
                StartCoroutine(Shock());
            }
        }

        private IEnumerator Shock()
        {
            float bloodNeeded = 600; //half of 1200 which I've set as a hardcoded default blood volume for all units
            float deathTimer = 20f;


            while (totalBlood < bloodNeeded)
            {
                deathTimer -= Time.deltaTime;
                Debug.Log(deathTimer);
                if (deathTimer <= 0)
                {
                    string line = "<color=red>" + gameObject.name + " has bled out!</color>";
                    BattleReport.AddToBattleReport(line);
                    StopAllCoroutines(); //need this in case we are suffocating and die this way first
                    myBrain.Die();
                    yield break;
                }
                yield return null;
            }

            //if we've exited the while loop before the timer is up then we've re-accumulated the lost blood and can exit shock
            myBrain.ToggleState(Brain.State.Shock, false);
            yield break;
        }

        private IEnumerator Suffocating(BodyPart part)
        {
            float deathTimer = 20f;

            while (part.SeverityLevel() >= part.SuffocationThreshold()) 
            {
                deathTimer -= Time.deltaTime;
                Debug.Log(deathTimer);
                if (deathTimer <= 0)
                {
                    string line = "<color=red>" + gameObject.name + " has suffocated to death!" + "</color>";
                    BattleReport.AddToBattleReport(line);
                    StopAllCoroutines(); //need this in case we are in shock and die this way first
                    myBrain.Die();
                    yield break;
                }
                yield return null;
            }

            //if we've exited the while loop before the timer is up then we've re-accumulated the lost air and can exit suffocation
            myBrain.ToggleState(Brain.State.Suffocating, false);
            yield break;
        }
    #endregion

    #region Saving and Loading BodyParts

        public void LoadParts(List<BodyPart> savedbodyParts)
        {
            totalBlood = 1200f; // - (bodyPartDamage.Sum(x => x.Value) * 20);
            Debug.Log("Need to update load part damage, obsolete method?");
            if (onHealthLoaded != null)
                onHealthLoaded.Invoke();
        }

        public List<BodyPart> GetBodyParts()
        {
            return bodyParts;
        }

        #endregion

}
