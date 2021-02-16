using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;

public class WaterPuzzleController : MonoBehaviour
{
    public GameObject devCode;
    public GameObject elevator;
    public bool devSolved = false;
    public int[] code;
    public int[] userCode;
    private bool effectShown = false;
    // Start is called before the first frame update
    void Start()
    {
        code = new int[4];
        userCode = new int[4];
        for(int i = 0; i < 4; i++)
        {
            code[i] = Random.Range(0, 9);
            userCode[i] = -1;
        }
        devCode.GetComponent<TextMeshPro>().text = $"{code[0]}{code[1]}{code[2]}{code[3]}";
    }

    // Update is called once per frame
    void Update()
    {
        if (CodeCorrect() || devSolved)
        {
            elevator.GetComponent<PlatformMovementVertical>().enabled = true;
            if (!effectShown)
            {
                transform.GetComponent<ShowEffect>().Show();
                effectShown = true;
            }
        } else
        {
            elevator.GetComponent<PlatformMovementVertical>().enabled = false;
        }
    }

    private bool CodeCorrect()
    {
        for(int i = 0; i<4; i++)
        {
            if (code[i] != userCode[i]) return false;
        }
        return true;
    }
}
