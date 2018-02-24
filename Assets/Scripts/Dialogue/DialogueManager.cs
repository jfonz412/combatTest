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

    PlayerController player;

    #region Singleton

    public static DialogueManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of DialogueManager found");
            return;
        }
        instance = this;
    }
    #endregion

    // Use this for initialization
    void Start () {
        player = PlayerManager.instance.player.GetComponent<PlayerController>();
        sentences = new Queue<string>();
	}


    public void StartDialogue(Dialogue dialogue)
    {
        player.incapacitated = true; //disable player until dialogue has ended

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
        player.incapacitated = false;
        isOpen = false;
        dialogueWindow.SetBool("isOpen", isOpen);
        //Debug.Log("End of conversation");
    }
}
