using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPuzzleController : MonoBehaviour
{
    public Transform lever;
    private LeverPlatPuz manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = lever.GetComponent<LeverPlatPuz>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.leverState != 0)
        {
            if ((transform.position.y >= transform.parent.Find("Top").position.y && manager.leverState == 1) || (transform.position.y <= transform.parent.Find("Bottom").position.y && manager.leverState == -1))
            {
                manager.TurnOff();
            }
        }
    }
}
