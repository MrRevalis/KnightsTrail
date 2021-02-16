using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPlayer : MonoBehaviour
{
    public int damage = 40;
    private int destroyTime = 2;
    public Rigidbody2D rb;
    bool hasHit;

    void Start()
    {
        //rb.velocity = transform.right * speed;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(hasHit == false)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (rb.velocity.x == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D hitInfo)
    {
        hasHit = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        if (hitInfo.gameObject.name.Equals("Frog"))
        {
            hitInfo.gameObject.GetComponent<FrogMovement>().TakeDamage();
        }
        else if (hitInfo.gameObject.name.Equals("Slime"))
        {
            hitInfo.gameObject.GetComponent<SlimeMovement>().TakeDamage();
        }
        else if (hitInfo.gameObject.CompareTag("ArcherEnemy"))
        {
            hitInfo.gameObject.GetComponent<ArcherController>().TakeDamage();
        }
        else if (hitInfo.gameObject.name.Equals("ArcherSkeleton"))
        {
            hitInfo.gameObject.GetComponent<ArcherController>().TakeDamage();
        }
        else if (hitInfo.gameObject.CompareTag("ArcherEnemy"))
        {
            hitInfo.gameObject.GetComponent<ArcherController>().TakeDamage();
        }
        else if (hitInfo.gameObject.name.Equals("EnemySkeleton"))
        {
            hitInfo.gameObject.GetComponent<SkeletonBehaviour>().TakeDamage();
        }
        else if (hitInfo.gameObject.name.Equals("FlyingEye"))
        {
            hitInfo.gameObject.GetComponent<EyeFollowPlayer>().TakeDamage();
        }
        else if (hitInfo.gameObject.name.Equals("Goblin"))
        {
            hitInfo.gameObject.GetComponent<GoblinController>().TakeDamage();
        }
        else if (hitInfo.gameObject.name.Equals("MonsterSkeleton"))
        {
            hitInfo.gameObject.GetComponent<Guardian>().TakeDamage();
        }
        else if (hitInfo.gameObject.name.Equals("EvilWizardBoss"))
        {
            hitInfo.gameObject.GetComponent<BossScript>().TakeDamage();
        }
        else if (hitInfo.gameObject.name.Equals("EvilWizardBoss2"))
        {
            hitInfo.gameObject.GetComponent<Boss2Script>().TakeDamage();
        }
        else if (hitInfo.gameObject.name.Equals("Mushroom"))
        {
            hitInfo.gameObject.GetComponent<GoblinController>().TakeDamage();
        }
        else if (hitInfo.gameObject.name.Equals("Golem"))
        {
            hitInfo.gameObject.GetComponent<Golem>().TakeDamage();
        }
        else if (hitInfo.gameObject.name.Equals("Guardian"))
        {
            hitInfo.gameObject.GetComponent<Guardian>().TakeDamage();
        }
        else if (hitInfo.gameObject.name.Equals("Sprout"))
        {
            hitInfo.gameObject.GetComponent<Sprout>().TakeDamage();
        }
        else if (hitInfo.gameObject.name.Equals("GuardianSpitter"))
        {
            hitInfo.gameObject.GetComponent<ArcherController>().TakeDamage();
        }
        else if (hitInfo.gameObject.name.Equals("Stormhead"))
        {
            hitInfo.gameObject.GetComponent<Guardian>().TakeDamage();
        }
    }
}
