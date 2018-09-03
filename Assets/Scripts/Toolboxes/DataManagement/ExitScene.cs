using UnityEngine;

public class ExitScene : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;
    [SerializeField]
    private Vector3 playerSpawnPoint;
    private Transform player;

    private void Start()
    {
        ApplicationManager app = ApplicationManager.GetInstance();
        DataManager dm = app.GetDataManager();
        dm.SaveCurrentScene();
        player = ScriptToolbox.GetInstance().GetPlayerManager().player.transform;

        LevelManager.sceneExits.Add(this);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform == player)
        {
            if (sceneToLoad == null || sceneToLoad == "")
            {
                return;
            }
            else
            {
                LoadScene();
            }
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

    //places player in proper position for the level they are about to enter
    private void PositionPlayer()
    {
        player.position = playerSpawnPoint;
        Debug.Log("Player set to spawn at: " + playerSpawnPoint.x + ", " + playerSpawnPoint.y);

        //player.spawncompanions() //will spawn based on players pos
    }
}
