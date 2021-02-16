using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsPuzzleManager : MonoBehaviour
{
    public Transform PlatformA;
    public Transform PlatformB;
    public Transform PlatformC;
    public GameObject gongAudio;
    public GameObject AudioSource;
    public AudioClip sound;
    public float Level = 0f;
    public float margin = 0.2f;

    public bool[] found;
    private bool solved = false;
    private bool effectShown = false;

    private void Start()
    {
        found = new bool[3];
        for(int i = 0; i < 3; i++)
        {
            found[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlatformA.localPosition.y >= Level - margin && PlatformA.localPosition.y <= Level + margin)
        {
            found[0] = true;
        } else
        {
            found[0] = false;
        }
        if (PlatformB.localPosition.y >= Level - margin && PlatformB.localPosition.y <= Level + margin)
        {
            found[1] = true;
        }
        else
        {
            found[1] = false;
        }
        if (PlatformC.localPosition.y >= Level - margin && PlatformC.localPosition.y <= Level + margin)
        {
            found[2] = true;
        }
        else
        {
            found[2] = false;
        }

        if(found[0] && found[1] && found[2])
        {
            if (!solved)
            {
                gongAudio.GetComponent<AudioSource>().enabled = true;
                solved = true;
                FindObjectOfType<BridgeGateController>().platformSolved = true;
                if (!effectShown)
                {
                    transform.GetComponent<ShowEffect>().Show();
                    effectShown = true;
                }
            }
        }
        else { solved = false; FindObjectOfType<BridgeGateController>().platformSolved = false; }
    }
}
