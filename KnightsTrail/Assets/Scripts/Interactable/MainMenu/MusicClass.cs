using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicClass : MonoBehaviour
{
    public AudioSource audio;

    private float audio1Volume = 1f;

    private void Update()
    {
        Invoke(nameof(fadeOut), 12.5f);
    }

    void fadeOut()
    {
        if(audio1Volume > 0.1)
        {
            audio1Volume -= 0.1f * Time.deltaTime;
            audio.volume = audio1Volume;
        }
    }
}
