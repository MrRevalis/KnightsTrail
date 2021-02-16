using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Golem : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public float lineOfSightAngryMode;
    public GameObject player;
    public float lives;
    public Animator anim;

    public Transform attackPoint;
    public LayerMask playerLayer;
    public float distanceToAttack;

    public Rigidbody2D rb;
    public Collider2D OwnCollider;

    private Vector3 startPosition;

    private float time;
    private bool move = true;
    private bool attack = true;
    private bool attackChange;
    private bool isDead = false;
    private float lineOfSightValue;

    public AudioSource punchSound;
    public AudioSource hit;
    public AudioSource dead;

    private void Start()
    {
        attackChange = false;
        time = Time.time;
        startPosition = transform.position;
        lineOfSightValue = lineOfSight;
    }

    public void TakeDamage()
    {
        if (!isDead)
        {
            hit.Play();
            lives--;
            anim.SetBool("Angry", false);
            anim.SetTrigger("Hit");

            lineOfSight = lineOfSight * 2.5f;

            StartCoroutine(NormalRange());

            if (lives <= 0)
            {
                dead.Play();
                Debug.Log("SMIERC");
                isDead = true;
                FindObjectOfType<MoneySystem>().SpawnCoin(transform.position);
                FindObjectOfType<QuestManager>().ChangeGoalCount(this.gameObject);
                if (SceneManager.GetActiveScene().name == "FirstScene") FindObjectOfType<KingScript>().blocker3.SetActive(false);

                attack = false;
                anim.SetBool("Angry", false);
                anim.SetBool("Walk", false);
                anim.SetBool("Attack2", false);
                rb.isKinematic = false;
                speed = 0;
                anim.SetTrigger("Death");
                Destroy(gameObject, 1.5f);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "golemLimit")
        {
            move = false;
            anim.SetBool("Angry", false);
            anim.SetBool("Walk", false);
        }
    }

    void Update()
    {
        if (!isDead)
        {
            Vector3 playerSite = player.transform.position - transform.position;
            if (playerSite.x < 0)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                move = true;
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        }

        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (distanceFromPlayer < lineOfSightAngryMode)
        {
            if(move)
                anim.SetBool("Angry", true);
        }
        if (distanceFromPlayer > lineOfSightAngryMode)
        {
            anim.SetBool("Angry", false);
        }

        if (distanceFromPlayer < lineOfSight)
        {
            if (move)
            {
                anim.SetBool("Angry", false);
                anim.SetBool("Walk", true);
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        if (distanceFromPlayer < distanceToAttack)
        {
            if (Time.time >= time + 2.5f)
            {
                if(attack)
                    Attack();
                time = Time.time;
            }

        }
        else
        {
            anim.SetBool("Attack1", false);
            anim.SetBool("Attack2", false);
        }
    }

    void Attack()
    {
        attackChange = !attackChange;

        anim.SetBool("Walk", false);

        if (attackChange == false)
        {
            anim.SetBool("Attack1", true);
            anim.SetBool("Attack2", false);
        }
        else if (attackChange == true)
        {
            anim.SetBool("Attack2", true);
            anim.SetBool("Attack1", false);
        }
    }

    void DealDamage()
    {
        punchSound.Play();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, distanceToAttack, playerLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.CompareTag("Player"))
            {
                FindObjectOfType<HealthController>().TakeDamage();

                if (transform.position.x < enemy.transform.position.x)
                {
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(8f * 5f, transform.up.y * 5f), ForceMode2D.Impulse);
                }
                else
                    enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(-8f * 5f, transform.up.y * 5f), ForceMode2D.Impulse);
                   
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

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lineOfSightAngryMode);
    }
}
