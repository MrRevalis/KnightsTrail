using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroWizard : MonoBehaviour
{
    public GameObject wizard;
    public GameObject BackgroundEnd;
    public GameObject Text4;
    private Animator text4Anim;

    void WizardDisable()
    {
        text4Anim = Text4.GetComponent<Animator>();
        text4Anim.SetTrigger("FadeOut");
        Invoke(nameof(NotEnabled), 0.75f);
        Invoke(nameof(LoadScene), 1.5f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("FirstScene");
    }

    void NotEnabled()
    {
        BackgroundEnd.SetActive(true);
        Text4.SetActive(false);
        wizard.SetActive(false);
    }
}
