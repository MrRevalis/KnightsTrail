using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{

    public Animator animator;
    public uint lives;
    public Collider2D OwnCollider;
    public AudioSource hit;

    float moveSpeed = 1.5f;
    bool moveRight = true;
    bool shouldMove = true;
    bool shouldDie = false;

    float delay;

    void FixedUpdate()
    {
        if (shouldMove)
        {
            animator.SetBool("ShouldWalk", true);
            animator.SetBool("Attack", false);
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

        if (lives <= 0 && !shouldDie)
        {
            DeathTime();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("StartPoint"))
        {
            moveRight = true;
            Rotate();
        }

        if (collision.gameObject.name.Equals("EndPoint"))
        {
            moveRight = false;
            Rotate();
        }

        if(lives <= 0)
        {
            if (collision.gameObject.name.Equals("Player"))
            {
                Physics2D.IgnoreCollision(collision.collider, OwnCollider);
            }
        }

        if (collision.gameObject.name.Equals("Player"))
        {
            if(lives > 0)
            {
                animator.SetBool("ShouldWalk", false);
                animator.SetBool("Attack", true);
                shouldMove = false;
                delay = 1f;

                FindObjectOfType<HealthController>().TakeDamage();
                if (transform.position.x < collision.transform.position.x)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-7 * 5f, transform.up.y * 5f), ForceMode2D.Impulse);
                }
                else
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(7 * 5f, transform.up.y * 5f), ForceMode2D.Impulse);
            }
        }
    }

    private void Rotate()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    public void DeathTime()
    {
        shouldDie = true;
        shouldMove = false;
        delay = 10000;
        animator.SetBool("Attack", false);
        animator.SetBool("ShouldWalk", false);
        animator.SetBool("isDead", true);
        FindObjectOfType<QuestManager>().ChangeGoalCount(this.gameObject);
        FindObjectOfType<MoneySystem>().SpawnCoin(transform.position);
        Destroy(transform.parent.gameObject, 1.5f);
    }

    public void TakeDamage()
    {
        if (lives > 0)
        {
            hit.Play();
            animator.SetTrigger("Hit");
            animator.SetBool("ShouldWalk", false);
            shouldMove = false;
            delay = 1.75f;
            lives--;
        }
    }
}
