using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<HealthController>().TakeDamage();
            collision.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10 * 5f, transform.up.y * 30f), ForceMode2D.Impulse);
        }
    }
}
