using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject fire;
    public GameObject player;

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceFromPlayer <= 10)
            fire.SetActive(true);
        else
            fire.SetActive(false);

    }
}
