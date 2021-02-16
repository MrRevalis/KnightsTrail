using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Quest quest;
    public GameObject questPanel;
    public Text desciption;
    public Text questGoal;
    public Text goalAmount;
    public AudioSource questCompleted;

    private bool firstQuestBool;

    private void Start()
    {
        var firstQuest = new Quest(QuestGoal.TALK, "Talk to the king", 20, 0, string.Empty, string.Empty, string.Empty, "King");
        firstQuestBool = true;
        ChangeQuest(firstQuest);
    }

    public void ChangeQuest(Quest newQuest)
    {
        Debug.Log(newQuest.Description);
        quest = newQuest;
        desciption.text = quest.Description;
        goalAmount.text = "x"+quest.Reward.ToString();
        switch (newQuest.Type)
        {
            case QuestGoal.KILL:
                questGoal.text = "Kill " + quest.KillType + " " + quest.KillCounter + "/" + quest.KillGoal;
                if (firstQuestBool == false)
                    questCompleted.Play();
                break;
            case QuestGoal.ARRIVE:
                questGoal.text = "Arrive to " + quest.ArriveType;
                if (firstQuestBool == false)
                    questCompleted.Play();
                break;
            case QuestGoal.USE:
                questGoal.text = "Use " + quest.UseType;
                if (firstQuestBool == false)
                    questCompleted.Play();
                break;
            case QuestGoal.TALK:
                questGoal.text = "Talk to the " + quest.TalkType;
                if(firstQuestBool == false)
                    questCompleted.Play();
                Debug.Log("Byle co " + firstQuestBool);
                quest.Finished = true;
                break;
        }
        firstQuestBool = false;
        Debug.LogWarning(quest.KillType);
        if (quest.Type == QuestGoal.KILL && quest.KillCounter >= quest.KillGoal)
            ChangeGoalCount(new GameObject() { tag = quest.KillType });
    }

    public void ChangeGoalCount(GameObject gameObject)
    {
        if (quest.Type == QuestGoal.KILL && gameObject.tag.Equals(quest.KillType))
        {
            quest.KillCounter++;
            questGoal.text = quest.KillType + " " + quest.KillCounter + "/" + quest.KillGoal;
            if (quest.KillCounter >= quest.KillGoal)
            {
                quest.Finished = true;
                questGoal.text = "Goal achieved, return to the king";
            }
        }
        else if(quest.Type == QuestGoal.ARRIVE && gameObject.tag.Equals(quest.ArriveType))
        {
            //Dotarto do danego miejsca
            quest.Finished = true;
        }
        else if(quest.Type == QuestGoal.USE && gameObject.tag.Equals(quest.UseType))
        {
            //Uzyto danego itemu
        }
    }


}
