using UnityEngine;

public class PriceChecker : MonoBehaviour {

    public static float AppraiseItem(Item item, string exchangeType)
    {
        if(exchangeType == "Sale")
        {
            //WILL REQUIRE INPUT FROM MULTIPLE OTHER "TRACKER" CLASSES WHICH WILL TRACK THIS INFORMATION
            //fancy economy modifiers
            //fancy stat modifiers

            //fancy race modifiers
            //fancy npc relationship modifiers 

            return item.baseValue * 0.9f;
        }
        else if(exchangeType == "Purchase")
        {
            //modifiers
            return item.baseValue;
        }

        Debug.LogWarning("exchangeType could not be determined");
        return item.baseValue;
    }
}
