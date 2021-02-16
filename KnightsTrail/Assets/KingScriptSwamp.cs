using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class KingScriptSwamp : MonoBehaviour
{
    public CinemachineVirtualCamera vCamera;
    public GameObject playerInstance;
    public KingDialogueTriggerSwamp dialogueTrigger;
    public BottomDialogueManager dialogueMenager;
    public QuestManager questManager;
    private List<Quest> questList;
    private Animator anim;

    public GameObject blocker1;
    public AudioSource teleportSound;

    private List<GameObject> questBlockers = new List<GameObject>();

    private bool canInteract = false;
    private bool dialogueStart = false;
    private float baseCameraSize;
    private int dialoguePart;
    public int questPart;
    private bool secondPosition = false;

    private Vector3 kingPosition = new Vector3(0, 0, 0);

    //Pierwszy quest w QuestManager
    void Start()
    {
        questBlockers.Add(blocker1);
        anim = GetComponent<Animator>();
        baseCameraSize = vCamera.m_Lens.OrthographicSize;
        kingPosition = transform.position;
        dialoguePart = 0;
        questPart = 0;
        questList = new List<Quest>();
        CreateQuest(QuestGoal.KILL, "Kill the wizard", 200, 1, "Wizard", string.Empty, string.Empty, string.Empty);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
            canInteract = true;

        if (Input.GetKeyDown(KeyCode.E) && !dialogueStart)
            StartInteraction(collision.gameObject);

        FindObjectOfType<QuestManager>().ChangeGoalCount(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && !dialogueStart)
            StartInteraction(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.gameObject.name.Equals("Player"))
            canInteract = false;
    }

    private void Update()
    {
        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E) && !dialogueStart)
                StartInteraction(playerInstance);
        }

        if (canInteract && Input.GetKeyDown(KeyCode.R))
        {
            if (!dialogueMenager.DisplayNextSentence() && questManager.quest.Finished)
            {
                if (questPart < questList.Count)
                {
                    FindObjectOfType<MoneySystem>().AddCoin(questManager.quest.Reward);
                    questManager.ChangeQuest(questList[questPart]);
                    questBlockers[questPart].SetActive(false);
                    dialoguePart++;
                    questPart++;
                }
                else
                {
                    FindObjectOfType<MoneySystem>().AddCoin(questManager.quest.Reward);
                    questManager.questPanel.SetActive(false);
                    SceneManager.LoadScene("Outro");
                }
                StopInteracting(playerInstance);
                if (questPart == 1)
                {
                    teleportSound.Play();
                    anim.SetBool("kingDisappeared", true);
                    Invoke(nameof(Appeared), 0.5f);
                }
            }
        }

        if (!canInteract)
        {
            StopInteracting(playerInstance);
        }

        Vector3 playerSite = playerInstance.transform.position - transform.position;
        if (playerSite.x < 0)
            transform.rotation = new Quaternion(0, 180, 0, 0);
        else
            transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void StartInteraction(GameObject player)
    {
        dialogueStart = true;
        if (questManager.quest.Finished == false)
        {
            Debug.Log("Not finished");
            dialogueTrigger.TriggerDialogue();
            //StopInteracting(player);
            return;
        }
        vCamera.m_Lens.OrthographicSize = 2.5f;
        vCamera.Follow = transform;
        if (dialoguePart < dialogueTrigger.dialogue.Count)
            dialogueTrigger.TriggerDialogue(dialoguePart);
        else
            StopInteracting(playerInstance);
    }

    private void StopInteracting(GameObject player)
    {
        dialogueStart = false;
        vCamera.m_Lens.OrthographicSize = baseCameraSize;
        vCamera.Follow = playerInstance.transform;
        dialogueMenager.EndDialog();
    }

    private void CreateQuest(QuestGoal type, string description, int reward, int killGoal, string killType, string arriveType, string useType, string talkType)
    {
        var newQuest = new Quest(type, description, reward, killGoal, killType, arriveType, useType, talkType);
        questList.Add(newQuest);
    }

    private void Appeared()
    {
        gameObject.SetActive(false);
        anim.SetBool("kingDisappeared", false);
    }

    public void RemoveBlockers(int n)
    {
        for (int i = 0; i < n; i++)
        {
            questBlockers[i].SetActive(false);
        }
        questPart = n;
        dialoguePart = n;
    }
}