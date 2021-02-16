using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingDialogueTrigger : MonoBehaviour
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
        firstDialogue.sentences[0] = "Hello there.";
        firstDialogue.sentences[1] = "You finally arrived! ";
        firstDialogue.sentences[2] = "With your help, I am sure we will defeat the dark power from the swamp.";
        firstDialogue.sentences[3] = "But before that i have other quests to test your strength and abilities.";
        firstDialogue.sentences[4] = "Go and kill this little frog, but be careful event tho she is small, she is also dangerous.";
        firstDialogue.sentences[5] = "After that come back to me.";
        dialogue.Add(firstDialogue);

        //Create dialogue
        var secondDialogue = new Dialogue();
        secondDialogue.name = "King Zbyszek";
        secondDialogue.sentences = new string[5];
        secondDialogue.sentences[0] = "I see that you killed this nasty frog.";
        secondDialogue.sentences[1] = "Amazing job, but i have other quest for you!";
        secondDialogue.sentences[2] = "Go kill a golem, who watches over the treasure.";
        secondDialogue.sentences[3] = "He is in the mine just under our feet.";
        secondDialogue.sentences[4] = "And then find me in the village on the east.";
        dialogue.Add(secondDialogue);
        //Dissapeared

        var thirdDialogue = new Dialogue();
        thirdDialogue.name = "King Zbyszek";
        thirdDialogue.sentences = new string[6];
        thirdDialogue.sentences[0] = "You killed it!";
        thirdDialogue.sentences[1] = "I see you got a health potion.";
        thirdDialogue.sentences[2] = "Remember to use you potion when your health is low.";
        thirdDialogue.sentences[3] = "Now its time for you to visit the castle.";
        thirdDialogue.sentences[4] = "Visit the blacksmith and then jump on the platforms.";
        thirdDialogue.sentences[5] = "Don't forget to upgrade your armor!.";
        dialogue.Add(thirdDialogue);
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
