using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterJarBlocker : MonoBehaviour
{
    public Transform jar;
    public Transform jar1;
    public Transform jar2;
    public Transform jar3;
    private Transform player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.transform;
            player.GetComponent<PlayerMovement>().carryingStuff = false;

            jar.GetComponent<WaterJar>().DropJar();
            jar1.GetComponent<WaterJar>().DropJar();
            jar2.GetComponent<WaterJar>().DropJar();
            jar3.GetComponent<WaterJar>().DropJar();
        }
    }
}
