using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private MusicManager musicManager;

    private void Start()
    {
        musicManager = FindObjectOfType<MusicManager>(); //this is bad but I'm leaving it in because it's just the options menu
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
    }

    private void Update()
    {
        musicManager.ChangeVolume(volumeSlider.value);
    }

    public void SaveAndExit()
    {
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
        ApplicationManager.GetInstance().GetLevelManager().LoadScene("Title"); //will eventually un-hardcode this to load previous scene
    }
}
