using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    private AudioClip[] sceneMusic;
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
        sceneMusic = ApplicationManager.GetInstance().GetSceneMusic().GetMusic();
        SceneManager.sceneLoaded += SceneWasLoaded;
        DontDestroyOnLoad(gameObject);
        Play(0);
    }

    private void SceneWasLoaded(Scene scene, LoadSceneMode mode) //requires these params in order to be delegated
    {
        Play(0);
    }

    public void Play(int trackNumber)
    {
        if(sceneMusic.Length > trackNumber) //should make sure we stay in range
        {
            if (currentMusic != sceneMusic[trackNumber])
            {
                audioSource.clip = sceneMusic[trackNumber];
                audioSource.loop = true;
                audioSource.Play();
                currentMusic = sceneMusic[trackNumber];
            }
        }
    }

    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume;
    }
}