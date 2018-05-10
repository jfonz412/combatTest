using UnityEngine;

public class SetStartVolume : MonoBehaviour
{
    private MusicManager musicManager;
    private float volume = 100f; 

    // Use this for initialization
    void Start()
    {
        musicManager = GetComponent<MusicManager>();
        volume = PlayerPrefsManager.GetMasterVolume();
        Debug.Log(volume);
        musicManager.ChangeVolume(volume); 
    }
}
