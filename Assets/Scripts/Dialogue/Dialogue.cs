using UnityEngine;

[System.Serializable]
public class Dialogue {

    public NPCStateMachine unit; //to pass to Dialogue manager so it knows who is talking
    public string name;

    [TextArea(3,10)]
    public string[] sentences;
}
