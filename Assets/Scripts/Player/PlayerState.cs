using UnityEngine;

public class PlayerState : MonoBehaviour {

    public enum PlayerStates { Idle, Speaking, Fighting, Shopping, Dead }

    PlayerStates currentState;
    //PlayerController player;
    //public bool canMove = false;

    // Use this for initialization
    void Start()
    {
        //player = PlayerManager.instance.player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void SetPlayerState(PlayerStates newState) //possibly make this return a bool that can tell sender if state switch was successful or not
    {

        switch (newState)
        {
            case PlayerStates.Idle:
                currentState = newState;
                break;
            case PlayerStates.Speaking:
                currentState = newState;
                break;
            case PlayerStates.Fighting:
                currentState = newState;
                break;
            case PlayerStates.Shopping:
                currentState = newState;
                break;
            case PlayerStates.Dead:
                currentState = newState;
                break;
            default:
                Debug.LogError("Unknown player state");
                break;
        }
        Debug.Log("Player state is: " + currentState);
    }

    public bool CheckPlayerState(PlayerStates[] stateChecks) //possibly take an array of states to check
    {
        for (int i = 0; i < stateChecks.Length; i++)
        {
            if (currentState == stateChecks[i])
            {
                return true;
            }
        }
        return false;
    }
}
