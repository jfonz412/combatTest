using UnityEngine.UI;
using UnityEngine;

public class ShopDialogue : MonoBehaviour {

    public Text currentMessage;
    LoadShop.MyShopDialogue[] currentDialogue;

    public void LoadShopDialogue(LoadShop.MyShopDialogue[] dialogue)
    {
        currentDialogue = dialogue;
        for(int i = 0; i < currentDialogue.Length; i++)
        {
            if(currentDialogue[i].messageType == LoadShop.MessageType.WELCOME)
            {
                currentMessage.text = currentDialogue[i].message;
                Debug.Log(currentDialogue[i].message);
            }
        }
    }

    public void SetCurrentMessage(LoadShop.MessageType type)
    {
        for (int i = 0; i < currentDialogue.Length; i++)
        {
            if (currentDialogue[i].messageType == type)
            {
                currentMessage.text = currentDialogue[i].message;
                Debug.Log(currentDialogue[i].message);
            }
        }
    }
}
