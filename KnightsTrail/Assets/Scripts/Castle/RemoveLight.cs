using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveLight : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Transform player = collision.transform;
            player.position = new Vector3(player.position.x, player.position.y, 0f);
        }
    }
}
