using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomDialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public GameObject dialogueBox;

    public Text nameText;
    public Text dialogueText;

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

        foreach (string x in dialogue.sentences)
        {
            sentences.Enqueue(x);
        }

        DisplayNextSentence();
    }

    public bool DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return false;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        return true;
    }

    public void EndDialog()
    {
        dialogueBox.SetActive(false);
        //Debug.Log("KONIEC DIALOGU");
    }
}
