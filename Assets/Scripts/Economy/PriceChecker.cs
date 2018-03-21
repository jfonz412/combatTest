using UnityEngine;

public class PriceChecker : MonoBehaviour {

    public static float AppraiseItem(Item item, string exchangeType)
    {
        if(exchangeType == "Sale")
        {
            //fancy economy modifiers
            //fancy stat modifiers
            //fancy race modifiers
            //fancy npc relationship modifiers
            return item.value * 0.9f;
        }
        else if(exchangeType == "Purchase")
        {
            //modifiers
            return item.value;
        }

        Debug.LogWarning("exchangeType could not be determined");
        return item.value;
    }
}
