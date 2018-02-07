using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public GameObject dialogueWindow;

    public Text nameText;
    public Text dialogueText;

    [HideInInspector]
    public bool isOpen = false;

    Queue<string> sentences;

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
        sentences = new Queue<string>();
	}


    public void StartDialogue(Dialogue dialogue)
    {
        isOpen = true;
        dialogueWindow.SetActive(isOpen);

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
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        isOpen = false;
        dialogueWindow.SetActive(isOpen);

        Debug.Log("End of conversation");
    }
}
