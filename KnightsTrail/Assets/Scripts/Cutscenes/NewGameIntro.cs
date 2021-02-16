using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewGameIntro : MonoBehaviour
{
    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;
    public GameObject Text4;

    private Animator text1;
    private Animator text2;
    private Animator text3;
    private Animator text4;

    public GameObject BackgroundRed;

    public GameObject BackgroundEnd;
    public GameObject wizard;
    private Animator wizardAnim;

    public void Awake()
    {
        text1 = Text1.GetComponent<Animator>();
        text2 = Text2.GetComponent<Animator>();
        text3 = Text3.GetComponent<Animator>();
        text4 = Text4.GetComponent<Animator>();

        wizardAnim = wizard.GetComponent<Animator>();

        Invoke(nameof(EnableText1), 1f);
        Invoke(nameof(DisableText1EnableText2ShowWizardo), 6f);
    }

    void EnableText1()
    {
        Text1.SetActive(true);
    }

    void DisableText1EnableText2ShowWizardo()
    {
        Text1.SetActive(false);
        Text2.SetActive(true);
        BackgroundRed.SetActive(true);
        wizard.SetActive(true);
        Invoke(nameof(DisableText2EnableText3), 5f);
    }

    void DisableText2EnableText3()
    {
        Text2.SetActive(false);
        Text3.SetActive(true);
        Invoke(nameof(DisableText3EnableText4), 5f);
    }

    void DisableText3EnableText4()
    {
        Text3.SetActive(false);
        Text4.SetActive(true);
        Invoke(nameof(WizardAttack), 2f);
    }

    void WizardAttack()
    {
        wizardAnim.SetTrigger("Attack");
    }
}
