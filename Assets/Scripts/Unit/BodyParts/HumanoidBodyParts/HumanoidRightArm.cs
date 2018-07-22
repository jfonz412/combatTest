using System.Collections.Generic;

public class HumanoidRightArm : BodyPart
{
    protected override void AssignPartStats()
    {
        armorType = Item.EquipmentSlot.Chest; //put this before callback is assigned in base class
        name = "right arm";
        functioningLimit = 4;
        //knockoutThreshold = 0;
        vomitThreshold = 5;
        //rockedThreshold = 0;
        downedThreshold = 3;
        //cantBreathThreshold = 0;
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

    protected override Item Unarmed()
    {
        return MasterItemList.Fist();
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
                     "The force of the {1} cracking a bone in {0}'s " + name + "!",
                     "The force of the {1} crushes the bones of {0}'s " + name + "!",
                     "The force of the {1} completely pulverizes {0}'s " + name + "!"
                }
            },

                        //0 is name, 1 is weapon 2 is armor
            {Item.AttackType.Stab, new string[]
                {
                     "The {1} pokes at the skin of {0}'s " + name + "!",
                     "The {1} slices into flesh of {0}'s " + name + "!",
                     "The {1} stabs into {0}'s " + name + "!",
                     "The {1} stabs deep into {0}'s " + name + "!",
                     "The {1} seriously gores {0}'s " + name + "!",
                     "The blade of the {1} passes completely through {0}'s " + name + "!"
                }
            }
        };

    }
    #endregion

}
