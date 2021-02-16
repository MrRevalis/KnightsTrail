using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    //Opis
    public string Description { get; set; }
    //Nagroda
    public int Reward { get; set; }
    //Czyli co gracz musi zrobiæ
    public QuestGoal Type { get; set; }
    //Ilosc zabojstw
    public int KillCounter { get; set; } = 0;
    //Ile ma zabiæ
    public int KillGoal { get; set; }
    //Typ potworkow
    public string KillType { get; set; }
    //Do kogo ma przybyc
    public string ArriveType { get; set; }
    //Co ma uzyc
    public string UseType { get; set; }
    //Z kim pogadac
    public string TalkType { get; set; }
    public bool Finished { get; set; } = false;

    public Quest(QuestGoal type, string description, int reward, int killGoal, string killType, string arriveType, string useType, string talkType)
    {
        this.Description = description;
        this.Reward = reward;
        this.Type = type;
        this.KillCounter = 0;
        this.KillGoal = killGoal;
        this.KillType = killType;
        this.ArriveType = arriveType;
        this.UseType = useType;
        this.TalkType = talkType;
    }
}

public enum QuestGoal
{
    USE,
    KILL,
    ARRIVE,
    TALK
}
