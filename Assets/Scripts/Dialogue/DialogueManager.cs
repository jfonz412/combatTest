using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Animator dialogueWindow;

    public Text nameText;
    public Text dialogueText;

    [HideInInspector]
    public bool isOpen = false;

    Queue<string> sentences;


    // Use this for initialization
    void Start () {
        sentences = new Queue<string>();
	}

    public void StartDialogue(Dialogue dialogue)
    {
        //player.incapacitated = true; //disable player until dialogue has ended
        PlayerState.SetPlayerState(PlayerState.PlayerStates.Speaking);

        isOpen = true;
        dialogueWindow.SetBool("isOpen", isOpen);

        nameText.text = dialogue.name;

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
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentance)
    {
        dialogueText.text = "";
        foreach(char letter in sentance.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null; //waits a single frame
        }
    }

    void EndDialogue()
    {
        //player.incapacitated = false;
        PlayerState.SetPlayerState(PlayerState.PlayerStates.Idle);

        isOpen = false;
        dialogueWindow.SetBool("isOpen", isOpen);
    }
}
