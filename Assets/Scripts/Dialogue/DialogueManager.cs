using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Animator dialogueWindow;

    public Text nameText;
    public Text dialogueText;
    public UnitStateMachine unitTalking;

    [HideInInspector]
    public bool isOpen = false;

    Queue<string> sentences;

    // Use this for initialization
    void Start () {
        sentences = new Queue<string>();
	}

    public void StartDialogue(Dialogue dialogue)
    {
        isOpen = true;
        dialogueWindow.SetBool("isOpen", isOpen);

        nameText.text = dialogue.name;
        unitTalking = dialogue.unit;

        sentences.Clear(); //clear que of any old sentences

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); //adds sentances to our que
        }

        DisplayNextSentence();
    }
	
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            SoftDialogueExit();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentance)
    {
        dialogueText.text = "";
        foreach(char letter in sentance.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null; //waits a single frame
        }
    }

    //dialogue ended via continue button
    private void SoftDialogueExit() 
    {
        unitTalking.RequestChangeState(StateMachine.States.Idle);
        ScriptToolbox.GetInstance().GetPlayerManager().playerStateMachine.RequestChangeState(StateMachine.States.Idle);
        CloseDialogueWindow();
    }

    public void UnitExitingDialogueState(bool playerRequestedExit = false)
    {
        if (isOpen)
        {
            CloseDialogueWindow();
            unitTalking.RequestChangeState(StateMachine.States.Idle);
        }

        //if the player requested the change, put the other unit into the Idle state
        if (!playerRequestedExit)
        {
            ScriptToolbox.GetInstance().GetPlayerManager().playerStateMachine.RequestChangeState(StateMachine.States.Idle);
        }
    }

    public void CloseDialogueWindow()
    {
        isOpen = false;
        dialogueWindow.SetBool("isOpen", isOpen);
    }
}
