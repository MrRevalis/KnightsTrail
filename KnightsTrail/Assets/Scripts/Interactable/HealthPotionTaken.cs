using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionTaken : MonoBehaviour
{
    public AudioSource takePotion;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            takePotion.Play();
            FindObjectOfType<HealthController>().AddPotion();
            FindObjectOfType<HealthController>().AddPotion();
            Destroy(gameObject);
        }
    }
}
