using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    [Range(0.1f,1.1f)]
    public float hurtPeriod = 0.5f;
    public GameObject player;
    private float lastHurt;

    [Range(1f, 6f)]
    public float onTime = 4f;
    [Range(1f, 6f)]
    public float offTime = 3f;

    private float lastTime = 0f;
    private bool fire = true;

    private ParticleSystem ps;
    private AudioSource audio;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        audio = GetComponent<AudioSource>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            if(Time.time - lastHurt > hurtPeriod)
            {
                other.GetComponent<Animator>().SetBool("PlayerHurt", true);
                FindObjectOfType<HealthController>().TakeDamage();
                //if (other.transform.position.x - transform.parent.position.x < 0)
                //    other.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10 * 5f, other.transform.up.y * 7f), ForceMode2D.Impulse);
                //else
                //    other.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7f, ForceMode2D.Impulse);
                lastHurt = Time.time;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (fire)
        {
            if(Time.time - lastTime > onTime)
            {
                if (distanceFromPlayer > 10)
                    audio.Stop();
                ps.Stop();
                lastTime = Time.time;
                fire = false;
            }
        } else
        {
            if (Time.time - lastTime > offTime)
            {
                if (distanceFromPlayer <= 10)
                    audio.Play();
                ps.Play();
                lastTime = Time.time;
                fire = true;
            }
        }
    }
}
