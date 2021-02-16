using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    public Animator animator;
    public uint lives;

    public AudioSource hit;
    public AudioSource dead;

    float moveSpeed = 1.5f;
    bool moveRight = true;
    bool shouldMove = true;
    float lastDamage;
    float delay;

    void FixedUpdate()
    {
        if(lives > 0)
        {
            if (shouldMove)
            {
                animator.SetBool("shouldJump", true);
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
        }

        if (lives <= 0)
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

        if (collision.gameObject.name.Equals("Player"))
        {
            animator.SetBool("shouldJump", false);
            shouldMove = false;
            delay = 3f;
        }
    }

    private void Rotate()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    public void DeadSoundPlay()
    {
        dead.Play();
    }

    public void DeathTime()
    {
        moveSpeed = 0;
        animator.SetBool("isDead", true);
        FindObjectOfType<QuestManager>().ChangeGoalCount(this.gameObject);
        Destroy(transform.parent.gameObject, 1f);
    }
    public void TakeDamage()
    {
        if (Time.time - lastDamage > 0.5 && lives >= 1)
        {
            hit.Play();
            animator.SetBool("shouldJump", false);
            shouldMove = false;
            delay = 1.25f;
            lastDamage = Time.time;
            lives--;
        }
    }
}
