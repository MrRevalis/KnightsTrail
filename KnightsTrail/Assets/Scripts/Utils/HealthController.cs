using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public Animator anim;
    public Transform Player;
    public Text ExtraPotions;
    public int MaxHealth;
    public AudioSource hurt;
    public AudioClip[] AudioClips;
    private int i = 0;

    public GameObject GameOverUI;

    private Slider slider;
    public int currentHealth;
    public int potions = 0;

    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.maxValue = MaxHealth;
        currentHealth = MaxHealth;
        slider.value = currentHealth;
        ExtraPotions.text = potions.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakePotion();
        }
        if (currentHealth == 0) OnPlayerDie();
        //DEBUG CHEATS
        if (Input.GetKeyDown(KeyCode.PageUp)) AddLive();
        else if (Input.GetKeyDown(KeyCode.PageDown)) TakeDamage();
        else if (Input.GetKeyDown(KeyCode.Home)) Resurrect();
    }

    void OnPlayerDie()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().CanMove = false;
        GameOverUI.SetActive(true);
    }

    public void Resurrect()
    {
        Time.timeScale = 1f;
        currentHealth = MaxHealth;
        slider.value = currentHealth;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().CanMove = true;
        GameOverUI.SetActive(false);
    }

    public void TakeDamage()
    {
        if(currentHealth > 0)
        {
            if (i == 4)
                i = 0;
            hurt.clip = AudioClips[i++];
            hurt.Play();    
            currentHealth--;
            slider.value = currentHealth;
            Player.Find("DamageEffect").GetComponent<ParticleSystem>().Play();
        }
    }

    public void AddLive()
    {
        if (currentHealth < MaxHealth)
        {
            currentHealth++;
            slider.value = currentHealth;
        }
    }

    public void TakePotion()
    {
        if(potions > 0 && currentHealth < MaxHealth)
        {
            potions--;
            currentHealth++;
            slider.value = currentHealth;
            ExtraPotions.text = potions.ToString();
        }
    }

    public void AddPotion()
    {
        potions++;
        ExtraPotions.text = potions.ToString();
    }

    public void IncreaseMaxLife()
    {
        AddLive();
        MaxHealth++;
        slider.maxValue = MaxHealth;
    }

    public void IncreaseMaxLife(int lives)
    {
        MaxHealth += lives;
        slider.maxValue = MaxHealth;
    }
    public void AddLive(int lives)
    {
        currentHealth+= lives;
        slider.value = currentHealth;
    }

    public bool UpdateData()
    {
        if (slider == null) return false;
        slider.maxValue = MaxHealth;
        slider.value = currentHealth;
        
        ExtraPotions.text = potions.ToString();
        return true;
    }
}
