using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueMenager : MonoBehaviour
{
    private Queue<string> sentences;
    public GameObject dialogueBox;

    public Text nameText;
    public Text dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();

        dialogueBox.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueBox.SetActive(true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string x in dialogue.sentences)
        {
            sentences.Enqueue(x);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    public void EndDialog()
    {
        dialogueBox.SetActive(false);
        //Debug.Log("KONIEC DIALOGU");
    }
}
