using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject leadsTo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = leadsTo.transform.position;
            collision.transform.GetComponent<PlayerMovement>().CanMove = true;
        }
    }
}
