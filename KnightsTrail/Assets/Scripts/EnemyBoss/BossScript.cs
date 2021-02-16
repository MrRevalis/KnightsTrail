using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public float lives;
    public GameObject Player;
    public GameObject Boss2;
    public GameObject BlackOut;
    private Animator panel;
    public Animator anim;
    public Collider2D OwnCollider;
    public AudioSource attack;
    public AudioSource hit;

    public Transform attackPoint;
    public LayerMask playerLayer;
    public float distanceToAttack;

    public Slider HealthBar;

    private float time;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, OwnCollider);
        }

        if (collision.gameObject.tag == "KingCompanion")
        {
            Physics2D.IgnoreCollision(collision.collider, OwnCollider);
        }
    }

    private void Start()
    {
        time = Time.time;
        panel = BlackOut.GetComponent<Animator>();
    }

    public void TakeDamage()
    {
        lives--;
        anim.SetTrigger("Hit");
        hit.Play();

        if (lives <= 10)
        {
            speed = 0;
            anim.SetTrigger("Die");
            Destroy(gameObject, 0.5f);
            Boss2.SetActive(true);

            BlackOut.SetActive(true);
            panel.SetTrigger("FadeIn");
        }
    }

    void Update()
    {
        HealthBar.value = lives;

        anim.SetBool("Walk", false);

        if (lives > 0)
        {
            Vector3 enemySite = Player.transform.position - transform.position;
            if (enemySite.x < 0)
                transform.rotation = new Quaternion(0, 180, 0, 0);
            else
                transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        float distancefromTarget = Vector2.Distance(Player.transform.position, transform.position);
        if (distancefromTarget < lineOfSight)
        {
            anim.SetBool("Walk", true);
            transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, speed * Time.deltaTime);
        }

        if (distancefromTarget < distanceToAttack)
        {
            if (Time.time >= time + 2.5f)
            {
                Attack();
                time = Time.time;
            }

        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }

    void Attack()
    {
        attack.Play();
        anim.SetBool("Attack", true);
    }

    void DealDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, distanceToAttack, playerLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.CompareTag("Player"))
            {
                FindObjectOfType<HealthController>().TakeDamage();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);

        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, distanceToAttack);
    }
}
