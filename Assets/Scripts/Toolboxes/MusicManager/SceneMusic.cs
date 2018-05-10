using UnityEngine;

//this script just holds this scene's music
public class SceneMusic : MonoBehaviour {
    [SerializeField]
    private AudioClip[] sceneMusic; //maybe eventually turn this into a list/array of strings/constant strings?

    public AudioClip[] GetMusic()
    {
        return sceneMusic;
    }
}
