﻿using UnityEngine;

//REPLACED BY BRAIN STATES
public class PlayerState : MonoBehaviour {
    /*
    public enum PlayerStates { Idle, Speaking, Fighting, Shopping, Dead, Prompt, Paused, BattleReport }

    [SerializeField]
    private static PlayerStates currentState;

    public static PlayerStates GetPlayerState()
    {
        return currentState;
    }

    public static void SetPlayerState(PlayerStates newState) //possibly make this return a bool that can tell sender if state switch was successful or not
    {
        //CAN PROBABLY JUST DO 
            //currentState = newState
        //AND CATCH ANY INVALID STATES, NOT SURE WHY i HAVE THIS SWITCH STATEMENT
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
            case PlayerStates.Prompt:
                currentState = newState;
                break;
            case PlayerStates.BattleReport:
                currentState = newState;
                break;
            case PlayerStates.Paused:
                currentState = newState;
                break;
            default:
                Debug.LogError("Unknown player state");
                break;
        }
        //Debug.Log("Player state is: " + currentState);
    }

    public static bool CheckPlayerState(PlayerStates[] stateChecks) //possibly take an array of states to check
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
    */
}
