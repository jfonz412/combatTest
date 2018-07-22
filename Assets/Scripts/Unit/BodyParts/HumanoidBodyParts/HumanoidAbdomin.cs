using System.Collections.Generic;

public class HumanoidAbdomin : BodyPart {
    protected override void AssignPartStats()
    {
        armorType = Item.EquipmentSlot.Chest; //put this before callback is assigned in base class

        functioningLimit = 4;
        //knockoutThreshold = 0;
        vomitThreshold = 3;
        //rockedThreshold = 0;
        downedThreshold = 3;
        cantBreathThreshold = 2;
        suffocationThreshold = 6; //need to set this because we do a 'can't breathe' check for this part
    }

    protected override void StatusChecks(int severity)
    {
        DownedCheck(severity);
        VomitCheck(severity);
        CantBreathCheck(severity);
        Bleed(severity);
    }

    #region Injury Strings
    protected override void SetInjuryStrings()
    {
        myInjuryStrings = new Dictionary<Item.AttackType, string[]>()
        {
            //0 is name, 1 is weapon 2 is armor
            {Item.AttackType.BluntImpact, new string[]
                {
                     "The force of the {1} leaves a light bruise on {0}'s belly!",
                     "The force of the {1} bruises {0}'s belly!",
                     "The force of the {1} heavily bruises {0}'s belly!",
                     "The force of the {1} bruises {0}'s guts",
                     "The force of the {1} against {0}'s belly causes serious internal bleeding!",
                     "The force of the {1} obliterates {0}'s innards!"
                }
            },

                        //0 is name, 1 is weapon 2 is armor
            {Item.AttackType.Stab, new string[]
                {
                     "The point of the {1} pokes at the skin of {0}'s belly!",
                     "The point of the {1} pokes into the flesh of {0}'s belly!",
                     "The point of the {1} tears through the muscle of {0}'s belly!",
                     "The point of the {1} pokes {0} in the guts!",
                     "The blade of the {1} cuts deep into {0}'s belly!",
                     "The blade of the {1} pierces completely through {0}'s belly!"
                }
            }
        };

    }
    #endregion
}
