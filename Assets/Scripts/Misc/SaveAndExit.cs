using UnityEngine.UI;
using UnityEngine;

public class SaveAndExit : MonoBehaviour {
    private Button button;
    private ApplicationManager game;

    [SerializeField]
    private bool exitAfterSave;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SaveGame); //(delegate { ApplicationManager.GetInstance().GetLevelManager().LoadScene(scene); });
        game = ApplicationManager.GetInstance();
    }

    private void SaveGame()
    {
        game.GetDataManager().SaveToPerm(); 

        if (exitAfterSave)
        {
            game.GetLevelManager().LoadScene("Title");
        }
    }
}
