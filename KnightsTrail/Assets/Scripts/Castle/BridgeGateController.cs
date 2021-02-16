using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeGateController : MonoBehaviour
{
    public Transform lever;
    public ParticleSystem platformsBall;
    public ParticleSystem leversBall;

    public bool platformSolved = false;
    public bool leversSolved = false;

    // Update is called once per frame
    void Update()
    {
        if(platformSolved && leversSolved)
        {
            lever.GetComponent<CastleGateLever>().OpenTheGate();
        }
        if (platformSolved)
        {
            platformsBall.startColor = Color.green;
        }
        else platformsBall.startColor = new Color(105, 80, 0, 255);
        if (leversSolved)
        {
            leversBall.startColor = Color.green;
        }
        else leversBall.startColor = new Color(105, 80, 0, 255);
    }
}
