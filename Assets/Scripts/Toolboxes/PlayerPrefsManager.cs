using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour
{
    private const string MASTER_VOLUME_KEY = "master_volume";

    //Set a float via Unity's Player Prefs
    public static void SetMasterVolume(float volume)
    {

        if (volume >= 0f && volume <= 1f) //ensure we don't go out of range
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Invalid volume input");
        }
    }

    //Return a float via Unity's Player Prefs
    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }
}