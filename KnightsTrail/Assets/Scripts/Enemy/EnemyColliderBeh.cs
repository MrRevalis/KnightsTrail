using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderBeh : MonoBehaviour
{
    public int health;

    public Animator anim;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        /*Debug.Log("Enemy died!");
        transform.parent.GetComponent<EnemyBehaviour>().moveSpeed = 0;
        transform.parent.GetComponent<Animator>().SetTrigger("Dead");
        Destroy(gameObject, 5);*/
    }

}
