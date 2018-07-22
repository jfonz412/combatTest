using System.Collections.Generic;

public class HumanoidHead : BodyPart
{
    protected override void AssignPartStats()
    {
        armorType = Item.EquipmentSlot.Head; //put this before callback is assigned in base class
        name = "head";
        functioningLimit = 5;
        knockoutThreshold = 3;
        vomitThreshold = 3;
        rockedThreshold = 2;
        downedThreshold = 3;
        //cantBreathThreshold = 3;
        //suffocationThreshold = 4;
    }

    protected override void StatusChecks(int severity)
    {
        RockedCheck(severity);
        DownedCheck(severity);
        VomitCheck(severity);
        //CantBreathCheck(severity);
        KnockoutCheck(severity);
    }

    #region Injury Strings
    protected override void SetInjuryStrings()
    {
        myInjuryStrings = new Dictionary<Item.AttackType, string[]>()
        {
            //0 is name, 1 is weapon 2 is armor
            {Item.AttackType.BluntImpact, new string[]
                {
                     "The force of the {1} leaves a light bruise on {0}'s " + name + "!",
                     "The force of the {1} bruises {0}'s " + name + "!",
                     "The force of the {1} heavily bruises {0}'s " + name + "!",
                     "The force of the {1} gives {0} a serious concussion",
                     "The force of the {1} cracks {0}'s skull!",
                     "The force of the {1} completely caves in {0}'s " + name + "!"
                }
            },

                        //0 is name, 1 is weapon 2 is armor
            {Item.AttackType.Stab, new string[]
                {
                     "The point of the {1} pokes at the skin of {0}'s " + name + "!",
                     "The point of the {1} leaves a decent gash flesh of {0}'s " + name + "!",
                     "The point of the {1} jabs into {0}'s " + name + ", the neck is pulled with the force",
                     "The point of the {1} slams into the " + name + " and cracks {0}'s skull!",
                     "The blade of the {1} jams halfway into {0}'s skull!",
                     "The blade of the {1} pierces completely through {0}'s " + name + "!"
                }
            }
        };

    }
    #endregion
}
