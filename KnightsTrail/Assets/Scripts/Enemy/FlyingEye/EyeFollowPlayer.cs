using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollowPlayer : MonoBehaviour
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
    private bool isDead = false;
    private float lineOfSightValue;

    public AudioSource hit;

    private void Start()
    {
        time = Time.time;
        lineOfSightValue = lineOfSight;
    }

    public void TakeDamage()
    {
        if (!isDead)
        {
            hit.Play();
            lives--;
            anim.SetTrigger("Hit");
            lineOfSight = lineOfSight * 2.5f;

            StartCoroutine(NormalRange());

            if (lives <= 0)
            {
                isDead = true;
                anim.SetBool("Attack", false);
                rb.gravityScale = 2;
                rb.isKinematic = false;
                speed = 0;
                anim.SetTrigger("Die");
                Destroy(gameObject, 1f);
                FindObjectOfType<MoneySystem>().SpawnCoin(transform.position);
            }
        }
        IEnumerator NormalRange()
        {
            yield return new WaitForSeconds(3f);
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

    void Update()
    {
        Vector3 playerSite = player.transform.position - transform.position;
        if (playerSite.x < 0)
            transform.rotation = new Quaternion(0, 180, 0, 0);
        else
            transform.rotation = new Quaternion(0, 0, 0, 0);

        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
        if(distanceFromPlayer < lineOfSight)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        
        if(distanceFromPlayer < distanceToAttack)
        {
            if (Time.time >= time + 1.5f)
            {
                if(rb.gravityScale != 2)
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
