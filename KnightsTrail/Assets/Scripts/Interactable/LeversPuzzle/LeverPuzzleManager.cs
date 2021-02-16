using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzleManager : MonoBehaviour
{
    public bool[] leverStates;
    private ParticleSystem[] lamps;
    private bool effectShown = false;

    // Start is called before the first frame update
    void Start()
    {
        leverStates = new bool[4];
        lamps = new ParticleSystem[4];
        for(int i = 0; i < 4; i++)
        {
            leverStates[i] = false;
            lamps[i] = transform.Find("Lamp" + i).GetComponent<ParticleSystem>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeState(int lever)
    {
        leverStates[lever] = !leverStates[lever];
        Logic();
    }

    void Logic()
    {
        bool solved = true;

        bool a0 = leverStates[0] && leverStates[2];
        bool a1 = leverStates[0] && leverStates[1];
        bool o0 = leverStates[1] || leverStates[3];
        bool o1 = leverStates[2] || leverStates[3];

        bool a2 = o0 && a1;

        bool o2 = !o1 || !a0;
        bool o3 = a0 || a2;
        bool o4 = !a0 || !o1;

        bool a3 = o2 && o4;
        bool a4 = o3 && !o1;

        if(a3) lamps[0].startColor = Color.green;
        else
        {
            solved = false;
            lamps[0].startColor = new Color(105, 80, 0, 255);
        }

        if (o3) lamps[1].startColor = Color.green;
        else
        {
            solved = false;
            lamps[1].startColor = new Color(105, 80, 0, 255);
        }

        if (o3) lamps[2].startColor = Color.green;
        else
        {
            solved = false;
            lamps[2].startColor = new Color(105, 80, 0, 255);
        }

        if (a4) lamps[3].startColor = Color.green;
        else
        {
            solved = false;
            lamps[3].startColor = new Color(105, 80, 0, 255);
        }

        if (solved)
        {
            FindObjectOfType<BridgeGateController>().leversSolved = true;
            if (!effectShown)
            {
                transform.GetComponent<ShowEffect>().Show();
                effectShown = true;
            }
        }
        else FindObjectOfType<BridgeGateController>().leversSolved = false;
    }
}
