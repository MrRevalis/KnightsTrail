using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinV2 : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public GameObject player;
    public float lives;
    public Animator anim;

    public Transform attackPoint;
    public LayerMask playerLayer;
    public float distanceToAttack;

    public Rigidbody2D rb;
    public Collider2D OwnCollider;

    private float time;
    private bool attack = true;
    private bool dead = false;
    private float speedValue;
    private float lineOfSightValue;

    public AudioSource hit;

    private void Start()
    {
        time = Time.time;
        speedValue = speed;
        lineOfSightValue = lineOfSight;
    }

    public void TakeDamage()
    {
        if (!dead)
        {
            hit.Play();
            anim.SetTrigger("hit");
            lives--;
            lineOfSight = lineOfSight * 2.5f;
            speed = speed * 1.5f;

            StartCoroutine(NormalRange());

            if (lives <= 0)
            {
                dead = true;
                anim.SetBool("death", true);
                attack = false;
                anim.SetBool("walk", false);
                anim.SetBool("attack", false);
                speed = 0;
                FindObjectOfType<QuestManager>().ChangeGoalCount(this.gameObject);
                FindObjectOfType<MoneySystem>().SpawnCoin(transform.position);
                Destroy(gameObject, 1.5f);
            }
        }
        IEnumerator NormalRange()
        {
            yield return new WaitForSeconds(3f);
            speed = speedValue;
            lineOfSight = lineOfSightValue;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, OwnCollider);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "rightLimit")
        {
            anim.SetBool("Walk", false);
            speed = 0;
        }
    }

    void Update()
    {
        if (!dead)
        {
            Vector3 playerSite = player.transform.position - transform.position;
            if (playerSite.x < 0)
            {
                if (!dead)
                {
                    speed = speedValue;
                }
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            else
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }

        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (distanceFromPlayer < lineOfSight)
        {
            if (speed != 0)
                anim.SetBool("walk", true);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("walk", false);
        }

        if (distanceFromPlayer < distanceToAttack)
        {
            if (Time.time >= time + 3f)
            {
                if (attack)
                    Attack();
                time = Time.time;
            }

        }
        else
        {
            anim.SetBool("attack", false);
        }
    }

    void Attack()
    {
        anim.SetBool("walk", false);
        anim.SetBool("attack", true);
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
