using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] levelMusic;
    [SerializeField]
    private AudioSource audioSource;
    private int currentLevel = 0;
    private AudioClip currentMusic;

    public static GameObject musicManager;

    void Awake()
    {
        if(musicManager != null)
        {
            Destroy(gameObject);
        }
        else
        {
            musicManager = gameObject;
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += LevelWasLoaded;
        DontDestroyOnLoad(gameObject);
        Play(levelMusic[currentLevel]);
    }

    //maybe call this in LevelManager, we might not want to play a clip everytime a scene is loaded
    //either way we need to be able to tell this class what the currentLevel is so it loads the proper AudioClip
    private void LevelWasLoaded(Scene scene, LoadSceneMode mode) //requires these params in order to be "delegated"
    {
        if (currentMusic != levelMusic[currentLevel]) //don't start the track over if it's already playing
        {
            Play(levelMusic[currentLevel]);
        }
        else
        {
            Debug.LogWarning("No music!!");
        }
    }

    private void Play(AudioClip music)
    {
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.Play();
        currentMusic = music;
    }

    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume;
    }
}