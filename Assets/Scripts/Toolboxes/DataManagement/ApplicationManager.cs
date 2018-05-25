using UnityEngine;

public class ApplicationManager : MonoBehaviour {
    private static ApplicationManager instance;
    private DataManager dataManager;
    private LevelManager levelManager;
    private SceneMusic sceneMusic;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    //leave these in Start() because I want them to load after the other Toolboxes gather their components
    private void Start()
    {
        dataManager = GetComponent<DataManager>();
        levelManager = GetComponent<LevelManager>();
    }

    public static ApplicationManager GetInstance()
    {
        return instance;
    }

    public DataManager GetDataManager()
    {
        if (dataManager == null)
        {
            dataManager = GetComponent<DataManager>();
        }
        return dataManager;
    }

    public LevelManager GetLevelManager()
    {
        return levelManager;
    }

    public SceneMusic GetSceneMusic()
    {
        //this MIGHT take care of some of the script order issues I'm having...or it might make it worse, not sure yet
        //I think overall it will cut down on the missing scripts because AppManager doesn't have to be created last?
        if(sceneMusic == null)
        {
            sceneMusic = GetComponent<SceneMusic>();
        }
        return sceneMusic;
    }
}
