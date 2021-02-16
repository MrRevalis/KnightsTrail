using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    public int coins = 0;
    public Text coinsLabel;
    public GameObject Coin;

    private void Start()
    {
        coinsLabel.text = coins.ToString();
    }

    private void Update()
    {
        //cheat
        if (Input.GetKeyUp(KeyCode.UpArrow)) AddCoin();
        if (Input.GetKeyUp(KeyCode.DownArrow)) RemoveCoin(1);
    }

    public void AddCoin(int amount = 1)
    {
        coins+=amount;
        coinsLabel.text = coins.ToString();
    }

    public void RemoveCoin(int amount)
    {
        coins -= amount;
        coinsLabel.text = coins.ToString();
    }

    public void SpawnCoin(Vector3 position)
    {
        Instantiate(Coin, position, Coin.transform.rotation);
    }

    public void UpdateData()
    {
        coinsLabel.text = coins.ToString();
    }
}
