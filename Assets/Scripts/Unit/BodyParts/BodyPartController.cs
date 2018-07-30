using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class BodyPartController : MonoBehaviour {
    private EquipmentManager equipment;
    private CombatSkills mySkills;
    private UnitReactions unitReactions;
    private UnitAnimController anim;
    private AttackReactionSkills attackReaction;
    private Brain myBrain;
    private DefaultEquipment defaultEquipment;
    public float totalBlood;

    private List<BodyPart> bodyParts = new List<BodyPart>();
    [SerializeField]
    private List<BodyPart> vitalParts;

    //for attack controller
    public List<BodyPart> attack1Parts;
    public List<BodyPart> attack2Parts;

    //move this to BodyPart?
    private Brain.State[] vulnerableStates = new Brain.State[]
    {
        Brain.State.Suffocating,
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
        unitReactions = GetComponent<UnitReactions>();
        anim = GetComponent<UnitAnimController>();
        myBrain = GetComponent<Brain>();
        defaultEquipment = GetComponent<DefaultEquipment>();
        mySkills = GetComponent<CombatSkills>();
        mySkills.onSkillGained += UpdateSkills;
        UpdateSkills();       

        AddBodyParts(); //collects the bodyparts from the unit, part stats should be set by now
        LoadAttackParts(); //saves attack parts into a list for the AttackController to reference
        CalculateTotalBlood(); //bodyparts needed for this

        defaultEquipment.EquipLoadout(this);
    }

    private void CalculateTotalBlood()
    {
        int totalSeverity = 0;
        for(int i = 0; i < bodyParts.Count; i++)
        {
            totalSeverity += bodyParts[i].SeverityLevel();
        }
        totalBlood = (bodyParts.Count * 100f) - (25f * totalSeverity);
    }

    #region Public Methods
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
    public void AddBodyParts()
    {
        BodyPart[] bp = GetComponents<BodyPart>();
        bodyParts = bp.ToList();
    }

#endregion

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
    private void LoadAttackParts()
    {
        Attack1Parts();
        Attack2Parts();
    }

    //returns a list of parts with attack1
    private void Attack1Parts()
    {
        for (int i = 0; i < bodyParts.Count; i++)
        {
            if (bodyParts[i].attack1)
            {
                attack1Parts.Add(bodyParts[i]);
                //Debug.Log("added " + bodyParts[i] + " for " + gameObject.name);
            }
        }
    }

    //returns a list of pasts with attack2
    private void Attack2Parts()
    {
        for (int i = 0; i < bodyParts.Count; i++)
        {
            if (bodyParts[i].attack2)
            {
                attack2Parts.Add(bodyParts[i]);
                //Debug.Log("added " + bodyParts[i] + " for " + gameObject.name);
            }
        }
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

    //applies saved info to bodyparts
    public void LoadSavedParts(BodyPart.PartInfo[] info)
    {
        for (int i = 0; i < bodyParts.Count; i++)
        {
             bodyParts[i].UnpackSavedPartInfo(info[i]);
        }

        CalculateTotalBlood();
    }

    public BodyPart.PartInfo[] GetBodyParts()
    {
        BodyPart.PartInfo[] info = new BodyPart.PartInfo[bodyParts.Count];

        for(int i = 0; i < bodyParts.Count; i++)
        {
            info[i] = bodyParts[i].PackagePartInfo();
        }

        return info;
    }

    #endregion

    private void UpdateSkills()
    {
        attackReaction = mySkills.GetAttackReactionSkills();
    }
}
