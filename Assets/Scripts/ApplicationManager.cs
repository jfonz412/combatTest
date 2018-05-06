using UnityEngine;

public class ApplicationManager : MonoBehaviour {
    private static ApplicationManager instance;
    private DataManager dataManager;
    private LevelManager levelManager;

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
        return dataManager;
    }

    public LevelManager GetLevelManager()
    {
        return levelManager;
    }
}
