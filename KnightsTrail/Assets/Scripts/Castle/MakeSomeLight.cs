using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSomeLight : MonoBehaviour
{
    public float range = 2f;
    public float intensity = 0.5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Transform player = collision.transform;
            player.position = new Vector3(player.position.x, player.position.y, -1f);
            player.Find("Point Light").GetComponent<Light>().range = range;
            player.Find("Point Light").GetComponent<Light>().intensity = intensity;
        }
    }
}
