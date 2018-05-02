using UnityEngine;

public class ScriptToolbox : MonoBehaviour {

    private static ScriptToolbox instance;
    private DialogueManager dialogueManager;
    private CloseOpenWindows windowCloser;
    private ShopDialogue shopDialogue;
    private PlayerWallet playerWallet;
    private PlayerManager playerManager;
    private UnitReactionManager unitReactionManager;

    #region Singleton

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of DialogueManager found");
            return;
        }
        instance = this;
    } 
    #endregion

    void Start()
    {
        dialogueManager = GetComponent<DialogueManager>();
        windowCloser = GetComponent<CloseOpenWindows>();
        shopDialogue = GetComponent<ShopDialogue>();
        playerWallet = GetComponent<PlayerWallet>();
        playerManager = GetComponent<PlayerManager>();
        unitReactionManager = GetComponent<UnitReactionManager>();
    }

    public static ScriptToolbox GetInstance()
    {
        return instance;
    }

    public DialogueManager GetDialogueManager()
    {
        return dialogueManager;
    }

    public CloseOpenWindows GetWindowCloser()
    {
        return windowCloser;
    }
    public ShopDialogue GetShopDialogue()
    {
        return shopDialogue;
    }

    public PlayerWallet GetPlayerWallet()
    {
        return playerWallet;
    }

    public PlayerManager GetPlayerManager()
    {
        return playerManager;
    }

    public UnitReactionManager GetUnitReactionManager()
    {
        return unitReactionManager;
    }
}
