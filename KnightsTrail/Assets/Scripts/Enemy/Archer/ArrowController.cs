using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Rigidbody2D rb;
    private HealthController healthController;
    private bool hasHit;

    public float minForce = 10f;
    public float maxForce = 13.5f;

    // Start is called before the first frame update
    void Start()
    {
        healthController = FindObjectOfType<HealthController>();
        rb = transform.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * Random.Range(minForce, maxForce);
    }

    private void Update()
    {
        if (hasHit == false)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (rb.velocity.x == 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasHit = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        if (collision.transform.CompareTag("Player"))
        {
            healthController.TakeDamage();
        }
    }
}
