using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopGame : MonoBehaviour
{
    public AudioSource background;

    public void Stop()
    {
        background.Stop();
        FindObjectOfType<GameController>().StopGame();
    }
}
