using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public Transform coinToSpawn;
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
    public AudioSource attackDamage;

    public void playAttackDamageSound()
    {
        attackDamage.Play();
    }

    public void stopAttackDamageSound()
    {
        attackDamage.Stop();
    }

    private void Start()
    {
        time = Time.time;
        speedValue = speed;
        lineOfSightValue = lineOfSight;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void TakeDamage()
    {
        if (!dead)
        {
            hit.Play();
            anim.SetTrigger("Hit");
            lives--;
            lineOfSight = lineOfSight * 2.5f;
            speed = speed * 1.5f;
            if (dead)
                speed = 0;

            StartCoroutine(NormalRange());

            if (lives <= 0)
            {
                dead = true;
                FindObjectOfType<MoneySystem>().SpawnCoin(coinToSpawn.position);
                FindObjectOfType<QuestManager>().ChangeGoalCount(this.gameObject);
                anim.SetBool("Death", true);
                attack = false;
                anim.SetBool("Walk", false);
                anim.SetBool("Attack2", false);
                speed = 0;
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
        Debug.Log("speed " + speed);
        if (!dead)
        {
            Vector3 playerSite = player.transform.position - transform.position;
            if (playerSite.x < 0)
            {
                speed = speedValue;
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
            if(speed != 0)
                anim.SetBool("Walk", true);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Walk", false);
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
            anim.SetBool("Attack2", false);
        }
    }

    void Attack()
    {
        anim.SetBool("Walk", false);
        anim.SetBool("Attack2", true);
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
