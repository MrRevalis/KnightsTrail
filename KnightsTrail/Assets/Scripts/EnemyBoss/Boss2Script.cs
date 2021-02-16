using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss2Script : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public float lives;
    public GameObject Player;
    public GameObject Blocker;
    public GameObject KingToTalk;
    public Animator anim;
    public Collider2D OwnCollider;
    public AudioSource hit;
    public AudioSource attack;

    public AudioSource music;
    public AudioClip music2;

    public Transform attackPoint;
    public LayerMask playerLayer;
    public float distanceToAttack;

    public Slider HealthBar;
    public GameObject HealthBarSlider;

    private float time;
    private bool attackChange;

    public GameObject BlackOut;

    private void Start()
    {
        Invoke(nameof(BlackOutFalse), 1.5f);
        attackChange = false;
        time = Time.time;
    }

    private void BlackOutFalse()
    {
        BlackOut.SetActive(false);
    }

    public void playSpellSound()
    {
        attack.Play();
    }

    public void TakeDamage()
    {
        if(lives > 0)
        {
            lives--;
            anim.SetTrigger("Hit");
            hit.Play();

            if (lives <= 0)
            {
                speed = 0;
                anim.SetTrigger("Die");
                Destroy(gameObject, 1f);
                KingToTalk.SetActive(true);
                HealthBarSlider.SetActive(false);
                music.clip = music2;
                music.Play();
                Blocker.SetActive(false);
                FindObjectOfType<QuestManager>().ChangeGoalCount(this.gameObject);
            }
        }
    }

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
            anim.SetBool("Attack2", false);
        }
    }

    void Attack()
    {
        attackChange = !attackChange;

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
