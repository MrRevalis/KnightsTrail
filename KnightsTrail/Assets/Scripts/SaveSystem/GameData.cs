using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int health;
    public int maxHealth;
    public int healthPotions;
    public int coins;
    public string currentScene;
    public int stage;

    public bool isFullScreen;
    public int resolution;
    public float volume;

    //quest
    public string Description;
    public int Reward;
    public int Type;
    public int KillCounter;
    public int KillGoal;
    public string KillType;
    public string ArriveType;
    public string UseType;
    public string TalkType;
    public bool Finished;
    //king barriers
    public int questPart;
    //armour
    public float armour;
}
