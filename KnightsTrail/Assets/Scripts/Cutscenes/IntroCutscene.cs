using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutscene : MonoBehaviour
{
    private bool startedCutscene = false;
    public GameObject healthPanel;
    public GameObject playerObject;
    public Animator camAnim;
    public GameObject questPanel;
    public GameObject locationInfo;
    public GameObject coin;
    private AudioSource walkingSound;
    private float volumeOrg;

    public Transform target;

    private PlayerMovement playerScript;
    private PlayerCombat playerCombat;
    private Bow playerBow;
    private Animator playerAnim;
    private Rigidbody2D playerRB;
    private float moveSpeed = 35f;

    private void Awake()
    {
        walkingSound = playerObject.GetComponent<CharacterController2D>().Walk;
        playerObject.GetComponent<Bow>().intro = true;
        volumeOrg = walkingSound.volume;
        walkingSound.volume = 0;
        healthPanel.SetActive(false);
        questPanel.SetActive(false);
        locationInfo.SetActive(true);
        coin.SetActive(false);
        playerScript = playerObject.GetComponent<PlayerMovement>();
        playerCombat = playerObject.GetComponent<PlayerCombat>();
        playerBow = playerObject.GetComponent<Bow>();
        playerAnim = playerObject.GetComponent<Animator>();
        playerRB = playerObject.GetComponent<Rigidbody2D>();

        if (startedCutscene == false)
        {
            camAnim.SetBool("CutsceneIntro", true);
            Invoke(nameof(StopCutscene), 3f);
            playerScript.movementSpeed = 0f;
            startedCutscene = true;
            playerBow.InputDisabled = true;
            playerCombat.InputDisabled = true;
            playerScript.isDisabled = true;
        }
    }

    void StopCutscene()
    {
        playerObject.GetComponent<Bow>().intro = false;
        walkingSound.volume = volumeOrg;
        camAnim.SetBool("CutsceneIntro", false);
        playerScript.movementSpeed = 40f;
        healthPanel.SetActive(true);
        FindObjectOfType<HealthController>().currentHealth = FindObjectOfType<HealthController>().MaxHealth;
        questPanel.SetActive(true);
        coin.SetActive(true);
        locationInfo.SetActive(false);
        playerRB.velocity = new Vector2(0, playerRB.velocity.y);
        playerBow.InputDisabled = false;
        playerCombat.InputDisabled = false;
        playerScript.isDisabled = false;
        Destroy(gameObject);
    }

    void Update()
    {
        Move();
        SelectTarget();
    }

    void Move()
    {
        playerAnim.SetBool("IntroRun", true);
        playerRB.velocity = new Vector2(7.1f, playerRB.velocity.y);
    }

    void SelectTarget()
    {
        float distance = Vector2.Distance(playerObject.transform.position, target.position);

        if (distance < 0.3)
        {
            playerAnim.SetBool("IntroRun", false);
            playerRB.velocity = new Vector2(0, playerRB.velocity.y);
        }
    }
}
