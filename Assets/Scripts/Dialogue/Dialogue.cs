using UnityEngine;

[System.Serializable]
public class Dialogue {
    public UnitStateMachine unit;
    public string name;

    [TextArea(3,10)]
    public string[] sentences;
}
