using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoxPuzzleBlocker : MonoBehaviour
{
    private Transform player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.transform;
            Debug.Log("carry " + player.GetComponent<PlayerMovement>().carryingStuff);
            if (player.GetComponent<PlayerMovement>().carryingStuff == false)
            {
                Physics2D.IgnoreCollision(collision.collider, GetComponent<BoxCollider2D>());
            }
        }
    }
}
