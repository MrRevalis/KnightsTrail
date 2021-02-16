using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBossFight : MonoBehaviour
{
    public GameObject healthPanel;
    public GameObject playerObject;
    public Animator camAnim;
    public GameObject questPanel;
    public GameObject bossHpSlider;

    public AudioSource music;
    public AudioClip music2;

    public GameObject panelBlackout;
    private Animator panel;

    public GameObject Wizard1;
    private BossScript wiz1Script;
    private float speedOrg;

    public GameObject KingCompanion;
    private KingCompanion kingScript;
    public GameObject Blocker;

    private PlayerMovement playerScript;
    private PlayerCombat playerCombat;
    private Bow playerBow;
    private Animator playerAnim;
    private Rigidbody2D playerRB;
    private float moveSpeed = 35f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            music.clip = music2;
            music.Play();
            healthPanel.SetActive(false);
            questPanel.SetActive(false);
            bossHpSlider.SetActive(false);
            Blocker.SetActive(true);
            Wizard1.SetActive(true);
            KingCompanion.SetActive(true);
            panelBlackout.SetActive(true);

            panel = panelBlackout.GetComponent<Animator>();

            playerScript = playerObject.GetComponent<PlayerMovement>();
            playerCombat = playerObject.GetComponent<PlayerCombat>();
            playerBow = playerObject.GetComponent<Bow>();
            playerAnim = playerObject.GetComponent<Animator>();
            playerRB = playerObject.GetComponent<Rigidbody2D>();

            camAnim.SetBool("CutsceneIntro", true);
            panel.SetTrigger("FadeIn");
            panelBlackout.SetActive(true);
            Invoke(nameof(StopCutscene), 2f);
            playerScript.movementSpeed = 0f;
            playerBow.InputDisabled = true;
            playerCombat.InputDisabled = true;
            playerScript.isDisabled = true;
        }
    }

    private void StopCutscene()
    {
        camAnim.SetBool("CutsceneIntro", false);
        playerScript.movementSpeed = 40f;
        healthPanel.SetActive(true);
        questPanel.SetActive(true);
        bossHpSlider.SetActive(true);
        panelBlackout.SetActive(false);

        playerBow.InputDisabled = false;
        playerCombat.InputDisabled = false;
        playerScript.isDisabled = false;
        Destroy(gameObject);
    }
}
