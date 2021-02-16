using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditLightController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 1.5f;
    public int lives = 2;
    public float attackDistance = 1f;
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
        if (player.position.x > leftLimit.position.x && player.position.x < rightLimit.position.x)
        {
            if (player.position.x > transform.position.x)
            {
                if (!moveRight)
                {
                    moveRight = true;
                    Rotate();
                }
            }
            else
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
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

        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("walk", false);
            animator.SetBool("attack", true);
            delay = 3f;
            shouldMove = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("walk", true);
            animator.SetBool("attack", false);
            delay = 3f;
            shouldMove = true;
        }
    }

    public void AttackEnd()
    {
        if (Vector2.Distance(transform.position, player.position) < attackDistance)
        {
            FindObjectOfType<HealthController>().TakeDamage();
        }
        animator.SetBool("attack", false);
    }

    public void TakeDamage()
    {
        lives--;
        shouldMove = false;
        delay = 3f;
        animator.SetBool("walk", false);
        animator.SetTrigger("hit");
        if (lives <= 0)
        {
            animator.SetTrigger("death");
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
