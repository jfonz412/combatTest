using System.Collections.Generic;

public class HumanoidNeck : BodyPart
{
    protected override void AssignPartStats()
    {
        armorSlot = Item.EquipmentSlot.Chest; //put this before callback is assigned in base class
        name = "neck";
        functioningLimit = 4;
        //knockoutThreshold = 3;
        //vomitThreshold = 5;
        //rockedThreshold = 2;
        downedThreshold = 3;
        cantBreathThreshold = 3;
        suffocationThreshold = 4;

        bleedRate = 3f;
    }

    protected override void StatusChecks(int severity)
    {
        //RockedCheck(severity);
        DownedCheck(severity);
        //VomitCheck(severity);
        CantBreathCheck(severity);
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
                     "The force of the {1} bruises {0}'s " + name + "!",
                     "The force of the {1} heavily bruises {0}'s " + name + "!",
                     "The force of the {1} seriously bruises {0}'s " + name + "!",
                     "The force of the {1} crushes the meat of {0}'s " + name + "",
                     "The force of the {1} crumples part of {0}'s " + name + "!",
                     "The force of the {1} completely crushes {0}'s " + name + "!"
                }
            },

                        //0 is name, 1 is weapon 2 is armor
            {Item.AttackType.Stab, new string[]
                {
                     "The point of the {1} pokes into the flesh of {0}'s " + name + "!",
                     "The point of the {1} stabs into {0}'s " + name + "!",
                     "The point of the {1} stabs deeply into {0}'s " + name + "",
                     "The blade of the {1} pierces completely through {0}'s " + name + "!",
                     "The blade of the {1} pierces completely through {0}'s " + name + "!",
                     "The blade of the {1} pierces completely through {0}'s " + name + "!"
                }
            }
        };

    }
    #endregion
}

