using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameObject Shop;
    public GameObject blacksmithShop;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        //show/hide shop
        /*if (Input.GetKeyDown(KeyCode.J))
        {
            if (Shop.activeSelf)
            {
                Shop.SetActive(false);
                StartGame();
            } else
            {
                StopGame();
                Shop.SetActive(true);
            }
        }*/
    }
    public void StartGame()
    {
        Time.timeScale = 1f;
    }
    public void StopGame()
    {
        Time.timeScale = 0f;
    }
    public void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void HideShop()
    {
        Shop = GameObject.FindWithTag("ShopPanel");
        Shop.SetActive(false);
        StartGame();
    }

    public void HideBlacksmith()
    {
        blacksmithShop.SetActive(false);
    }
}
