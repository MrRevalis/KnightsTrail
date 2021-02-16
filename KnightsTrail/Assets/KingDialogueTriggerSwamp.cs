using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingDialogueTriggerSwamp : MonoBehaviour
{
    public List<Dialogue> dialogue;
    public Dialogue baseDialogue;

    public void Start()
    {
        baseDialogue = new Dialogue();
        baseDialogue.name = "King Zbyszek";
        baseDialogue.sentences = new string[1];
        baseDialogue.sentences[0] = "Complete your quest, then we can talk.";

        var firstDialogue = new Dialogue();
        firstDialogue.name = "King Zbyszek";
        firstDialogue.sentences = new string[6];
        firstDialogue.sentences[0] = "I see that you arrived.";
        firstDialogue.sentences[1] = "Stronger than ever.";
        firstDialogue.sentences[2] = "I brought some backup, where you can upgrade or refill you equipment.";
        firstDialogue.sentences[3] = "After that we will fight for dead with wizard.";
        firstDialogue.sentences[4] = "Yes, I said 'we', because I will join my forces with you.";
        firstDialogue.sentences[5] = "Together we are unstopable.";
        dialogue.Add(firstDialogue);

        //Create dialogue
        var secondDialogue = new Dialogue();
        secondDialogue.name = "King Zbyszek";
        secondDialogue.sentences = new string[5];
        secondDialogue.sentences[0] = "We did it";
        secondDialogue.sentences[1] = "He is dead.";
        secondDialogue.sentences[2] = "We can finally live in peace.";
        secondDialogue.sentences[3] = "Because of your heroism.";
        secondDialogue.sentences[4] = "Thank you very much and good luck in your future mission.";
        dialogue.Add(secondDialogue);

    }

    public void TriggerDialogue(int index)
    {
        Debug.Log("Pokaz test");
        FindObjectOfType<BottomDialogueManager>().StartDialogue(dialogue[index]);
    }

    public void TriggerDialogue()
    {
        Debug.Log("Pokaz test pusty");
        FindObjectOfType<BottomDialogueManager>().StartDialogue(baseDialogue);
    }
}
