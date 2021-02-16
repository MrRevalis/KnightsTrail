using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 1.5f;
    public int lives = 2;
    public float attackDistance = 1f;
    public Collider2D OwnCollider;
    public AudioSource hit;
    bool moveRight = true;
    bool shouldMove = true;
    float delay;

    private Animator animator;
    private Transform leftLimit;
    private Transform rightLimit;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        leftLimit = transform.parent.Find("LeftLimit");
        rightLimit = transform.parent.Find("RightLimit");
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            animator.SetBool("walk", true);
            if (moveRight)
            {
                transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
            }
        }
        else
        {
            delay -= Time.deltaTime;
            if (delay < 0)
                shouldMove = true;
        }
        if(player.position.x > leftLimit.position.x && player.position.x < rightLimit.position.x)
        {
            if(player.position.x > transform.position.x)
            {
                if (!moveRight)
                {
                    moveRight = true;
                    Rotate();
                }
            } else
            {
                if (moveRight)
                {
                    moveRight = false;
                    Rotate();
                }
            }
        }
    }

    private void Rotate()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && lives > 0)
        {
            //Physics2D.IgnoreCollision(collision.collider, OwnCollider);
            animator.SetBool("walk", false);
            animator.SetBool("attack", true);
            delay = 0.5f;
            shouldMove = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name.Equals("LeftLimit"))
        {
            if (!moveRight)
            {
                moveRight = true;
                Rotate();
            }
        }

        if (collision.gameObject.name.Equals("RightLimit"))
        {
            if (moveRight)
            {
                moveRight = false;
                Rotate();
            }
        }
    }

    public void AttackEnd()
    {
        if(Vector2.Distance(transform.position, player.position) < attackDistance)
        {
            FindObjectOfType<HealthController>().TakeDamage();
        }
        animator.SetBool("attack", false);
    }

    public void TakeDamage()
    {
        if(lives > 0)
        {
            hit.Play();
            lives--;
            shouldMove = false;
            delay = 1.25f;
            animator.SetBool("walk", false);
            animator.SetTrigger("hit");
            if (lives <= 0)
            {
                animator.SetBool("attack", false);
                animator.SetTrigger("death");
            }
        }
        else
            animator.SetTrigger("death");
    }

    void Die()
    {
        FindObjectOfType<QuestManager>().ChangeGoalCount(this.gameObject);
        FindObjectOfType<MoneySystem>().SpawnCoin(transform.position);
        Destroy(gameObject);
    }
}
