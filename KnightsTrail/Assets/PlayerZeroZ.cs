using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZeroZ : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Transform p = collision.transform;
            p.position = new Vector3(p.position.x, p.position.y, 0f);
        }
    }
}
