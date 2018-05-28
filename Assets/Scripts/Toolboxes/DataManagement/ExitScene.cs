using UnityEngine;

public class ExitScene : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;
    [SerializeField]
    private Vector3 playerSpawnPoint;

    private void Start()
    {
        ApplicationManager app = ApplicationManager.GetInstance();
        DataManager dm = app.GetDataManager();
        dm.SaveCurrentScene();
    }

    private void OnTriggerEnter2D()
    {
        if(sceneToLoad == null || sceneToLoad == "")
        {
            return;
        }
        else
        {
            LoadScene();
        }

    }

    private void LoadScene()
    {
        ApplicationManager app = ApplicationManager.GetInstance();
        DataManager dm = app.GetDataManager();

        PositionPlayer();
        dm.SaveDataToTemp();
        app.GetLevelManager().LoadScene(sceneToLoad);
    }

    private void PositionPlayer()
    {
        Transform player = ScriptToolbox.GetInstance().GetPlayerManager().player.transform;

        player.position = playerSpawnPoint;
        Debug.Log("Player set to spawn at: " + playerSpawnPoint.x + ", " + playerSpawnPoint.y);
    }
}
