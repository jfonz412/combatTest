using System.Collections.Generic;

public class HumanoidChest : BodyPart
{
    protected override void AssignPartStats()
    {
        armorType = Item.EquipmentSlot.Chest; //put this before callback is assigned in base class
        name = "chest";
        functioningLimit = 4;
        //knockoutThreshold = 0;
        //vomitThreshold = 4;
        //rockedThreshold = 0;
        downedThreshold = 3;
        cantBreathThreshold = 3;
        suffocationThreshold = 4;
    }

    protected override void StatusChecks(int severity)
    {
        DownedCheck(severity);
        //VomitCheck(severity);
        CantBreathCheck(severity);
        
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
                     "The force of the {1} cracks the ribs of {0}'s " + name + "",
                     "The force of the {1} shatters the ribs of {0}'s " + name + "!",
                     "The force of the {1} completely caves in {0}'s " + name + "!"
                }
            },

                        //0 is name, 1 is weapon 2 is armor
            {Item.AttackType.Stab, new string[]
                {
                     "The point of the {1} pokes at the skin of {0}'s " + name + "!",
                     "The point of the {1} pokes into the flesh of {0}'s " + name + "!",
                     "The point of the {1} tears through the muscle of {0}'s " + name + "",
                     "The point of the {1} jams into the ribs {0}'s " + name + "!",
                     "The blade of the {1} cuts through the ribs of {0}'s " + name + "!",
                     "The blade of the {1} pierces completely through {0}'s " + name + "!"
                }
            }
        };

    }
    #endregion
}