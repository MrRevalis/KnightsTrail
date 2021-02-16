using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public string currentScene;
    #region public objects
    public GameObject healthControllerContainer;
    public GameObject moneySystemContainer;
    public GameObject? king;
    public GameObject questContainer;
    public GameObject menuContainer;
    public GameObject blacksmithContainer;
    #endregion
    #region private scripts
    private HealthController healthController;
    private MoneySystem moneySystem;
    private QuestManager questManager;
    private PauseMenuScript menu;
    private BlacksmithController blacksmith;
    #endregion
    private GameData savedData;
    private bool loadSuccess = true;

    public void Save()
    {
        var data = new GameData();
        data.currentScene = currentScene;
        //reseltion etc
        if (PlayerPrefs.HasKey("Volume"))
        {
            data.volume = PlayerPrefs.GetFloat("Volume");
        }
        if (PlayerPrefs.HasKey("isFullscreen"))
        {
            if (PlayerPrefs.GetString("isFullscreen") == "False")
                data.isFullScreen = false;
            else
                data.isFullScreen = true;
        }
        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            data.resolution = PlayerPrefs.GetInt("ResolutionIndex");
        }
        //health
        data.health = healthController.currentHealth;
        data.healthPotions = healthController.potions;
        data.maxHealth = healthController.MaxHealth;
        //money
        data.coins = moneySystem.coins;
        //quest
        var quest = questManager.quest;
        data.Description = quest.Description;
        data.Reward = quest.Reward;
        data.KillCounter = quest.KillCounter;
        data.KillGoal = quest.KillGoal;
        data.KillType = quest.KillType;
        data.ArriveType = quest.ArriveType;
        data.UseType = quest.UseType;
        data.TalkType = quest.TalkType;
        data.Finished = quest.Finished;
        switch (quest.Type)
        {
            case QuestGoal.USE:
                data.Type = 0; break;
            case QuestGoal.KILL:
                data.Type = 1; break;
            case QuestGoal.ARRIVE:
                data.Type = 2; break;
            case QuestGoal.TALK:
                data.Type = 3; break;
        }
        //king barriers
        var ks = king != null ? king.GetComponent<KingScript>() : null;
        if(ks != null)
            data.questPart = ks.questPart;
        else if(savedData != null)
        {
            data.questPart = savedData.questPart;
        }
        //armour
        data.armour = blacksmith.slider.value;

        SaveWorker.SaveGame(data);
    }

    private void Load()
    {
        GameData data = SaveWorker.Load();
        savedData = data;
        LoadData();
    }

    private void LoadData()
    {
        if (savedData != null)
        {
            Debug.Log("loading");
            //healt and potions
            healthController.currentHealth = savedData.health;
            healthController.MaxHealth = savedData.maxHealth;
            healthController.potions = savedData.healthPotions;
            loadSuccess = healthController.UpdateData();
            //money
            moneySystem.coins = savedData.coins;
            moneySystem.UpdateData();
            //resolution etc.
            menu.SetFullscreen(savedData.isFullScreen);
            menu.SetScreenSize(savedData.resolution);
            menu.SetVolume(savedData.volume);
            //quest
            QuestGoal[] qtypes = { QuestGoal.USE, QuestGoal.KILL, QuestGoal.ARRIVE, QuestGoal.TALK };
            var quest = new Quest(qtypes[savedData.Type], savedData.Description, savedData.Reward, savedData.KillGoal, savedData.KillType, savedData.ArriveType, savedData.UseType, savedData.TalkType);
            quest.KillCounter = savedData.KillCounter;
            quest.Finished = savedData.Finished;
            questManager.ChangeQuest(quest);
            //king barriers
            if (king != null)
            {
                king.GetComponent<KingScript>().RemoveBlockers(savedData.questPart);
            }
            //armour
            blacksmith.LoadSavedArmour(savedData.armour);
        }
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        healthController = healthControllerContainer.GetComponent<HealthController>();
        moneySystem = moneySystemContainer.GetComponent<MoneySystem>();
        questManager = questContainer.GetComponent<QuestManager>();
        menu = menuContainer.GetComponent<PauseMenuScript>();
        blacksmith = blacksmithContainer.GetComponent<BlacksmithController>();

        if (SaveWorker.SaveExists() && PlayerPrefs.GetString("newGame") == "false")
        {
            Load();
        }
        if(currentScene == "FirstScene")
        {
            PlayerPrefs.SetString("newGame", "false");
        }

        Save();
    }

    private void Update()
    {
        if (!loadSuccess)
        {
            LoadData();
        }
        //if (Input.GetKeyUp(KeyCode.U))
        //{
        //    Save();
        //}
        //if (Input.GetKeyUp(KeyCode.P))
        //{
        //    Load();
        //}
    }
}
