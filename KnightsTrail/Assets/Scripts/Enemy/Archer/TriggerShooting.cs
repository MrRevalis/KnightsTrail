using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerShooting : MonoBehaviour
{
    private ArcherController enemyParent;

    private void Awake()
    {
        enemyParent = GetComponentInParent<ArcherController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            enemyParent.StartAttacking();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyParent.StopAttacking();
        }
    }
}
