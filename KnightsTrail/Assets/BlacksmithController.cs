using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlacksmithController : MonoBehaviour
{

    public Button silverButton;
    public Button goldButton;
    public Button diamondButton;
    public Slider slider;
    public AudioSource buySound;

    public AnimatorOverrideController silver;
    public AnimatorOverrideController gold;
    public AnimatorOverrideController diamond;

    private MoneySystem money;
    void Start()
    {
        money = FindObjectOfType<MoneySystem>();
        silverButton.onClick.AddListener(SilverButton);
        goldButton.onClick.AddListener(GoldButton);
        diamondButton.onClick.AddListener(DiamondButton);
    }

    private void SilverButton()
    {
        if(money.coins >= 100)
        {
            buySound.Play();
            money.RemoveCoin(100);
            slider.value = 1;
            silverButton.interactable = false;
            goldButton.interactable = true;
            var player = GameObject.FindGameObjectWithTag("Player");
            var animator = player.GetComponent<Animator>();
            //animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Assets/Resources/PlayerSilver");
            animator.runtimeAnimatorController = silver;
            FindObjectOfType<HealthController>().IncreaseMaxLife(2);
            FindObjectOfType<HealthController>().AddLive(2);
        }
    }

    private void GoldButton()
    {
        if (money.coins >= 120)
        {
            buySound.Play();
            money.RemoveCoin(120);
            slider.value = 2;
            goldButton.interactable = false;
            diamondButton.interactable = true;
            var player = GameObject.FindGameObjectWithTag("Player");
            var animator = player.GetComponent<Animator>();
            animator.runtimeAnimatorController = gold;
            FindObjectOfType<HealthController>().IncreaseMaxLife(4);
            FindObjectOfType<HealthController>().AddLive(4);
        }
    }

    private void DiamondButton()
    {
        if (money.coins >= 150)
        {
            buySound.Play();
            money.RemoveCoin(150);
            slider.value = 3;
            diamondButton.interactable = false;
            var player = GameObject.FindGameObjectWithTag("Player");
            var animator = player.GetComponent<Animator>();
            animator.runtimeAnimatorController = diamond;
            FindObjectOfType<HealthController>().IncreaseMaxLife(6);
            FindObjectOfType<HealthController>().AddLive(6);
        }
    }

    public void LoadSavedArmour(float n)
    {
        slider.value = n;
        //W tym miejscu pêtla po guzikach do n i ustawiæ je na nieaktywne a reszta na aktywne
        Button[] buttonArray = new Button[] { silverButton, goldButton, diamondButton };
        for(var i = 0; i < buttonArray.Length; i++)
        {
            if (i + 1 <= n)
            {
                buttonArray[i].interactable = false;
            }
            else
                buttonArray[i].interactable = true;
        }

        var player = GameObject.FindGameObjectWithTag("Player");
        var animator = player.GetComponent<Animator>();
        switch (n)
        {
            case 1: animator.runtimeAnimatorController = silver; break;
            case 2: animator.runtimeAnimatorController = gold; break;
            case 3: animator.runtimeAnimatorController = diamond; break;
        }
    }
}
