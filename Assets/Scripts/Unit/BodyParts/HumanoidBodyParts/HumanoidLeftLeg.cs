using System.Collections.Generic;

public class HumanoidLeftLeg : BodyPart
{
    protected override void AssignPartStats()
    {
        armorType = Item.EquipmentSlot.Legs; //put this before callback is assigned in base class

        functioningLimit = 4;
        //knockoutThreshold = 3;
        vomitThreshold = 4;
        //rockedThreshold = 2;
        downedThreshold = 3;
        //cantBreathThreshold = 3;
        //suffocationThreshold = 4;
    }

    protected override void StatusChecks(int severity)
    {
        //RockedCheck(severity);
        DownedCheck(severity);
        VomitCheck(severity);
        //CantBreathCheck(severity);
        //KnockoutCheck(severity);
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
                     "The force of the {1} leaves a light bruise on {0}'s left leg!",
                     "The force of the {1} bruises {0}'s left leg!",
                     "The force of the {1} heavily bruises {0}'s left leg!",
                     "The force of the {1} cracking a bone in {0}'s left leg!",
                     "The force of the {1} crushes the bones of {0}'s left leg!",
                     "The force of the {1} completely pulverizes {0}'s left leg!"
                }
            },

                        //0 is name, 1 is weapon 2 is armor
            {Item.AttackType.Stab, new string[]
                {
                     "The {1} pokes at the skin of {0}'s left leg!",
                     "The {1} slices into flesh of {0}'s left leg!",
                     "The {1} stabs into {0}'s left leg!",
                     "The {1} stabs deep into {0}'s left leg!",
                     "The {1} seriously gores {0}'s left leg!",
                     "The blade of the {1} passes completely through {0}'s left leg!"
                }
            }
        };

    }
    #endregion
}