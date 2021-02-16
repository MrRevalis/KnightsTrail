using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Outro : MonoBehaviour
{
    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;
    public GameObject Text4;

    private Animator text1;
    private Animator text2;
    private Animator text3;
    private Animator text4;

    public GameObject BackgroundOrg;
    public GameObject BackgroundOrg2;
    public GameObject BackgroundPodmianka;
    public GameObject BackgroundPodmianka2;

    public GameObject BackgroundEnd;
    public GameObject playerChar;
    private Animator playerAnim;

    public void Awake()
    {
        text1 = Text1.GetComponent<Animator>();
        text2 = Text2.GetComponent<Animator>();
        text3 = Text3.GetComponent<Animator>();
        text4 = Text4.GetComponent<Animator>();
        playerAnim = playerChar.GetComponent<Animator>();

        Invoke(nameof(EnableText1), 1f);
        Invoke(nameof(DisableText1EnableText2ShowWizardo), 6f);
    }

    void EnableText1()
    {
        Text1.SetActive(true);
        playerChar.SetActive(true);
    }

    void DisableText1EnableText2ShowWizardo()
    {
        Text1.SetActive(false);
        Text2.SetActive(true);
        Invoke(nameof(DisableText2EnableText3), 5f);
    }

    void DisableText2EnableText3()
    {
        playerAnim.SetTrigger("Run");
        BackgroundOrg.SetActive(false);
        BackgroundOrg2.SetActive(false);
        BackgroundPodmianka.SetActive(true);
        BackgroundPodmianka.SetActive(true);
        BackgroundPodmianka2.SetActive(true);
        Text2.SetActive(false);
        Text3.SetActive(true);
        Invoke(nameof(DisableText3EnableText4), 5f);     
    }

    void LoadScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void DisableText3EnableText4()
    {
        Text3.SetActive(false);
        Text4.SetActive(true);
        Invoke(nameof(LoadScene), 3.5f);
    }
}
