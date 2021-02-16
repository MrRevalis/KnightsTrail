using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public Button healthPotionBtn;
    public int healthPotionPrice = 10;
    public Text healthPotionLabel;

    public Button extraLifeBtn;
    public int extraLifePrice = 50;
    public Text extraLifeLabel;

    public AudioSource moneyHandle;

    private MoneySystem money;
    private HealthController health;

    // Start is called before the first frame update
    void Start()
    {
        money = FindObjectOfType<MoneySystem>();
        health = FindObjectOfType<HealthController>();
        healthPotionLabel.text = healthPotionPrice.ToString();
        extraLifeLabel.text = extraLifePrice.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(money.coins >= healthPotionPrice) healthPotionBtn.interactable = true;
        else healthPotionBtn.interactable = false;

        if (money.coins >= extraLifePrice) extraLifeBtn.interactable = true;
        else extraLifeBtn.interactable = false;
    }

    public void Buy(string item)
    {
        switch (item)
        {
            case "healthPotion":
                if(money.coins >= healthPotionPrice)
                {
                    money.RemoveCoin(healthPotionPrice);
                    health.AddPotion();
                    moneyHandle.Play();
                }
                break;
            case "extraLife":
                if (money.coins >= extraLifePrice)
                {
                    money.RemoveCoin(extraLifePrice);
                    health.IncreaseMaxLife();
                    moneyHandle.Play();
                }
                break;
        }
    }
}
