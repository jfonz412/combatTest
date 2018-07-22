using System;
using System.Collections.Generic;

[Serializable]
public class HumanoidRightFoot : BodyPart
{
    protected override void AssignPartStats()
    {
        armorType = Item.EquipmentSlot.Feet; //put this before callback is assigned in base class
        name = "right foot";
        functioningLimit = 4;
        //knockoutThreshold = 3;
        vomitThreshold = 5;
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
                     "The force of the {1} cracks the bones in {0}'s " + name + "!",
                     "The force of the {1} crushes the bones of {0}'s " + name + "!",
                     "The force of the {1} completely obliterates {0}'s " + name + "!"
                }
            },

                        //0 is name, 1 is weapon 2 is armor
            {Item.AttackType.Stab, new string[]
                {
                     "The {1} pokes at the skin of {0}'s " + name + "!",
                     "The {1} slices at the skin of {0}'s " + name + "!",
                     "The {1} stabs into {0}'s " + name + "!",
                     "The {1} stabs completely through {0}'s " + name + "!",
                     "The {1} fractures the bones in {0}'s " + name + "!",
                     "The {1} completely mangles {0}'s " + name + "!"
                }
            }
        };

    }
    #endregion
}